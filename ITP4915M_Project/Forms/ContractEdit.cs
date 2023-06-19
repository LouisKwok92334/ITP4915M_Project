using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class ContractEdit : Form
    {
        private string agreementId;
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

        public ContractEdit(string agreementId)
        {
            InitializeComponent();
            this.agreementId = agreementId;
        }

        private void ContractEdit_Load(object sender, EventArgs e)
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string quantity = txtQty.Text;
            string cost = txtCost.Text;
            string restaurantName = txtDeliveryAddress.Text;
            string updateQuery = @"UPDATE purchase_order 
                             SET delivery_address = r.restaurant_address 
                             FROM purchase_order AS po 
                             JOIN restaurant r ON po.restaurant_address = r.restaurant_address 
                             WHERE r.restaurant_name = @restaurantName";



            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    

                    using (OleDbCommand command = new OleDbCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@restaurantName", restaurantName);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Contract details updated successfully.");
                        }
                        else
                        {
                            MessageBox.Show("No rows were affected. Contract details update failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating contract details: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

