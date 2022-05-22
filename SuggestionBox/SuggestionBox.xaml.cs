namespace SuggestionBoxLib;

public sealed partial class SuggestionBox
{
    private static readonly Key[] Keys = {Key.Up, Key.Down, Key.Enter};

    public static readonly DependencyProperty VariableProperty =
        DependencyProperty.Register(nameof(Variable), typeof(string), typeof(SuggestionBox),
            new PropertyMetadata(null, PropertyChangedCallback));

    public static readonly DependencyProperty SelectorProperty =
        DependencyProperty.Register(nameof(Selector), typeof(Func<object, IEnumerable<object>>), typeof(SuggestionBox),
            new PropertyMetadata((Func<object, IEnumerable<object>>) (_ =>
                throw new InvalidOperationException("Selector not initialized before usage!"))));

    public static readonly DependencyProperty SelectionProperty =
        DependencyProperty.Register(nameof(Selection), typeof(object), typeof(SuggestionBox),
            new PropertyMetadata());

    #region Constructors

    public SuggestionBox()
    {
        InitializeComponent();

        _silverBullet = new SilverBullet<object?>(
            (_, suggestions) => Suggestions.ItemsSource = suggestions.ToArray()
        );
    }

    #endregion

    public string? Variable
    {
        get => (string) GetValue(VariableProperty);
        set => SetValue(VariableProperty, value);
    }

    public Func<object, IEnumerable<object>> Selector
    {
        get => (Func<object, IEnumerable<object>>) GetValue(SelectorProperty);
        set => SetValue(SelectorProperty, value);
    }

    public object? Selection
    {
        get => GetValue(SelectionProperty);
        set => SetValue(SelectionProperty, value);
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not SuggestionBox self)
            throw new ArgumentException($"Dependency object passed is not a {nameof(SuggestionBox)}!",
                nameof(d));

        if (e.Property != VariableProperty) return;

        if (e.NewValue is not (null or string))
            throw new ArgumentException($"New value is neither null or of type {typeof(string)}!");

        self.Input.Clear();

        if (e.NewValue is not string variable) return;

        self._silverBullet.SetVariable(variable, null);
    }

    #region Event Handlers

    private void Input_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (!ReferenceEquals(e.OriginalSource, Input)) return;

        e.Handled = true;

        if (Input.Text.Length == 0)
            _silverBullet.LoadSuggestions(null);
        else
            Suggestions.ItemsSource = Selector(Input.Text).ToArray();
    }


    private async void Input_OnPreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Handled || !ReferenceEquals(e.OriginalSource, Input) || !Keys.Contains(e.Key)) return;

        if (Variable is null)
            throw new InvalidOperationException($"\"{nameof(Variable)}\" property not set before use!");

        // ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (e.Key)
            // ReSharper restore SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        {
            case Key.Up:
                _ignoreSelection = true;

                if (Suggestions.SelectedIndex == -1)
                    Suggestions.SelectedIndex = Suggestions.Items.Count - 1;
                else
                    Suggestions.SelectedIndex -= 1;

                break;
            case Key.Down:
                _ignoreSelection = true;

                if (Suggestions.SelectedIndex + 1 == Suggestions.Items.Count)
                    Suggestions.SelectedIndex = -1;
                else
                    Suggestions.SelectedIndex += 1;

                break;
            case Key.Enter:
                if (Suggestions.SelectedItem is null)
                {
                    if (Suggestions.Items.Count != 0) Suggestions.SelectedIndex = 0;

                    break;
                }

                var selectionString = Suggestions.SelectedItem.ToString() ??
                                      throw new InvalidOperationException(
                                          "Selection couldn't be converted to string!");

                Selection = Suggestions.SelectedItem;

                Input.Text = selectionString;

                await _silverBullet.SelectionMade(null, selectionString);

                Suggestions.Focus();

                SelectionMade?.Invoke(this, new SelectionMadeEventArgs(this, Suggestions.SelectedItem));

                break;
            default:
                throw new NotImplementedException();
        }

        e.Handled = true;
    }

    private async void Suggestions_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.Handled || !ReferenceEquals(e.OriginalSource, Suggestions) || Suggestions.SelectedItem is null) return;

        if (Suggestions.SelectedItem is not string selection)
            selection = Suggestions.SelectedItem.ToString() ??
                        throw new InvalidOperationException("Selected item couldn't be converted to string!");

        e.Handled = true;

        if (_ignoreSelection)
        {
            _ignoreSelection = false;

            return;
        }

        Selection = Suggestions.SelectedItem;

        Input.Text = selection;

        await _silverBullet.SelectionMade(null, selection);

        Suggestions.Focus();

        SelectionMade?.Invoke(this, new SelectionMadeEventArgs(this, selection));
    }

    private void UserControl_OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        Suggestions.SelectedIndex = -1;
    }

    #endregion

    #region Fields & Properties

    private readonly SilverBullet<object?> _silverBullet;

    private bool _ignoreSelection;

    #endregion

    #region SelectionMade Event

    public event SelectionMadeHandler? SelectionMade;

    public delegate void SelectionMadeHandler(object sender, SelectionMadeEventArgs args);

    public class SelectionMadeEventArgs
    {
        public SelectionMadeEventArgs(object originalSource, object selection)
        {
            OriginalSource = originalSource;

            Selection = selection;
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        public object OriginalSource { get; }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
        public object Selection { get; }

        public bool Handled { get; set; }
    }

    #endregion
}