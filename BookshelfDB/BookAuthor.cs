namespace BookshelfDB;

internal class BookAuthor
{
    public ulong BookId { get; set; }

    public Book Book { get; set; }

    public ulong AuthorId { get; set; }

    public Author Author { get; set; }
}