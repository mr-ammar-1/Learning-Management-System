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

namespace LMS.Controls
{
    public partial class ClassControl : UserControl
    {
        public ClassControl()
        {
            InitializeComponent();
            LoadTermNames();
        }
        int editKey;
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

        private void DeleteData(string tableName, string idColumnName, DataGridView dataGridView)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    int selectedRow = dataGridView.SelectedRows[0].Index;
                    int id = Convert.ToInt32(dataGridView.Rows[selectedRow].Cells[idColumnName].Value);

                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string updateQuery = $"UPDATE {tableName} SET Status = 'Inactive' WHERE {idColumnName} = @ID";
                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@ID", id);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"{tableName} data is deleted successfully");
                            }
                            else
                            {
                                MessageBox.Show($"Failed to delete");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting data: {ex.Message}");
            }
        }

        private string ShowEditDialog(string existingValue)
        {
            return MessageBox.Show($"Edit the value: {existingValue}", "Edit", MessageBoxButtons.OKCancel) == DialogResult.OK ? "EditedValue" : string.Empty;
        }
        private void LoadTermNames()
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private bool AddClass(string className, int termId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Class (ClassName, TermID) VALUES (@ClassName, @TermID)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@ClassName", className);
                    command.Parameters.AddWithValue("@TermID", termId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string className = materialMultiLineTextBox21.Text;

            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("Please enter a class name");
                return;
            }
            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Please select a term");
                return;
            }
            int selectedTermID = (int)comboBox3.SelectedValue;

            if (AddClass(className, selectedTermID))
            {
                MessageBox.Show("Class added successfully");
                // Refresh class list or update UI as needed
            }
            else
            {
                MessageBox.Show("Failed to add class");
            }
        }
        private bool AddTerm(string termName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO Term (TermName) VALUES (@TermName)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@TermName", termName);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            string termName = materialMultiLineTextBox24.Text;

            if (string.IsNullOrEmpty(termName))
            {
                MessageBox.Show("Please enter a term name");
                return;
            }

            if (AddTerm(termName))
            {
                MessageBox.Show("Term added successfully");
                LoadTermNames();
                LoadDataIntoDataGridView("Select TermID,termname from term", dataGridView2);
            }
            else
            {
                MessageBox.Show("Failed to add term");
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("SELECT TermID,TermName FROM Term where status ='Active'", dataGridView2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("SELECT ClassID,ClassName,TermID FROM Class where status ='Active'", dataGridView1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DeleteData("Class", "ClassID", dataGridView1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DeleteData("Term", "TermID", dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ClassName = materialMultiLineTextBox21.Text;
                int termID = int.Parse(comboBox3.Text);

                // Check if the new term name is the same as the existing term name
                if (!IsDuplicateClass(ClassName,termID, editKey))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE Class SET ClassName = @ClassName,termID=@termID WHERE ClassID = @ClassID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@ClassID", editKey);
                            command.Parameters.AddWithValue("@ClassName", materialMultiLineTextBox21.Text);
                            command.Parameters.AddWithValue("@termID", int.Parse(comboBox3.Text));
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully updated in the database");
                                LoadTermNames();
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

        private bool IsDuplicateClass(string className, int termID, int editKey)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Check if a term with the same name exists excluding the current term
                    string checkDuplicateQuery = "SELECT COUNT(*) FROM Class WHERE ClassName = @ClassName AND TermID = @TermID";

                    using (SqlCommand command = new SqlCommand(checkDuplicateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ClassID", editKey);
                        command.Parameters.AddWithValue("@ClassName", materialMultiLineTextBox21.Text);
                        command.Parameters.AddWithValue("@termID", int.Parse(comboBox3.Text));

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

        private bool IsDuplicateTerm(string termName, int editKey)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Check if a term with the same name exists excluding the current term
                    string checkDuplicateQuery = "SELECT COUNT(*) FROM Term WHERE TermName = @TermName AND TermID <> @TermID";

                    using (SqlCommand command = new SqlCommand(checkDuplicateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TermName", termName);
                        command.Parameters.AddWithValue("@TermID", editKey);

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
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve the new term name from TextBox control
                string newTermName = materialMultiLineTextBox24.Text;

                // Check if the new term name is the same as the existing term name
                if (!IsDuplicateTerm(newTermName, editKey))
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string updateQuery = "UPDATE Term SET TermName = @NewTermName WHERE TermID = @TermID";

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@TermID", editKey);
                            command.Parameters.AddWithValue("@NewTermName", newTermName);

                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Successfully updated in the database");
                                LoadTermNames();
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                    // Retrieve data from the clicked row
                    editKey = int.Parse(row.Cells["TermID"].Value.ToString());
                    string termName = row.Cells["TermName"].Value.ToString();

                    // Populate the fields in your form with the clicked row data
                    materialMultiLineTextBox24.Text = termName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
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
                    editKey =int.Parse(row.Cells["ClassID"].Value.ToString());
                    string Classname= row.Cells["ClassName"].Value.ToString();
                    string TermID = row.Cells["TermID"].Value.ToString();

                    // Populate the fields in your form with the clicked row data
                    materialMultiLineTextBox21.Text = Classname;
                    comboBox3.Text = TermID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
            }
        }
    }
}
