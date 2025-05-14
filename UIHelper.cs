using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public static class UIHelper
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect,
                                                        int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public static void MakeRounded(Control ctrl, int radius = 15)
        {
            ctrl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width, ctrl.Height, radius, radius));
            ctrl.Resize += (s, e) =>
            {
                ctrl.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, ctrl.Width, ctrl.Height, radius, radius));
            };
        }

        public static void RoundAllControls(Control parent, int radius = 15)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button || c is Panel || c is TextBox)
                    MakeRounded(c, radius);

                if (c.HasChildren)
                    RoundAllControls(c, radius);
            }
        }
    }
}
