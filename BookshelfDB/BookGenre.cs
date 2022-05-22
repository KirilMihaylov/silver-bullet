namespace BookshelfDB;

internal class BookGenre
{
    public ulong BookId { get; set; }

    public Book Book { get; set; }

    public ulong GenreId { get; set; }

    public Genre Genre { get; set; }
}