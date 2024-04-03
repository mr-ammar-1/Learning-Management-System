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
    public partial class addCourse : UserControl
    {
        public addCourse()
        {
            InitializeComponent(); 
            loadTerm();
            loadCourses();
            loadSections();
            loadTeachers();
        }

        private void loadTerm()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select termid from Term", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["TermID"];
                IDValues.Add(value1);
            }
            comboBox2.DataSource = IDValues;
        }
        private void loadSections()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select sectionid from section", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["sectionid"];
                IDValues.Add(value1);
            }
            comboBox4.DataSource = IDValues;
        }
        private void loadCourses()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select courseid from course", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["courseid"];
                IDValues.Add(value1);
            }
            comboBox3.DataSource = IDValues;
        }
        private void loadTeachers()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select studentid from student", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["studentid"];
                IDValues.Add(value1);
            }
            comboBox5.DataSource = IDValues;
        }
        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        private void button4_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select * from TeachercourseEnrollment where status='active'", dataGridView2);
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

        private void button5_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE teacherCourseEnrollment SET Status = 'Inactive' WHERE enrollmentid = @enrollmentid";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@enrollmentid", key);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update successful
                        MessageBox.Show("Enrollment deleted");
                    }
                    else
                    {
                        // No rows were affected (user not found or already inactive)
                        MessageBox.Show("User not found or already inactive");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int teacherID = int.Parse(comboBox5.Text);
                int courseID = int.Parse(comboBox3.Text); // Replace with the actual CourseID
                int classID = int.Parse(comboBox1.Text); // Replace with the actual ClassID
                int sectionID = int.Parse(comboBox4.Text); // Replace with the actual SectionID
                int termID = int.Parse(comboBox2.Text);

                // Check if the new term name is the same as the existing term name

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE teacherCourseEnrollment SET teacherID=@teacherID, CourseID=@CourseID, ClassID=@ClassID, SectionID=@SectionID, TermID=@TermID WHERE EnrollmentID = @EnrollmentID";

                    using (SqlCommand command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@enrollmentID", key);
                        command.Parameters.AddWithValue("@teacherID", teacherID);
                        command.Parameters.AddWithValue("@CourseID", courseID);
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@SectionID", sectionID);
                        command.Parameters.AddWithValue("@TermID", termID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Successfully updated in the database");
                            loadTerm();
                            loadTeachers();
                            loadCourses();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update in the database");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating term in the database: {ex.Message}");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                    // Retrieve data from the clicked row
                    key = int.Parse(row.Cells["enrollmentid"].Value.ToString());
                    comboBox4.Text = row.Cells["sectionid"].Value.ToString();
                    comboBox3.Text = row.Cells["courseid"].Value.ToString();
                    comboBox1.Text = row.Cells["classid"].Value.ToString();
                    comboBox2.Text = row.Cells["termid"].Value.ToString();
                    comboBox5.Text = row.Cells["teacherid"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate selected values from ComboBoxes
                if (!int.TryParse(comboBox5.Text, out int teacherID) || teacherID <= 0)
                {
                    MessageBox.Show("Please select a valid Teacher.");
                    return;
                }

                if (!int.TryParse(comboBox3.Text, out int courseID) || courseID <= 0)
                {
                    MessageBox.Show("Please select a valid course.");
                    return;
                }

                if (!int.TryParse(comboBox1.Text, out int classID) || classID <= 0)
                {
                    MessageBox.Show("Please select a valid class.");
                    return;
                }

                if (!int.TryParse(comboBox4.Text, out int sectionID) || sectionID <= 0)
                {
                    MessageBox.Show("Please select a valid section.");
                    return;
                }

                if (!int.TryParse(comboBox2.Text, out int termID) || termID <= 0)
                {
                    MessageBox.Show("Please select a valid term.");
                    return;
                }

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string insertQuery = @"
            INSERT INTO teacherCourseEnrollment (teacherID, CourseID, ClassID, SectionID, TermID, Status, CreatedAt, UpdatedAt)
            VALUES (@teacherID, @CourseID, @ClassID, @SectionID, @TermID, 'Active', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@teacherID", teacherID);
                        command.Parameters.AddWithValue("@CourseID", courseID);
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@SectionID", sectionID);
                        command.Parameters.AddWithValue("@TermID", termID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Course registration successful");
                        }
                        else
                        {
                            MessageBox.Show("Failed to register the course");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear existing items in the sectionComboBox
                comboBox1.Items.Clear();

                // Check if a class is selected
                if (comboBox2.SelectedItem != null)
                {
                    int selectedTermID = Convert.ToInt32(comboBox2.SelectedValue);

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Query to retrieve sections for the selected class
                        string query = "SELECT classid FROM class WHERE termid = @termid";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@termid", selectedTermID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Populate the sections ComboBox
                                while (reader.Read())
                                {
                                    int classid = Convert.ToInt32(reader["classid"]);
                                    comboBox1.Items.Add((classid).ToString());
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
