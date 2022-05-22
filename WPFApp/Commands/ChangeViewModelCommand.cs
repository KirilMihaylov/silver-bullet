namespace WPFApp.Commands;

public interface IChangeViewModel<in T>
{
    public void SetViewModel(T viewModel);
}

/// <inheritdoc />
public class ChangeViewModelCommand<T, TViewModel> : ICommand where T : class, IChangeViewModel<TViewModel>
{
    private T? _element;

    public ChangeViewModelCommand(T element)
    {
        _element = element;
    }

    /// <inheritdoc />
    public bool CanExecute(object? parameter)
    {
        return _element is not null && parameter is null or TViewModel;
    }

    /// <inheritdoc />
    public void Execute(object? parameter)
    {
        if (_element is null) throw new InvalidOperationException("Command cannot be executed!");

        if (parameter is not TViewModel viewModel)
            throw new AggregateException($"Command parameter is not of type \"{typeof(TViewModel)}\"!");

        _element.SetViewModel(viewModel);

        _element = null;

        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc />
    public event EventHandler? CanExecuteChanged;
}