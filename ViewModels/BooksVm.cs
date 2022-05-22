namespace ViewModelsLib;

public sealed class BooksVm : GenericVm
{
    #region Constructors

    public BooksVm() : base(FirstInternalDelegate.Key, FirstInternalDelegate.Value)
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
            new KeyValuePair<string, InternalDelegates>("Title",
                new InternalDelegates {Selector = TitleSelector, ResultsRetriever = TitleResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("Author",
                new InternalDelegates {Selector = AuthorSelector, ResultsRetriever = AuthorResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("Publisher",
                new InternalDelegates {Selector = PublisherSelector, ResultsRetriever = PublisherResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("Genre",
                new InternalDelegates {Selector = GenreSelector, ResultsRetriever = GenreResultsRetriever}),
            new KeyValuePair<string, InternalDelegates>("Year Published",
                new InternalDelegates
                    {Selector = YearPublishedSelector, ResultsRetriever = YearPublishedResultsRetriever})
        }));

    private static readonly KeyValuePair<string, InternalDelegates> FirstInternalDelegate =
        StaticInternalDelegatesMap.First();

    #endregion

    #region Result Retrievers

    private static IEnumerable<IEnumerable<LabelValuePair>> AuthorResultsRetriever(object o)
    {
        if (o is not string authorName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return BookToPropertiesList(BooksModel.Authors
            .Where(author => author.Name.Equals(authorName))
            .Include(author => author.Books)
            .SelectMany(author => author.Books));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> PublisherResultsRetriever(object o)
    {
        if (o is not string publisherName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return BookToPropertiesList(BooksModel.Publishers
            .Where(publisher => publisher.Name.Equals(publisherName))
            .Include(publisher => publisher.Books)
            .SelectMany(publisher => publisher.Books));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> GenreResultsRetriever(object o)
    {
        if (o is not string genreName)
            throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return BookToPropertiesList(BooksModel.Genres.Where(genre => genre.Name.Equals(genreName))
            .Include(genre => genre.Books)
            .SelectMany(genre => genre.Books));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> YearPublishedResultsRetriever(object o)
    {
        if (o is long year || long.TryParse(o.ToString(), out year))
            return BookToPropertiesList(BooksModel.Books
                .Where(book => book.YearPublished == year));

        throw new ArgumentException(
            $"Argument passed is not of type \"{typeof(long)}\" or valid {typeof(string)} representation of it!");
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> TitleResultsRetriever(object o)
    {
        if (o is not string title) throw new ArgumentException($"Argument passed is not of type \"{typeof(string)}\"");

        return BookToPropertiesList(BooksModel.Books
            .Where(book => book.Title.Equals(title)));
    }

    private static IEnumerable<IEnumerable<LabelValuePair>> BookToPropertiesList(IQueryable<Book> queryable)
    {
        return queryable
            .Include(book => book.Authors)
            .Include(book => book.Publishers)
            .Include(book => book.Genres)
            .AsEnumerable()
            .Select(book => new List<LabelValuePair>
            {
                new() {Label = "Title", Value = book.Title},
                new() {Label = "Year Published", Value = book.YearPublished},
                new()
                {
                    Label = "Author",
                    Value = ListValues(book.Authors.Select(author => author.Name))
                },
                new()
                {
                    Label = "Publishers",
                    Value = ListValues(book.Publishers.Select(publisher => publisher.Name))
                },
                new()
                {
                    Label = "Genres",
                    Value = ListValues(book.Genres.Select(genre => genre.Name))
                },
                new()
                {
                    Label = "Page Count",
                    Value = book.PageCount
                },
                new()
                {
                    Label = "Hard Cover",
                    Value = book.HardCover ? "Yes" : "No"
                }
            }.OrderBy(labelValuePair => labelValuePair.Label));
    }

    #endregion

    #region Selectors

    private static IEnumerable<object> TitleSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return BooksModel.Books
            .Select(book => book.Title)
            .Where(currentString.Length < 2
                ? name => name.ToLower().StartsWith(currentString)
                : name => name.ToLower().Contains(currentString)).Take(5);
    }

    private static IEnumerable<object> AuthorSelector(object current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return BooksModel.Authors
            .Select(author => author.Name)
            .Where(currentString.Length < 2
                ? name => name.ToLower().StartsWith(currentString)
                : name => name.ToLower().Contains(currentString)).Take(5);
    }

    private static IEnumerable<object> PublisherSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString()?.ToLower() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return BooksModel.Publishers
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

        return BooksModel.Genres
            .Select(genre => genre.Name)
            .Where(currentString.Length < 2
                ? name => name.ToLower().StartsWith(currentString)
                : name => name.ToLower().Contains(currentString)).Take(5);
    }

    private static IEnumerable<object> YearPublishedSelector(object? current)
    {
        if (current is null) return Array.Empty<object>();

        var currentString = current.ToString() ??
                            throw new InvalidOperationException("Current selection couldn't be converted to string!");

        return BooksModel.Books.Select(book => book.YearPublished)
            .Where(currentString.Length < 2
                ? year => year.ToString().StartsWith(currentString)
                : year => year.ToString().Contains(currentString)).Take(5)
            .Select(year => (object) year);
    }

    #endregion
}