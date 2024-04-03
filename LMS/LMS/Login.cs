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
using System.Data.SqlClient;
using LMS.Controls;

namespace LMS
{
    public partial class Login : Dashboard
    {
        public Login()
        {
            InitializeComponent();
        }
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";
        private void Login_Load(object sender, EventArgs e)
        {

        }
        public static class UserInfo
        {
            public static string Username { get; set; }
            public static string Password { get; set; }
            public static string Role { get; set; }
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
        private bool AuthenticateUser(string username, string password, out string role)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Role FROM Credentials WHERE Username = @Username AND Password = @Password AND Status = 'Active'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        role = result.ToString();
                        return true;
                    }
                }
            }

            role = null;
            return false;
        }
        private void OpenFormBasedOnRole(string role)
        {
            if (role == "Admin")
            {
                FirstDashboard fd = new FirstDashboard();
                mainClass.viewWindow(fd, this, MdiParent);
                UserInfo.Username = materialTextBox1.Text;
                UserInfo.Password = materialTextBox4.Text;
                UserInfo.Role = "Admin";
            }
            else if(role=="Teacher")
            {
                TeacherDashboard fd = new TeacherDashboard();
                mainClass.viewWindow(fd, this, MdiParent);
                UserInfo.Username = materialTextBox1.Text;
                UserInfo.Password = materialTextBox4.Text;
                UserInfo.Role = "Teacher";
            }
            else if (role == "Student")
            {
                StudentDashboard fd = new StudentDashboard();
                mainClass.viewWindow(fd, this, MdiParent);
                UserInfo.Username = materialTextBox1.Text;
                UserInfo.Password = materialTextBox4.Text;
                UserInfo.Role = "Student";
            }
        }
        private void materialButton1_Click(object sender, EventArgs e)
        {
            string username = materialTextBox1.Text;
            string password = materialTextBox4.Text;

            if (AuthenticateUser(username, password, out string role))
            {
                // Authentication successful, open the form based on the role
                OpenFormBasedOnRole(role);
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }

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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            ForgotPassword fd = new ForgotPassword();
            mainClass.viewWindow(fd, this, MdiParent);
        }
    }
}
