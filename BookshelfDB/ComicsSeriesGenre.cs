namespace BookshelfDB;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal class ComicsSeriesGenre
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong ComicsSeriesId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ComicsSeries ComicsSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal ulong GenreId { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal Genre Genre { get; set; }
}