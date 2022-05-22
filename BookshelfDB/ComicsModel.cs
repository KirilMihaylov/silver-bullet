namespace BookshelfDB;

public static class ComicsModel
{
    static ComicsModel()
    {
        Publishers.Load();

        Genres.Load();

        ComicsSeries.Load();

        Comics.Load();
    }

    public static IQueryable<Publisher> Publishers =>
        SharedModel.Model.Publishers.Include(publisher => publisher.ComicsSeries)
            .Where(author => author.ComicsSeries.Any());

    public static IQueryable<Genre> Genres =>
        SharedModel.Model.Genres.Include(genre => genre.ComicsSeries).Where(genre => genre.ComicsSeries.Any());

    public static DbSet<ComicsSeries> ComicsSeries => SharedModel.Model.ComicsSeries;

    public static DbSet<Comics> Comics => SharedModel.Model.Comics;
}