namespace BookshelfDB;

public static class MagazinesModel
{
    static MagazinesModel()
    {
        Publishers.Load();

        Genres.Load();

        MagazineSeries.Load();

        Magazines.Load();
    }

    public static IQueryable<Publisher> Publishers => SharedModel.Model.Publishers
        .Include(publisher => publisher.MagazineSeries)
        .Where(publisher => publisher.MagazineSeries.Any());

    public static IQueryable<Genre> Genres => SharedModel.Model.Genres
        .Include(genre => genre.MagazineSeries)
        .Where(genre => genre.MagazineSeries.Any());

    public static DbSet<MagazineSeries> MagazineSeries => SharedModel.Model.MagazineSeries;

    public static DbSet<Magazine> Magazines => SharedModel.Model.Magazines;
}