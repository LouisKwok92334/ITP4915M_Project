
namespace ITP4915M_Project.Forms
{
    partial class RequestManagement
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.txtRestaurant = new System.Windows.Forms.TextBox();
            this.txtStaff = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lisAdd = new System.Windows.Forms.ListBox();
            this.butSubmit = new System.Windows.Forms.Button();
            this.lisItem = new System.Windows.Forms.ListBox();
            this.btnClean = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 317);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Restaurant ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Staff Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 359);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Create Date";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(180, 356);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(123, 22);
            this.textBox2.TabIndex = 7;
            this.textBox2.TextChanged += new System.EventHandler(this.txtCreateDate_TextChanged);
            // 
            // txtRestaurant
            // 
            this.txtRestaurant.Location = new System.Drawing.Point(180, 307);
            this.txtRestaurant.Name = "txtRestaurant";
            this.txtRestaurant.ReadOnly = true;
            this.txtRestaurant.Size = new System.Drawing.Size(123, 22);
            this.txtRestaurant.TabIndex = 9;
            this.txtRestaurant.TextChanged += new System.EventHandler(this.txtRestaurant_TextChanged);
            // 
            // txtStaff
            // 
            this.txtStaff.Location = new System.Drawing.Point(180, 264);
            this.txtStaff.Name = "txtStaff";
            this.txtStaff.ReadOnly = true;
            this.txtStaff.Size = new System.Drawing.Size(123, 22);
            this.txtStaff.TabIndex = 10;
            this.txtStaff.TextChanged += new System.EventHandler(this.txtStaffName_TextChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(354, 153);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(66, 27);
            this.btnRemove.TabIndex = 35;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(354, 99);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 27);
            this.btnAdd.TabIndex = 34;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lisAdd
            // 
            this.lisAdd.FormattingEnabled = true;
            this.lisAdd.ItemHeight = 12;
            this.lisAdd.Location = new System.Drawing.Point(445, 40);
            this.lisAdd.Margin = new System.Windows.Forms.Padding(2);
            this.lisAdd.Name = "lisAdd";
            this.lisAdd.Size = new System.Drawing.Size(202, 196);
            this.lisAdd.TabIndex = 33;
            // 
            // butSubmit
            // 
            this.butSubmit.Location = new System.Drawing.Point(573, 383);
            this.butSubmit.Margin = new System.Windows.Forms.Padding(2);
            this.butSubmit.Name = "butSubmit";
            this.butSubmit.Size = new System.Drawing.Size(74, 27);
            this.butSubmit.TabIndex = 29;
            this.butSubmit.Text = "Submit";
            this.butSubmit.UseVisualStyleBackColor = true;
            this.butSubmit.Click += new System.EventHandler(this.butSubmit_Click);
            // 
            // lisItem
            // 
            this.lisItem.FormattingEnabled = true;
            this.lisItem.ItemHeight = 12;
            this.lisItem.Location = new System.Drawing.Point(107, 40);
            this.lisItem.Margin = new System.Windows.Forms.Padding(2);
            this.lisItem.Name = "lisItem";
            this.lisItem.Size = new System.Drawing.Size(202, 196);
            this.lisItem.TabIndex = 28;
            this.lisItem.SelectedIndexChanged += new System.EventHandler(this.lisItem_SelectedIndexChanged);
            // 
            // btnClean
            // 
            this.btnClean.Location = new System.Drawing.Point(479, 383);
            this.btnClean.Margin = new System.Windows.Forms.Padding(2);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(74, 27);
            this.btnClean.TabIndex = 36;
            this.btnClean.Text = "Clean";
            this.btnClean.UseVisualStyleBackColor = true;
            // 
            // RequestManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.txtStaff);
            this.Controls.Add(this.txtRestaurant);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lisAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.butSubmit);
            this.Controls.Add(this.lisItem);
            this.Name = "RequestManagement";
            this.Text = "RequestManagement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtStaff;
        private System.Windows.Forms.TextBox txtRestaurant;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lisAdd;
        private System.Windows.Forms.Button butSubmit;
        private System.Windows.Forms.ListBox lisItem;
        private System.Windows.Forms.Button btnClean;
    }
}