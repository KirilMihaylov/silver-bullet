namespace WinFormsApp;

public partial class LabelValue : UserControl
{
    private Font _font;

    public LabelValue()
    {
        InitializeComponent();
    }

    public LabelValue(string label, string value) : this()
    {
        LabelText = label;

        ValueText = value;
    }

    public override Font Font
    {
        get => _font;
        set
        {
            _font = value;

            label.Font = value;

            this.value.Font = value;
        }
    }

    public string LabelText
    {
        get => label.Text;
        set => label.Text = value;
    }

    public string ValueText
    {
        get => value.Text;
        set => this.value.Text = value;
    }

    private void OnLocationChanged(object sender, EventArgs e)
    {
        tableLayoutPanel.Location = Location;

        PerformLayout();
    }
}