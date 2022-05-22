namespace BookshelfDB;

public class Book
{
    [Key] public ulong Id;

    public ICollection<Author> Authors { get; set; }

    internal ICollection<BookAuthor> BookAuthors { get; set; }

    public ICollection<Publisher> Publishers { get; set; }

    internal ICollection<BookPublisher> BookPublishers { get; set; }

    public long YearPublished { get; set; }

    public long PageCount { get; set; }

    public ICollection<Genre> Genres { get; set; }

    internal ICollection<BookGenre> BookGenres { get; set; }

    public bool HardCover { get; set; }

    public string Title { get; set; }
}