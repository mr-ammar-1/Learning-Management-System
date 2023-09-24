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

namespace LMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void getCredentialInfoOnLoginButton()
        {
            string userName = textBox1.Text;
            string password = textBox2.Text;
            string connectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS Project;Integrated Security=True";
            string query = "SELECT Username, Password, Role FROM credentials WHERE userName = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Use a parameterized query to prevent SQL injection
                    command.Parameters.AddWithValue("@Username", userName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string dbPassword = reader["Password"].ToString();
                            string dbRole = reader["Role"].ToString();

                            // Check if the provided password matches the database password
                            if (password == dbPassword)
                            {
                                // Login Successful
                                Console.WriteLine("Login Successful!");

                                // Open another form (replace with your form code)
                                // Form2 form2 = new Form2();
                                // form2.Show();

                                // Close the current form (console application)
                                // Environment.Exit(0);
                            }
                            else
                            {
                                // Password is incorrect
                                MessageBox.Show("Your password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            // Username not found
                            MessageBox.Show("Your username is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
            
        }
        public bool isValidString(string s)
        {
            if (s==string.Empty)
            {
                string st = s + " cannot be empty";
                MessageBox.Show(st, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(isValidString(textBox1.Text) && isValidString(textBox2.Text))
            {
                getCredentialInfoOnLoginButton();
            }
        }
    }
}
