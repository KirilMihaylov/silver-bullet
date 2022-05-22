namespace BookshelfDB;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class Comics
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [Key]
    public ulong Id { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong SeriesId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public ComicsSeries Series { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public string Title { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public string PublicationNumber { get; set; }
}