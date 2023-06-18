using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace ITP4915M_Project
{
    public partial class Login : Form
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";

        public Login()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            if (ValidateCredentials(username, password))
            {
                MessageBox.Show("Login successful!");

                GlobalUser.StaffID = GetStaffID(username);
                GlobalUser.StaffName = GetStaffName(username);
                GlobalUser.Title = GetTitle(username);

                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private bool ValidateCredentials(string username, string password)
        {
            bool isValid = false;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Staff WHERE userName = @username AND password = @password";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        int count = (int)command.ExecuteScalar();
                        isValid = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while validating credentials: " + ex.Message);
            }

            return isValid;
        }

        private string GetStaffID(string username)
        {
            string staffID = null;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT staffID FROM Staff WHERE userName = @username";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        staffID = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving staff ID: " + ex.Message);
            }

            return staffID;
        }

        private string GetStaffName(string username)
        {
            string staffName = null;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT staffName FROM Staff WHERE userName = @username";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        staffName = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving staff name: " + ex.Message);
            }

            return staffName;
        }

        private string GetTitle(string username)
        {
            string title = null;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT title FROM Staff WHERE userName = @username";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        title = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving title: " + ex.Message);
            }

            return title;
        }

        public static class GlobalUser
<<<<<<< HEAD
        {
            public static string StaffID { get; set; }
            public static string StaffName { get; set; }
            public static string Title { get; set; }
        }
=======
{
    public static string StaffID { get; set; }
    public static string StaffName { get; set; }
    public static string Title { get; set; }
}
>>>>>>> parent of 8dc441a (PasswordChar)

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            if (txtUserName.Text == "UserName")
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
<<<<<<< HEAD
=======
                txtUserName.TextAlign = HorizontalAlignment.Left;
>>>>>>> parent of 8dc441a (PasswordChar)
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "UserName";
                txtUserName.ForeColor = Color.Silver;
<<<<<<< HEAD
=======
                txtUserName.TextAlign = HorizontalAlignment.Center;
>>>>>>> parent of 8dc441a (PasswordChar)
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
<<<<<<< HEAD
=======

>>>>>>> parent of 8dc441a (PasswordChar)
            if (txtPassword.Text == "Password")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.Black;
<<<<<<< HEAD
                txtPassword.UseSystemPasswordChar = true;
            }
=======
                txtPassword.TextAlign = HorizontalAlignment.Left;
                txtPassword.PasswordChar = '*';
            }

>>>>>>> parent of 8dc441a (PasswordChar)
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "Password";
                txtPassword.ForeColor = Color.Silver;
<<<<<<< HEAD
                txtPassword.UseSystemPasswordChar = false;
            }
=======
                txtPassword.TextAlign = HorizontalAlignment.Center;
                txtPassword.PasswordChar = '\0';
            }

>>>>>>> parent of 8dc441a (PasswordChar)
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
