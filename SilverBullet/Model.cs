namespace SilverBulletLib;

internal class Model : DbContext
{
    private static readonly string DbPath =
        "Data Source=" + Path.Join(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData
            ),
            "silver-bullet.db"
        );

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public DbSet<Suggestion> Suggestions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(DbPath);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        {
            var entity = modelBuilder.Entity<Suggestion>();

            entity.HasKey(suggestion => new {suggestion.Variable, suggestion.Order});

            entity.Property(suggestion => suggestion.Value).IsRequired().IsUnicode();
        }
    }
}

internal class Suggestion
{
    internal string Variable { init; get; }

    internal byte Order { get; set; }

    internal string Value { get; init; }
}