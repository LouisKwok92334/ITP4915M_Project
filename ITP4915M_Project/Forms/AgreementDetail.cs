using System;
using System.Collections.Generic;
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
            string query = "SELECT a.agreement_id, s.supplier_name, a.effectived_at, a.end_at, r.restaurant_name, r.restaurant_id, a.status " +
                "FROM (((agreement AS a " +
                "INNER JOIN supplier AS s ON a.supplier_id = s.supplier_id) " +
                "INNER JOIN purchase_order AS p ON a.agreement_id = p.agreement_id) " +
                "LEFT JOIN restaurant AS r ON p.restaurant_id = r.restaurant_id) " +
                "WHERE a.agreement_id = ?";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", agreementId);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox3.Text = reader["agreement_id"].ToString();
                                txtSupplier.Text = reader["supplier_name"].ToString();
                                string restaurantName = reader["restaurant_name"].ToString();
                                txtDeliveryAddress.Text = string.IsNullOrEmpty(restaurantName) ? "Warehouse" : restaurantName;
                                txtStart.Text = reader["effectived_at"].ToString();
                                txtEnd.Text = reader["end_at"].ToString();
                                txtStatus.Text = reader["status"].ToString();

                                string itemQuery = "SELECT i.item_name, i.item_id " +
                                                   "FROM item AS i, agreement AS a, request_items AS r " +
                                                   "WHERE r.item_id = i.item_id " +
                                                   "AND r.request_id = a.request_item_id " +
                                                   "AND a.agreement_id = ?";

                                using (OleDbCommand itemCommand = new OleDbCommand(itemQuery, connection))
                                {
                                    itemCommand.Parameters.AddWithValue("?", agreementId);

                                    using (OleDbDataReader itemReader = itemCommand.ExecuteReader())
                                    {
                                        if (itemReader.HasRows)
                                        {
                                            DataTable dt = new DataTable();
                                            dt.Load(itemReader);
                                            dataGridView1.DataSource = dt;
                                        }
                                    }
                                }

                                string detailQuery = "SELECT agreement_detail_id, delivery_schedule, account_info, quantity, cost " +
                                                   "FROM agreement_detail " +
                                                   "WHERE agreement_id = ?";

                                using (OleDbCommand detailCommand = new OleDbCommand(detailQuery, connection))
                                {
                                    detailCommand.Parameters.AddWithValue("?", agreementId);

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
            DialogResult editChoice = MessageBox.Show("Do you want to edit the delivery location? Click 'No' to edit the status.",
                                                      "Edit choice", MessageBoxButtons.YesNoCancel);

            if (editChoice == DialogResult.Yes)
            {
                ShowEditDeliveryLocationPrompt();
            }
            else if (editChoice == DialogResult.No)
            {
                ShowEditStatusPrompt();
            }
        }

        private void ShowEditDeliveryLocationPrompt()
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Select new delivery Location",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = "New delivery Location" };
            ComboBox comboBox = new ComboBox() { Left = 50, Top = 50, Width = 400 };
            comboBox.DataSource = new BindingSource(LoadRestaurantNames(), null);
            comboBox.DisplayMember = "Key";
            comboBox.ValueMember = "Value";
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender2, e2) => { prompt.Close(); };
            prompt.Controls.Add(comboBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            DialogResult result = prompt.ShowDialog();
            if (result == DialogResult.OK)
            {
                int? newRestaurantId = comboBox.SelectedValue as int?;
                UpdateName(newRestaurantId);
            }
        }

        private Dictionary<string, int?> LoadRestaurantNames()
        {
            Dictionary<string, int?> restaurantNames = new Dictionary<string, int?>();
            restaurantNames.Add("Warehouse", null);

            string query = "SELECT restaurant_id, restaurant_name FROM restaurant";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int restaurantId = Convert.ToInt32(reader["restaurant_id"]);
                                string restaurantName = reader["restaurant_name"].ToString();
                                restaurantNames.Add(restaurantName, restaurantId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading restaurant names: " + ex.Message);
            }

            return restaurantNames;
        }

        private void UpdateName(int? newRestaurantId)
        {
            string query = "UPDATE purchase_order SET restaurant_id = @NewRestaurantId " +
                           "WHERE agreement_id = @AgreementId";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                    command.Parameters.AddWithValue("@NewRestaurantId", newRestaurantId.HasValue ? (object)newRestaurantId.Value : DBNull.Value);
                        command.Parameters.AddWithValue("@AgreementId", agreementId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Restaurant ID updated successfully!");
                        LoadData(); // refresh the data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the restaurant ID: " + ex.Message);
            }
        }

        private void ShowEditStatusPrompt()
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Select new status",
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = "New status" };
            ComboBox comboBox = new ComboBox() { Left = 50, Top = 50, Width = 400 };
            comboBox.Items.AddRange(new string[] { "Created", "Sent", "Accepted", "Rejected", "Canceled" });
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender2, e2) => { prompt.Close(); };
            prompt.Controls.Add(comboBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            DialogResult result = prompt.ShowDialog();
            if (result == DialogResult.OK)
            {
                string newStatus = comboBox.SelectedItem.ToString();
                UpdateStatus(newStatus);
            }
        }

        private void UpdateStatus(string newStatus)
        {
            string query = "UPDATE agreement SET status = @NewStatus " +
                           "WHERE agreement_id = @AgreementId";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NewStatus", newStatus);
                        command.Parameters.AddWithValue("@AgreementId", agreementId);

                        command.ExecuteNonQuery();

                        MessageBox.Show("Status updated successfully!");
                        LoadData(); // refresh the data
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while updating the status: " + ex.Message);
            }
        }
    }
}