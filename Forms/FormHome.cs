using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormHome : BaseForm
    {
        public FormHome() { BuildUi(); }

        private void BuildUi()
        {
            Label title = new Label { Text = Members.ProjectTitle, Font = Theme.Title, ForeColor = Theme.Blue, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(70, 22), Size = new Size(660, 42) };
            Label subtitle = new Label { Text = "Prove you are a real Dek Banshi", Font = Theme.Bold, ForeColor = Color.FromArgb(70, 70, 70), TextAlign = ContentAlignment.MiddleCenter, Location = new Point(100, 66), Size = new Size(600, 28) };
            Content.Controls.Add(title);
            Content.Controls.Add(subtitle);
            AddMember(145, Assets.Get("member1", delegate { return Assets.CreateSilhouette("member1"); }), Members.Member1Name, Members.Member1Id);
            AddMember(455, Assets.Get("member2", delegate { return Assets.CreateSilhouette("member2"); }), Members.Member2Name, Members.Member2Id);
            Y2KButton next = new Y2KButton { Text = "Next", FaceColor = Theme.Green, Location = new Point(470, 465), Size = new Size(120, 44) };
            next.Click += delegate { Nav.Go(this, new FormQ1()); };
            Y2KButton exit = new Y2KButton { Text = "Exit", FaceColor = Theme.Pink, Location = new Point(610, 465), Size = new Size(120, 44) };
            exit.Click += delegate { Nav.ExitApp(); };
            Content.Controls.Add(next);
            Content.Controls.Add(exit);
            Content.Paint += delegate(object sender, PaintEventArgs e)
            {
                using (Pen pen = new Pen(Theme.Pink, 5F))
                    e.Graphics.DrawRectangle(pen, 65, 15, 670, 450);
            };
        }

        private void AddMember(int x, Image image, string name, string id)
        {
            Panel panel = new Panel { Location = new Point(x, 110), Size = new Size(200, 340), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            PictureBox photo = new PictureBox { Image = image, SizeMode = PictureBoxSizeMode.Zoom, Location = new Point(19, 18), Size = new Size(160, 160), BorderStyle = BorderStyle.FixedSingle };
            Label memberName = new Label { Text = name, Font = Theme.Bold, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(8, 198), Size = new Size(182, 30) };
            Label studentId = new Label { Text = id, Font = Theme.Main, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(8, 235), Size = new Size(182, 26), ForeColor = Theme.Blue };
            panel.Controls.Add(photo);
            panel.Controls.Add(memberName);
            panel.Controls.Add(studentId);
            Content.Controls.Add(panel);
        }
    }
}
