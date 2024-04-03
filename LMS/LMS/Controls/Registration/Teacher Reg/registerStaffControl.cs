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

namespace LMS.Controls
{
    public partial class registerStaffControl : UserControl
    {
        public registerStaffControl()
        {
            InitializeComponent();
        }
        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        public bool CheckValidations()
        {
            if (mainControllerClass.checkEmpty(materialMultiLineTextBox21.Text) || !mainControllerClass.isAlphabetic(materialMultiLineTextBox21.Text))
            {
                MessageBox.Show($"Please fill Name fields", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(comboBox1.Text) || comboBox1.Items.IndexOf(comboBox1.Text) == -1)
            {
                MessageBox.Show($"Please fill Gender field correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox24.Text) || !mainControllerClass.isAlphabetic(materialMultiLineTextBox24.Text))
            {
                MessageBox.Show($"Please fill Email field correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox22.Text))
            {
                MessageBox.Show($"Please fill password field correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox23.Text) || !mainControllerClass.isAlphabetic(materialMultiLineTextBox23.Text))
            {
                MessageBox.Show("Please fill father name field correctly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox28.Text))
            {
                MessageBox.Show("Please fill Registration Number field correctly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox27.Text))
            {
                MessageBox.Show("Please fill Phone Number field correctly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox25.Text) || !mainControllerClass.isAlphabetic(materialMultiLineTextBox25.Text))
            {
                MessageBox.Show($"Please fill Province field correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox26.Text) || !mainControllerClass.isAlphabetic(materialMultiLineTextBox26.Text))
            {
                MessageBox.Show($"Please fill Province City correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // All validations passed
            return true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            StaffControl dc = new StaffControl();
            mainControllerClass.showControls(dc, Content);
            Content.BringToFront();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StaffControl dc = new StaffControl();
            mainControllerClass.showControls(dc, Content);
            Content.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidations())
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Assume you have TextBox controls for each student attribute
                        string name = materialMultiLineTextBox21.Text;
                        string gender = comboBox1.SelectedItem?.ToString(); 
                        string fatherName = materialMultiLineTextBox23.Text;
                        DateTime dob = dateTimePicker1.Value;
                        string email = materialMultiLineTextBox24.Text;
                        string password = materialMultiLineTextBox22.Text;
                        string regNo = materialMultiLineTextBox28.Text;
                        string phone = materialMultiLineTextBox27.Text;
                        string city = materialMultiLineTextBox25.Text;
                        string province = materialMultiLineTextBox26.Text;

                        string insertCredentialsQuery = @"
                                 INSERT INTO Credentials (Username, Password, Role, Status, CreatedAt, UpdatedAt) 
                                 VALUES (@Username, @Password, 'Teacher', 'Active', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";

                        using (SqlCommand credentialsCommand = new SqlCommand(insertCredentialsQuery, connection))
                        {
                            // Add parameters for credentials
                            credentialsCommand.Parameters.AddWithValue("@Username", email); // Assuming email is used as the username
                            credentialsCommand.Parameters.AddWithValue("@Password", password); // You may want to hash the password for security

                            // Execute the query for credentials
                            credentialsCommand.ExecuteScalar();
                        }

                        // Insert query with parameters
                        string insertQuery = @"
                        INSERT INTO Teacher (Name, Gender, FatherName, DOB, Email, Password, RegNo, Phone, City,Province ,Status, CreatedAt, UpdatedAt) 
                        VALUES (@Name, @Gender, @FatherName, @DOB, @Email, @Password, @RegNo, @Phone, @City, @Province, 'Active', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Gender", gender);
                            command.Parameters.AddWithValue("@FatherName", fatherName);
                            command.Parameters.AddWithValue("@DOB", dob);
                            command.Parameters.AddWithValue("@Email", email);
                            command.Parameters.AddWithValue("@Password", password);
                            command.Parameters.AddWithValue("@RegNo", regNo);
                            command.Parameters.AddWithValue("@Phone", phone);
                            command.Parameters.AddWithValue("@City", city);
                            command.Parameters.AddWithValue("@Province", province);


                            // Execute the query
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Teacher added successfully");
                                // Add any additional logic or UI updates
                            }
                            else
                            {
                                MessageBox.Show("Failed to add student");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding student: {ex.Message}");
            }
        }
    }
}
