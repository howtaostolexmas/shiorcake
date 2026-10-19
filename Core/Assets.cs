using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Resources;
using DekBanshiCaptcha.Properties;

namespace DekBanshiCaptcha.Core
{
    public static class Assets
    {
        public static Image Get(string name, Func<Image> fallback)
        {
            try
            {
                Image image = Resources.ResourceManager.GetObject(name) as Image;
                if (image != null)
                    return image;
            }
            catch (MissingManifestResourceException)
            {
            }
            catch (InvalidOperationException)
            {
            }
            return fallback == null ? CreatePlaceholder(name, 160, 160) : fallback();
        }

        public static Bitmap CreatePlaceholder(string name, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Brush background = new SolidBrush(Color.FromArgb(210, 210, 210)))
            using (Pen border = new Pen(Color.FromArgb(120, 120, 120), 2F))
            using (Font font = new Font("Tahoma", 9F, FontStyle.Bold))
            {
                graphics.Clear(Color.White);
                graphics.FillRectangle(background, 4, 4, width - 8, height - 8);
                graphics.DrawRectangle(border, 4, 4, width - 9, height - 9);
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                graphics.DrawString(name, font, Brushes.Black, new RectangleF(8, 8, width - 16, height - 16), format);
            }
            return bitmap;
        }

        public static Bitmap CreateSilhouette(string name)
        {
            Bitmap bitmap = new Bitmap(160, 160);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Brush background = new SolidBrush(Color.FromArgb(222, 232, 238)))
            using (Brush silhouette = new SolidBrush(Color.FromArgb(74, 144, 226)))
            using (Font font = new Font("Tahoma", 8F, FontStyle.Bold))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.White);
                graphics.FillRectangle(background, 0, 0, 160, 160);
                graphics.FillEllipse(silhouette, 55, 24, 50, 50);
                graphics.FillEllipse(silhouette, 34, 76, 92, 100);
                graphics.DrawString(name, font, Brushes.Black, new RectangleF(4, 136, 152, 18), new StringFormat { Alignment = StringAlignment.Center });
            }
            return bitmap;
        }

        public static Bitmap CreateMapPlaceholder(string name, int width, int height)
        {
            Bitmap bitmap = CreatePlaceholder(name, width, height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Pen roads = new Pen(Color.White, 12F))
            using (Brush block = new SolidBrush(Color.FromArgb(150, 170, 150)))
            using (Font font = new Font("Tahoma", 14F, FontStyle.Bold))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.FillRectangle(block, width / 8, height / 5, width / 4, height / 3);
                graphics.FillRectangle(block, width / 2, height / 6, width / 3, height / 3);
                graphics.FillRectangle(block, width / 3, height * 3 / 5, width / 3, height / 4);
                graphics.DrawLine(roads, 0, height / 2, width, height / 2);
                graphics.DrawLine(roads, width / 2, 0, width / 2, height);
                graphics.DrawString(name, font, Brushes.Black, new PointF(12, 10));
            }
            return bitmap;
        }
    }
}
