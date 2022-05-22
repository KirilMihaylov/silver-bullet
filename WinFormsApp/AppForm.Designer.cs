namespace WinFormsApp;

partial class AppForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();

        this.mainTableLayoutPanel = new();
        this.viewButtonsTableLayoutPanel = new();
        this.suggestionsTableLayoutPanel = new();
        this.switchResultsTableLayoutPanel = new();
        this.booksViewButton = new();
        this.magazinesViewButton = new();
        this.comicsViewButton = new();
        this.variableSelectorComboBox = new();
        this.suggestionBox = new();
        this.suggestionBoxElementHost = new();
        this.resultsTwoColumnsList = new();
        this.previousResultButton = new();
        this.nextResultButton = new();
        this.mainTableLayoutPanel.SuspendLayout();
        this.viewButtonsTableLayoutPanel.SuspendLayout();
        this.suggestionsTableLayoutPanel.SuspendLayout();
        this.switchResultsTableLayoutPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // mainTableLayoutPanel
        // 
        this.mainTableLayoutPanel.ColumnCount = 1;
        this.mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        this.mainTableLayoutPanel.Controls.Add(this.viewButtonsTableLayoutPanel, 0, 0);
        this.mainTableLayoutPanel.Controls.Add(this.suggestionsTableLayoutPanel, 0, 1);
        this.mainTableLayoutPanel.Controls.Add(this.resultsTwoColumnsList, 0, 2);
        this.mainTableLayoutPanel.Controls.Add(this.switchResultsTableLayoutPanel, 0, 3);
        this.mainTableLayoutPanel.Dock = DockStyle.Fill;
        this.mainTableLayoutPanel.Margin = Padding.Empty;
        this.mainTableLayoutPanel.Padding = Padding.Empty;
        this.mainTableLayoutPanel.RowCount = 4;
        this.mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692307F));
        this.mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692307F));
        this.mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 76.92308F));
        this.mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 7.692307F));
        this.mainTableLayoutPanel.TabStop = false;
        // 
        // viewButtonsTableLayoutPanel
        // 
        this.viewButtonsTableLayoutPanel.ColumnCount = 3;
        this.viewButtonsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        this.viewButtonsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        this.viewButtonsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        this.viewButtonsTableLayoutPanel.Controls.Add(this.booksViewButton, 0, 0);
        this.viewButtonsTableLayoutPanel.Controls.Add(this.magazinesViewButton, 1, 0);
        this.viewButtonsTableLayoutPanel.Controls.Add(this.comicsViewButton, 2, 0);
        this.viewButtonsTableLayoutPanel.Dock = DockStyle.Fill;
        this.viewButtonsTableLayoutPanel.Margin = Padding.Empty;
        this.viewButtonsTableLayoutPanel.Padding = Padding.Empty;
        this.viewButtonsTableLayoutPanel.RowCount = 1;
        this.viewButtonsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        this.viewButtonsTableLayoutPanel.TabStop = false;
        // 
        // booksViewButton
        // 
        this.booksViewButton.Click += ChangeViewButton_OnClick;
        this.booksViewButton.Dock = DockStyle.Fill;
        this.booksViewButton.Margin = Padding.Empty;
        this.booksViewButton.Padding = Padding.Empty;
        this.booksViewButton.TabIndex = 5;
        this.booksViewButton.Text = "Books";
        this.booksViewButton.UseVisualStyleBackColor = true;
        // 
        // magazinesViewButton
        // 
        this.magazinesViewButton.Click += ChangeViewButton_OnClick;
        this.magazinesViewButton.Dock = DockStyle.Fill;
        this.magazinesViewButton.Margin = Padding.Empty;
        this.magazinesViewButton.Padding = Padding.Empty;
        this.magazinesViewButton.TabIndex = 6;
        this.magazinesViewButton.Text = "Magazines";
        this.magazinesViewButton.UseVisualStyleBackColor = true;
        // 
        // comicsViewButton
        // 
        this.comicsViewButton.Click += ChangeViewButton_OnClick;
        this.comicsViewButton.Dock = DockStyle.Fill;
        this.comicsViewButton.Margin = Padding.Empty;
        this.comicsViewButton.Padding = Padding.Empty;
        this.comicsViewButton.TabIndex = 7;
        this.comicsViewButton.Text = "Comics";
        this.comicsViewButton.UseVisualStyleBackColor = true;
        // 
        // suggestionsTableLayoutPanel
        // 
        this.suggestionsTableLayoutPanel.ColumnCount = 2;
        this.suggestionsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
        this.suggestionsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.66666F));
        this.suggestionsTableLayoutPanel.Controls.Add(this.variableSelectorComboBox, 0, 0);
        this.suggestionsTableLayoutPanel.Controls.Add(this.suggestionBoxElementHost, 1, 0);
        this.suggestionsTableLayoutPanel.Dock = DockStyle.Fill;
        this.suggestionsTableLayoutPanel.Margin = Padding.Empty;
        this.suggestionsTableLayoutPanel.Padding = Padding.Empty;
        this.suggestionsTableLayoutPanel.RowCount = 1;
        this.suggestionsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        this.suggestionsTableLayoutPanel.TabStop = false;
        // 
        // variableSelectorComboBox
        // 
        this.variableSelectorComboBox.Dock = DockStyle.Fill;
        this.variableSelectorComboBox.Font = new Font(
            "Century",
            11.25F,
            FontStyle.Regular,
            GraphicsUnit.Point
        );
        this.variableSelectorComboBox.FormattingEnabled = true;
        this.variableSelectorComboBox.Margin = Padding.Empty;
        this.variableSelectorComboBox.Padding = Padding.Empty;
        this.variableSelectorComboBox.SelectionChangeCommitted +=
            VariableSelectorComboBox_OnSelectionChangeCommitted;
        this.variableSelectorComboBox.TabIndex = 0;
        // 
        // suggestionBox
        // 
        this.suggestionBox.SelectionMade += SuggestionBox_OnSelectionMade;
        // 
        // suggestionBoxElementHost
        // 
        this.suggestionBoxElementHost.Child = this.suggestionBox;
        this.suggestionBoxElementHost.Dock = DockStyle.Fill;
        this.suggestionBoxElementHost.Font = new Font(
            "Century",
            11.25F,
            FontStyle.Regular,
            GraphicsUnit.Point
        );
        this.suggestionBoxElementHost.Margin = Padding.Empty;
        this.suggestionBoxElementHost.Padding = Padding.Empty;
        this.suggestionBoxElementHost.TabIndex = 1;
        // 
        // resultsTwoColumnsList
        // 
        this.resultsTwoColumnsList.Dock = DockStyle.Fill;
        this.resultsTwoColumnsList.Margin = Padding.Empty;
        this.resultsTwoColumnsList.Padding = Padding.Empty;
        this.resultsTwoColumnsList.TabIndex = 2;
        this.resultsTwoColumnsList.TabStop = true;
        // 
        // switchResultsTableLayoutPanel
        // 
        this.switchResultsTableLayoutPanel.ColumnCount = 2;
        this.switchResultsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.switchResultsTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.switchResultsTableLayoutPanel.Controls.Add(this.nextResultButton, 1, 0);
        this.switchResultsTableLayoutPanel.Controls.Add(this.previousResultButton, 0, 0);
        this.switchResultsTableLayoutPanel.Dock = DockStyle.Fill;
        this.switchResultsTableLayoutPanel.Margin = Padding.Empty;
        this.switchResultsTableLayoutPanel.Padding = Padding.Empty;
        this.switchResultsTableLayoutPanel.RowCount = 1;
        this.switchResultsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        this.switchResultsTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
        this.switchResultsTableLayoutPanel.TabStop = false;
        // 
        // previousResultButton
        // 
        this.previousResultButton.Click += this.ChangeResultButton_OnClick;
        this.previousResultButton.Dock = DockStyle.Fill;
        this.previousResultButton.Enabled = false;
        this.previousResultButton.Margin = Padding.Empty;
        this.previousResultButton.Padding = Padding.Empty;
        this.previousResultButton.TabIndex = 3;
        this.previousResultButton.Text = "Previous";
        this.previousResultButton.UseVisualStyleBackColor = true;
        // 
        // nextResultButton
        // 
        this.nextResultButton.Click += ChangeResultButton_OnClick;
        this.nextResultButton.Dock = DockStyle.Fill;
        this.nextResultButton.Enabled = false;
        this.nextResultButton.Margin = Padding.Empty;
        this.nextResultButton.Padding = Padding.Empty;
        this.nextResultButton.TabIndex = 4;
        this.nextResultButton.Text = "Next";
        this.nextResultButton.UseVisualStyleBackColor = true;
        // 
        // AppForm
        // 
        this.AutoScaleDimensions = new SizeF(7F, 15F);
        this.AutoScaleMode = AutoScaleMode.Font;
        this.Controls.Add(this.mainTableLayoutPanel);
        this.Load += this.AppForm_Load;
        this.Margin = Padding.Empty;
        this.MinimumSize = new Size(640, 480);
        this.Padding = Padding.Empty;
        this.Size = new Size(640, 480);
        this.Text = "WinForms App";
        this.viewButtonsTableLayoutPanel.ResumeLayout(false);
        this.suggestionsTableLayoutPanel.ResumeLayout(false);
        this.switchResultsTableLayoutPanel.ResumeLayout(false);
        this.mainTableLayoutPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel mainTableLayoutPanel;
    private TableLayoutPanel viewButtonsTableLayoutPanel;
    private TableLayoutPanel suggestionsTableLayoutPanel;
    private TableLayoutPanel switchResultsTableLayoutPanel;
    private Button booksViewButton;
    private Button magazinesViewButton;
    private Button comicsViewButton;
    private ComboBox variableSelectorComboBox;
    private SuggestionBox suggestionBox;
    private ElementHost suggestionBoxElementHost;
    private TwoColumnsList resultsTwoColumnsList;
    private Button previousResultButton;
    private Button nextResultButton;
}