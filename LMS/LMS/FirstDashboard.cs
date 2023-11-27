using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMS.Controls;
namespace LMS
{
    public partial class FirstDashboard : Dashboard
    {
        public FirstDashboard()
        {
            InitializeComponent();
        }

        private void FirstDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdmissionControls dc = new AdmissionControls();
            showControls(dc);
        }
        public void showControls(Control control)
        {
            Content.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.BringToFront();
            control.Focus();
            Content.Controls.Add(control);
        }
        public void LoadUserControlIntoPanel(UserControl userControl)
        {
            Content.Controls.Clear();
            Content.Controls.Add(userControl);
            userControl.Dock = DockStyle.Fill;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
