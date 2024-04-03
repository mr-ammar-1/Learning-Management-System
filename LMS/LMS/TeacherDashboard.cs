using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMS.Controls.Attendance.Student;
using LMS.Controls.Assignment;
using LMS.Controls.Courses;
using LMS.Controls.Profile;
using LMS.Classes;

namespace LMS
{
    public partial class TeacherDashboard : Dashboard
    {
        public TeacherDashboard()
        {
            InitializeComponent();
        }

        private void TeacherDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddupdateAttendance dc = new AddupdateAttendance();
            mainControllerClass.showControls(dc, Content);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addCourse dc = new addCourse();
            mainControllerClass.showControls(dc, Content);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AssignmentControl dc = new AssignmentControl();
            mainControllerClass.showControls(dc, Content);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TeacherProfile dc = new TeacherProfile();
            mainControllerClass.showControls(dc, Content);
        }
    }
}
