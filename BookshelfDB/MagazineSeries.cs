namespace BookshelfDB;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class MagazineSeries
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [Key]
    public ulong Id { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public ICollection<Publisher> Publishers { get; set; }

    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ICollection<MagazineSeriesPublisher> MagazineSeriesPublishers { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public long FirstYearPublished { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public ICollection<Genre> Genres { get; set; }

    [SuppressMessage("ReSharper", "CollectionNeverUpdated.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ICollection<MagazineSeriesGenre> MagazineSeriesGenres { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public ICollection<Magazine> Magazines { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public string Name { get; set; }
}