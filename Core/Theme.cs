using System.Drawing;

namespace DekBanshiCaptcha.Core
{
    public static class Theme
    {
        public static readonly Color Cyan = Color.FromArgb(0, 174, 239);
        public static readonly Color Blue = Color.FromArgb(20, 40, 230);
        public static readonly Color Grey = Color.FromArgb(190, 180, 175);
        public static readonly Color Green = Color.FromArgb(50, 240, 100);
        public static readonly Color Yellow = Color.FromArgb(245, 245, 60);
        public static readonly Color Pink = Color.FromArgb(255, 100, 200);
        public static readonly Color HeaderBlue = Color.FromArgb(74, 144, 226);
        public static readonly Color TileGrey = Color.FromArgb(204, 204, 204);
        public static readonly Color SelectedBlue = Color.FromArgb(74, 144, 226);
        public static readonly Color ErrorRed = Color.FromArgb(224, 0, 0);
        public static readonly Font Main = new Font("Tahoma", 10F, FontStyle.Regular);
        public static readonly Font Bold = new Font("Tahoma", 10F, FontStyle.Bold);
        public static readonly Font Title = new Font("Tahoma", 22F, FontStyle.Bold);
        public static readonly Font Small = new Font("Tahoma", 8.5F, FontStyle.Regular);
    }
}
