using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS
{
    public partial class Login : Dashboard
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void materialCheckbox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void materialTextBox1_Click(object sender, EventArgs e)
        {
            if (materialTextBox1.Text == "Enter Username")
            {
                materialTextBox1.Text = "";
                materialTextBox1.ForeColor = Color.Black;
            }
        }

        private void materialTextBox1_Leave(object sender, EventArgs e)
        {
            if (materialTextBox1.Text == "Enter Username")
            {
                materialTextBox1.Text = "";
                materialTextBox1.ForeColor = Color.Silver;
            }
        }

        private void materialTextBox4_Click(object sender, EventArgs e)
        {
            if (materialTextBox4.Text == "Password")
            {
                materialTextBox4.Text = "";
                materialTextBox4.ForeColor = Color.Black;
            }
        }
        private void materialTextBox4_Leave(object sender, EventArgs e)
        {
            if (materialTextBox4.Text == "Password")
            {
                materialTextBox4.Text = "";
                materialTextBox4.ForeColor = Color.Silver;
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

        }

        private void materialCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialCheckbox1.Checked)
            {
                materialTextBox4.Password = false;
            }
            else
            {
                materialTextBox4.Password = true;
            }
        }
    }
}
