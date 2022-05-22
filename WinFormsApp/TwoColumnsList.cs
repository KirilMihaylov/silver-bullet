namespace WinFormsApp;

public partial class TwoColumnsList : UserControl
{
    public TwoColumnsList()
    {
        InitializeComponent();
    }

    public IEnumerable<Control> Items
    {
        get => _items;
        set
        {
            _items = value.ToList();

            foreach (var labelValue in _items) labelValue.Font = Font;

            SuspendLayout();

            Controls.Clear();

            for (var index = 0; index < (_items.Count + 1) / 2; index += 1)
            {
                var left = _items[index << 1];

                var right = _items.ElementAtOrDefault((index << 1) + 1);

                left.TabIndex = index << 1;

                if (right is not null) right.TabIndex = (index << 1) + 1;

                var tableLayoutPanel = NewTableLayoutPanel(left, right);

                Controls.Add(tableLayoutPanel);

                tableLayoutPanel.ResumeLayout(false);

                tableLayoutPanel.PerformLayout();
            }

            ResumeLayout(false);

            PerformLayout();
        }
    }

    private void OnLayout(object sender, LayoutEventArgs e)
    {
        var nextY = 0;

        foreach (Control control in Controls)
        {
            control.Location = new Point(0, nextY);

            control.PerformLayout();

            nextY += control.Height;
        }
    }

    private TableLayoutPanel NewTableLayoutPanel(Control left, Control? right)
    {
        var tableLayoutPanel = new TableLayoutPanel();

        tableLayoutPanel.SuspendLayout();

        tableLayoutPanel.ColumnCount = 2;
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        left.Dock = DockStyle.Fill;
        left.Location = Point.Empty;
        tableLayoutPanel.Controls.Add(left, 0, 0);
        if (right is null)
        {
            tableLayoutPanel.Height = left.Height;
        }
        else
        {
            right.Dock = DockStyle.Fill;
            right.Location = Point.Empty;
            tableLayoutPanel.Controls.Add(right, 1, 0);
            tableLayoutPanel.Height = left.Height < right.Height ? right.Height : left.Height;
        }

        tableLayoutPanel.Location = new Point(0, 0);
        tableLayoutPanel.Margin = new Padding(0);
        tableLayoutPanel.RowCount = 1;
        tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel.Size = new Size(300, 23);
        tableLayoutPanel.TabIndex = 0;
        tableLayoutPanel.Width = Width;

        return tableLayoutPanel;
    }

    private void OnFontChanged(object sender, EventArgs e)
    {
        foreach (Control control in Controls) control.Font = Font;

        PerformLayout();
    }

    private void OnSizeChanged(object sender, EventArgs e)
    {
        foreach (Control control in Controls) control.Width = Width;
    }
}