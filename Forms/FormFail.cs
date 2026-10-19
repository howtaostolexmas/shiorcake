using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormFail : BaseForm
    {
        public FormFail() { BuildUi(); }

        private void BuildUi()
        {
            Label message = new Label { Text = "you are not dek Banshi :(" , Font = Theme.Title, ForeColor = Theme.ErrorRed, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(60, 100), Size = new Size(680, 60) };
            Panel art = new Panel { Location = new Point(340, 185), Size = new Size(120, 100) };
            art.Paint += delegate(object sender, PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Brush blue = new SolidBrush(Theme.HeaderBlue)) e.Graphics.FillEllipse(blue, 20, 10, 80, 65);
                using (Pen white = new Pen(Color.White, 4F)) { e.Graphics.DrawEllipse(white, 37, 30, 10, 10); e.Graphics.DrawEllipse(white, 72, 30, 10, 10); e.Graphics.DrawArc(white, 42, 49, 38, 20, 200, 140); }
                using (Pen dark = new Pen(Theme.Blue, 3F)) { e.Graphics.DrawLine(dark, 60, 10, 60, 0); e.Graphics.DrawEllipse(dark, 56, 0, 8, 8); }
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
