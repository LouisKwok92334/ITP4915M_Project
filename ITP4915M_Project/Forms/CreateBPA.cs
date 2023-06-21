using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace ITP4915M_Project.Forms
{
    public partial class CreateBPA : Form
    {
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb;Persist Security Info=False;";
        private List<string> itemNames = new List<string>();
        private List<string> addedItems = new List<string>();
        private List<string> supplierNames = new List<string>(); // Declare this at the top of the class
        private Dictionary<string, decimal> itemPrices = new Dictionary<string, decimal>();
        private string quantity;
        public CreateBPA()
        {
            InitializeComponent();
            LoadItems(); // Load the items from the database
            LoadSuppliers(); // Load the supplier names from the database
        }

        private void LoadItems()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Load items from the "item" table
                    string itemQuery = "SELECT item_name FROM item";
                    OleDbCommand itemCommand = new OleDbCommand(itemQuery, connection);
                    OleDbDataReader itemReader = itemCommand.ExecuteReader();

                    while (itemReader.Read())
                    {
                        string itemName = itemReader.GetString(0); // Assuming item_name is the first column
                        itemNames.Add(itemName);
                    }

                    UpdateListBoxes();

                    itemReader.Close();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading items: " + ex.Message);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Load supplier names from the "Supplier" table
                    string supplierQuery = "SELECT supplier_name FROM Supplier";
                    OleDbCommand supplierCommand = new OleDbCommand(supplierQuery, connection);
                    OleDbDataReader supplierReader = supplierCommand.ExecuteReader();

                    while (supplierReader.Read())
                    {
                        string supplierName = supplierReader.GetString(0); // Assuming supplier_name is the first column
                        supplierNames.Add(supplierName);
                    }

                    cmoSupplier.DataSource = null;
                    cmoSupplier.DataSource = supplierNames;

                    supplierReader.Close();

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading suppliers: " + ex.Message);
            }
        }

        private void UpdateListBoxes()
        {
            lisItem.DataSource = null;
            lisItem.DataSource = itemNames;

            lisAdd.DataSource = null;
            lisAdd.DataSource = addedItems;

            // Add the item prices to lisAdd display
            List<string> itemsWithPrices = new List<string>();
            foreach (string addedItem in addedItems)
            {
                string[] parts = addedItem.Split('-');
                if (parts.Length > 0)
                {
                    string itemName = parts[0].Trim();
                    if (itemPrices.TryGetValue(itemName, out decimal itemPrice))
                    {
                        string itemWithPrice = $"{addedItem} - Price: {itemPrice.ToString("0.00")}";
                        itemsWithPrices.Add(itemWithPrice);
                    }
                }
            }
            lisAdd.DataSource = null;
            lisAdd.DataSource = itemsWithPrices;
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
                    // Prompt the user to enter the item price
                    string itemPriceInput = Prompt.ShowDialog("Enter the price for " + selectedItem, "Enter Price");

                    // Check if item price is a valid number
                    decimal itemPrice;
                    if (decimal.TryParse(itemPriceInput, out itemPrice))
                    {
                        // Calculate the total price for the selected item and quantity
                        decimal totalPrice = itemPrice * quantityNum;

                        // Add the total price to the existing txtTotalPrice value
                        if (decimal.TryParse(txtTotalPrice.Text, out decimal currentTotalPrice))
                        {
                            decimal newTotalPrice = currentTotalPrice + totalPrice;
                            txtTotalPrice.Text = newTotalPrice.ToString("0.00");
                        }
                        else
                        {
                            txtTotalPrice.Text = totalPrice.ToString("0.00");
                        }

                        // Add the item and its price to the dictionary
                        itemPrices[selectedItem] = itemPrice;

                        // Add the item with the quantity to the list
                        addedItems.Add(selectedItem + " - Quantity: " + quantityNum);
                        itemNames.Remove(selectedItem);

                        UpdateListBoxes();
                    }
                    else
                    {
                        MessageBox.Show("Invalid item price. Please enter a valid number.");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid quantity. Please enter a valid number.");
                }
            }
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

              
                    decimal itemPrice = GetItemPrice(itemName);

            
                    decimal totalPriceToDeduct = itemPrice * quantity;

      
                    if (decimal.TryParse(txtTotalPrice.Text, out decimal currentTotalPrice))
                    {
                        decimal newTotalPrice = currentTotalPrice - totalPriceToDeduct;
                        txtTotalPrice.Text = newTotalPrice.ToString("0.00");
                    }

                    // Remove the item from the dictionary and the list
                    itemPrices.Remove(itemName);
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


       

        private void butNext_Click(object sender, EventArgs e)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO bpa (supplier_id, effectived_date, end_date, status, total_price) " +
                                            "VALUES (?, ?, ?, ?, ?)";
                    OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection);

                    int supplierId = GetSupplierId(cmoSupplier.SelectedItem.ToString());
                    insertCommand.Parameters.AddWithValue("@supplier_id", supplierId);
                    insertCommand.Parameters.AddWithValue("@effectived_date", dtpEffectivedDate.Value.ToString("MM/dd/yyyy"));
                    insertCommand.Parameters.AddWithValue("@end_date", dtpEndDate.Value.ToString("MM/dd/yyyy"));
                    insertCommand.Parameters.AddWithValue("@status", txtStatus.Text.ToString());

                    decimal totalPrice;
                    if (decimal.TryParse(txtTotalPrice.Text, out totalPrice))
                    {
                        insertCommand.Parameters.AddWithValue("@total_price", totalPrice);
                    }
                    else
                    {
                        MessageBox.Show("Invalid total price. Please enter a valid number.");
                        return;
                    }

                    insertCommand.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("BPA data uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while uploading BPA data: " + ex.Message);
            }
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO bpa_item (bpa_id, item_id, qty, price) " +
                                            "VALUES (?, ?, ?, ?)";
                    OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection);

                    // Loop through each item in the list
                    foreach (var listItem in lisAdd.Items)
                    {
                        string selectedItem = listItem.ToString();

                        string[] parts = selectedItem.Split('-');
                        if (parts.Length > 0)
                        {
                            string itemName = parts[0].Trim();

                            int itemId = GetItemId(itemName);

                            decimal itemPrice = GetItemPrice(itemName);

                            int bpaId = GetLatestBpaId();

                            insertCommand.Parameters.Clear(); // Clear parameters before adding new ones
                            insertCommand.Parameters.AddWithValue("@bpa_id", bpaId);
                            insertCommand.Parameters.AddWithValue("@item_id", itemId);
                            insertCommand.Parameters.AddWithValue("@qty", quantity);
                            insertCommand.Parameters.AddWithValue("@price", itemPrice);

                            insertCommand.ExecuteNonQuery();
                        }
                    }

                    connection.Close();

                    MessageBox.Show("BPA item data uploaded successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while uploading BPA item data: " + ex.Message);
            }

        }

        private decimal GetItemPrice(string itemName)
        {
            if (itemPrices.TryGetValue(itemName, out decimal price))
            {
                return price;
            }
            else
            {
                throw new Exception($"The item '{itemName}' does not have a recorded price.");
            }
        }

        private int GetLatestBpaId()
        {
            int latestBpaId = -1;
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MAX(bpa_id) FROM bpa";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out latestBpaId))
                    {
                        return latestBpaId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while getting the latest BPA ID: " + ex.Message);
            }
            return latestBpaId;
        }
        private int GetItemId(string itemName)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Query the "item" table to retrieve the item_id based on the item name
                    string query = "SELECT item_id FROM item WHERE item_name = @item_name";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@item_name", itemName);

                    OleDbDataReader reader = command.ExecuteReader();

                    // Using while loop to read through the results
                    while (reader.Read())
                    {
                        // Check if the first column is DBNull
                        if (reader[0] == DBNull.Value)
                        {
                            MessageBox.Show("The first column of the result set is DBNull.");
                            continue;
                        }

                        // Check if the first column can be parsed as an integer
                        if (int.TryParse(reader[0].ToString(), out int itemId))
                        {
                            return itemId;
                        }
                        else
                        {
                            MessageBox.Show($"Cannot parse '{reader[0].ToString()}' as an integer.");
                        }
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving item_id: " + ex.Message);
                return 0;
            }
        }

        private int GetSupplierId(string supplierName)
        {
            try
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Query the "Supplier" table to retrieve the supplier_id based on the supplier name
                    string query = "SELECT supplier_id FROM supplier WHERE supplier_name = ?";
                    OleDbCommand command = new OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("", supplierName);

                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int supplierId))
                    {
                        return supplierId;
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while retrieving supplier_id: " + ex.Message);
                return 0;
            }
        }
    }
}