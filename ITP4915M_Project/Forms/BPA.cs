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
            StringBuilder query = new StringBuilder("SELECT * FROM bpa WHERE 1=1");

            if (!string.IsNullOrEmpty(bpaId))
                query.Append(" AND bpa_id = ?");

            if (!string.IsNullOrEmpty(itemId))
                query.Append(" AND item_id = ?");

            if (effectivedDate.HasValue)
                query.Append(" AND effectived_date >= ?");

            if (endDate.HasValue)
                query.Append(" AND end_date <= ?");

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query.ToString(), connection);

                if (!string.IsNullOrEmpty(bpaId))
                    command.Parameters.AddWithValue("@bpaId", bpaId);
                if (!string.IsNullOrEmpty(itemId))
                    command.Parameters.AddWithValue("@itemId", itemId);
                if (effectivedDate.HasValue)
                    command.Parameters.AddWithValue("@effectivedDate", effectivedDate.Value.ToString("#MM/dd/yyyy#"));

                if (endDate.HasValue)
                    command.Parameters.AddWithValue("@endDate", endDate.Value.ToString("#MM/dd/yyyy#"));


                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
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
    FROM bpa 
    INNER JOIN bpa_item ON bpa.bpa_id = bpa_item.bpa_id
    INNER JOIN item ON bpa_item.item_id = item.item_id
    WHERE (bpa.bpa_id = ? OR ? = 0) AND 
    (bpa_item.item_id = ? OR ? = 0) AND 
    (bpa.effectived_date >= ? OR ? IS NULL) AND 
    (bpa.end_date <= ? OR ? IS NULL) ";

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(query, connection);

                command.Parameters.AddWithValue("@bpaId", string.IsNullOrEmpty(bpaId) ? (object)DBNull.Value : bpaId);
                command.Parameters.AddWithValue("@bpaId2", string.IsNullOrEmpty(bpaId) ? (object)DBNull.Value : bpaId);
                command.Parameters.AddWithValue("@itemId", string.IsNullOrEmpty(itemId) ? (object)DBNull.Value : itemId);
                command.Parameters.AddWithValue("@itemId2", string.IsNullOrEmpty(itemId) ? (object)DBNull.Value : itemId);
                command.Parameters.AddWithValue("@effectivedDate", effectivedDate == DateTime.MinValue ? (object)DBNull.Value : effectivedDate);
                command.Parameters.AddWithValue("@effectivedDate2", effectivedDate == DateTime.MinValue ? (object)DBNull.Value : effectivedDate);
                command.Parameters.AddWithValue("@endDate", endDate == DateTime.MinValue ? (object)DBNull.Value : endDate);
                command.Parameters.AddWithValue("@endDate2", endDate == DateTime.MinValue ? (object)DBNull.Value : endDate);

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
