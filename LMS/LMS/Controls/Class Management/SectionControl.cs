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
namespace LMS.Controls
{
    public partial class SectionControl : UserControl
    {
        public SectionControl()
        {
            InitializeComponent();
            LoadClassIDs();
        }

        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";
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
        private bool AddSection(string sectionName, int ClassID)
        {
            if (!IsDuplicateSection(sectionName, ClassID, key))
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Section (SectionName, ClassID) VALUES (@sectionName, @ClassID)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {

                        command.Parameters.AddWithValue("@sectionName", sectionName);
                        command.Parameters.AddWithValue("@ClassID", ClassID);

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
        private void button3_Click(object sender, EventArgs e)
        {
            string sectionName = materialMultiLineTextBox21.Text;

            if (string.IsNullOrEmpty(sectionName))
            {
                MessageBox.Show("Please enter a section name");
                return;
            }
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select a ClassID");
                return;
            }
            int selectedClassID = (int)comboBox3.SelectedValue;

            if (AddSection(sectionName, selectedClassID))
            {
                MessageBox.Show("Section added successfully");
                LoadDataIntoDataGridView("Select SectionID,SectionName,ClassID from Section where status='Active'", dataGridView1);
            }
            else
            {
                MessageBox.Show("Failed to add Section");
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select SectionID,SectionName,ClassID from Section", dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string SectionName = materialMultiLineTextBox21.Text;
                int ClassID = int.Parse(comboBox3.Text);

                if (!IsDuplicateSection(SectionName, ClassID,key))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE Section SET SectionName = @SectionName,ClassID=@ClassID WHERE SectionID = @SectionID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@SectionID",key);
                            command.Parameters.AddWithValue("@SectionName", materialMultiLineTextBox21.Text);
                            command.Parameters.AddWithValue("@ClassID", int.Parse(comboBox3.Text));
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully updated in the database");
                                LoadClassIDs();
                                LoadDataIntoDataGridView("Select SectionID,SectionName,ClassID from Section", dataGridView1);
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

        private bool IsDuplicateSection(string sectionName, int classID, int key)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Check if a term with the same name exists excluding the current term
                    string checkDuplicateQuery = "SELECT COUNT(*) FROM Section WHERE SectionName = @SectionName AND ClassID = @ClassID";

                    using (SqlCommand command = new SqlCommand(checkDuplicateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SectionID", key);
                        command.Parameters.AddWithValue("@SectionName", materialMultiLineTextBox21.Text);
                        command.Parameters.AddWithValue("@ClassID", int.Parse(comboBox3.Text));

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

        private void button5_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("delete Section WHERE sectionId=@sectionId", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@sectionId", key);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            LoadDataIntoDataGridView("Select SectionID,SectionName,ClassID from Section", dataGridView1);
            MessageBox.Show("Successfully deleted from the DataBase");
            
        }
        private void LoadClassIDs()
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                    // Retrieve data from the clicked row
                    key = int.Parse(row.Cells["SectionID"].Value.ToString());
                    string SectionName = row.Cells["SectionName"].Value.ToString();
                    string ClassID = row.Cells["ClassID"].Value.ToString();

                    // Populate the fields in your form with the clicked row data
                    materialMultiLineTextBox21.Text = SectionName;
                    comboBox3.Text = ClassID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
            }
        }
    }
}
