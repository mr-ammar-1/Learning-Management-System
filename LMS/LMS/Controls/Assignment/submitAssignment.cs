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

namespace LMS.Controls.Assignment
{
    public partial class submitAssignment : UserControl
    {
        public submitAssignment()
        {
            InitializeComponent();
            loadstudents();
            loadCourses();
            loadAssignments();
        }

        private void loadAssignments()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select assignmentid from assignment where status='active'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["assignmentid"];
                IDValues.Add(value1);
            }
            comboBox3.DataSource = IDValues;
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
            comboBox1.DataSource = IDValues;
        }

        private void loadstudents()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select studentid from student where status='active'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["studentid"];
                IDValues.Add(value1);
            }
            comboBox4.DataSource = IDValues;
        }

        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select StudentID,courseID,AssignmentID,SubmissionDate from assignmentSubmission where status='Active'", dataGridView1);
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
            string updateQuery = "UPDATE assignmentSubmission SET Status = 'Inactive' WHERE submissionID = @submissionID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@submissionID", key);

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Retrieve data from the clicked row
                    key = int.Parse(row.Cells["submissionID"].Value.ToString());

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

                    if (!int.TryParse(comboBox1.Text, out int courseID) || courseID <= 0)
                    {
                        MessageBox.Show("Please select a valid course.");
                        return;
                    }

                    if (!int.TryParse(comboBox3.Text, out int assignmentID) || assignmentID <= 0)
                    {
                        MessageBox.Show("Please select a valid Assignment ID.");
                        return;
                    }

                    if (!int.TryParse(comboBox4.Text, out int studentid) || studentid <= 0)
                    {
                        MessageBox.Show("Please select a valid section.");
                        return;
                    }

                

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string insertQuery = @"
                INSERT INTO assignmentSubmission (StudentID, assignmentID, courseID, SubmissionDate, Status, CreatedAt, UpdatedAt)
                VALUES (@StudentID, @assignmentID, @courseID, @SubmissionDate, 'Active', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";

                        using (SqlCommand command = new SqlCommand(insertQuery, connection))
                        {
                            command.Parameters.AddWithValue("@StudentID", comboBox4);
                            command.Parameters.AddWithValue("@assignmentID", comboBox3);
                            command.Parameters.AddWithValue("@courseID", comboBox1);
                            command.Parameters.AddWithValue("@SubmissionDate", dateTimePicker1.Value);

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
    }
}
