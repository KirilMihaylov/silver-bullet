namespace BookshelfDB;

/// <inheritdoc />
internal class Model : DbContext
{
    private static readonly string DbPath =
        "Data Source=" + Path.Join(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData
            ),
            "bookshelf.db"
        );

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<Author> Authors { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<Publisher> Publishers { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<Genre> Genres { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<Book> Books { get; set; }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal DbSet<BookPublisher> BookPublishers { get; set; }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal DbSet<BookAuthor> BookAuthors { get; set; }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal DbSet<BookGenre> BookGenres { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<MagazineSeries> MagazineSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal DbSet<MagazineSeriesPublisher> MagazineSeriesPublishers { get; set; }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal DbSet<MagazineSeriesGenre> MagazineSeriesGenres { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<Magazine> Magazines { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<ComicsSeries> ComicsSeries { get; set; }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal DbSet<ComicsSeriesPublisher> ComicsSeriesPublishers { get; set; }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal DbSet<ComicsSeriesGenre> ComicsSeriesGenres { get; set; }

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    internal DbSet<Comics> Comics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DbPath);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        {
            var entity = modelBuilder.Entity<Author>();

            entity.HasKey(author => author.Id);

            entity.Property(author => author.Name);
        }

        {
            var entity = modelBuilder.Entity<Publisher>();

            entity.HasKey(publisher => publisher.Id);

            entity.Property(publisher => publisher.Name);
        }

        {
            var entity = modelBuilder.Entity<Genre>();

            entity.HasKey(genre => genre.Id);

            entity.Property(genre => genre.Name);
        }

        {
            var entity = modelBuilder.Entity<Book>();

            entity.HasKey(book => book.Id);

            entity.Property(book => book.YearPublished);

            entity.Property(book => book.PageCount);

            entity.Property(book => book.HardCover);

            entity.Property(book => book.Title);

            entity.HasMany(book => book.Authors).WithMany(author => author.Books).UsingEntity<BookAuthor>(builder =>
                    builder.HasOne(bookAuthor => bookAuthor.Author).WithMany(author => author.BookAuthors)
                        .HasForeignKey(bookAuthor => bookAuthor.AuthorId), builder =>
                    builder.HasOne(bookAuthor => bookAuthor.Book).WithMany(book => book.BookAuthors)
                        .HasForeignKey(bookAuthor => bookAuthor.BookId),
                builder => builder.HasKey(bookAuthor => new {bookAuthor.BookId, bookAuthor.AuthorId}));

            entity.HasMany(book => book.Publishers).WithMany(publisher => publisher.Books).UsingEntity<BookPublisher>(
                builder =>
                    builder.HasOne(bookPublisher => bookPublisher.Publisher)
                        .WithMany(publisher => publisher.BookPublishers)
                        .HasForeignKey(bookPublisher => bookPublisher.PublisherId), builder =>
                    builder.HasOne(bookPublisher => bookPublisher.Book).WithMany(book => book.BookPublishers)
                        .HasForeignKey(bookPublisher => bookPublisher.BookId),
                builder => builder.HasKey(bookPublisher => new {bookPublisher.BookId, bookPublisher.PublisherId}));

            entity.HasMany(book => book.Genres).WithMany(genre => genre.Books).UsingEntity<BookGenre>(builder =>
                    builder.HasOne(bookGenre => bookGenre.Genre).WithMany(genre => genre.BookGenres)
                        .HasForeignKey(bookGenre => bookGenre.GenreId), builder =>
                    builder.HasOne(bookGenre => bookGenre.Book).WithMany(book => book.BookGenres)
                        .HasForeignKey(bookGenre => bookGenre.BookId),
                builder => builder.HasKey(bookGenre => new {bookGenre.BookId, bookGenre.GenreId}));
        }

        {
            var entity = modelBuilder.Entity<MagazineSeries>();

            entity.HasKey(magazineSeries => magazineSeries.Id);

            entity.Property(magazineSeries => magazineSeries.Name);

            entity.Property(magazineSeries => magazineSeries.FirstYearPublished);

            entity.HasMany(magazineSeries => magazineSeries.Magazines).WithOne(magazine => magazine.Series)
                .HasPrincipalKey(magazineSeries => magazineSeries.Id)
                .HasForeignKey(magazine => magazine.SeriesId);

            entity.HasMany(magazineSeries => magazineSeries.Publishers).WithMany(genre => genre.MagazineSeries)
                .UsingEntity<MagazineSeriesPublisher>(builder =>
                        builder.HasOne(magazineSeriesPublisher => magazineSeriesPublisher.Publisher)
                            .WithMany(publisher => publisher.MagazineSeriesPublishers)
                            .HasForeignKey(magazineSeriesPublisher => magazineSeriesPublisher.PublisherId), builder =>
                        builder.HasOne(magazineSeriesPublisher => magazineSeriesPublisher.MagazineSeries)
                            .WithMany(magazineSeries => magazineSeries.MagazineSeriesPublishers)
                            .HasForeignKey(magazineSeriesPublisher => magazineSeriesPublisher.MagazineSeriesId),
                    builder => builder.HasKey(bookGenre => new {bookGenre.MagazineSeriesId, bookGenre.PublisherId}));

            entity.HasMany(magazineSeries => magazineSeries.Genres).WithMany(genre => genre.MagazineSeries)
                .UsingEntity<MagazineSeriesGenre>(builder =>
                        builder.HasOne(magazineSeriesGenre => magazineSeriesGenre.Genre)
                            .WithMany(genre => genre.MagazineSeriesGenres)
                            .HasForeignKey(magazineSeriesGenre => magazineSeriesGenre.GenreId), builder =>
                        builder.HasOne(magazineSeriesGenre => magazineSeriesGenre.MagazineSeries)
                            .WithMany(magazineSeries => magazineSeries.MagazineSeriesGenres)
                            .HasForeignKey(magazineSeriesGenre => magazineSeriesGenre.MagazineSeriesId),
                    builder => builder.HasKey(magazineSeriesGenre =>
                        new {magazineSeriesGenre.MagazineSeriesId, magazineSeriesGenre.GenreId}));
        }

        {
            var entity = modelBuilder.Entity<Magazine>();

            entity.HasKey(magazine => magazine.Id);

            entity.Property(magazineSeries => magazineSeries.Title);

            entity.Property(magazineSeries => magazineSeries.PublicationNumber);

            entity.HasOne(magazineSeries => magazineSeries.Series).WithMany(magazineSeries => magazineSeries.Magazines)
                .HasPrincipalKey(magazineSeries => magazineSeries.Id)
                .HasForeignKey(magazine => magazine.SeriesId);
        }

        {
            var entity = modelBuilder.Entity<ComicsSeries>();

            entity.HasKey(comicsSeries => comicsSeries.Id);

            entity.Property(comicsSeries => comicsSeries.Name);

            entity.Property(comicsSeries => comicsSeries.FirstYearPublished);

            entity.HasMany(comicsSeries => comicsSeries.Comics).WithOne(comics => comics.Series)
                .HasPrincipalKey(comicsSeries => comicsSeries.Id)
                .HasForeignKey(comics => comics.SeriesId);

            entity.HasMany(comicsSeries => comicsSeries.Publishers).WithMany(publisher => publisher.ComicsSeries)
                .UsingEntity<ComicsSeriesPublisher>(builder =>
                        builder.HasOne(comicsSeriesPublisher => comicsSeriesPublisher.Publisher)
                            .WithMany(publisher => publisher.ComicsSeriesPublishers)
                            .HasForeignKey(comicsSeriesPublisher => comicsSeriesPublisher.PublisherId), builder =>
                        builder.HasOne(comicsSeriesPublisher => comicsSeriesPublisher.ComicsSeries)
                            .WithMany(comicsSeries => comicsSeries.ComicsSeriesPublishers)
                            .HasForeignKey(comicsSeriesPublisher => comicsSeriesPublisher.ComicsSeriesId),
                    builder => builder.HasKey(comicsSeriesPublisher => new
                        {comicsSeriesPublisher.ComicsSeriesId, comicsSeriesPublisher.PublisherId}));

            entity.HasMany(comicsSeries => comicsSeries.Genres).WithMany(genre => genre.ComicsSeries)
                .UsingEntity<ComicsSeriesGenre>(builder =>
                        builder.HasOne(comicsSeriesGenre => comicsSeriesGenre.Genre)
                            .WithMany(genre => genre.ComicsSeriesGenres)
                            .HasForeignKey(comicsSeriesGenre => comicsSeriesGenre.GenreId), builder =>
                        builder.HasOne(comicsSeriesGenre => comicsSeriesGenre.ComicsSeries)
                            .WithMany(comicsSeries => comicsSeries.ComicsSeriesGenres)
                            .HasForeignKey(comicsSeriesGenre => comicsSeriesGenre.ComicsSeriesId),
                    builder => builder.HasKey(comicsSeriesGenre =>
                        new {comicsSeriesGenre.ComicsSeriesId, comicsSeriesGenre.GenreId}));
        }

        {
            var entity = modelBuilder.Entity<Comics>();

            entity.HasKey(comics => comics.Id);

            entity.Property(comics => comics.Title);

            entity.Property(comics => comics.PublicationNumber);

            entity.HasOne(comics => comics.Series).WithMany(comicsSeries => comicsSeries.Comics)
                .HasPrincipalKey(comicsSeries => comicsSeries.Id)
                .HasForeignKey(comics => comics.SeriesId);
        }
    }
}