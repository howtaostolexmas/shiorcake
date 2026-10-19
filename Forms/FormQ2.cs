using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ2 : BaseForm
    {
        private readonly TileGrid grid = new TileGrid();
        private readonly int[] correct = { 0, 2, 3, 4, 5, 6, 9, 12, 13, 14 };
        private readonly string[] labels = { "มหิต", "โรงอาหารคณะอื่น", "มหิต", "มหิต", "มหิต", "มหิต", "มหิต", "ภาพหลอก", "โรงอาหารคณะอื่น", "มหิต", "โรงอาหารคณะอื่น", "โรงอาหารคณะอื่น", "มหิต", "มหิต", "มหิต", "โรงอาหารคณะอื่น" };
        public FormQ2() { BuildUi(); }

        private void BuildUi()
        {
            Label question = new Label { Text = "Question 2 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            CaptchaCard card = new CaptchaCard(new Size(393, 393)) { Location = new Point(203, 36), Prompt = "Select all the squares with a", Subject = "โรงอาหารมหิต" };
            List<TileSpec> tiles = new List<TileSpec>();
            for (int i = 0; i < labels.Length; i++)
            {
                int index = i;
                Color text = (i == 1 || i == 7 || i == 8 || i == 10 || i == 11 || i == 15) ? Theme.ErrorRed : Color.Black;
                TileSpec spec = new TileSpec { Text = labels[i], TextColor = text, BackColor = Theme.TileGrey };
                spec.Image = Assets.Get("q2_" + i.ToString("00"), delegate { return CreateLabelImage(labels[index], text); });
                tiles.Add(spec);
            }
            grid.Build(4, 4, new Size(96, 96), 3, tiles);
            grid.Location = new Point(0, 0);
            card.Body.Controls.Add(grid);
            card.RefreshClicked += delegate { grid.ClearSelection(); };
            card.VerifyClicked += delegate { Decide(); };
            Content.Controls.Add(question);
            Content.Controls.Add(card);
        }

        private Image CreateLabelImage(string label, Color color)
        {
            Bitmap bitmap = new Bitmap(96, 96);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Font font = new Font("Tahoma", 16F, FontStyle.Bold))
            {
                graphics.Clear(Theme.TileGrey);
                TextRenderer.DrawText(graphics, label, font, new Rectangle(3, 3, 90, 90), color, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
            }
            return bitmap;
        }

        private void Decide()
        {
            if (grid.SelectionEquals(correct)) Nav.Go(this, new FormQ3());
            else Nav.Go(this, new FormFail());
        }
    }
}
