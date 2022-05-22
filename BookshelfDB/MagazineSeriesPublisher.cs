namespace BookshelfDB;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class MagazineSeriesPublisher
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong MagazineSeriesId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal MagazineSeries MagazineSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong PublisherId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal Publisher Publisher { get; set; }
}