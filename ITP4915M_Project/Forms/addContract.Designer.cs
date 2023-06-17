namespace ITP4915M_Project.Forms
{
    partial class addContract
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
            this.butNext = new System.Windows.Forms.Button();
            this.lisItem = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lisAdd = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.rdoPlanned = new System.Windows.Forms.RadioButton();
            this.rdoContract = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butNext
            // 
            this.butNext.Location = new System.Drawing.Point(1544, 912);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(244, 54);
            this.butNext.TabIndex = 3;
            this.butNext.Text = "Next";
            this.butNext.UseVisualStyleBackColor = true;
            // 
            // lisItem
            // 
            this.lisItem.FormattingEnabled = true;
            this.lisItem.ItemHeight = 24;
            this.lisItem.Location = new System.Drawing.Point(114, 203);
            this.lisItem.Name = "lisItem";
            this.lisItem.Size = new System.Drawing.Size(617, 388);
            this.lisItem.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(262, 101);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(334, 36);
            this.textBox1.TabIndex = 6;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(1544, 62);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(241, 36);
            this.txtID.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "Search Item";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1477, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 24);
            this.label5.TabIndex = 15;
            this.label5.Text = "ID";
            // 
            // lisAdd
            // 
            this.lisAdd.FormattingEnabled = true;
            this.lisAdd.ItemHeight = 24;
            this.lisAdd.Location = new System.Drawing.Point(1120, 203);
            this.lisAdd.Name = "lisAdd";
            this.lisAdd.Size = new System.Drawing.Size(665, 388);
            this.lisAdd.TabIndex = 16;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(866, 325);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(143, 54);
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(866, 424);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(143, 54);
            this.btnRemove.TabIndex = 18;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // rdoPlanned
            // 
            this.rdoPlanned.AutoSize = true;
            this.rdoPlanned.Location = new System.Drawing.Point(247, 67);
            this.rdoPlanned.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rdoPlanned.Name = "rdoPlanned";
            this.rdoPlanned.Size = new System.Drawing.Size(160, 28);
            this.rdoPlanned.TabIndex = 20;
            this.rdoPlanned.TabStop = true;
            this.rdoPlanned.Text = "Planned P.O.";
            this.rdoPlanned.UseVisualStyleBackColor = true;
            // 
            // rdoContract
            // 
            this.rdoContract.AutoSize = true;
            this.rdoContract.Location = new System.Drawing.Point(652, 67);
            this.rdoContract.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.rdoContract.Name = "rdoContract";
            this.rdoContract.Size = new System.Drawing.Size(162, 28);
            this.rdoContract.TabIndex = 19;
            this.rdoContract.TabStop = true;
            this.rdoContract.Text = "Standard P.O";
            this.rdoContract.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(78, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 24);
            this.label4.TabIndex = 23;
            this.label4.Text = "Total Price";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(247, 227);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(293, 36);
            this.txtPrice.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(78, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 24);
            this.label6.TabIndex = 24;
            this.label6.Text = "Contract Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 24);
            this.label7.TabIndex = 26;
            this.label7.Text = "Supplier";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(247, 146);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(293, 36);
            this.txtSupplier.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSupplier);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.rdoContract);
            this.groupBox1.Controls.Add(this.rdoPlanned);
            this.groupBox1.Controls.Add(this.txtPrice);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(114, 643);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.groupBox1.Size = new System.Drawing.Size(1017, 323);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contract Info";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(479, 67);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(85, 28);
            this.radioButton1.TabIndex = 27;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "BPA";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(627, 106);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(106, 35);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(1495, 643);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(293, 36);
            this.txtQty.TabIndex = 28;
            this.txtQty.TextChanged += new System.EventHandler(this.txtQty_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1373, 655);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 24);
            this.label2.TabIndex = 29;
            this.label2.Text = "Quantity";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // addContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1895, 1010);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lisAdd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.butNext);
            this.Controls.Add(this.lisItem);
            this.Name = "addContract";
            this.Text = "Add Contract";
            this.Load += new System.EventHandler(this.addContract_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button butNext;
        private System.Windows.Forms.ListBox lisItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lisAdd;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.RadioButton rdoPlanned;
        private System.Windows.Forms.RadioButton rdoContract;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label2;
    }
}