﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITP4915M_Project;
using System.Windows.Forms;
using System.Data.OleDb;
using ITP4915M_Project.Models;

namespace ITP4915M_Project
{
    public partial class Edit : Form
    {
        private const string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=ITP4915.accdb";
        private Item _item;

        public Edit(Item item)
        {
            InitializeComponent();

            _item = item;

            textBox4.Text = _item.Id.ToString();
            txtName.Text = _item.Name;
            txtSupplier.Text = _item.Supplier;
            txtStack.Text = _item.Stack.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Retrieve the updated values from the textboxes
            string newName = txtName.Text;
            string newSupplier = txtSupplier.Text;
            int newStack = int.Parse(txtStack.Text);

            // Update the item in the database
            UpdateItem(_item.Id, newName, newSupplier, newStack);
        }

        private void UpdateItem(int itemId, string newName, string newSupplier, int newStack)
        {
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                int? supplierId = GetSupplierIdByName(newSupplier);

                if (supplierId == null)
                {
                    MessageBox.Show("Supplier not found.");
                    return;
                }

                StringBuilder queryBuilder = new StringBuilder("UPDATE item SET ");

                if (!string.IsNullOrEmpty(newName))
                {
                    queryBuilder.Append($"item_name = '{newName}', ");
                }

                queryBuilder.Append($"supplier_id = {supplierId.Value}, ");

                if (newStack != 0)
                {
                    queryBuilder.Append($"stock = {newStack}, ");
                }

                // Remove the trailing comma and space
                queryBuilder.Length -= 2;

                queryBuilder.Append($" WHERE item_id = {itemId}");

                string query = queryBuilder.ToString();

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Edit successful");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private int? GetSupplierIdByName(string supplierName)
        {
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                string query = $"SELECT supplier_id FROM supplier WHERE supplier_name = '{supplierName}'";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        OleDbDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader["supplier_id"]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            return null; // Supplier not found
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int itemId = _item.Id;// Get the itemId based on the selected item in your UI, e.g., from a DataGridView or a ListBox

    if (itemId == 0)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            DialogResult confirmResult = MessageBox.Show("Are you sure you want to delete this item?",
                                                         "Confirm Delete",
                                                         MessageBoxButtons.YesNo);

            if (confirmResult == DialogResult.Yes)
            {
                DeleteItem(itemId);
            }
        }

        private void DeleteItem(int itemId)
        {
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                string query = $"DELETE FROM item WHERE item_id = {itemId}";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        int affectedRows = cmd.ExecuteNonQuery();

                        if (affectedRows > 0)
                        {
                            MessageBox.Show("Item deleted successfully.");
                            // Refresh the UI to reflect the deletion, e.g., update the DataGridView or ListBox
                        }
                        else
                        {
                            MessageBox.Show("Item deletion failed: No rows were affected in the database.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            }
        }
    }
}
