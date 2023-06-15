using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace ITP4915M_Project.Forms
{
    public partial class ItemManagement : Form
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

        public ItemManagement()
        {
            InitializeComponent();
            FillComboBoxes();
        }

      private void btnSearch_Click(object sender, EventArgs e)
{
    string categoryCondition = cboCategory.SelectedValue != null ? $"AND category_id = {cboCategory.SelectedValue}" : "";
    string supplierCondition = cboSupplier.SelectedValue != null ? $"AND item.supplier_id = {cboSupplier.SelectedValue}" : "";  // Changed supplier_id to item.supplier_id
    string queryString = $"SELECT item.*, supplier.supplier_name FROM item INNER JOIN supplier ON item.supplier_id = supplier.supplier_id WHERE item_name LIKE '%{txtsearchItem.Text}%' {categoryCondition} {supplierCondition}";


            // Get the data
            DataTable dt = GetData(queryString);

    // Display the data in dataGridView1
    dataGridView1.DataSource = dt;
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
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];

                txtName.Text = Convert.ToString(selectedRow.Cells["item_name"].Value);
                txtSupplier.Text = Convert.ToString(selectedRow.Cells["supplier_name"].Value);
                txtId.Text = Convert.ToString(selectedRow.Cells["item_id"].Value);
                txtStock.Text = Convert.ToString(selectedRow.Cells["stock"].Value);
            }
        }

        private void FillComboBoxes()
        {
            // Fill the cboCategory
            string categoryQuery = "SELECT category_id, category_name FROM category";
            DataTable dtCategory = GetData(categoryQuery);
            cboCategory.DataSource = dtCategory;
            cboCategory.ValueMember = "category_id";  // The actual value for the item
            cboCategory.DisplayMember = "category_name";  // The value shown to the user

            // Fill the cboSupplier
            string supplierQuery = "SELECT supplier_id, supplier_name FROM supplier";
            DataTable dtSupplier = GetData(supplierQuery);
            cboSupplier.DataSource = dtSupplier;
            cboSupplier.ValueMember = "supplier_id"; // The actual value for the item
            cboSupplier.DisplayMember = "supplier_name"; // The value shown to the user
        }

    }
}
