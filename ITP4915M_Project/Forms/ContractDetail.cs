using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class ContractDetail : Form
    {
        private string agreementId;
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

        public ContractDetail(string agreementId)
        {
            InitializeComponent();
            this.agreementId = agreementId;
        }

        private void ContractDetail_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string query = "SELECT a.agreement_id, s.supplier_name, p.delivery_address, a.effectived_at, a.end_at " +
                           "FROM ((agreement AS a " +
                           "INNER JOIN supplier AS s ON a.supplier_id = s.supplier_id) " +
                           "INNER JOIN purchase_order AS p ON a.agreement_id = p.agreement_id) " +
                           "WHERE a.agreement_id = @AgreementId";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AgreementId", agreementId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox3.Text = reader["agreement_id"].ToString();
                                txtSupplier.Text = reader["supplier_name"].ToString();
                                txtDeliveryAddress.Text = reader["delivery_address"].ToString();
                                txtStart.Text = reader["effectived_at"].ToString();
                                txtEnd.Text = reader["end_at"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No data found for this agreement ID.");
                            }
                        }

                        string itemQuery = "SELECT i.item_name, i.item_id " +
                                      "FROM item AS i, agreement AS a, request_items AS r " +
                                      "WHERE r.item_id = i.item_id " +
                                      "AND r.request_id = a.request_item_id " +
                                      "AND a.agreement_id = ?";

                        using (OleDbCommand itemCommand = new OleDbCommand(itemQuery, connection))
                        {
                            itemCommand.Parameters.AddWithValue("@AgreementId", agreementId);

                            using (OleDbDataReader itemReader = itemCommand.ExecuteReader())
                            {
                                if (itemReader.HasRows)
                                {
                                    DataTable dt = new DataTable();
                                    dt.Load(itemReader);

                                    // Bind DataTable to DataGridView
                                    dataGridView1.DataSource = dt;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading contract details: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Perform any actions or navigation logic you want when the back button is clicked

            // For example, you can close the current form and go back to the previous form
            this.Close();
        }

    }
}
