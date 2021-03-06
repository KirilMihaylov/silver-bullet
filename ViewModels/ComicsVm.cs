namespace ViewModelsLib;

public sealed class ComicsVm : GenericVm
{
    #region Constructors

    public ComicsVm() : base(FirstInternalDelegate.Key, FirstInternalDelegate.Value)
    {
    }

    #endregion

    #region Overriden Methods

    protected override IReadOnlyDictionary<string, InternalDelegates> InternalDelegatesMap()
    {
        return StaticInternalDelegatesMap;
    }

    #endregion

    #region Private Static Fields

    private static readonly IReadOnlyDictionary<string, InternalDelegates> StaticInternalDelegatesMap =
        new SortedDictionary<string, InternalDelegates>(new Dictionary<string, InternalDelegates>(new[]
        {
            new KeyValuePair<string, InternalDelegates>("Publisher",
                new InternalDelegates {Selector = PublisherSelector, ResultsRetriever = PublisherResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("Genre",
                new InternalDelegates {Selector = GenreSelector, ResultsRetriever = GenreResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("First Year Published",
                new InternalDelegates
                    {Selector = FirstYearPublishedSelector, ResultsRetriever = FirstYearPublishedResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("Series Name",
                new InternalDelegates
                    {Selector = SeriesNameSelector, ResultsRetriever = SeriesNameResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("Magazine Title",
                new InternalDelegates
                    {Selector = MagazineTitleSelector, ResultsRetriever = ComicsTitleResultsRetriever})
        }));

    private static readonly KeyValuePair<string, InternalDelegates> FirstInternalDelegate =
        StaticInternalDelegatesMap.First();

    #endregion

    #region Result Retrievers

    private static IEnumerable<IEnumerable<LabelValuePair>> PublisherResultsRetriever(object o)
    {
        if (o is not string publisherName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return ComicsToPropertiesList(ComicsModel.Publishers
            .Where(publisher => publisher.Name.Equals(publisherName))
            .Include(publisher => publisher.ComicsSeries)
            .SelectMany(publisher => publisher.ComicsSeries)
            .Include(comicsSeries => comicsSeries.Comics)
            .SelectMany(comicsSeries => comicsSeries.Comics));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> GenreResultsRetriever(object o)
    {
        if (o is not string genreName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return ComicsToPropertiesList(ComicsModel.Genres.Where(genre => genre.Name.Equals(genreName))
            .Include(genre => genre.ComicsSeries)
            .SelectMany(genre => genre.ComicsSeries)
            .Include(comicsSeries => comicsSeries.Comics)
            .SelectMany(comicsSeries => comicsSeries.Comics));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> FirstYearPublishedResultsRetriever(object o)
    {
        if (o is long year || long.TryParse(o.ToString(), out year))
            return ComicsToPropertiesList(ComicsModel.ComicsSeries
                .Where(comicsSeries => comicsSeries.FirstYearPublished == year)
                .Include(comicsSeries => comicsSeries.Comics)
                .SelectMany(comicsSeries => comicsSeries.Comics));

        throw new ArgumentException($"Argument passed is not of type \"{typeof(long)}\"");
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> SeriesNameResultsRetriever(object o)
    {
        if (o is not string seriesName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return ComicsToPropertiesList(ComicsModel.ComicsSeries
            .Where(comicsSeries => comicsSeries.Name.Equals(seriesName))
            .Include(comicsSeries => comicsSeries.Comics)
            .SelectMany(comicsSeries => comicsSeries.Comics));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> ComicsTitleResultsRetriever(object o)
    {
        if (o is not string comicsTitle)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return ComicsToPropertiesList(ComicsModel.Comics
            .Where(comics => comics.Title.Equals(comicsTitle)));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> ComicsToPropertiesList(IQueryable<Comics> queryable)
    {
        return queryable
            .Include(comics => comics.Series)
            .Include(comics => comics.Series.Publishers)
            .Include(comics => comics.Series.Genres)
            .AsEnumerable()
            .Select(comics => new List<LabelValuePair>
            {
                new() {Label = "Title", Value = comics.Title},
                new() {Label = "Publication Number", Value = comics.PublicationNumber},
                new() {Label = "First Year Published", Value = comics.Series.FirstYearPublished},
                new() {Label = "Series Name", Value = comics.Series.Name},
                new()
                {
                    Label = "Publishers",
                    Value = ListValues(comics.Series.Publishers.Select(publisher => publisher.Name))
                },
                new()
                {
                    Label = "Genres",
                    Value = ListValues(comics.Series.Genres.Select(genre => genre.Name))
                }
            }.OrderBy(labelValuePair => labelValuePair.Label));
    }

    #endregion

    #region Selectors

    private static IEnumerable<object> PublisherSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return ComicsModel.Publishers
            .Select(publisher => publisher.Name)
            .Where(currentString.Length < 2
                ? name => name.ToLower().StartsWith(currentString)
                : name => name.ToLower().Contains(currentString)).Take(5);
    }

    private static IEnumerable<object> GenreSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return ComicsModel.Genres
            .Select(genre => genre.Name)
            .Where(currentString.Length < 2
                ? name => name.ToLower().StartsWith(currentString)
                : name => name.ToLower().Contains(currentString)).Take(5);
    }

    private static IEnumerable<object> FirstYearPublishedSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return ComicsModel.ComicsSeries
            .Where(currentString.Length < 2
                ? comicsSeries => comicsSeries.FirstYearPublished.ToString().StartsWith(currentString)
                : comicsSeries => comicsSeries.FirstYearPublished.ToString().Contains(currentString)).Take(5)
            .Select(comicsSeries => (object) comicsSeries.FirstYearPublished);
    }

    private static IEnumerable<object> SeriesNameSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return ComicsModel.ComicsSeries.Select(comicsSeries => comicsSeries.Name)
            .Where(currentString.Length < 2
                ? name => name.ToLower().StartsWith(currentString)
                : name => name.ToLower().Contains(currentString)).Take(5);
    }

    private static IEnumerable<object> MagazineTitleSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return ComicsModel.Comics.Select(comics => comics.Title)
            .Where(currentString.Length < 2
                ? title => title.ToLower().StartsWith(currentString)
                : title => title.ToLower().Contains(currentString)).Take(5);
    }

    #endregion
}