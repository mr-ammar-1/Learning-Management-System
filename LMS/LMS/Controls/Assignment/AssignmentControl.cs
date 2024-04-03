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
    public partial class AssignmentControl : UserControl
    {
        public AssignmentControl()
        {
            InitializeComponent();
            loadCourses();
            loadClasses();
        }
        private void loadClasses()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select classid from class", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["classid"];
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
        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        private void button5_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select AssignmentID,AssignmentName,courseid,classid,sectionid from assignment where status='Active'", dataGridView2);
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                    // Retrieve data from the clicked row
                    key = int.Parse(row.Cells["assignmentid"].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE assignment SET Status = 'Inactive' WHERE assignmentid = @assignmentid";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@assignmentid", key);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update successful
                        MessageBox.Show("Assignment deleted");
                    }
                    else
                    {
                        // No rows were affected (user not found or already inactive)
                        MessageBox.Show("User not found or already inactive");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Assuming you have the necessary information
                    string assignmentName = materialMultiLineTextBox21.Text;
                    int courseID = int.Parse(comboBox1.Text);
                    int classID = int.Parse(comboBox3.Text);
                    int sectionID = int.Parse(comboBox2.Text);

                    string insertQuery = @"
            INSERT INTO Assignment (AssignmentName, CourseID, ClassID, SectionID, CreatedAt, UpdatedAt, Status)
            VALUES (@AssignmentName, @CourseID, @ClassID, @SectionID, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, 'Active');";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@AssignmentName", assignmentName);
                        command.Parameters.AddWithValue("@CourseID", courseID);
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@SectionID", sectionID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Assignment added successfully");
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the assignment");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void C(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Clear existing items in the sectionComboBox
                comboBox2.Items.Clear();

                // Check if a class is selected
                if (comboBox3.SelectedItem != null)
                {
                    int selectedClassID = Convert.ToInt32(comboBox3.SelectedValue);

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        // Query to retrieve sections for the selected class
                        string query = "SELECT SectionID FROM Section WHERE ClassID = @ClassID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ClassID", selectedClassID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                // Populate the sections ComboBox
                                while (reader.Read())
                                {
                                    int sectionID = Convert.ToInt32(reader["SectionID"]);
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
