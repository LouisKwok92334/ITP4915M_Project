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
            string query = "SELECT * FROM staff WHERE (status <> 'Cancelled' OR status IS NULL)";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    _bindingSource.DataSource = dataTable;
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }




        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to mark the selected record as cancelled?", "Confirm Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
                    string staffId = dataGridView1.SelectedRows[0].Cells["staff_id"].Value.ToString();

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        try
                        {
                            string updateStaffQuery = "UPDATE staff SET status = 'Cancelled' WHERE staff_id = ?";
                            using (OleDbCommand updateStaffCommand = new OleDbCommand(updateStaffQuery, connection))
                            {
                                updateStaffCommand.Parameters.AddWithValue("@StaffId", staffId);
                                int rowsAffected = updateStaffCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("The selected record has been marked as cancelled.", "Cancel Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred while cancelling the record. Please try again.", "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (OleDbException ex)
                        {
                            MessageBox.Show("An error occurred while cancelling the record: " + ex.Message, "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Please select a record to cancel.", "No Record Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            string query = "SELECT staff.staff_id, staff.login_name, staff.login_password, staff.staff_name, role.role_name FROM staff INNER JOIN role ON staff.role_id = role.role_id WHERE staff.staff_id = ?";

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
                                reader.GetString(3),
                                reader.GetString(4)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchValue = txtSearch.Text;

            if (string.IsNullOrWhiteSpace(searchValue))
            {
                MessageBox.Show("Please enter a name to search.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT * FROM staff WHERE staff_name LIKE ? AND (status <> 'Cancelled' OR status IS NULL)";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    // Use '%' as wildcard for LIKE
                    command.Parameters.AddWithValue("@staff_name", "%" + searchValue + "%");

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        _bindingSource.DataSource = dataTable;

                        if (_bindingSource.Count > 0)
                        {
                            MessageBox.Show($"Found {_bindingSource.Count} matching records");
                        }
                        else
                        {
                            MessageBox.Show("No matching records found");
                        }
                    }
                }

                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Create a new user with default or blank values
            User newUser = new User(0, "", "", "", "");

            EditUser editUserForm = new EditUser(newUser);
            if (editUserForm.ShowDialog() == DialogResult.OK)
            {
                // Reload data after adding the user
                LoadData();
            }
        }

    }
}
