namespace BookshelfDB;

public class Genre
{
    [Key] public ulong Id { get; set; }

    public string Name { get; set; }

    public ICollection<Book> Books { get; set; }

    internal ICollection<BookGenre> BookGenres { get; set; }

    public ICollection<MagazineSeries> MagazineSeries { get; set; }

    internal ICollection<MagazineSeriesGenre> MagazineSeriesGenres { get; set; }

    public ICollection<ComicsSeries> ComicsSeries { get; set; }

    internal ICollection<ComicsSeriesGenre> ComicsSeriesGenres { get; set; }
}