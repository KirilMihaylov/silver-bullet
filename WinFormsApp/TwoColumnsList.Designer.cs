namespace WinFormsApp;

partial class TwoColumnsList
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();

        // 
        // TwoColumnsList
        // 
        this.AutoScaleMode = AutoScaleMode.Inherit;
        this.FontChanged += OnFontChanged;
        this.Layout += OnLayout;
        this.Margin = new Padding(0);
        this.SizeChanged += OnSizeChanged;
    }

    #endregion

    private List<Control> _items = new();
}