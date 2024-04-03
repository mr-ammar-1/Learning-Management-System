using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMS.Controls.Attendance.Teacher;
using LMS.Classes;

namespace LMS.Controls.Attendance.Teacher
{
    public partial class teachersattendance : UserControl
    {
        public teachersattendance()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {

            addUpdateAttendance AUA = new addUpdateAttendance();
            mainControllerClass.showControls(AUA, Content);
            Content.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
