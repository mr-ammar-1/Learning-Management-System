using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LMS.Controls.Courses;
using LMS.Controls.Assignment;
using LMS.Controls;
using LMS.Controls.Profile;
using LMS.Classes;
using LMS.Controls.Fees;

namespace LMS
{
    public partial class StudentDashboard : Dashboard
    {
        public StudentDashboard()
        {
            InitializeComponent();
        }

        private void StudentDashboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Courses C = new Courses();
            mainControllerClass.showControls(C, Content);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            submitFees FC = new submitFees();
            mainControllerClass.showControls(FC, Content);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            submitAssignment AC = new submitAssignment();
            mainControllerClass.showControls(AC, Content);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            profile p = new profile();
            mainControllerClass.showControls(p, Content);
        }
    }
}
