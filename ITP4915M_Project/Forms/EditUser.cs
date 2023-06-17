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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate the form inputs (e.g. check for empty fields) before updating the user

            _user.LoginName = txtLoginName.Text;
            _user.Password = txtPassword.Text;
            _user.StaffName = txtStaffName.Text;

            UpdateUser(_user);

        }

        private void UpdateUser(User user)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
            string query = "UPDATE staff SET login_name = ?, login_password = ?, staff_name = ? WHERE staff_id = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoginName", user.LoginName);
                    command.Parameters.AddWithValue("@LoginPassword", user.Password);
                    command.Parameters.AddWithValue("@StaffName", user.StaffName);
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
}