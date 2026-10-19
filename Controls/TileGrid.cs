using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Controls
{
    public class TileSpec
    {
        public Image Image;
        public string Text;
        public Color TextColor;
        public Color BackColor;
    }

    public class TileGrid : Panel
    {
        private readonly HashSet<int> selected = new HashSet<int>();
        public HashSet<int> Selected { get { return selected; } }
        public event EventHandler SelectionChanged;

        public TileGrid()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
        }

        public void Build(int rows, int cols, Size tileSize, int gap, IList<TileSpec> tiles)
        {
            Controls.Clear();
            selected.Clear();
            for (int index = 0; index < rows * cols; index++)
            {
                TileSpec spec = tiles != null && index < tiles.Count ? tiles[index] : null;
                Tile tile = new Tile(index, spec, tileSize);
                tile.Location = new Point((index % cols) * (tileSize.Width + gap), (index / cols) * (tileSize.Height + gap));
                tile.Click += TileClicked;
                Controls.Add(tile);
            }
            Size = new Size(cols * tileSize.Width + (cols - 1) * gap, rows * tileSize.Height + (rows - 1) * gap);
        }

        public bool SelectionEquals(IEnumerable<int> correct)
        {
            HashSet<int> expected = new HashSet<int>(correct);
            return selected.SetEquals(expected);
        }

        public void ClearSelection()
        {
            if (selected.Count == 0)
                return;
            selected.Clear();
            foreach (Control control in Controls)
                control.Invalidate();
            RaiseSelectionChanged();
        }

        private void TileClicked(object sender, EventArgs e)
        {
            Tile tile = (Tile)sender;
            if (!selected.Add(tile.Index))
                selected.Remove(tile.Index);
            tile.IsSelected = selected.Contains(tile.Index);
            RaiseSelectionChanged();
        }

        private void RaiseSelectionChanged()
        {
            EventHandler handler = SelectionChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private sealed class Tile : Control
        {
            public int Index { get; private set; }
            public bool IsSelected { get; set; }
            private readonly TileSpec spec;
            private bool hovered;

            public Tile(int index, TileSpec spec, Size size)
            {
                Index = index;
                this.spec = spec;
                Size = size;
                Cursor = Cursors.Hand;
                Font = new Font("Tahoma", 12F, FontStyle.Bold);
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
                BackColor = spec != null && spec.BackColor != Color.Empty ? spec.BackColor : Theme.TileGrey;
                MouseEnter += delegate { hovered = true; Invalidate(); };
                MouseLeave += delegate { hovered = false; Invalidate(); };
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(BackColor);
                int inset = IsSelected ? 4 : 0;
                Rectangle content = new Rectangle(inset, inset, Width - inset * 2, Height - inset * 2);
                using (Brush brush = new SolidBrush(BackColor))
                    graphics.FillRectangle(brush, content);
                if (spec != null && spec.Image != null)
                    graphics.DrawImage(spec.Image, content);
                else if (spec != null && !String.IsNullOrEmpty(spec.Text))
                    TextRenderer.DrawText(graphics, spec.Text, Font, content, spec.TextColor == Color.Empty ? Color.Black : spec.TextColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
                Color border = IsSelected ? Theme.SelectedBlue : (hovered ? Color.FromArgb(100, 100, 100) : Color.FromArgb(220, 220, 220));
                int borderWidth = IsSelected ? 4 : (hovered ? 1 : 0);
                if (borderWidth > 0)
                    using (Pen pen = new Pen(border, borderWidth))
                        graphics.DrawRectangle(pen, borderWidth / 2, borderWidth / 2, Width - borderWidth, Height - borderWidth);
                if (IsSelected)
                {
                    graphics.FillEllipse(Brushes.White, 3, 3, 20, 20);
                    using (Pen check = new Pen(Theme.SelectedBlue, 2.5F))
                    {
                        graphics.DrawLine(check, 8, 13, 11, 16);
                        graphics.DrawLine(check, 11, 16, 17, 9);
                    }
                }
            }
        }
    }
}
