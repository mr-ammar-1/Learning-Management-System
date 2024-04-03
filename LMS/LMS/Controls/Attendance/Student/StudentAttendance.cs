using LMS.Classes;
using LMS.Controls.Attendance.Student;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS.Controls.Attendance.Student
{
    public partial class StudentAttendance : UserControl
    {
        public StudentAttendance()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            AddupdateAttendance AUA = new AddupdateAttendance();
            mainControllerClass.showControls(AUA, Content);
            Content.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddupdateAttendance AUA = new AddupdateAttendance();
            mainControllerClass.showControls(AUA, Content);
            Content.BringToFront();
        }
    }
}
