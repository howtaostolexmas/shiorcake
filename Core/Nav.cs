using System.Windows.Forms;
using DekBanshiCaptcha.Forms;

namespace DekBanshiCaptcha.Core
{
    public static class Nav
    {
        public static void Go(Form from, Form to)
        {
            to.StartPosition = FormStartPosition.Manual;
            to.Location = from.Location;
            to.Show();
            from.Hide();
        }

        public static void Restart(Form from)
        {
            Go(from, new FormQ1());
        }

        public static void ExitApp()
        {
            Application.Exit();
        }
    }
}
