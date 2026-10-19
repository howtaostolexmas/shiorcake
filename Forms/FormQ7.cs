using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ7 : BaseForm
    {
        private readonly TileGrid grid = new TileGrid();
        public FormQ7() { BuildUi(); }

        private void BuildUi()
        {
            Label question = new Label { Text = "Question 7 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            CaptchaCard card = new CaptchaCard(new Size(393, 393)) { Location = new Point(203, 36), Prompt = "Select all the squares with a", Subject = "สีประจำคณะ" };
            List<TileSpec> tiles = new List<TileSpec>();
            for (int i = 0; i < 16; i++)
                tiles.Add(new TileSpec { BackColor = GetTileColor(i) });
            grid.Build(4, 4, new Size(96, 96), 3, tiles);
            card.Body.Controls.Add(grid);
            card.RefreshClicked += delegate { grid.ClearSelection(); };
            card.VerifyClicked += delegate { Decide(); };
            Content.Controls.Add(question);
            Content.Controls.Add(card);
        }

        private Color GetTileColor(int index)
        {
            if (index == 6) return Color.FromArgb(0, 161, 223);
            if (index == 12) return Color.FromArgb(139, 35, 50);
            int red = 0;
            int green = 110 + (120 * index / 15);
            int blue = 160 + (95 * index / 15);
            Color color = Color.FromArgb(red, green, blue);
#if DEBUG
            Debug.Assert(Math.Abs(color.R - 0) >= 10 || Math.Abs(color.G - 161) >= 10 || Math.Abs(color.B - 223) >= 10);
#endif
            return color;
        }

        private void Decide()
        {
            if (grid.SelectionEquals(new[] { 12 })) Nav.Go(this, new FormSpecial());
            else if (grid.SelectionEquals(new[] { 6 })) Nav.Go(this, new FormSuccess());
            else Nav.Go(this, new FormFail());
        }
    }
}
