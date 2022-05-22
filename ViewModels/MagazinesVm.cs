namespace ViewModelsLib;

public sealed class MagazinesVm : GenericVm
{
    #region Constructors

    public MagazinesVm() : base(FirstInternalDelegate.Key, FirstInternalDelegate.Value)
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
                    {Selector = MagazineTitleSelector, ResultsRetriever = MagazineTitleResultsRetriever})
        }));

    private static readonly KeyValuePair<string, InternalDelegates> FirstInternalDelegate =
        StaticInternalDelegatesMap.First();

    #endregion

    #region Result Retrievers

    private static IEnumerable<IEnumerable<LabelValuePair>> PublisherResultsRetriever(object o)
    {
        if (o is not string publisherName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return MagazineToPropertiesList(MagazinesModel.Publishers
            .Where(publisher => publisher.Name.Equals(publisherName))
            .Include(publisher => publisher.MagazineSeries)
            .SelectMany(publisher => publisher.MagazineSeries)
            .Include(magazineSeries => magazineSeries.Magazines)
            .SelectMany(magazineSeries => magazineSeries.Magazines));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> GenreResultsRetriever(object o)
    {
        if (o is not string genreName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return MagazineToPropertiesList(MagazinesModel.Genres.Where(genre => genre.Name.Equals(genreName))
            .Include(genre => genre.MagazineSeries)
            .SelectMany(genre => genre.MagazineSeries)
            .Include(magazineSeries => magazineSeries.Magazines)
            .SelectMany(magazineSeries => magazineSeries.Magazines));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> FirstYearPublishedResultsRetriever(object o)
    {
        if (o is long year || long.TryParse(o.ToString(), out year))
            return MagazineToPropertiesList(MagazinesModel.MagazineSeries
                .Where(magazineSeries => magazineSeries.FirstYearPublished == year)
                .Include(magazineSeries => magazineSeries.Magazines)
                .SelectMany(magazineSeries => magazineSeries.Magazines));

        throw new ArgumentException($"Argument passed is not of type \"{typeof(long)}\"");
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> SeriesNameResultsRetriever(object o)
    {
        if (o is not string seriesName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return MagazineToPropertiesList(MagazinesModel.MagazineSeries
            .Where(magazineSeries => magazineSeries.Name.Equals(seriesName))
            .Include(magazineSeries => magazineSeries.Magazines)
            .SelectMany(magazineSeries => magazineSeries.Magazines));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> MagazineTitleResultsRetriever(object o)
    {
        if (o is not string magazineTitle)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return MagazineToPropertiesList(MagazinesModel.Magazines
            .Where(magazine => magazine.Title.Equals(magazineTitle)));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> MagazineToPropertiesList(IQueryable<Magazine> queryable)
    {
        return queryable
            .Include(magazine => magazine.Series)
            .Include(magazine => magazine.Series.Publishers)
            .Include(magazine => magazine.Series.Genres)
            .AsEnumerable()
            .Select(magazine => new List<LabelValuePair>
            {
                new() {Label = "Title", Value = magazine.Title},
                new() {Label = "Publication Number", Value = magazine.PublicationNumber},
                new() {Label = "First Year Published", Value = magazine.Series.FirstYearPublished},
                new() {Label = "Series Name", Value = magazine.Series.Name},
                new()
                {
                    Label = "Publishers",
                    Value = ListValues(magazine.Series.Publishers.Select(publisher => publisher.Name))
                },
                new()
                {
                    Label = "Genres",
                    Value = ListValues(magazine.Series.Genres.Select(genre => genre.Name))
                }
            });
    }

    #endregion

    #region Selectors

    private static IEnumerable<object> PublisherSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return MagazinesModel.Publishers
            .Include(publisher => publisher.MagazineSeries)
            .Where(publisher => publisher.MagazineSeries.Any())
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

        return MagazinesModel.Genres
            .Include(genre => genre.MagazineSeries)
            .Where(genre => genre.MagazineSeries.Any())
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

        return MagazinesModel.MagazineSeries
            .Where(currentString.Length < 2
                ? magazineSeries => magazineSeries.FirstYearPublished.ToString().StartsWith(currentString)
                : magazineSeries => magazineSeries.FirstYearPublished.ToString().Contains(currentString)).Take(5)
            .Select(magazineSeries => (object) magazineSeries.FirstYearPublished);
    }

    private static IEnumerable<object> SeriesNameSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return MagazinesModel.MagazineSeries.Select(magazine => magazine.Name)
            .Where(currentString.Length < 2
                ? name => name.ToLower().StartsWith(currentString)
                : name => name.ToLower().Contains(currentString)).Take(5);
    }

    private static IEnumerable<object> MagazineTitleSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return MagazinesModel.Magazines.Select(magazine => magazine.Title)
            .Where(currentString.Length < 2
                ? title => title.ToLower().StartsWith(currentString)
                : title => title.ToLower().Contains(currentString)).Take(5);
    }

    #endregion
}