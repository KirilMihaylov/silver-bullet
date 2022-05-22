namespace WinFormsApp;

partial class LabelValue
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

        this.tableLayoutPanel = new TableLayoutPanel();
        this.label = new Label();
        this.value = new TextBox();
        this.tableLayoutPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // tableLayoutPanel
        // 
        this.tableLayoutPanel.ColumnCount = 2;
        this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.tableLayoutPanel.Controls.Add(this.label, 0, 0);
        this.tableLayoutPanel.Controls.Add(this.value, 1, 0);
        this.tableLayoutPanel.Dock = DockStyle.Fill;
        this.tableLayoutPanel.Location = Location;
        this.tableLayoutPanel.Margin = new Padding(0);
        this.tableLayoutPanel.Name = "tableLayoutPanel";
        this.tableLayoutPanel.RowCount = 1;
        this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        this.tableLayoutPanel.Size = new Size(300, 23);
        this.tableLayoutPanel.TabIndex = 0;
        // 
        // label
        // 
        this.label.Dock = DockStyle.Fill;
        this.label.Location = new Point(0, 0);
        this.label.Margin = new Padding(0);
        this.label.Size = new Size(150, 23);
        this.label.TabIndex = 0;
        this.label.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // value
        // 
        this.value.Dock = DockStyle.Fill;
        this.value.Location = new Point(150, 0);
        this.value.Margin = new Padding(0);
        this.value.ReadOnly = true;
        this.value.Size = new Size(150, 23);
        this.value.TabIndex = 0;
        // 
        // LabelValue
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add(this.tableLayoutPanel);
        this.Margin = new Padding(0);
        this.Size = new Size(300, 23);
        this.LocationChanged += OnLocationChanged;
        this.tableLayoutPanel.ResumeLayout(false);
        this.tableLayoutPanel.PerformLayout();
        this.ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel tableLayoutPanel;
    private Label label;
    private TextBox value;
}