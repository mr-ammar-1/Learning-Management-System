using LMS.Classes;
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
namespace LMS.Controls.Class_Management
{
    public partial class manageClass : UserControl
    {
        public manageClass()
        {
            InitializeComponent();
        }
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        private void button1_Click(object sender, EventArgs e)
        {
            ClassControl RF = new ClassControl();
            mainControllerClass.showControls(RF, Content);
            Content.BringToFront();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SectionControl RF = new SectionControl();
            mainControllerClass.showControls(RF, Content);
            Content.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("SELECT Term.TermName AS Term, Class.ClassName AS Class, Section.SectionName AS Section FROM Term JOIN Class ON Term.TermID = Class.TermID JOIN Section ON Class.ClassID = Section.ClassID", dataGridView1);
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

                private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Content_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
