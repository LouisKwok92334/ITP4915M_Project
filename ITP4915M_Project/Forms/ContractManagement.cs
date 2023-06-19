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
    public partial class ContractManagement : Form
    {
        public ContractManagement()
        {
            InitializeComponent();
            LoadData();
        }

        private void ContractManagement_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text.Trim();
            string contractType = "";
            string status = "";
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (dateTimePicker1.Checked)
                startDate = dateTimePicker1.Value.Date;

            if (dateTimePicker2.Checked)
                endDate = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            if (rdoPlanned.Checked)
                contractType = "Planned";
            else if (rdoContract.Checked)
                contractType = "Standard";

            if (cxActive.Checked && !cbInactive.Checked)
                status = "Active";
            else if (!cxActive.Checked && cbInactive.Checked)
                status = "Inactive";

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

            string query = "SELECT agreement_id, supplier_id, agreement_type, created_at, end_at, status FROM agreement WHERE 1=1 ";

            if (!string.IsNullOrWhiteSpace(contractType))
                query += "AND agreement_type LIKE @ContractType ";

            if (!string.IsNullOrWhiteSpace(status))
                query += "AND status = @Status ";

            if (startDate != null && endDate != null)
                query += "AND (created_at >= @StartDate AND created_at <= @EndDate) ";

            if (!string.IsNullOrWhiteSpace(searchValue))
                query += "AND (agreement_id LIKE @SearchValue OR supplier_id LIKE @SearchValue) ";

            if (!string.IsNullOrWhiteSpace(txtSupplier.Text))
                query += "AND supplier_id LIKE @SearchValue ";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(contractType))
                        command.Parameters.AddWithValue("@ContractType", "%" + contractType + "%");

                    if (!string.IsNullOrWhiteSpace(status))
                        command.Parameters.AddWithValue("@Status", status);

                    if (startDate != null && endDate != null)
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate.Value);
                        command.Parameters.AddWithValue("@EndDate", endDate.Value);
                    }

                    if (!string.IsNullOrWhiteSpace(searchValue))
                        command.Parameters.AddWithValue("@SearchValue", "%" + searchValue + "%");

                    if (!string.IsNullOrWhiteSpace(txtSupplier.Text))
                        command.Parameters.AddWithValue("@SearchValue", "%" + txtSupplier.Text.Trim() + "%");

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }




        private void LoadData()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT agreement_id AS AgreementId, supplier_id, agreement_type, created_at, end_at FROM agreement";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadData();

            txtSearch.Text = "";
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            rdoPlanned.Checked = false;
            rdoContract.Checked = false;

            cxActive.Checked = false;
            cbInactive.Checked = false;
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow selectedRow = dataGridView1.CurrentRow;
                string agreementId = selectedRow.Cells["AgreementId"].Value.ToString();
                AgreementDetail agreementDetailForm = new AgreementDetail(agreementId);
                agreementDetailForm.Show();
            }
        }

        private void btnViewBPA_Click(object sender, EventArgs e)
        {
            BPA bpa = new BPA();
            bpa.Show();
        }
    }
}
