using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormSuccess : BaseForm
    {
        public FormSuccess() { BuildUi(); }

        private void BuildUi()
        {
            Label message = new Label { Text = "you are dek Banshi :)" , Font = Theme.Title, ForeColor = Color.FromArgb(0, 150, 95), TextAlign = ContentAlignment.MiddleCenter, Location = new Point(60, 100), Size = new Size(680, 60) };
            Panel art = new Panel { Location = new Point(300, 185), Size = new Size(200, 100) };
            art.Paint += delegate(object sender, PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Theme.Pink, 4F))
                    for (int i = 0; i < 7; i++)
                    {
                        int x = 15 + i * 27;
                        int y = 15 + (i % 3) * 21;
                        e.Graphics.DrawLine(pen, x - 7, y, x + 7, y);
                        e.Graphics.DrawLine(pen, x, y - 7, x, y + 7);
                        e.Graphics.DrawLine(pen, x - 5, y - 5, x + 5, y + 5);
                        e.Graphics.DrawLine(pen, x - 5, y + 5, x + 5, y - 5);
                    }
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
