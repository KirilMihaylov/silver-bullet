namespace WPFApp;

[SuppressMessage("ReSharper", "ClassWithVirtualMembersNeverInherited.Global")]
public partial class AppWindow : IChangeViewModel<GenericVm>, IChangeResult, ISelectionMade
{
    public GenericVm ViewModel { get; }

    public ChangeViewModelCommand<AppWindow, GenericVm> ChangeViewModelCommand { get; }

    public ChangeResultCommand ChangeResultCommand { get; }

    public SelectionMadeCommand SelectionMadeCommand { get; }

    public bool HasPreviousResult()
    {
        return ViewModel.HasPreviousResult;
    }

    public void PreviousResult()
    {
        ViewModel.PreviousResult();
    }

    public bool HasNextResult()
    {
        return ViewModel.HasNextResult;
    }

    public void NextResult()
    {
        ViewModel.NextResult();
    }

    public void SetViewModel(GenericVm viewModel)
    {
        Hide();

        new AppWindow(viewModel).Show();

        Close();
    }

    public void SelectionMade(object selection)
    {
        ViewModel.Selection = selection;
    }

    #region Constructors

    private AppWindow(GenericVm viewModel)
    {
        ViewModel = viewModel;

        ChangeViewModelCommand = new ChangeViewModelCommand<AppWindow, GenericVm>(this);

        ChangeResultCommand = new ChangeResultCommand(this);

        ViewModel.PropertyChanged += (sender, args) =>
        {
            if (nameof(ViewModel.HasPreviousResult).Equals(args.PropertyName) ||
                nameof(ViewModel.HasNextResult).Equals(args.PropertyName))
                ChangeResultCommand.RaiseCanExecuteChanged();
        };

        SelectionMadeCommand = new SelectionMadeCommand(this);

        InitializeComponent();
    }

    public AppWindow() : this(new BooksVm())
    {
    }

    #endregion
}