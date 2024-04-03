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

namespace LMS.Controls.Courses
{
    public partial class adminCourse : UserControl
    {
        public adminCourse()
        {
            InitializeComponent();
        }
        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";
        private void button8_Click(object sender, EventArgs e)
        {
            string CourseName = materialMultiLineTextBox24.Text;
            string CourseCode = materialMultiLineTextBox21.Text;
            if (string.IsNullOrEmpty(CourseName))
            {
                MessageBox.Show("Please enter a Course name");
                return;
            }
            if (string.IsNullOrEmpty(CourseCode))
            {
                MessageBox.Show("Please enter a Course Code");
                return;
            }

            if (AddCourse(CourseName, CourseCode))
            {
                MessageBox.Show("Course added successfully");
                LoadDataIntoDataGridView("Select CourseID,CourseName,CourseCode from Course", dataGridView2);
            }
            else
            {
                MessageBox.Show("Failed to add Course");
            }
        }
        private bool AddCourse(string CourseName, string Coursecode)
        {
            if (!IsDuplicateSection(CourseName, Coursecode, key))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Course (CourseName, CourseCode) VALUES (@CourseName, @CourseCode)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CourseName", CourseName);
                        command.Parameters.AddWithValue("@CourseCode", Coursecode);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            else
            {
                MessageBox.Show("Duplicate term found. Please enter a unique term name.");
                return false;
            }
        }

        private bool IsDuplicateSection(string courseName, string coursecode, int key)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Check if a term with the same name exists excluding the current term
                    string checkDuplicateQuery = "SELECT COUNT(*) FROM course WHERE courseName = @courseName AND Coursecode = @Coursecode";

                    using (SqlCommand command = new SqlCommand(checkDuplicateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CourseID", key);
                        command.Parameters.AddWithValue("@courseName", materialMultiLineTextBox24.Text);
                        command.Parameters.AddWithValue("@coursecode", materialMultiLineTextBox21.Text);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // If count is greater than 0, a duplicate term exists
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking duplicate term: {ex.Message}");
                return false; // Assume false in case of an exception
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select CourseID,CourseName,Coursecode from Course", dataGridView2);

        }
        private void LoadDataIntoDataGridView(string query, DataGridView dataGridView)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            // Bind the DataTable to the DataGridView
                            dataGridView.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                string CourseName = materialMultiLineTextBox24.Text;
                string CourseCode = materialMultiLineTextBox21.Text;

                if (!IsDuplicateSection(CourseName, CourseCode, key))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE Course SET CourseName = @CourseName,CourseCode=@CourseCode WHERE CourseID = @CourseID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@CourseID", key);
                            command.Parameters.AddWithValue("@CourseName", materialMultiLineTextBox24.Text);
                            command.Parameters.AddWithValue("@CourseCode", materialMultiLineTextBox21.Text);
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully updated in the database");
                                LoadDataIntoDataGridView("Select CourseID,CourseName,CourseCode from Course", dataGridView2);
                            }
                            else
                            {
                                MessageBox.Show("Failed to update in the database");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Duplicate term found. Please enter a unique term name.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating term in the database: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete course  where courseId=@courseId", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@courseId", key);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            LoadDataIntoDataGridView("Select courseId,courseName,courseCode from course", dataGridView2);
            MessageBox.Show("Successfully deleted from the DataBase");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                    // Retrieve data from the clicked row
                    key = int.Parse(row.Cells["CourseID"].Value.ToString());
                    string CourseName = row.Cells["CourseName"].Value.ToString();
                    string CourseCode = row.Cells["CourseCode"].Value.ToString();

                    // Populate the fields in your form with the clicked row data
                    materialMultiLineTextBox24.Text = CourseName;
                    materialMultiLineTextBox21.Text = CourseCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
            }
        }
    }
}
