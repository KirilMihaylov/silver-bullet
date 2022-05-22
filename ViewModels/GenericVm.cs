namespace ViewModelsLib;

public abstract class GenericVm : INotifyPropertyChanged
{
    #region Constructors

    protected GenericVm(string? variable, InternalDelegates internalDelegates)
    {
        Variable = _variable = variable;

        Selector = _selector = internalDelegates.Selector;

        ResultsRetriever = _resultsRetriever = internalDelegates.ResultsRetriever;
    }

    #endregion

    #region Helper Methods

    protected static string ListValues(IEnumerable<string> enumerable)
    {
        var queryable = enumerable.AsQueryable();

        return queryable.Any()
            ? queryable.Skip(1).Aggregate(queryable.First(), (aggregated, value) => $"{aggregated}, {value}")
            : string.Empty;
    }

    #endregion

    #region Protected Abstract Methods

    protected abstract IReadOnlyDictionary<string, InternalDelegates> InternalDelegatesMap();

    #endregion

    #region Properties

    #region Variables Property

    public IEnumerable<string> Variables => InternalDelegatesMap().Keys;

    #endregion

    #region Selection Property

    public object? Selection
    {
        set
        {
            OnPropertyChanged();

            Results = value is null ? Array.Empty<IEnumerable<LabelValuePair>>() : ResultsRetriever(value).ToArray();
        }
    }

    #endregion

    #region Variable Property

    private string? _variable;

    public string? Variable
    {
        get => _variable;
        set
        {
            if (value is not null && !Variables.Contains(value))
                throw new ArgumentOutOfRangeException(nameof(value), value, "Variable not in defined range!");

            _variable = value;

            OnPropertyChanged();

            if (value is null) return;

            var delegates = InternalDelegatesMap()[value];

            Selector = delegates.Selector;

            ResultsRetriever = delegates.ResultsRetriever;
        }
    }

    #endregion

    #region Selector Property

    private Func<object?, IEnumerable<object>> _selector;

    public Func<object?, IEnumerable<object>> Selector
    {
        get => _selector;
        private set
        {
            _selector = value;

            OnPropertyChanged();
        }
    }

    #endregion

    #region ResultsRetriever Property

    private Func<object, IEnumerable<IEnumerable<LabelValuePair>>> _resultsRetriever;

    private Func<object, IEnumerable<IEnumerable<LabelValuePair>>> ResultsRetriever
    {
        get => _resultsRetriever;
        set
        {
            _resultsRetriever = value;

            Results = Array.Empty<IEnumerable<LabelValuePair>>();

            OnPropertyChanged();
        }
    }

    #endregion

    #region ResultsItemsSource Property

    private IEnumerable<LabelValuePair> _resultsItemsSource = Array.Empty<LabelValuePair>();

    public IEnumerable<LabelValuePair> ResultsItemsSource
    {
        get => _resultsItemsSource;
        private set
        {
            _resultsItemsSource = value;

            OnPropertyChanged();
        }
    }

    #endregion

    #region Results Property

    private IList<IEnumerable<LabelValuePair>> _results = Array.Empty<IEnumerable<LabelValuePair>>();

    private IList<IEnumerable<LabelValuePair>> Results
    {
        get => _results;
        set
        {
            _results = value;

            HasPreviousResult = false;

            HasNextResult = value.Count > 1;

            OnPropertyChanged();

            ResultsItemsSource = value.Count == 0 ? Array.Empty<LabelValuePair>() : value.First();
        }
    }

    #endregion

    #endregion

    #region Result Switching

    #region Properties

    #region CurrentIndex Property

    private int _currentIndex;

    private int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            if (value < 0 || Results.Count <= value) throw new ArgumentOutOfRangeException(nameof(value));

            _currentIndex = value;

            OnPropertyChanged();

            ResultsItemsSource = Results[value];
        }
    }

    #endregion

    #region HasPreviousResult Property

    private bool _hasPreviousResult;

    public bool HasPreviousResult
    {
        get => _hasPreviousResult;
        private set
        {
            _hasPreviousResult = value;

            OnPropertyChanged();
        }
    }

    #endregion

    #region HasNextResult Property

    private bool _hasNextResult;

    public bool HasNextResult
    {
        get => _hasNextResult;
        private set
        {
            _hasNextResult = value;

            OnPropertyChanged();
        }
    }

    #endregion

    #endregion

    #region Result Switcher Methods

    public void PreviousResult()
    {
        HasPreviousResult = CurrentIndex != 1;

        HasNextResult = true;

        CurrentIndex -= 1;
    }

    public void NextResult()
    {
        HasPreviousResult = true;

        HasNextResult = CurrentIndex + 2 < Results.Count;

        CurrentIndex += 1;
    }

    #endregion

    #endregion

    #region INotifyPropertyChanged implementation

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
}

public struct LabelValuePair
{
    public string Label { init; get; }
    public object Value { init; get; }
}

public struct InternalDelegates
{
    public Func<object, IEnumerable<object>> Selector;
    public Func<object, IEnumerable<IEnumerable<LabelValuePair>>> ResultsRetriever;
}