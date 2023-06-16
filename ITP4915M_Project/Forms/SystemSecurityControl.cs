using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class SystemSecurityControl : Form
    {
        public SystemSecurityControl()
        {
            InitializeComponent();
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
                dataGridView1.DataSource = dataTable;
            }
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
                            // Delete the 'restaurant' records related to the staff
                            string deleteRestaurantQuery = "DELETE FROM restaurant WHERE staff_id = @StaffId";
                            using (OleDbCommand deleteRestaurantCommand = new OleDbCommand(deleteRestaurantQuery, connection))
                            {
                                deleteRestaurantCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRestaurantCommand.ExecuteNonQuery();
                            }

                            // Delete the 'request_items' records related to the staff
                            string deleteRequestItemsQuery = "DELETE FROM request_items WHERE request_id IN (SELECT request_id FROM request WHERE create_staff_id = @StaffId OR processed_staff_id = @StaffId)";
                            using (OleDbCommand deleteRequestItemsCommand = new OleDbCommand(deleteRequestItemsQuery, connection))
                            {
                                deleteRequestItemsCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRequestItemsCommand.ExecuteNonQuery();
                            }

                            // Delete the 'request' records related to the staff
                            string deleteRequestQuery = "DELETE FROM request WHERE create_staff_id = @StaffId OR processed_staff_id = @StaffId";
                            using (OleDbCommand deleteRequestCommand = new OleDbCommand(deleteRequestQuery, connection))
                            {
                                deleteRequestCommand.Parameters.AddWithValue("@StaffId", staffId);
                                deleteRequestCommand.ExecuteNonQuery();
                            }

                            // Delete the 'staff' record
                            string deleteStaffQuery = "DELETE FROM staff WHERE StaffId = @StaffId";
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
    }
}
