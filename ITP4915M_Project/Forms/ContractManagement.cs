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
            cboSupplier.DataSource = new BindingSource(LoadSupplierNames(), null);
            cboSupplier.DisplayMember = "Key";
            cboSupplier.ValueMember = "Value";
            cboSupplier.SelectedIndex = -1; // 不选择任何项
            cboStatus.Items.AddRange(new string[] { "Created", "Sent", "Accepted", "Rejected", "Canceled" });
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



            if (!string.IsNullOrEmpty(cboStatus.Text)) // Check if an item is selected in the ComboBox
            {
                switch (cboStatus.Text)
                {
                    case "Created":
                        status = "Created";
                        break;
                    case "Sent":
                        status = "Sent";
                        break;
                    case "Accepted":
                        status = "Accepted";
                        break;
                    case "Rejected":
                        status = "Rejected";
                        break;
                    case "Canceled":
                        status = "Canceled";
                        break;
                    default:

                        status = "";
                        break;
                }
            }
            else
            {

                status = "";
            }
           

            // Check which contract type RadioButton is checked
            if (rdoPlanned.Checked)
            {
                contractType = "Planned";
            }
            else if (rdoContract.Checked)
            {
                contractType = "Standard";
            }
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

            string query = "SELECT a.agreement_id AS AgreementId, s.supplier_name, a.agreement_type, a.status " +
     "FROM agreement a " +
     "INNER JOIN supplier s ON a.supplier_id = s.supplier_id " + "WHERE 1 = 1";


            if (!string.IsNullOrWhiteSpace(status))
                query += "AND status = @Status ";

            if (!string.IsNullOrWhiteSpace(contractType))
                query += "AND a.agreement_type = @ContractType ";

            if (!string.IsNullOrWhiteSpace(searchValue))
                query += "AND (a.agreement_id LIKE @SearchValue) ";

            if (!string.IsNullOrWhiteSpace(cboSupplier.Text))
                query += "AND s.supplier_id = @SupplierId ";

            if (!string.IsNullOrWhiteSpace(contractType))
                query += "AND a.agreement_type = @ContractType ";


            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                   
                    if (!string.IsNullOrWhiteSpace(status))
                        command.Parameters.AddWithValue("@Status", status);

                    if (!string.IsNullOrWhiteSpace(searchValue))
                        command.Parameters.AddWithValue("@SearchValue", searchValue);

                    if (!string.IsNullOrWhiteSpace(contractType))
                        command.Parameters.AddWithValue("@ContractType", contractType);

                    if (!string.IsNullOrWhiteSpace(cboSupplier.Text))
                        command.Parameters.AddWithValue("@SupplierId", cboSupplier.SelectedValue);

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

        private Dictionary<string, int> LoadSupplierNames()
        {
            Dictionary<string, int> supplierNames = new Dictionary<string, int>();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT supplier_id, supplier_name FROM supplier";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int supplierId = Convert.ToInt32(reader["supplier_id"]);
                            string supplierName = reader["supplier_name"].ToString();
                            supplierNames.Add(supplierName, supplierId);
                        }
                    }
                }
            }

            return supplierNames;
        }

        private void LoadData()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT a.agreement_id AS AgreementId, s.supplier_name, a.agreement_type, a.created_at,a.status " +
                  "FROM agreement a " +
                  "INNER JOIN supplier s ON a.supplier_id = s.supplier_id";

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
            dataGridView1.Refresh();

            txtSearch.Text = "";
            rdoPlanned.Checked = false;
            rdoContract.Checked = false;
            cboSupplier.Text = "";
            cboStatus.Text = "";
            cboSupplier.Text = "";

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

        private void btnCreateBPA_Click(object sender, EventArgs e)
        {
            CreateBPA createBPA = new CreateBPA();
            createBPA.Show();
        }

        private void btnViewBPA_Click(object sender, EventArgs e)
        {
            BPA bpa = new BPA();
            bpa.Show();
        }
    }
}
