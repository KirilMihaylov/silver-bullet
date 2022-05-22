namespace BookshelfDB;

public static class BooksModel
{
    static BooksModel()
    {
        Authors.Load();

        Publishers.Load();

        Genres.Load();

        Books.Load();
    }

    public static IQueryable<Author> Authors =>
        SharedModel.Model.Authors.Include(author => author.Books).Where(author => author.Books.Any());

    public static IQueryable<Publisher> Publishers =>
        SharedModel.Model.Publishers.Include(publisher => publisher.Books).Where(publisher => publisher.Books.Any());

    public static IQueryable<Genre> Genres =>
        SharedModel.Model.Genres.Include(genre => genre.Books).Where(genre => genre.Books.Any());

    public static DbSet<Book> Books => SharedModel.Model.Books;
}