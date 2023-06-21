using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ITP4915M_Project.Forms
{
    public partial class ItemAdd : Form
    {
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

        public ItemAdd()
        {
            InitializeComponent();
        }

        private void ItemAdd_Load(object sender, EventArgs e)
        {
            FillComboBox(cboSupplier, "supplier", "supplier_id", "supplier_name");
            FillComboBox(cmoCategory, "category", "category_id", "category_name");
        }

        private void FillComboBox(ComboBox comboBox, string tableName, string valueMember, string displayMember)
        {
            string query = $"SELECT {valueMember}, {displayMember} FROM {tableName}";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    comboBox.DataSource = dataTable;
                    comboBox.ValueMember = valueMember;
                    comboBox.DisplayMember = displayMember;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
    {
        string itemName = txtItemName.Text.Trim();
        int stock = int.Parse(txtStock.Text);
        string status = txtstatus.Text.Trim();
        int supplierId = (int)cboSupplier.SelectedValue;
        int categoryId = (int)cmoCategory.SelectedValue;

        string query = "INSERT INTO item (item_name, category_id, virtual_id, stock, supplier_id, status) " +
                       "VALUES (@ItemName, @CategoryId, NULL, @Stock, @SupplierId, @Status)";

        try
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ItemName", itemName);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                command.Parameters.AddWithValue("@Stock", stock);
                command.Parameters.AddWithValue("@SupplierId", supplierId);
                command.Parameters.AddWithValue("@Status", status);

                connection.Open();
                command.ExecuteNonQuery();
            }

            MessageBox.Show("Item has been added successfully!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }


    }
}
