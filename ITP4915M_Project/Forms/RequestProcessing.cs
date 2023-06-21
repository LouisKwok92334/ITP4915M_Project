using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace ITP4915M_Project.Forms
{
    public partial class RequestProcessing : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
        public RequestProcessing()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = cbStatus.Text;

            // Create and open the connection
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // SQL query to retrieve purchase_order data with matching status
                string query = "SELECT * FROM purchase_order WHERE status = @Status";

                // Create the OleDbCommand object
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    // Add parameter for the status value
                    command.Parameters.AddWithValue("@Status", status);

                    // Create a DataTable to store the results
                    DataTable dataTable = new DataTable();

                    // Create a data adapter and fill the DataTable
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }

                    // Bind the DataTable to a DataGridView or any other control for display
                    dataGridView1.DataSource = dataTable;

                }
            }
        }

        private void RequestProcessing_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet1.purchase_order' 資料表。您可以視需要進行移動或移除。
            this.purchase_orderTableAdapter1.Fill(this.iTP4915DataSet1.purchase_order);
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet.purchase_order' 資料表。您可以視需要進行移動或移除。
            this.purchase_orderTableAdapter.Fill(this.iTP4915DataSet.purchase_order);

        }
    }
}
