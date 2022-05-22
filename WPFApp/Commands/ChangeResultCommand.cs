namespace WPFApp.Commands;

public interface IChangeResult
{
    public bool HasPreviousResult();

    public void PreviousResult();

    public bool HasNextResult();

    public void NextResult();
}

/// <inheritdoc />
public class ChangeResultCommand : ICommand
{
    private readonly IChangeResult _changeResult;

    public ChangeResultCommand(IChangeResult changeResult)
    {
        _changeResult = changeResult;
    }

    /// <inheritdoc />
    public bool CanExecute(object? parameter)
    {
        return parameter is bool next && (next ? _changeResult.HasNextResult() : _changeResult.HasPreviousResult());
    }

    /// <inheritdoc />
    public void Execute(object? parameter)
    {
        if (parameter is not bool next)
            throw new AggregateException($"Command parameter is not of type \"{typeof(bool)}\"!");

        if (next)
        {
            _changeResult.NextResult();

            return;
        }

        _changeResult.PreviousResult();
    }

    /// <inheritdoc />
    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}