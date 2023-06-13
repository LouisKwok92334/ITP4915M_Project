using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITP4915M_Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public static class GlobalUser
        {
            public static string StaffID { get; set; }
            public static string StaffName { get; set; }
            public static string Title { get; set; }
        }

        public static class GlobalTimer
        {
            public static TimeSpan TimeElapsed { get; set; }
            public static System.Windows.Forms.Timer Timer { get; set; }
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {

        }
    }
}
