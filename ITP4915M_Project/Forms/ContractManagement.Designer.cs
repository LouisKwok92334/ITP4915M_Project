
namespace ITP4915M_Project.Forms
{
    partial class ContractManagement
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.rdoContract = new System.Windows.Forms.RadioButton();
            this.rdoPlanned = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cxActive = new System.Windows.Forms.CheckBox();
            this.cbInactive = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnViewBPA = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(404, 44);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 38;
            this.dataGridView1.Size = new System.Drawing.Size(433, 394);
            this.dataGridView1.TabIndex = 0;
            // 
            // rdoContract
            // 
            this.rdoContract.AutoSize = true;
            this.rdoContract.Location = new System.Drawing.Point(22, 84);
            this.rdoContract.Margin = new System.Windows.Forms.Padding(2);
            this.rdoContract.Name = "rdoContract";
            this.rdoContract.Size = new System.Drawing.Size(84, 16);
            this.rdoContract.TabIndex = 28;
            this.rdoContract.TabStop = true;
            this.rdoContract.Text = "Standard P.O";
            this.rdoContract.UseVisualStyleBackColor = true;
            // 
            // rdoPlanned
            // 
            this.rdoPlanned.AutoSize = true;
            this.rdoPlanned.Location = new System.Drawing.Point(22, 47);
            this.rdoPlanned.Margin = new System.Windows.Forms.Padding(2);
            this.rdoPlanned.Name = "rdoPlanned";
            this.rdoPlanned.Size = new System.Drawing.Size(83, 16);
            this.rdoPlanned.TabIndex = 29;
            this.rdoPlanned.TabStop = true;
            this.rdoPlanned.Text = "Planned P.O.";
            this.rdoPlanned.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoContract);
            this.groupBox1.Controls.Add(this.rdoPlanned);
            this.groupBox1.Location = new System.Drawing.Point(53, 44);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(331, 138);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contract Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 263);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 34;
            this.label7.Text = "Supplier";
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(121, 260);
            this.txtSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(183, 22);
            this.txtSupplier.TabIndex = 33;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(121, 352);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(183, 22);
            this.dateTimePicker1.TabIndex = 35;
            this.dateTimePicker1.Value = new System.DateTime(2019, 12, 16, 0, 0, 0, 0);
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(121, 402);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(183, 22);
            this.dateTimePicker2.TabIndex = 36;
            this.dateTimePicker2.Value = new System.DateTime(2026, 9, 25, 0, 0, 0, 0);
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 357);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 12);
            this.label2.TabIndex = 37;
            this.label2.Text = "Start";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 410);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "End";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(48, 456);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(256, 30);
            this.btnSearch.TabIndex = 40;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(768, 456);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(2);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(69, 30);
            this.btnDetail.TabIndex = 41;
            this.btnDetail.Text = "Detail";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 310);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 12);
            this.label10.TabIndex = 45;
            this.label10.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 205);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 12);
            this.label1.TabIndex = 48;
            this.label1.Text = "Contract";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(121, 203);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(183, 22);
            this.txtSearch.TabIndex = 47;
            // 
            // cxActive
            // 
            this.cxActive.AutoSize = true;
            this.cxActive.Location = new System.Drawing.Point(120, 310);
            this.cxActive.Margin = new System.Windows.Forms.Padding(2);
            this.cxActive.Name = "cxActive";
            this.cxActive.Size = new System.Drawing.Size(54, 16);
            this.cxActive.TabIndex = 49;
            this.cxActive.Text = "Active";
            this.cxActive.UseVisualStyleBackColor = true;
            // 
            // cbInactive
            // 
            this.cbInactive.AutoSize = true;
            this.cbInactive.Location = new System.Drawing.Point(203, 310);
            this.cbInactive.Margin = new System.Windows.Forms.Padding(2);
            this.cbInactive.Name = "cbInactive";
            this.cbInactive.Size = new System.Drawing.Size(61, 16);
            this.cbInactive.TabIndex = 50;
            this.cbInactive.Text = "Inactive";
            this.cbInactive.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(684, 456);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(69, 30);
            this.btnReset.TabIndex = 51;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnViewBPA
            // 
            this.btnViewBPA.Location = new System.Drawing.Point(404, 456);
            this.btnViewBPA.Margin = new System.Windows.Forms.Padding(2);
            this.btnViewBPA.Name = "btnViewBPA";
            this.btnViewBPA.Size = new System.Drawing.Size(78, 30);
            this.btnViewBPA.TabIndex = 52;
            this.btnViewBPA.Text = "View BPA";
            this.btnViewBPA.UseVisualStyleBackColor = true;
            this.btnViewBPA.Click += new System.EventHandler(this.btnViewBPA_Click);
            // 
            // ContractManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 505);
            this.Controls.Add(this.btnViewBPA);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cbInactive);
            this.Controls.Add(this.cxActive);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtSupplier);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ContractManagement";
            this.Text = "ContractManagement";
            this.Load += new System.EventHandler(this.ContractManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RadioButton rdoContract;
        private System.Windows.Forms.RadioButton rdoPlanned;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.CheckBox cxActive;
        private System.Windows.Forms.CheckBox cbInactive;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnViewBPA;
    }
}