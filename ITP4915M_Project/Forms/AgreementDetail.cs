using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class AgreementDetail : Form
    {
        private string agreementId;
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

        public AgreementDetail(string agreementId)
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
            string query = "SELECT a.agreement_id, s.supplier_name, p.delivery_address, a.effectived_at, a.end_at, r.restaurant_name, i.inventory_Name " +
                           "FROM ((((agreement AS a " +
                           "INNER JOIN supplier AS s ON a.supplier_id = s.supplier_id) " +
                           "INNER JOIN purchase_order AS p ON a.agreement_id = p.agreement_id) " +
                           "LEFT JOIN restaurant AS r ON p.delivery_address = r.restaurant_address) " +
                           "LEFT JOIN inventory AS i ON p.delivery_address = i.restaurant_address) " +
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

                                // If delivery address matches a restaurant, show the restaurant name, 
                                // else if it matches an inventory, show the inventory name,
                                // else show the delivery address.
                                if (!reader.IsDBNull(reader.GetOrdinal("restaurant_name")))
                                {
                                    txtDeliveryAddress.Text = reader["restaurant_name"].ToString();
                                }
                                else if (!reader.IsDBNull(reader.GetOrdinal("inventory_Name")))
                                {
                                    txtDeliveryAddress.Text = reader["inventory_Name"].ToString();
                                }
                                else
                                {
                                    txtDeliveryAddress.Text = reader["delivery_address"].ToString();
                                }

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

                    string detailQuery = "SELECT agreement_detail_id, delivery_schedule, account_info, quantity, cost " +
                                 "FROM agreement_detail " +
                                 "WHERE agreement_id = ?";

                    using (OleDbCommand detailCommand = new OleDbCommand(detailQuery, connection))
                    {
                        detailCommand.Parameters.AddWithValue("@AgreementId", agreementId);

                        using (OleDbDataReader detailReader = detailCommand.ExecuteReader())
                        {
                            if (detailReader.HasRows)
                            {
                                if (detailReader.Read())
                                {
                                    txtQty.Text = detailReader["quantity"].ToString();
                                    txtCost.Text = detailReader["cost"].ToString();
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
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Enter new delivery address",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = "New Name" };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender2, e2) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            DialogResult result = prompt.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newName = textBox.Text;
                UpdateName(newName);
            }
        }

        private void UpdateName(string newName)
        {
            string query = "UPDATE purchase_order SET delivery_address = @NewName " +
                           "WHERE agreement_id = @AgreementId";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewName", newName);
                        command.Parameters.AddWithValue("@AgreementId", agreementId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Name updated successfully!");
                        LoadData(); // refresh the data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the name: " + ex.Message);
            }
        }

    }
}