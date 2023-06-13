using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ITP4915M_Project
{
    public partial class Login : Form
    {
        // Database connection string
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database1.accdb";

        public Login()
        {
            InitializeComponent();
        }

        // Login button click event
        private void btnlogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text.Trim();
            string password = txtPassword.Text;

            // Validate credentials
            if (ValidateCredentials(username, password))
            {
                // Successful login
                MessageBox.Show("Login successful!");

                // Set global user properties
                GlobalUser.StaffID = GetStaffID(username);
                GlobalUser.StaffName = GetStaffName(username);
                GlobalUser.Title = GetTitle(username);

                // Perform any additional actions or open the main form
                // ...
                MainMenu mainMenu = new MainMenu();
                mainMenu.Show();

                // Hide the login form
                this.Hide();


            }
            else
            {
                // Invalid credentials
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        // Validate user credentials
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

        // Get staff ID from the database
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

        // Get staff name from the database
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

        // Get title from the database
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
{
    public static string StaffID { get; set; }
    public static string StaffName { get; set; }
    public static string Title { get; set; }
}

    }
}
