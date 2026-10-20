using System;
using System.Drawing;
using System.Windows.Forms;
using DekBanshiCaptcha.Controls;
using DekBanshiCaptcha.Core;

namespace DekBanshiCaptcha.Forms
{
    public class FormQ5 : BaseForm
    {
        private readonly TextBox answer = new TextBox();
        public FormQ5() { BuildUi(); }

        private void BuildUi()
        {
            Label question = new Label { Text = "Question 5 / 7", Font = Theme.Small, Location = new Point(14, 10), Size = new Size(130, 20) };
            CaptchaCard card = new CaptchaCard(new Size(550, 275)) { Location = new Point(125, 120), Prompt = "Find the answer of this question", Subject = String.Empty, ShowSubject = false };
            PictureBox expression = new PictureBox { Location = new Point(15, 8), Size = new Size(520, 170), SizeMode = PictureBoxSizeMode.StretchImage, Image = MathCaptchaRenderer.GetImage() };
            answer.Location = new Point(110, 194);
            answer.Size = new Size(310, 34);
            answer.Font = Theme.Main;
            answer.Text = "Answer";
            answer.ForeColor = Color.Gray;
            answer.GotFocus += delegate { if (answer.Text == "Answer") { answer.Text = String.Empty; answer.ForeColor = Color.Black; } };
            answer.LostFocus += delegate { if (String.IsNullOrWhiteSpace(answer.Text)) { answer.Text = "Answer"; answer.ForeColor = Color.Gray; } };
            answer.KeyDown += delegate(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { e.SuppressKeyPress = true; Decide(); } };
            Y2KButton submit = new Y2KButton { Text = "Submit", FaceColor = Theme.HeaderBlue, ForeColor = Color.White, Location = new Point(220, 235), Size = new Size(110, 34) };
            submit.Click += delegate { Decide(); };
            card.Body.Controls.Add(expression);
            card.Body.Controls.Add(answer);
            card.Body.Controls.Add(submit);
            card.RefreshClicked += delegate { answer.Text = "Answer"; answer.ForeColor = Color.Gray; };
            card.VerifyButton.Visible = false;
            Content.Controls.Add(question);
            Content.Controls.Add(card);
        }

        private void Decide()
        {
            string normalized = answer.Text.Trim().Replace(" ", String.Empty).Replace("−", "-").Replace("–", "-");
            if (normalized == "1/2" || normalized == "0.5") Nav.Go(this, new FormQ6());
            else Nav.Go(this, new FormFail());
        }
    }
}
