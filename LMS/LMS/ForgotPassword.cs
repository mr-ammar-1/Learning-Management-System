using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using LMS.Classes;
namespace LMS
{
    public partial class ForgotPassword : Dashboard
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";
        private void ForgotPassword_Load(object sender, EventArgs e)
        {

        }

        private void materialLabel4_Click(object sender, EventArgs e)
        {

            
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void materialTextBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void materialTextBox1_Leave(object sender, EventArgs e)
        {
            
        }

        private void materialTextBox4_Leave(object sender, EventArgs e)
        {
            
        }

        private void materialTextBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void materialTextBox2_Leave(object sender, EventArgs e)
        {
            
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Login l = new Login();
            mainClass.viewWindow(l, this, MdiParent);
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

        private void materialCheckbox2_CheckedChanged(object sender, EventArgs e)
        {
            if (materialCheckbox2.Checked)
            {
                materialTextBox2.Password = false;
            }
            else
            {
                materialTextBox2.Password = true;
            }
        }
        private bool CheckIfUsernameExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Credentials WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
        private bool ChangePassword(string username, string newPassword)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Credentials SET Password = @NewPassword, UpdatedAt = GETDATE() WHERE Username = @Username";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void materialButton1_Click(object sender, EventArgs e)
        {
            string username = materialTextBox1.Text;
            string newPassword = materialTextBox4.Text;
            string confirmPassword = materialTextBox2.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Please fill in all fields");
                return;
            }

            if (!CheckIfUsernameExists(username))
            {
                MessageBox.Show("Username not found");
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("New password and confirm password do not match");
                return;
            }

            if (ChangePassword(username, newPassword))
            {
                MessageBox.Show("Password changed successfully");
                // Close the form or navigate to login screen
            }
            else
            {
                MessageBox.Show("Failed to change password");
            }
        }
    }
}
