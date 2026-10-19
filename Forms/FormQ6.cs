using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ6 : BaseForm
    {
        private const int Rows = 2;
        private const int Cols = 2;
        private const int CorrectIndex = 0;
        private readonly TileGrid grid = new TileGrid();
        public FormQ6() { BuildUi(); }

        private void BuildUi()
        {
            Label question = new Label { Text = "Question 6 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            CaptchaCard card = new CaptchaCard(new Size(383, 383)) { Location = new Point(208, 40), Prompt = "Select the squares with", Subject = "Correct answer" };
            List<TileSpec> tiles = new List<TileSpec>();
            for (int i = 0; i < Rows * Cols; i++)
            {
                int index = i;
                Color tint = Color.FromArgb(205 + i * 8, 220 - i * 8, 232 - i * 9);
                tiles.Add(new TileSpec { Image = Assets.Get("q6_" + i, delegate { return CreateDistinctPlaceholder(index, tint); }), BackColor = Theme.TileGrey });
            }
            grid.Build(Rows, Cols, new Size(190, 190), 3, tiles);
            card.Body.Controls.Add(grid);
            card.RefreshClicked += delegate { grid.ClearSelection(); };
            card.VerifyClicked += delegate { Decide(); };
            Content.Controls.Add(question);
            Content.Controls.Add(card);
        }

        private Image CreateDistinctPlaceholder(int index, Color color)
        {
            Bitmap bitmap = new Bitmap(190, 190);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Brush brush = new SolidBrush(color))
            using (Font font = new Font("Tahoma", 12F, FontStyle.Bold))
            {
                graphics.Clear(Color.White);
                graphics.FillRectangle(brush, 5 + index * 5, 5 + index * 5, 180 - index * 10, 180 - index * 10);
                TextRenderer.DrawText(graphics, "q6_" + index, font, new Rectangle(0, 75, 190, 40), Color.Black, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            return bitmap;
        }

        private void Decide()
        {
            if (grid.SelectionEquals(new[] { CorrectIndex })) Nav.Go(this, new FormQ7());
            else Nav.Go(this, new FormFail());
        }
    }
}
