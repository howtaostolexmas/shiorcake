using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Controls
{
    public class Y2KButton : Button
    {
        public Color FaceColor { get; set; }

        public Y2KButton()
        {
            FaceColor = Theme.Grey;
            Font = Theme.Bold;
            Cursor = Cursors.Hand;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = FaceColor;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(FaceColor);
            Rectangle face = new Rectangle(1, 1, Width - 3, Height - 3);
            using (Brush brush = new SolidBrush(FaceColor))
                graphics.FillRectangle(brush, face);
            bool pressed = Capture && (Control.MouseButtons & MouseButtons.Left) != 0;
            Color light = pressed ? SystemColors.ControlDark : Color.White;
            Color dark = pressed ? Color.White : Color.FromArgb(75, 70, 70);
            using (Pen lightPen = new Pen(light, 2F))
            using (Pen darkPen = new Pen(dark, 2F))
            {
                graphics.DrawLine(lightPen, 0, Height - 1, 0, 0);
                graphics.DrawLine(lightPen, 0, 0, Width - 1, 0);
                graphics.DrawLine(darkPen, 0, Height - 1, Width - 1, Height - 1);
                graphics.DrawLine(darkPen, Width - 1, 0, Width - 1, Height - 1);
            }
            TextRenderer.DrawText(graphics, Text, Font, face, Enabled ? ForeColor : SystemColors.GrayText, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Invalidate();
        }
    }
}
