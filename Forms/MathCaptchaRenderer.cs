using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    internal static class MathCaptchaRenderer
    {
        public static Image GetImage()
        {
            return Assets.Get("q5_limit", delegate { return Create(520, 170); });
        }

        public static Bitmap Create(int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height);
            Random random = new Random(7919);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            using (Font font = new Font("Times New Roman", 22F, FontStyle.Italic))
            using (Pen fraction = new Pen(Color.Black, 2F))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.Clear(Color.White);
                DrawWavyText(graphics, "lim x→-1", font, 42, 62, random);
                DrawWavyText(graphics, "x³ - 6x² - x + 6", font, 215, 43, random);
                graphics.DrawLine(fraction, 205, 78, 440, 78);
                DrawWavyText(graphics, "x² - 1", font, 270, 105, random);
                for (int i = 0; i < 7; i++)
                {
                    using (Pen noise = new Pen(i % 2 == 0 ? Color.FromArgb(140, 205, 50, 70) : Color.FromArgb(140, 40, 90, 210), 0.8F))
                    {
                        Point[] points = new Point[5];
                        int y = random.Next(15, height - 15);
                        for (int j = 0; j < points.Length; j++)
                            points[j] = new Point(random.Next(0, width), y + random.Next(-8, 9));
                        graphics.DrawCurve(noise, points);
                    }
                }
            }
            return bitmap;
        }

        private static void DrawWavyText(Graphics graphics, string text, Font font, int x, int y, Random random)
        {
            float cursor = x;
            for (int i = 0; i < text.Length; i++)
            {
                string glyph = text[i].ToString();
                float width = graphics.MeasureString(glyph, font).Width;
                GraphicsState state = graphics.Save();
                graphics.TranslateTransform(cursor + width / 2F, y + random.Next(-3, 4));
                graphics.RotateTransform(random.Next(-5, 6));
                graphics.DrawString(glyph, font, Brushes.Black, -width / 2F, 0F);
                graphics.Restore(state);
                cursor += width - 2F;
            }
        }
    }
}
