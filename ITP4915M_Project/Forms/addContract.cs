﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class addContract : Form
    {
        private string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
        private List<string> itemNames = new List<string>();
        private List<string> addedItems = new List<string>();

        public addContract()
        {
            InitializeComponent();
            LoadItems(); // Load the items from the database
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

                    // Add the item name back to itemNames and remove the item with quantity from addedItems
                    itemNames.Add(itemName);
                    addedItems.Remove(selectedItem);
                    UpdateListBoxes();
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
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

    }
}
