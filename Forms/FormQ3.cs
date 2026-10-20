using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ3 : BaseForm
    {
        private readonly TileGrid grid = new TileGrid();

        // Fixed 10x10 grid (1 string = 1 row)
        private readonly string[] rows =
        {
            "LMAAINATNI",
            "TDPLRPKUDH",
            "RHBSTGHRSS",
            "NCPUGSKMTC",
            "IUGPSODTAA",
            "HGBNLIBPTC",
            "SECNUENEHG",
            "NOANNCDEPS",
            "ARGBRSGCSG",
            "BUGRBONLCS"
        };

        // Cell number = row * 10 + column
        private readonly int[] correctCells =
        {
            22, 33, 44, 55, 66, 77, 88, 99, // BUSINESS: diagonal, down-right
            90, 80, 70, 60, 50, 40,         // BANSHI: column 0, bottom to top
            28, 38, 48, 58,                 // STAT: column 8, top to bottom
            94, 83, 72                      // BBA: diagonal, up-left
        };

        // Trap word INTANIA: top row, right to left
        private readonly int[] intaniaCells = { 9, 8, 7, 6, 5, 4, 3 };

        public FormQ3() { BuildUi(); }

        private void BuildUi()
        {
            Label question = new Label { Text = "Question 3 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            CaptchaCard card = new CaptchaCard(new Size(378, 378)) { Location = new Point(211, 40), Prompt = "Select all the squares with a", Subject = "คำศัพท์หมวดเด็กคณะบัญชี (ทุกภาค)", UnderlineSubject = true };
            List<TileSpec> tiles = new List<TileSpec>();
            for (int row = 0; row < 10; row++)
                for (int col = 0; col < 10; col++)
                    tiles.Add(new TileSpec { Text = rows[row][col].ToString(), TextColor = Color.Black, BackColor = Theme.TileGrey });
            grid.Build(10, 10, new Size(36, 36), 2, tiles);
            card.Body.Controls.Add(grid);
            card.RefreshClicked += delegate { grid.ClearSelection(); };
            card.VerifyClicked += delegate { Decide(); };
            Content.Controls.Add(question);
            Content.Controls.Add(card);
        }

        private void Decide()
        {
            // Selecting the whole word INTANIA goes to the special ending, even with other squares selected
            if (grid.Selected.IsSupersetOf(intaniaCells)) Nav.Go(this, new FormSpecial());
            else if (grid.SelectionEquals(correctCells)) Nav.Go(this, new FormQ4());
            else Nav.Go(this, new FormFail());
        }
    }
}
