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

namespace LMS.Controls.Attendance.Teacher
{
    public partial class addUpdateAttendance : UserControl
    {
        public addUpdateAttendance()
        {
            InitializeComponent();
            loadTermIDs();
            loadTeacherIDs();
            loadSectionIDs();
        }

        private void loadSectionIDs()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select sectionID from section where status='Active'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["sectionid"];
                IDValues.Add(value1);
            }
            comboBox1.DataSource = IDValues;
        }

        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";
        private void button3_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select Attendanceid,TermID,ClassID,SectionID,TeacherID,Datee from teacherAttendance", dataGridView2);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string updateQuery = "delete TeacherAttendance WHERE AttendanceID = @AttendanceID; ";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@AttendanceID", key);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update successful
                        MessageBox.Show("Attendance Deleted");
                    }
                    else
                    {
                        // No rows were affected (user not found or already inactive)
                        MessageBox.Show("User not found or already inactive");
                    }
                }
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
                    key = int.Parse(row.Cells["attendanceID"].Value.ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
            }
        }
        private void loadTeacherIDs()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select teacherID from Teacher where status='Active'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["teacherID"];
                IDValues.Add(value1);
            }
            comboBox4.DataSource = IDValues;
        }
        private void loadTermIDs()
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
            comboBox3.DataSource = IDValues;
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
                    int selectedTermID = Convert.ToInt32(comboBox3.SelectedValue);

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
                                    comboBox2.Items.Add((classid).ToString());
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void back_Click(object sender, EventArgs e)
        {

        }

        private void Add_btn(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(comboBox4.Text, out int teacherID) || teacherID <= 0)
                {
                    MessageBox.Show("Invalid Teacher ID. Please select a valid value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Class ID
                if (!int.TryParse(comboBox2.Text, out int classID) || classID <= 0)
                {
                    MessageBox.Show("Invalid Class ID. Please select a valid value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Section ID
                if (!int.TryParse(comboBox1.Text, out int sectionID) || sectionID <= 0)
                {
                    MessageBox.Show("Invalid Section ID. Please select a valid value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Term ID
                if (!int.TryParse(comboBox3.Text, out int termID) || termID <= 0)
                {
                    MessageBox.Show("Invalid Term ID. Please select a valid value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Attendance Date
                DateTime attendanceDate = dateTimePicker2.Value;
                if (attendanceDate > DateTime.Now)
                {
                    MessageBox.Show("Invalid Attendance Date. Please select a date that is not in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate Attendance Status
                string attendanceStatus = comboBox5.Text.Trim();
                if (string.IsNullOrEmpty(attendanceStatus))
                {
                    MessageBox.Show("Attendance Status cannot be empty. Please select a valid value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string insertQuery = @"
            INSERT INTO TeacherAttendance (TeacherID, ClassID, SectionID, TermID, Datee, Status, CreatedAt, UpdatedAt)
            VALUES (@TeacherID, @ClassID, @SectionID, @TermID, @AttendanceDate, @Status, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TeacherID", teacherID);
                        command.Parameters.AddWithValue("@ClassID", classID);
                        command.Parameters.AddWithValue("@SectionID", sectionID);
                        command.Parameters.AddWithValue("@TermID", termID);
                        command.Parameters.AddWithValue("@AttendanceDate", attendanceDate);
                        command.Parameters.AddWithValue("@Status", attendanceStatus);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Attendance marked successfully");
                        }
                        else
                        {
                            MessageBox.Show("Failed to mark attendance");
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
