namespace WPFApp.Commands;

public interface ISelectionMade
{
    public void SelectionMade(object? selection);
}

/// <inheritdoc />
public class SelectionMadeCommand : ICommand
{
    private readonly ISelectionMade _selectionMade;

    public SelectionMadeCommand(ISelectionMade selectionMade)
    {
        _selectionMade = selectionMade;
    }

    /// <inheritdoc />
    public bool CanExecute(object? parameter)
    {
        return parameter is not null;
    }

    /// <inheritdoc />
    public void Execute(object? parameter)
    {
        if (parameter is null)
            throw new ArgumentNullException(nameof(parameter));

        _selectionMade.SelectionMade(parameter);
    }

    /// <inheritdoc />
    public event EventHandler? CanExecuteChanged;
}