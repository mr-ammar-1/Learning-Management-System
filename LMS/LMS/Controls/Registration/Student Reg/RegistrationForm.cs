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
    public partial class RegistrationForm : UserControl
    {
        public RegistrationForm()
        {
            InitializeComponent();
            loadClass();
        }
        int credentialsID;
        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        private void button3_Click(object sender, EventArgs e)
        {
            AdmissionControl dc = new AdmissionControl();
            mainControllerClass.showControls(dc, Content);
            Content.BringToFront();
        }
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
            else if ( mainControllerClass.checkEmpty(materialMultiLineTextBox27.Text))
            {
                MessageBox.Show("Please fill Phone Number field correctly", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(materialMultiLineTextBox25.Text))
            {
                MessageBox.Show($"Please fill City field correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(comboBox2.Text) || comboBox2.Items.IndexOf(comboBox2.Text) == -1)
            {
                MessageBox.Show($"Please fill Section field correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (mainControllerClass.checkEmpty(comboBox5.Text))
            {
                MessageBox.Show($"Please fill Class field correctly", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // All validations passed
            return true;
        }
        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RegistrationForm_Load(object sender, EventArgs e)
        {

        }


        private void Save_btn_Click(object sender, EventArgs e)
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
                        string gender = comboBox1.SelectedItem?.ToString(); // Update based on your UI control
                        string fatherName = materialMultiLineTextBox23.Text;
                        DateTime dob = dateTimePicker1.Value;
                        string email = materialMultiLineTextBox24.Text;
                        string password = materialMultiLineTextBox22.Text;
                        string regNo = materialMultiLineTextBox28.Text;
                        string phone = materialMultiLineTextBox27.Text;
                        string city = materialMultiLineTextBox25.Text;
                        int sectionID = Convert.ToInt32(comboBox2.SelectedValue); // Update based on your UI control
                        int classID = Convert.ToInt32(comboBox5.SelectedValue); // Update based on your UI control
                        
                        string insertCredentialsQuery = @"
                                 INSERT INTO Credentials (Username, Password, Role, Status, CreatedAt, UpdatedAt) 
                                 VALUES (@Username, @Password, 'Student', 'Active', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";

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
                        INSERT INTO Student (Name, Gender, FatherName, DOB, Email, Password, RegNo, Phone, City, SectionID, ClassID, Status, CreatedAt, UpdatedAt) 
                        VALUES (@Name, @Gender, @FatherName, @DOB, @Email, @Password, @RegNo, @Phone, @City, @SectionID, @ClassID, 'Active', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)";

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
                            command.Parameters.AddWithValue("@SectionID", sectionID);
                            command.Parameters.AddWithValue("@ClassID", classID);

                            
                            // Execute the query
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Student added successfully");
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
        private void loadClass()
        {
            List<object> IDValuess = new List<object>();
            SqlCommand cmdd = new SqlCommand("Select ClassID from Class", con);
            SqlDataAdapter daa = new SqlDataAdapter(cmdd);
            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            foreach (DataRow row in dtt.Rows)
            {
                object value1 = row["classid"];
                IDValuess.Add(value1);
            }
            comboBox5.DataSource = IDValuess;
        }
        private void Edit_btn(object sender, EventArgs e)
        {
            AdmissionControl dc = new AdmissionControl();
            mainControllerClass.showControls(dc, Content);
            Content.BringToFront();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                // Clear existing items in the sectionComboBox
                comboBox2.Items.Clear();

                // Check if a class is selected
                if (comboBox5.SelectedItem != null)
                {
                    int selectedClassID = Convert.ToInt32(comboBox5.SelectedValue);

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Query to retrieve sections for the selected class
                        string query = "SELECT SectionID, SectionName FROM Section WHERE ClassID = @ClassID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ClassID", selectedClassID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Populate the sections ComboBox
                                while (reader.Read())
                                {
                                    int sectionID = Convert.ToInt32(reader["SectionID"]);
                                    string sectionName = reader["SectionName"].ToString();
                                    comboBox2.Items.Add((sectionID).ToString());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sections: {ex.Message}");
            }
        }
    }
}
