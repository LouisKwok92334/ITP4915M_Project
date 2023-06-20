using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using ITP4915M_Project.Models;


namespace ITP4915M_Project.Forms
{
    public partial class ItemManagement : Form
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
        private bool isEditMode = false;
        private string selectedItemId = "";
        public ItemManagement()
        {
            InitializeComponent();

      
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchItem = txtsearchItem.Text;


            string query = "SELECT i.item_id AS Id, i.item_name AS Name, i.virtual_id AS VirtualId, i.stock, i.status, s.supplier_name AS SupplierName " +
     "FROM item i " +
     "INNER JOIN supplier s ON i.supplier_id = s.supplier_id " +
     "WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(searchItem))
                query += "AND i.item_name LIKE @SearchItem ";

        

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(searchItem))
                            command.Parameters.AddWithValue("@SearchItem", "%" + searchItem + "%");

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtVirualID.Text = reader["VirtualId"].ToString();
                                txtId.Text = reader["Id"].ToString();
                                txtName.Text = reader["Name"].ToString();
                                txtStock.Text = reader["stock"].ToString();
                                txtStatus.Text = reader["status"].ToString();
                                txtSupplierName.Text = reader["SupplierName"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while searching: " + ex.Message);
            }
            string query2 = "SELECT ri.created_at AS CreatedAt, ri.quantity AS Quantity, ri.total_price AS TotalPrice, ri.updated_at AS updatedAt " +
    "FROM request_items ri " +
    "WHERE 1 = 1 ";
            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                using (OleDbCommand command = new OleDbCommand(query2, connection))
                {
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

        //private DataTable GetData(string queryString)
        //{
        //    DataTable dt = new DataTable();

        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        using (OleDbCommand cmd = new OleDbCommand(queryString, conn))
        //        {
        //            try
        //            {
        //                conn.Open();

        //                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //                da.Fill(dt);
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }

        //    return dt;
        //}

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView1.CurrentRow != null)
        //    {
        //        int itemId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["item_id"].Value);

        //        // Retrieve the item details from the database
        //        ITP4915M_Project.Models.Item selectedItem = GetItemById(itemId);

        //        if (selectedItem != null)
        //        {
        //            var editForm = new Edit(selectedItem);
        //            editForm.Show();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Item not found.");
        //        }
        //    }
        //}

        //private ITP4915M_Project.Models.Item GetItemById(int itemId)
        //{
        //    using (OleDbConnection conn = new OleDbConnection(ConnectionString))
        //    {
        //        string query = $"SELECT item_id, item_name, supplier_name, stock FROM item INNER JOIN supplier ON item.supplier_id = supplier.supplier_id WHERE item_id = {itemId}";


        //        using (OleDbCommand cmd = new OleDbCommand(query, conn))
        //        {
        //            try
        //            {
        //                conn.Open();
        //                OleDbDataReader reader = cmd.ExecuteReader();
        //                if (reader.Read())
        //                {
        //                    ITP4915M_Project.Models.Item item = new ITP4915M_Project.Models.Item(); // Note the full namespace here
        //                    item.Id = Convert.ToInt32(reader["item_id"]);
        //                    item.Name = Convert.ToString(reader["item_name"]);
        //                    item.Supplier = Convert.ToString(reader["supplier_name"]);
        //                    item.Stack = Convert.ToInt32(reader["stock"]);
        //                    // Set other properties as needed
        //                    return item;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }
        //        }
        //    }

        //    return null; // Item not found
        //}

        private void btnAddItem_Click(object sender, EventArgs e)
        {

        }

        private void btnBPA_Click(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT b.bpa_id, s.supplier_name AS SupplierName, b.quantity AS Quantity, b.total_price AS TotalPrice, b.effectived_date, b.end_date, b.status " +
                    "FROM bpa b " +
                    "INNER JOIN supplier s ON b.supplier_id = s.supplier_id";



            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isEditMode) // If not in edit mode, enable editing
            {
                txtStock.ReadOnly = false;
                txtStatus.ReadOnly = false;
                btnEdit.Text = "OK";
                isEditMode = true;
            }
            else // If in edit mode, save changes and disable editing
            {
                // Update the database with the new values from txtStock and txtStatus
                UpdateDatabase(txtStock.Text, txtStatus.Text);

                txtStock.ReadOnly = true;
                txtStatus.ReadOnly = true;
                btnEdit.Text = "Edit";
                isEditMode = false;
            }
        }
        private void UpdateDatabase(string newStock, string newStatus)
        {
            selectedItemId = txtId.Text;
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string itemId = selectedItemId; // Replace with the appropriate item ID

            string query = "UPDATE item SET stock = @stock, status = @status WHERE item_id = @itemId";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@stock", newStock);
                    command.Parameters.AddWithValue("@status", newStatus);
                    command.Parameters.AddWithValue("@itemId", itemId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }
    }
}
