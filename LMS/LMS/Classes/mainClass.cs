using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LMS.Classes
{
    public class mainClass : PictureBox
    {
        public static void viewWindow(Form openwindow,Form closeWindow,Form MDIParent)
        {
            closeWindow.Close();
            openwindow.WindowState = FormWindowState.Maximized;
            openwindow.MdiParent = MDIParent;
            openwindow.Show();

        }

    }
}
