using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedPanel : Panel
{
    private int _borderRadius = 10;

    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(10)]
    [Description("Sets the border radius of the panel.")]
    public int BorderRadius
    {
        get { return _borderRadius; }
        set
        {
            _borderRadius = value;
            UpdateRegion();
        }
    }

    protected override void OnResize(EventArgs eventargs)
    {
        base.OnResize(eventargs);
        UpdateRegion();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        // Optionally, draw a border
        using (Pen pen = new Pen(this.BackColor, 1)) // Use the panel's background color for the border
        {
            e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }
    }

    private void UpdateRegion()
    {
        GraphicsPath path = new GraphicsPath();
        path.AddArc(0, 0, BorderRadius, BorderRadius, 180, 90);
        path.AddArc(Width - BorderRadius, 0, BorderRadius, BorderRadius, 270, 90);
        path.AddArc(Width - BorderRadius, Height - BorderRadius, BorderRadius, BorderRadius, 0, 90);
        path.AddArc(0, Height - BorderRadius, BorderRadius, BorderRadius, 90, 90);
        path.CloseFigure();
        Region = new Region(path);
    }
}
