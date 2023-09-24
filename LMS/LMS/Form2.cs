using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace LMS
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string connectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS Project;Integrated Security=True";

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
            Form form1 = new Form1();
            form1.Show();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string enteredEmail = textBox1.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define the SQL query to retrieve the username and password based on the email
                string query = "SELECT username, password FROM credentials WHERE email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add the email parameter to the query
                    command.Parameters.AddWithValue("@Email", enteredEmail);

                    // Execute the query to retrieve username and password
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) // Check if a record was found
                    {
                        string username = reader["username"].ToString();
                        string password = reader["password"].ToString();
                        SendEmail(enteredEmail, "LMS Credentials", $"Your credentials are:\nUsername: {username}\nPassword: {password}");
                        MessageBox.Show("Credentials sent to your email.");
                    }
                    else
                    {
                        MessageBox.Show("No account found with this email.");
                    }
                }
            }

        }
        private void SendEmail(string to, string subject, string body)
        {
            // Configure your Gmail SMTP settings
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mian17744@gmail.com", "ljiffgijoshevxdm"),
                EnableSsl = true,
            };

            MailMessage message = new MailMessage("mian17744@gmail.com", to)
            {
                Subject = subject,
                Body = body,
            };

            client.Send(message);
        }
    }
}
