namespace BookshelfDB;

internal class BookPublisher
{
    public ulong BookId { get; set; }

    public Book Book { get; set; }

    public ulong PublisherId { get; set; }

    public Publisher Publisher { get; set; }
}