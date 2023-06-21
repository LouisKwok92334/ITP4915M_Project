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
    public partial class RequestManagement : Form
    {
        public RequestManagement()
        {
            InitializeComponent();
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string staffName = GlobalUser.StaffName;
            textBox1.Text = staffName;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string restaurant = GlobalUser.Restaurant;
            textBox1.Text = restaurant;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
