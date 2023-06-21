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
    public partial class MainMenu : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        //Constructor
        public MainMenu()
        {
            InitializeComponent();
            random = new Random();
            label1.Text = GlobalUser.StaffName;
            label2.Text = GlobalUser.Title;
        }

        //Method
        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                DisableButton();
                Color color = SelectThemeColor();
                currentButton = (Button)btnSender;
                currentButton.BackColor = color;
                currentButton.ForeColor = Color.White;
                currentButton.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold);
                panelTitleBar.BackColor = color;
                panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                ThemeColor.PrimaryColor = color;
                ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if(previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if(activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitle.Text = childForm.Text;
        }

        private void btnRequestManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.RequestManagement(), sender);
        }

        private void btnItemManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.ItemManagement(), sender);
        }

        private void btnContractManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.ContractManagement(), sender);
        }

        private void btnRequestProcessing_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.RequestProcessing(), sender);
        }

        private void btnPurchaseOrderProcessing_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.PurchaseOrderProcessing(), sender);
        }

        private void btnLogisticManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.LogisticManagement(), sender);
        }

        private void btnMasterDataMaintenance_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.MasterDataMaintenance(), sender);
        }

        private void btnSystemSecurityControl_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.SystemSecurityControl(), sender);
        }

        private void btnDatabaseImplementation_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.DatabaseImplementation(), sender);
        }
    }
}
