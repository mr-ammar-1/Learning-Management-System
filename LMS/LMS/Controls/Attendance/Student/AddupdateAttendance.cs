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
namespace LMS.Controls.Attendance.Student
{
    public partial class AddupdateAttendance : UserControl
    {
        public AddupdateAttendance()
        {
            InitializeComponent();
            loadTermIDs();
            loadStudentIDs();
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
        private void loadStudentIDs()
        {
            List<object> IDValues = new List<object>();
            SqlCommand cmd = new SqlCommand("Select studentID from STUDENT where status='Active'", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                object value1 = row["STUDENTID"];
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
        int key;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True");
        private const string ConnectionString = "Data Source=DESKTOP-MG3JGVI;Initial Catalog=LMS;Integrated Security=True";

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView("Select Attendanceid,TermID,ClassID,SectionID,studentID,Datee from studentAttendance", dataGridView2);
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
    }
}
