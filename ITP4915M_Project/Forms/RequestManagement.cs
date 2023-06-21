using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class RequestManagement : Form
    {
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb;Persist Security Info=False;";
        private List<string> itemNames = new List<string>();
        private List<string> addedItems = new List<string>();
        private string quantity;

        public RequestManagement()
        {
            InitializeComponent();
            LoadRequestData();
        }

        private void LoadRequestData()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string itemQuery = "SELECT item_name FROM item";
                    OleDbCommand itemCommand = new OleDbCommand(itemQuery, connection);
                    OleDbDataReader itemReader = itemCommand.ExecuteReader();

                    while (itemReader.Read())
                    {
                        string itemName = itemReader.GetString(0);
                        itemNames.Add(itemName);
                    }

                    itemReader.Close();

                    connection.Close();
                }

                // Set the DataSource property of lisItem
                lisItem.DataSource = itemNames;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading items: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lisItem.SelectedItem != null)
            {
                string selectedItem = lisItem.SelectedItem.ToString();

                // Check if the item is already added
                if (addedItems.Contains(selectedItem))
                {
                    MessageBox.Show("This item is already added.");
                    return;
                }

                // Prompt the user to enter the quantity
                quantity = Prompt.ShowDialog("Enter the quantity for " + selectedItem, "Enter Quantity");

                // Check if quantity is a valid number
                int quantityNum;
                if (int.TryParse(quantity, out quantityNum))
                {
                    // Add the item with the quantity to the list
                    addedItems.Add(selectedItem + " - Quantity: " + quantityNum);
                    itemNames.Remove(selectedItem);

                    UpdateListBoxes();
                }
                else
                {
                    MessageBox.Show("Invalid quantity. Please enter a valid number.");
                }
            }
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 170,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Add", Left = 350, Width = 100, Top = 90, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        private void UpdateListBoxes()
        {
            lisItem.DataSource = null;
            lisItem.DataSource = itemNames;

            lisAdd.DataSource = null;
            lisAdd.DataSource = addedItems;
        }

        private void lisItem_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lisAdd.SelectedItem != null)
            {
                string selectedItem = lisAdd.SelectedItem.ToString();

                string[] parts = selectedItem.Split('-');
                if (parts.Length > 0)
                {
                    string itemName = parts[0].Trim();

                    int quantity = ExtractQuantityFromSelectedItem(selectedItem);

                    // Remove the item from the list
                    addedItems.Remove(selectedItem);
                    itemNames.Add(itemName);
                    UpdateListBoxes();
                }
            }
        }

        private int ExtractQuantityFromSelectedItem(string selectedItem)
        {
            string[] parts = selectedItem.Split('-');
            if (parts.Length > 1)
            {
                string quantityPart = parts[1].Trim(); // Get the quantity part
                string quantityString = quantityPart.Replace("Quantity:", "").Trim(); // Extract the quantity value
                if (int.TryParse(quantityString, out int quantity))
                {
                    return quantity;
                }
            }
            return 0;
        }

        private void butSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Insert a new record into the 'request' table
                    string insertRequestQuery = "INSERT INTO request (create_staff_id, processed_staff_id, status, created_at, updated_at) " +
                        "VALUES (@createStaffId, @processedStaffId, @status, @createdAt, @updatedAt)";
                    OleDbCommand insertRequestCommand = new OleDbCommand(insertRequestQuery, connection);
                    insertRequestCommand.Parameters.AddWithValue("@createStaffId", 1);
                    insertRequestCommand.Parameters.AddWithValue("@processedStaffId", 3);
                    insertRequestCommand.Parameters.AddWithValue("@status", "Pending");
                    insertRequestCommand.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("MM/dd/yyyy"));
                    insertRequestCommand.Parameters.AddWithValue("@updatedAt", DateTime.Now.ToString("MM/dd/yyyy"));

                    insertRequestCommand.ExecuteNonQuery();

                    // Retrieve the newly inserted request_id
                    string selectRequestIdQuery = "SELECT MAX(request_id) FROM request";
                    OleDbCommand selectRequestIdCommand = new OleDbCommand(selectRequestIdQuery, connection);
                    int requestId = (int)selectRequestIdCommand.ExecuteScalar();

                    // Insert records into the 'request_item' table
                    foreach (string addedItem in addedItems)
                    {
                        string[] parts = addedItem.Split('-');
                        if (parts.Length > 1)
                        {
                            string itemName = parts[0].Trim();
                            int quantity = ExtractQuantityFromSelectedItem(addedItem);
                            decimal totalPrice = CalculateTotalPrice(itemName, quantity);
                            int itemId = GetItemIdByName(itemName);

                            string insertRequestItemQuery = "INSERT INTO request_items (request_id, item_id, quantity, total_price, created_at, updated_at) " +
                                "VALUES (@requestId, @itemId, @quantity, @totalPrice, @createdAt, @updatedAt)";
                            OleDbCommand insertRequestItemCommand = new OleDbCommand(insertRequestItemQuery, connection);
                            insertRequestItemCommand.Parameters.AddWithValue("@requestId", requestId);
                            insertRequestItemCommand.Parameters.AddWithValue("@itemId", itemId);
                            insertRequestItemCommand.Parameters.AddWithValue("@quantity", quantity);
                            insertRequestItemCommand.Parameters.AddWithValue("@totalPrice", totalPrice);
                            insertRequestItemCommand.Parameters.AddWithValue("@createdAt", DateTime.Now.ToString("MM/dd/yyyy"));
                            insertRequestItemCommand.Parameters.AddWithValue("@updatedAt", DateTime.Now.ToString("MM/dd/yyyy"));

                            insertRequestItemCommand.ExecuteNonQuery();
                        }
                    }

                    connection.Close();

                    MessageBox.Show("Data uploaded successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while uploading data: " + ex.Message);
            }
        }

        private decimal CalculateTotalPrice(string itemName, int quantity)
        {
            // Replace this code with your actual calculation logic based on the item's price
            decimal itemPrice = 0; // Get the item's price from the database or elsewhere
            decimal totalPrice = itemPrice * quantity;
            return totalPrice;
        }

        private int GetItemIdByName(string itemName)
        {
            int itemId = 0; // Initialize the item ID

            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Query the item table to retrieve the item ID based on the item name
                    string query = "SELECT item_id FROM item WHERE item_name = @itemName";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@itemName", itemName);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        itemId = Convert.ToInt32(result);
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving the item's ID: " + ex.Message);
            }

            return itemId;
        }

        private void txtStaffName_TextChanged(object sender, EventArgs e)
        {
            string staffName = GlobalUser.StaffName;
            txtRestaurant.Text = staffName;
        }

        private void txtRestaurant_TextChanged(object sender, EventArgs e)
        {
            string restaurant = GlobalUser.Restaurant;
            txtRestaurant.Text = restaurant;
        }

        private void txtCreateDate_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

     
    }
}