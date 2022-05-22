namespace SilverBulletLib;

internal static class SilverBulletContext
{
    internal static readonly Model Context = new();

    static SilverBulletContext()
    {
        Context.Suggestions.Load();
    }
}

public class SilverBullet<T>
{
    public delegate void UpdateSuggestions(T arg, IEnumerable<string> suggestions);

    private readonly UpdateSuggestions _updateSuggestions;

    public SilverBullet(UpdateSuggestions updateSuggestions)
    {
        _updateSuggestions = updateSuggestions ?? throw new ArgumentNullException(nameof(updateSuggestions));
    }

    private static Model Context => SilverBulletContext.Context;

    private string Variable { get; set; }

    private IEnumerable<string> GetSuggestions()
    {
        return SilverBulletContext.Context.Suggestions
            .Where(suggestion => suggestion.Variable.Equals(Variable))
            .OrderBy(suggestion => suggestion.Order).Select(suggestion => suggestion.Value);
    }

    public void LoadSuggestions(T arg)
    {
        _updateSuggestions(arg, GetSuggestions());
    }

    public void SetVariable(string variable, T arg)
    {
        Variable = variable ?? throw new ArgumentNullException(nameof(variable));

        _updateSuggestions(arg, GetSuggestions());
    }

    public async Task SelectionMade(T arg, string selection)
    {
        if (selection is null) throw new ArgumentNullException(nameof(selection));

        if (selection.Equals(string.Empty)) return;

        if (Variable is null) throw new InvalidOperationException("Variable is not set!");

        var suggestions = new[] {new Suggestion {Variable = Variable, Order = 0, Value = selection}}.Union(Context
            .Suggestions
            .Where(suggestion => suggestion.Variable.Equals(Variable) && !suggestion.Value.Equals(selection))
            .OrderBy(suggestion => suggestion.Order).Take(4)).ToList();

        Context.Suggestions.RemoveRange(Context.Suggestions.Where(suggestion => suggestion.Variable.Equals(Variable)));

        await Context.SaveChangesAsync();

        for (byte index = 0; index < suggestions.Count; index++) suggestions[index].Order = index;

        await Context.Suggestions.AddRangeAsync(suggestions);

        await Context.SaveChangesAsync();

        _updateSuggestions(arg, GetSuggestions());
    }
}