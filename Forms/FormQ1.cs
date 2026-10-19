using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ1 : BaseForm
    {
        private readonly Timer verifyTimer = new Timer();
        private readonly Timer animationTimer = new Timer();
        private readonly Panel checkCard = new Panel();
        private readonly Y2KButton nextButton = new Y2KButton();
        private bool checkedAnswer;
        private bool verifying;
        private int spinnerAngle;

        public FormQ1() { BuildUi(); }

        private void BuildUi()
        {
            Label title = new Label { Text = "I'm Dek Banshi", Font = Theme.Title, ForeColor = Theme.Blue, Location = new Point(280, 62), Size = new Size(400, 42) };
            Label subtitle = new Label { Text = "Question 1: Checkbox", Font = Theme.Main, ForeColor = Color.Gray, TextAlign = ContentAlignment.MiddleCenter, Location = new Point(200, 110), Size = new Size(400, 28) };
            Label question = new Label { Text = "Question 1 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            Panel robot = new Panel { Location = new Point(215, 62), Size = new Size(52, 52) };
            robot.Paint += PaintRobot;
            checkCard.Location = new Point(250, 195);
            checkCard.Size = new Size(300, 74);
            checkCard.BackColor = Color.White;
            checkCard.BorderStyle = BorderStyle.FixedSingle;
            checkCard.Paint += PaintCheckCard;
            checkCard.Cursor = Cursors.Hand;
            checkCard.MouseClick += ToggleCheckbox;
            nextButton.Text = "Next";
            nextButton.FaceColor = Theme.Green;
            nextButton.Location = new Point(340, 350);
            nextButton.Size = new Size(120, 44);
            nextButton.Enabled = false;
            nextButton.Click += delegate { Nav.Go(this, new FormQ2()); };
            verifyTimer.Interval = 800;
            verifyTimer.Tick += VerificationFinished;
            animationTimer.Interval = 45;
            animationTimer.Tick += delegate { if (verifying) { spinnerAngle = (spinnerAngle + 30) % 360; checkCard.Invalidate(); } };
            Content.Controls.Add(question);
            Content.Controls.Add(title);
            Content.Controls.Add(subtitle);
            Content.Controls.Add(robot);
            Content.Controls.Add(checkCard);
            Content.Controls.Add(nextButton);
        }

        private void ToggleCheckbox(object sender, MouseEventArgs e)
        {
            if (e.X < 18 || e.X > 40 || e.Y < 24 || e.Y > 46)
                return;
            if (checkedAnswer || verifying)
                return;
            verifying = true;
            checkCard.Invalidate();
            animationTimer.Start();
            verifyTimer.Start();
        }

        private void VerificationFinished(object sender, EventArgs e)
        {
            verifyTimer.Stop();
            animationTimer.Stop();
            verifying = false;
            checkedAnswer = true;
            nextButton.Enabled = true;
            checkCard.Invalidate();
        }

        private void PaintCheckCard(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);
            using (Pen border = new Pen(Color.FromArgb(150, 150, 150)))
                g.DrawRectangle(border, 0, 0, 299, 73);
            if (!checkedAnswer && !verifying)
            {
                using (Pen pen = new Pen(Color.Gray, 2F)) g.DrawRectangle(pen, 18, 25, 20, 20);
            }
            else if (verifying)
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(Theme.HeaderBlue, 3F)) g.DrawArc(pen, 18, 24, 22, 22, spinnerAngle, 255);
            }
            else
            {
                using (Pen pen = new Pen(Theme.Green, 4F)) { g.DrawLine(pen, 18, 36, 25, 43); g.DrawLine(pen, 25, 43, 39, 26); }
            }
            TextRenderer.DrawText(g, "I'm Dek Banshi", Theme.Main, new Point(50, 27), Color.Black);
            using (Pen logo = new Pen(Theme.HeaderBlue, 2F)) { g.DrawArc(logo, 230, 13, 24, 24, 30, 280); g.DrawArc(logo, 230, 13, 24, 24, 210, 280); }
            TextRenderer.DrawText(g, "reCAPTCHA", Theme.Small, new Rectangle(211, 43, 75, 18), Color.Gray, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private void PaintRobot(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (Brush blue = new SolidBrush(Theme.HeaderBlue)) g.FillRoundedRectangle(blue, new Rectangle(5, 12, 42, 34), 8);
            using (Pen white = new Pen(Color.White, 3F)) { g.DrawEllipse(white, 14, 23, 6, 6); g.DrawEllipse(white, 32, 23, 6, 6); }
            using (Pen dark = new Pen(Theme.Blue, 3F)) { g.DrawLine(dark, 26, 12, 26, 4); g.DrawEllipse(dark, 23, 0, 6, 6); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) { verifyTimer.Dispose(); animationTimer.Dispose(); }
            base.Dispose(disposing);
        }
    }
}

namespace DekBanshiCaptcha.Forms
{
    internal static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle bounds, int radius)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                int diameter = radius * 2;
                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();
                graphics.FillPath(brush, path);
            }
        }
    }
}
