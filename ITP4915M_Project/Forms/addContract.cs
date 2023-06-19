using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class addContract : Form
    {
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb;Persist Security Info=False;";
        private List<string> itemNames = new List<string>();
        private List<string> addedItems = new List<string>();
        private List<string> supplierNames = new List<string>(); // Declare this at the top of the class
        private Dictionary<string, decimal> itemPrices = new Dictionary<string, decimal>();

        public addContract()
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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lisItem.SelectedItem != null)
            {
                string selectedItem = lisItem.SelectedItem.ToString();

                // Prompt the user to enter the quantity
                string quantity = Prompt.ShowDialog("Enter the quantity for " + selectedItem, "Enter Quantity");

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

                // Split the string to remove the quantity
                string[] parts = selectedItem.Split('-');
                if (parts.Length > 0)
                {
                    string itemName = parts[0].Trim(); // Get the item name, trim is used to remove leading or trailing spaces

                    // Extract the quantity from the selected item string
                    int quantity = ExtractQuantityFromSelectedItem(selectedItem);

                    // Calculate the item price
                    decimal itemPrice = GetItemPrice(itemName);

                    // Calculate the total price to be deducted
                    decimal totalPriceToDeduct = itemPrice * quantity;

                    // Subtract the total price to be deducted from the existing txtTotalPrice value
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

        private decimal GetItemPrice(string itemName)
        {
            if (itemPrices.TryGetValue(itemName, out decimal itemPrice))
            {
                return itemPrice;
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
            // Next button click event
        }

        private void cmoSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Supplier combo box selected index changed event
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            // Price text box text changed event
        }
    }
}