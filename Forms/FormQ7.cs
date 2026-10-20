using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ7 : BaseForm
    {
        private readonly TileGrid grid = new TileGrid();

        // 4x4 colours, left to right, top to bottom
        // index 6 = accounting blue (correct), index 12 = engineering maroon (trap)
        private readonly string[] tileColors =
        {
            "#036CA1", "#0082C2", "#0393DA", "#02ABFF",
            "#016D96", "#0088BD", "#049FDA", "#02B9FE",
            "#007A98", "#068EB3", "#02AFDC", "#04C9FE",
            "#8A2431", "#02ABC2", "#03C1DA", "#06E1FE"
        };

        public FormQ7() { BuildUi(); }

        private void BuildUi()
        {
            Label question = new Label { Text = "Question 7 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            CaptchaCard card = new CaptchaCard(new Size(393, 393)) { Location = new Point(203, 36), Prompt = "Select all the squares with a", Subject = "สีประจำคณะ" };
            List<TileSpec> tiles = new List<TileSpec>();
            for (int i = 0; i < tileColors.Length; i++)
                tiles.Add(new TileSpec { BackColor = ColorTranslator.FromHtml(tileColors[i]) });
            grid.Build(4, 4, new Size(96, 96), 3, tiles);
            card.Body.Controls.Add(grid);
            card.RefreshClicked += delegate { grid.ClearSelection(); };
            card.VerifyClicked += delegate { Decide(); };
            Content.Controls.Add(question);
            Content.Controls.Add(card);
        }

        private void Decide()
        {
            if (grid.SelectionEquals(new[] { 12 })) Nav.Go(this, new FormSpecial());
            else if (grid.SelectionEquals(new[] { 6 })) Nav.Go(this, new FormSuccess());
            else Nav.Go(this, new FormFail());
        }
    }
}
