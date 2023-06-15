using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITP4915M_Project.Forms
{
    public partial class ItemManagement : Form
    {
        public ItemManagement()
        {
            InitializeComponent();
        }

        private void ItemManagement_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet.inventory_log' 資料表。您可以視需要進行移動或移除。
            this.inventory_logTableAdapter.Fill(this.iTP4915DataSet.inventory_log);
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet.item' 資料表。您可以視需要進行移動或移除。
            this.itemTableAdapter.Fill(this.iTP4915DataSet.item);
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet.item' 資料表。您可以視需要進行移動或移除。
            this.itemTableAdapter.Fill(this.iTP4915DataSet.item);
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet.inventory_log' 資料表。您可以視需要進行移動或移除。
            this.inventory_logTableAdapter.Fill(this.iTP4915DataSet.inventory_log);

        }
    }
}
