using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class SystemSecurityControl : Form
    {
        private BindingSource _bindingSource;

        public SystemSecurityControl()
        {
            InitializeComponent();
            this.VisibleChanged += SystemSecurityControl_VisibleChanged;
            _bindingSource = new BindingSource();
            dataGridView1.DataSource = _bindingSource;
            LoadData();
            btnDelete.Click += new EventHandler(this.btnDelete_Click);
        }
        private void LoadData()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT * FROM staff";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                _bindingSource.DataSource = dataTable;
            }

            dataGridView1.Refresh(); // 强制重新绘制 dataGridView1 的内容
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete the selected record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
                    string staffId = dataGridView1.SelectedRows[0].Cells["staff_id"].Value.ToString();

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        try
                        {
                            string deleteMenuItemQuery = "DELETE FROM item WHERE item_id IN (SELECT item_id FROM restaurant WHERE staff_id = ?)";
                            using (OleDbCommand deleteMenuItemCommand = new OleDbCommand(deleteMenuItemQuery, connection))
                            {
                                deleteMenuItemCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteMenuItemCommand.ExecuteNonQuery();
                            }

                            string deleteRestaurantQuery = "DELETE FROM restaurant WHERE staff_id = ?";
                            using (OleDbCommand deleteRestaurantCommand = new OleDbCommand(deleteRestaurantQuery, connection))
                            {
                                deleteRestaurantCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRestaurantCommand.ExecuteNonQuery();
                            }

                            string deleteRequestItemsQuery = "DELETE FROM request_items WHERE request_id IN (SELECT request_id FROM request WHERE create_staff_id = ? OR processed_staff_id = ?)";
                            using (OleDbCommand deleteRequestItemsCommand = new OleDbCommand(deleteRequestItemsQuery, connection))
                            {
                                deleteRequestItemsCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRequestItemsCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRequestItemsCommand.ExecuteNonQuery();
                            }

                            // Delete from other related tables in a similar manner

                            string deleteRequestQuery = "DELETE FROM request WHERE create_staff_id = ? OR processed_staff_id = ?";
                            using (OleDbCommand deleteRequestCommand = new OleDbCommand(deleteRequestQuery, connection))
                            {
                                deleteRequestCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRequestCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRequestCommand.ExecuteNonQuery();
                            }

                            string deleteInventoryLogQuery = "DELETE FROM inventory_log WHERE staff_id = ?";
                            using (OleDbCommand deleteInventoryLogCommand = new OleDbCommand(deleteInventoryLogQuery, connection))
                            {
                                deleteInventoryLogCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteInventoryLogCommand.ExecuteNonQuery();
                            }

                            string deleteStaffQuery = "DELETE FROM staff WHERE staff_id = ?";
                            using (OleDbCommand deleteStaffCommand = new OleDbCommand(deleteStaffQuery, connection))
                            {
                                deleteStaffCommand.Parameters.AddWithValue("@StaffId", staffId);
                                int rowsAffected = deleteStaffCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("The selected record has been deleted.", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred while deleting the record. Please try again.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (OleDbException ex)
                        {
                            MessageBox.Show("An error occurred while deleting the record: " + ex.Message, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }

                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Please select a record to delete.", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

      private void btnEdit_Click(object sender, EventArgs e)
{
    if (dataGridView1.SelectedRows.Count > 0)
    {
        int staffId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["staff_id"].Value); 
        User user = GetUserById(staffId);

        if (user != null)
        {
            EditUser editUserForm = new EditUser(user);
            editUserForm.ShowDialog();

            // Reload data after editing the user.
            LoadData();
        }
        else
        {
            MessageBox.Show("User not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    else
    {
        MessageBox.Show("Please select a user to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
        private User GetUserById(int staffId)
        {
            User user = null;
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT staff_id, login_name, login_password, staff_name FROM staff WHERE staff_id = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StaffId", staffId);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3)
                            );
                        }
                    }
                }
            }

            return user;
        }

        
        private void SystemSecurityControl_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                LoadData();
            }
        }
    }
}
