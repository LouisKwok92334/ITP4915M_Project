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

namespace ITP4915M_Project.Forms
{
    public partial class BPA : Form
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

        public BPA()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData(string bpaId = "", string itemId = "", DateTime? effectivedDate = null, DateTime? endDate = null)
        {
            StringBuilder query = new StringBuilder(
                @"SELECT bpa.*, item.item_name 
        FROM (bpa 
        INNER JOIN bpa_item ON bpa.bpa_id = bpa_item.bpa_id)
        INNER JOIN item ON bpa_item.item_id = item.item_id
        WHERE (@bpaId IS NULL OR bpa.bpa_id = @bpaId) AND 
              (@itemId IS NULL OR bpa_item.item_id = @itemId) AND 
              (@effectivedDate IS NULL OR bpa.effectived_date >= @effectivedDate) AND 
              (@endDate IS NULL OR bpa.end_date <= @endDate)");

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query.ToString(), connection);

                command.Parameters.AddWithValue("@bpaId", string.IsNullOrEmpty(bpaId) ? (object)DBNull.Value : (object)bpaId);
                command.Parameters.AddWithValue("@itemId", string.IsNullOrEmpty(itemId) ? (object)DBNull.Value : (object)itemId);
                command.Parameters.AddWithValue("@effectivedDate", effectivedDate.HasValue ? (object)effectivedDate.Value : (object)DBNull.Value);
                command.Parameters.AddWithValue("@endDate", endDate.HasValue ? (object)endDate.Value : (object)DBNull.Value);

                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();

                try
                {
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (OleDbException ex)
                {
                    // Log or show the error message
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string bpaId = txtBpaid.Text;
            string itemId = txtItemID.Text;
            DateTime effectivedDate = dateTimePicker1.Value.Date;
            DateTime endDate = dateTimePicker2.Value.Date;

            string query = @"
        SELECT bpa.*, item.item_name 
        FROM (bpa 
        INNER JOIN bpa_item ON bpa.bpa_id = bpa_item.bpa_id)
        INNER JOIN item ON bpa_item.item_id = item.item_id
        WHERE (@bpaId IS NULL OR bpa.bpa_id = @bpaId) AND 
        (@itemId IS NULL OR bpa_item.item_id = @itemId) AND 
        (@effectivedDate IS NULL OR bpa.effectived_date >= @effectivedDate) AND 
        (@endDate IS NULL OR bpa.end_date <= @endDate)";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);

                command.Parameters.AddWithValue("@bpaId", string.IsNullOrEmpty(bpaId) ? (object)DBNull.Value : bpaId);
                command.Parameters.AddWithValue("@itemId", string.IsNullOrEmpty(itemId) ? (object)DBNull.Value : itemId);
                command.Parameters.AddWithValue("@effectivedDate", effectivedDate == DateTime.MinValue ? (object)DBNull.Value : effectivedDate);
                command.Parameters.AddWithValue("@endDate", endDate == DateTime.MinValue ? (object)DBNull.Value : endDate);

                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                DataTable dt = new DataTable();
                try
                {
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (OleDbException ex)
                {
                    // Log or show the error message
                    MessageBox.Show(ex.Message);
                }
            }
        }





        private void btnback_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
