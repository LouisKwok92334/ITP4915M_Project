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
    public partial class ContractManagement : Form
    {
        public ContractManagement()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ContractManagement_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet.agreement_detail' 資料表。您可以視需要進行移動或移除。
            this.agreement_detailTableAdapter.Fill(this.iTP4915DataSet.agreement_detail);
            // TODO: 這行程式碼會將資料載入 'iTP4915DataSet.agreement' 資料表。您可以視需要進行移動或移除。
            this.agreementTableAdapter.Fill(this.iTP4915DataSet.agreement);

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
