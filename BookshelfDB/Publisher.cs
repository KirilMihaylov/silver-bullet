namespace BookshelfDB;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class Publisher
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [Key]
    public ulong Id { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public string Name { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public ICollection<Book> Books { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    internal ICollection<BookPublisher> BookPublishers { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public ICollection<MagazineSeries> MagazineSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    internal ICollection<MagazineSeriesPublisher> MagazineSeriesPublishers { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public ICollection<ComicsSeries> ComicsSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    internal ICollection<ComicsSeriesPublisher> ComicsSeriesPublishers { get; set; }
}