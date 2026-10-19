using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DekBanshiCaptcha.Core
{
    public class BaseForm : Form
    {
        protected Panel Content { get; private set; }

        public BaseForm()
        {
            ClientSize = new Size(800, 600);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            AutoScaleMode = AutoScaleMode.None;
            DoubleBuffered = true;
            Font = Theme.Main;
            BackColor = Theme.Grey;
            BuildChrome();
        }

        private void BuildChrome()
        {
            Panel titleBar = new Panel();
            titleBar.Location = new Point(0, 0);
            titleBar.Size = new Size(800, 28);
            titleBar.Paint += PaintTitleBar;
            Controls.Add(titleBar);

            Content = new PatternPanel();
            Content.Location = new Point(0, 28);
            Content.Size = new Size(800, 550);
            Content.BackColor = Theme.Grey;
            Controls.Add(Content);

            Panel statusBar = new Panel();
            statusBar.Location = new Point(0, 578);
            statusBar.Size = new Size(800, 22);
            statusBar.BackColor = Color.FromArgb(215, 210, 207);
            statusBar.Paint += delegate(object sender, PaintEventArgs e)
            {
                e.Graphics.DrawLine(SystemPens.ControlDark, 0, 0, 800, 0);
                TextRenderer.DrawText(e.Graphics, "Done", Theme.Small, new Rectangle(8, 2, 100, 18), Color.DimGray, TextFormatFlags.VerticalCenter);
            };
            Controls.Add(statusBar);
        }

        private void PaintTitleBar(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 800, 28), Theme.Cyan, Theme.Blue, 0F))
                e.Graphics.FillRectangle(brush, 0, 0, 800, 28);
            TextRenderer.DrawText(e.Graphics, "DekBanshi.exe", Theme.Bold, new Point(8, 5), Color.White);
            for (int i = 0; i < 3; i++)
            {
                Rectangle button = new Rectangle(738 + i * 20, 5, 16, 16);
                ControlPaint.DrawButton(e.Graphics, button, ButtonState.Normal);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Application.Exit();
        }

        private sealed class PatternPanel : Panel
        {
            public PatternPanel()
            {
                DoubleBuffered = true;
            }

            protected override void OnPaintBackground(PaintEventArgs e)
            {
                e.Graphics.Clear(Theme.Grey);
                using (Pen pen = new Pen(Color.FromArgb(40, Color.White)))
                {
                    for (int x = 0; x < Width; x += 24)
                        e.Graphics.DrawLine(pen, x, 0, x, Height);
                    for (int y = 0; y < Height; y += 24)
                        e.Graphics.DrawLine(pen, 0, y, Width, y);
                }
            }
        }
    }
}
