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

        public ItemManagement()
        {
            InitializeComponent();
            FillComboBoxes();
            cboCategory.SelectedValue = false;
            cboSupplier.SelectedValue = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchItem = txtsearchItem.Text;
            int? categoryId = cboCategory.SelectedValue as int?;
            int? supplierId = cboSupplier.SelectedValue as int?;

            string query = "SELECT i.item_id AS Id, i.item_name AS Name, i.virtual_id AS VirtualId, i.stock, i.status, s.supplier_name AS SupplierName " +
     "FROM item i " +
     "INNER JOIN supplier s ON i.supplier_id = s.supplier_id " +
     "WHERE 1 = 1 ";

            if (!string.IsNullOrWhiteSpace(searchItem))
                query += "AND i.item_name LIKE @SearchItem ";

            if (categoryId != null)
                query += "AND i.category_id = @CategoryId ";

            if (supplierId != null)
                query += "AND i.supplier_id = @SupplierId ";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(searchItem))
                            command.Parameters.AddWithValue("@SearchItem", "%" + searchItem + "%");

                        if (categoryId != null)
                            command.Parameters.AddWithValue("@CategoryId", categoryId.Value);

                        if (supplierId != null)
                            command.Parameters.AddWithValue("@SupplierId", supplierId.Value);

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
        }

        private DataTable GetData(string queryString)
        {
            DataTable dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(queryString, conn))
                {
                    try
                    {
                        conn.Open();

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return dt;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];

                txtName.Text = Convert.ToString(selectedRow.Cells["item_name"].Value);
                txtSupplierName.Text = Convert.ToString(selectedRow.Cells["supplier_name"].Value);
                txtId.Text = Convert.ToString(selectedRow.Cells["item_id"].Value);
                txtStock.Text = Convert.ToString(selectedRow.Cells["stock"].Value);
            }
        }

        private void FillComboBoxes()
        {
            string categoryQuery = "SELECT category_id, category_name FROM category";
            DataTable dtCategory = GetData(categoryQuery);
            cboCategory.DataSource = dtCategory;
            cboCategory.ValueMember = "category_id";
            cboCategory.DisplayMember = "category_name";

            string supplierQuery = "SELECT supplier_id, supplier_name FROM supplier";
            DataTable dtSupplier = GetData(supplierQuery);
            cboSupplier.DataSource = dtSupplier;
            cboSupplier.ValueMember = "supplier_id";
            cboSupplier.DisplayMember = "supplier_name";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int itemId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["item_id"].Value);

                // Retrieve the item details from the database
                ITP4915M_Project.Models.Item selectedItem = GetItemById(itemId);

                if (selectedItem != null)
                {
                    var editForm = new Edit(selectedItem);
                    editForm.Show();
                }
                else
                {
                    MessageBox.Show("Item not found.");
                }
            }
        }

        private ITP4915M_Project.Models.Item GetItemById(int itemId)
        {
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                string query = $"SELECT item_id, item_name, supplier_name, stock FROM item INNER JOIN supplier ON item.supplier_id = supplier.supplier_id WHERE item_id = {itemId}";


                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            ITP4915M_Project.Models.Item item = new ITP4915M_Project.Models.Item(); // Note the full namespace here
                            item.Id = Convert.ToInt32(reader["item_id"]);
                            item.Name = Convert.ToString(reader["item_name"]);
                            item.Supplier = Convert.ToString(reader["supplier_name"]);
                            item.Stack = Convert.ToInt32(reader["stock"]);
                            // Set other properties as needed
                            return item;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return null; // Item not found
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {

        }
    }
}
