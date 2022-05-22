namespace BookshelfDB;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class MagazineSeriesGenre
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong MagazineSeriesId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal MagazineSeries MagazineSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong GenreId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal Genre Genre { get; set; }
}