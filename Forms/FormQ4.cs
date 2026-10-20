using System;
using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ4 : BaseForm
    {
        // ตึกไชยยศ box on the 700x400 map image
        private const int TargetLeft = 337;
        private const int TargetTop = 254;
        private const int TargetRight = 389;
        private const int TargetBottom = 308;
        private readonly PictureBox map = new PictureBox();
        private Point? marker;
        private readonly Image mapImage;

        public FormQ4() { mapImage = Assets.Get("q4_map", delegate { return Assets.CreateMapPlaceholder("q4_map", 700, 400); }); BuildUi(); }

        private void BuildUi()
        {
            Label question = new Label { Text = "Question 4 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            CaptchaCard card = new CaptchaCard(new Size(700, 400)) { Location = new Point(49, 30), Prompt = "Select the location of", Subject = "ตึกไชยยศ" };
            map.Location = Point.Empty;
            map.Size = new Size(700, 400);
            map.Image = mapImage;
            map.SizeMode = PictureBoxSizeMode.StretchImage;
            map.Paint += PaintMarker;
            map.MouseClick += MapClicked;
            card.VerifyButton.Enabled = false;
            card.VerifyClicked += delegate { Decide(); };
            card.Body.Controls.Add(map);
            card.RefreshClicked += delegate { marker = null; card.VerifyButton.Enabled = false; map.Invalidate(); };
            Content.Controls.Add(question);
            Content.Controls.Add(card);
        }

        private void MapClicked(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            if (mouse == null) return;
            marker = mouse.Location;
            ((CaptchaCard)map.Parent.Parent).VerifyButton.Enabled = true;
            map.Invalidate();
        }

        private void PaintMarker(object sender, PaintEventArgs e)
        {
            if (!marker.HasValue) return;
            using (Pen pen = new Pen(Theme.Pink, 4F))
            {
                Point p = marker.Value;
                e.Graphics.DrawLine(pen, p.X - 8, p.Y - 8, p.X + 8, p.Y + 8);
                e.Graphics.DrawLine(pen, p.X - 8, p.Y + 8, p.X + 8, p.Y - 8);
            }
        }

        private void Decide()
        {
            if (!marker.HasValue) { Nav.Go(this, new FormFail()); return; }
            int imageX = marker.Value.X * mapImage.Width / map.Width;
            int imageY = marker.Value.Y * mapImage.Height / map.Height;
            bool insideX = imageX >= TargetLeft && imageX <= TargetRight;
            bool insideY = imageY >= TargetTop && imageY <= TargetBottom;
            if (insideX && insideY) Nav.Go(this, new FormQ5());
            else Nav.Go(this, new FormFail());
        }
    }
}
