namespace BookshelfDB;

internal static class SharedModel
{
    internal static readonly Model Model = new();

    static SharedModel()
    {
        Model.Database.EnsureCreated();
    }
}