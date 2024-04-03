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
    public partial class StaffControl : UserControl
    {
        public StaffControl()
        {
            InitializeComponent();
        }
        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            registerStaffControl RSC = new registerStaffControl();
            mainControllerClass.showControls(RSC, Content);
            Content.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select * from teacher where status='Active'", Gridview);

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

        private void button2_Click(object sender, EventArgs e)
        {
            string updateQuery = "UPDATE teacher SET Status = 'Inactive' WHERE teacherID = @teacherID";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@teacherID", key);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Update successful
                        MessageBox.Show("Teacher deleted");
                    }
                    else
                    {
                        // No rows were affected (user not found or already inactive)
                        MessageBox.Show("User not found or already inactive");
                    }
                }
            }
        }

        private void Gridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = Gridview.Rows[e.RowIndex];

                    // Retrieve data from the clicked row
                    key = int.Parse(row.Cells["teacherid"].Value.ToString());

                    // Populate the fields in your form with the clicked row data

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling cell click: {ex.Message}");
            }
        }
    }
}
