using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Controls
{
    public class CaptchaCard : Panel
    {
        private readonly Panel header;
        private readonly Panel footer;
        private readonly Button refreshButton;
        private readonly Y2KButton verifyButton;
        private string prompt;
        private string subject;

        public Panel Body { get; private set; }
        public event EventHandler RefreshClicked;
        public event EventHandler VerifyClicked;
        public string Prompt { get { return prompt; } set { prompt = value; header.Invalidate(); } }
        public string Subject { get { return subject; } set { subject = value; header.Invalidate(); } }
        public bool ShowSubject { get; set; }
        public bool UnderlineSubject { get; set; }
        public string VerifyText { get { return verifyButton.Text; } set { verifyButton.Text = value; } }
        public Y2KButton VerifyButton { get { return verifyButton; } }

        public CaptchaCard(Size bodySize)
        {
            prompt = "Select all the squares with a";
            subject = String.Empty;
            ShowSubject = true;
            Size = new Size(bodySize.Width, 64 + bodySize.Height + 52);
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            header = new Panel { Location = new Point(0, 0), Size = new Size(bodySize.Width, 64), BackColor = Theme.HeaderBlue };
            header.Paint += PaintHeader;
            Body = new Panel { Location = new Point(0, 64), Size = bodySize, BackColor = Color.White };
            footer = new Panel { Location = new Point(0, 64 + bodySize.Height), Size = new Size(bodySize.Width, 52), BackColor = Color.White };
            refreshButton = new Button { Location = new Point(12, 8), Size = new Size(36, 36), FlatStyle = FlatStyle.Flat, Text = String.Empty, BackColor = Color.White, TabStop = false };
            refreshButton.FlatAppearance.BorderSize = 0;
            refreshButton.Paint += PaintRefresh;
            refreshButton.Click += delegate { EventHandler handler = RefreshClicked; if (handler != null) handler(this, EventArgs.Empty); };
            verifyButton = new Y2KButton { Text = "Verify", FaceColor = Theme.HeaderBlue, ForeColor = Color.White, Location = new Point(bodySize.Width - 112, 8), Size = new Size(96, 36) };
            verifyButton.Click += delegate { EventHandler handler = VerifyClicked; if (handler != null) handler(this, EventArgs.Empty); };
            footer.Controls.Add(refreshButton);
            footer.Controls.Add(verifyButton);
            Controls.Add(header);
            Controls.Add(Body);
            Controls.Add(footer);
        }

        private void PaintHeader(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Theme.HeaderBlue);
            TextRenderer.DrawText(e.Graphics, Prompt ?? String.Empty, Theme.Small, new Point(12, 8), Color.White);
            Font subjectFont = UnderlineSubject ? new Font("Tahoma", 14F, FontStyle.Bold | FontStyle.Underline) : new Font("Tahoma", 14F, FontStyle.Bold);
            using (subjectFont)
                TextRenderer.DrawText(e.Graphics, ShowSubject ? (Subject ?? String.Empty) : String.Empty, subjectFont, new Rectangle(12, 24, Width - 24, 34), Color.White, TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private void PaintRefresh(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Pen pen = new Pen(Theme.HeaderBlue, 2.2F))
            {
                e.Graphics.DrawArc(pen, 8, 8, 20, 20, 35, 270);
                e.Graphics.DrawArc(pen, 8, 8, 20, 20, 215, 270);
                e.Graphics.DrawLine(pen, 25, 7, 30, 8);
                e.Graphics.DrawLine(pen, 25, 7, 26, 12);
                e.Graphics.DrawLine(pen, 7, 26, 12, 27);
                e.Graphics.DrawLine(pen, 7, 26, 8, 21);
            }
        }
    }
}
