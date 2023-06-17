using System;
using System.Data.OleDb;
using System.Windows.Forms;
using ITP4915M_Project.Models;

namespace ITP4915M_Project.Forms
{
    public partial class EditUser : Form
    {
        private User _user;

        public EditUser(User user)
        {
            InitializeComponent();
            _user = user;
            PopulateFormFields();
            btnSave.Click += new EventHandler(this.btnSave_Click);
        }

        private void PopulateFormFields()
        {
            txtLoginName.Text = _user.LoginName;
            txtPassword.Text = _user.Password;
            txtStaffName.Text = _user.StaffName;
            LoadRoles();
            cboRole.SelectedItem = _user.Role;
        }

        private void LoadRoles()
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT role_name FROM role"; // Adjust the query according to your database schema

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cboRole.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate the form inputs (e.g. check for empty fields) before updating the user
            if (cboRole.SelectedItem != null)
            {
                _user.LoginName = txtLoginName.Text;
                _user.Password = txtPassword.Text;
                _user.StaffName = txtStaffName.Text;
                _user.Role = cboRole.SelectedItem.ToString(); // Update the role

                UpdateUser(_user);

                // Close the form after saving the changes.
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Please select a role for the user.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateUser(User user)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                if (user.ID == 0) // new user
                {
                    string query = "INSERT INTO staff (login_name, login_password, staff_name, role_id, created_at) VALUES (?, ?, ?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoginName", user.LoginName);
                        command.Parameters.AddWithValue("@LoginPassword", user.Password);
                        command.Parameters.AddWithValue("@StaffName", user.StaffName);
                        int roleId = GetRoleIdByName(user.Role);
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.Today);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("The record has been inserted successfully.", "Insertion Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while inserting the record. Please try again.", "Insertion Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } 
                else // existing user
                {
                    string query = "UPDATE staff SET login_name = ?, login_password = ?, staff_name = ?, role_id = ? WHERE staff_id = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoginName", user.LoginName);
                        command.Parameters.AddWithValue("@LoginPassword", user.Password);
                        command.Parameters.AddWithValue("@StaffName", user.StaffName);

                        int roleId = GetRoleIdByName(user.Role);
                        command.Parameters.AddWithValue("@RoleId", roleId);

                        command.Parameters.AddWithValue("@StaffId", user.ID);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("The record has been updated successfully.", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("An error occurred while updating the record. Please try again.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }


        private int GetRoleIdByName(string roleName)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "SELECT role_id FROM role WHERE role_name = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RoleName", roleName);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(0);
                        }
                        else
                        {
                            throw new ArgumentException("Role name not found in the database.", nameof(roleName));
                        }
                    }
                }
            }
        }
    }
}
