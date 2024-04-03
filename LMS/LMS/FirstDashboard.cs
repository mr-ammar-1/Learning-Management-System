using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMS.Classes;
using LMS.Controls.Class_Management;
using LMS.Controls;
using LMS.Controls.Courses;
using LMS.Controls.Attendance.Teacher;
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
            AdmissionControl dc = new AdmissionControl();
            mainControllerClass.showControls(dc,Content);
            
        }
 
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StaffControl SC = new StaffControl();
            mainControllerClass.showControls(SC, Content);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            manageClass mc = new manageClass();
            mainControllerClass.showControls(mc, Content);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addUpdateAttendance TA = new addUpdateAttendance();
            mainControllerClass.showControls(TA, Content);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            feesControl RC = new feesControl();
            mainControllerClass.showControls(RC, Content);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            adminCourse AC = new adminCourse();
            mainControllerClass.showControls(AC, Content);
        }
    }
}
