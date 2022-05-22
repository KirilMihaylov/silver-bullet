namespace WinFormsApp;

public partial class AppForm : Form
{
    private GenericVm _viewModel;

    public AppForm()
    {
        _viewModel = new BooksVm();

        InitializeComponent();
    }

    private GenericVm ViewModel
    {
        get => _viewModel;
        set
        {
            variableSelectorComboBox.SelectedIndex = -1;

            suggestionBox.Variable = null;

            resultsTwoColumnsList.Items = Array.Empty<Control>();

            _viewModel = value;

            variableSelectorComboBox.Items.Clear();

            variableSelectorComboBox.Items.AddRange(
                ViewModel.Variables.Select(variable => (object) variable).ToArray()
            );
        }
    }

    private void AppForm_Load(object sender, EventArgs e)
    {
        variableSelectorComboBox.Items.AddRange(
            ViewModel.Variables.Select(variable => (object) variable).ToArray()
        );
    }

    private void VariableSelectorComboBox_OnSelectionChangeCommitted(object sender, EventArgs e)
    {
        if (!ReferenceEquals(sender, variableSelectorComboBox)) return;

        ViewModel.Variable = variableSelectorComboBox.SelectedItem is null
            ? null
            : variableSelectorComboBox.SelectedItem as string ??
              throw new ArgumentException("Selected item is neither null or of type string!");

        suggestionBox.Variable = ViewModel.Variable;

        if (ViewModel.Variable is null)
        {
            suggestionBoxElementHost.Enabled = false;

            return;
        }

        suggestionBoxElementHost.Enabled = true;

        suggestionBox.Selector = ViewModel.Selector;
    }

    private void SuggestionBox_OnSelectionMade(object sender, SuggestionBox.SelectionMadeEventArgs args)
    {
        if (!ReferenceEquals(sender, suggestionBox)) return;

        args.Handled = true;

        ViewModel.Selection = args.Selection;

        RefreshResults();
    }

    private void ChangeResultButton_OnClick(object? sender, EventArgs e)
    {
        previousResultButton.Enabled = false;

        nextResultButton.Enabled = false;

        if (ReferenceEquals(sender, previousResultButton)) ViewModel.PreviousResult();
        else if (ReferenceEquals(sender, nextResultButton)) ViewModel.NextResult();
        else throw new InvalidOperationException();

        RefreshResults();
    }

    private void RefreshResults()
    {
        resultsTwoColumnsList.Items = ViewModel.ResultsItemsSource.Select(labelValuePair =>
            new LabelValue(labelValuePair.Label, labelValuePair.Value.ToString() ?? string.Empty));

        previousResultButton.Enabled = ViewModel.HasPreviousResult;

        nextResultButton.Enabled = ViewModel.HasNextResult;
    }

    private void ChangeViewButton_OnClick(object? sender, EventArgs e)
    {
        if (ReferenceEquals(sender, booksViewButton)) ViewModel = new BooksVm();
        else if (ReferenceEquals(sender, magazinesViewButton)) ViewModel = new MagazinesVm();
        else if (ReferenceEquals(sender, comicsViewButton)) ViewModel = new ComicsVm();
        else throw new InvalidOperationException();
    }
}