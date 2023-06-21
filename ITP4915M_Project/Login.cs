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
            string password = txtPassword.Text.Trim();

            if (ValidateCredentials(username, password))
            {
                MessageBox.Show("Login successful!");

                GlobalUser.StaffID = GetStaffID(username);
                GlobalUser.StaffName = GetStaffName(username);
                GlobalUser.Title = GetTitle(username);
                GlobalUser.Restaurant = GetRestaurant(username);

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

                    string query = "SELECT COUNT(*) FROM staff WHERE login_name = ? AND login_password = ?";
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

        private int GetStaffID(string username)
        {
            int staffID = 0;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT staff_id FROM staff WHERE login_name = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", username);

                        staffID = Convert.ToInt32(command.ExecuteScalar());
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

                    string query = "SELECT staff_name FROM staff WHERE login_name = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", username);

                        staffName = command.ExecuteScalar()?.ToString();
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
            string title = "";

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT R.role_name FROM staff AS S INNER JOIN role AS R ON S.role_id = R.role_id WHERE S.login_name = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", username);

                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                title = reader["role_name"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching the title: " + ex.Message);
            }

            return title;
        }

        private string GetRestaurant(string username)
        {
            string restaurant = null;

            try
            {
                using (OleDbConnection connection = new OleDbConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT R.restaurant_name FROM staff AS S INNER JOIN Restaurant AS R ON S.staff_id = R.staff_id WHERE S.login_name = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", username);

                        restaurant = command.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving restaurant: " + ex.Message);
            }

            return restaurant;
        }

        //private void txtUserName_Leave(object sender, EventArgs e)
        //{
        //    if (txtUserName.Text == "")
        //    {
        //        txtUserName.Text = "UserName";
        //        txtUserName.ForeColor = Color.Silver;
        //        txtUserName.TextAlign = HorizontalAlignment.Center;
        //    }
        //}

        //private void txtPassword_Enter(object sender, EventArgs e)
        //{
        //    if (txtPassword.Text == "Password")
        //    {
        //        txtPassword.Text = "";
        //        txtPassword.ForeColor = Color.Black;
        //    }
        //}

        //private void txtPassword_Leave(object sender, EventArgs e)
        //{
        //    if (txtPassword.Text == "")
        //    {
        //        txtPassword.Text = "Password";
        //        txtPassword.ForeColor = Color.Silver;
        //    }
        //}

        //private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (txtPassword.Text == "Password")
        //    {
        //        txtPassword.Text = "";
        //        txtPassword.ForeColor = Color.Black;
        //    }

        //    password += e.KeyChar;
        //    txtPassword.Text += "*";
        //    txtPassword.Select(txtPassword.Text.Length, 0);
        //    e.Handled = true;
        //}

        private void Login_Load(object sender, EventArgs e)
        {
            // Code to execute when the login form is loaded
        }
    }

    public static class GlobalUser
    {
        public static int StaffID { get; set; }
        public static string StaffName { get; set; }
        public static string Title { get; set; }
        public static string Restaurant { get; set; }
    }
}
