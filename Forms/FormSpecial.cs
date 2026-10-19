using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormSpecial : BaseForm
    {
        public FormSpecial() { BuildUi(); }

        private void BuildUi()
        {
            Label message = new Label { Text = "You are witsawa fang tua ma >:)" , Font = Theme.Title, ForeColor = Color.FromArgb(139, 35, 50), TextAlign = ContentAlignment.MiddleCenter, Location = new Point(35, 100), Size = new Size(730, 60) };
            Panel art = new Panel { Location = new Point(340, 190), Size = new Size(120, 90) };
            art.Paint += delegate(object sender, PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Brush red = new SolidBrush(Color.FromArgb(139, 35, 50))) e.Graphics.FillEllipse(red, 20, 8, 80, 70);
                using (Pen pink = new Pen(Theme.Pink, 4F)) { e.Graphics.DrawLine(pink, 38, 35, 51, 40); e.Graphics.DrawLine(pink, 69, 40, 82, 35); e.Graphics.DrawArc(pink, 40, 43, 42, 25, 10, 160); }
            };
            AddChoices(message, art);
        }

        private void AddChoices(Control message, Control art)
        {
            Label again = new Label { Text = "Try Again?", Font = Theme.Bold, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(250, 335), Size = new Size(300, 32) };
            Y2KButton yes = new Y2KButton { Text = "Yes", FaceColor = Theme.Green, Location = new Point(300, 390), Size = new Size(90, 42) };
            Y2KButton no = new Y2KButton { Text = "No", FaceColor = Theme.Pink, Location = new Point(410, 390), Size = new Size(90, 42) };
            yes.Click += delegate { Nav.Restart(this); };
            no.Click += delegate { Nav.ExitApp(); };
            Content.Controls.Add(message);
            Content.Controls.Add(art);
            Content.Controls.Add(again);
            Content.Controls.Add(yes);
            Content.Controls.Add(no);
        }
    }
}
