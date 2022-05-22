namespace BookshelfDB;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class ComicsSeriesPublisher
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong ComicsSeriesId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ComicsSeries ComicsSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong PublisherId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal Publisher Publisher { get; set; }
}