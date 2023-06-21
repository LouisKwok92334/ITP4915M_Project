
namespace ITP4915M_Project.Forms
{
    partial class RequestProcessing
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.iTP4915DataSet = new ITP4915M_Project.ITP4915DataSet();
            this.purchaseorderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.purchase_orderTableAdapter = new ITP4915M_Project.ITP4915DataSetTableAdapters.purchase_orderTableAdapter();
            this.iTP4915DataSet1 = new ITP4915M_Project.ITP4915DataSet1();
            this.purchaseorderBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.purchase_orderTableAdapter1 = new ITP4915M_Project.ITP4915DataSet1TableAdapters.purchase_orderTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iTP4915DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseorderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iTP4915DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseorderBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(451, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(304, 397);
            this.dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(91, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(178, 22);
            this.textBox1.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(194, 142);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "SearchID";
            // 
            // cbStatus
            // 
            this.cbStatus.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.purchaseorderBindingSource, "status", true));
            this.cbStatus.DataSource = this.purchaseorderBindingSource1;
            this.cbStatus.DisplayMember = "status";
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(91, 96);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(178, 20);
            this.cbStatus.TabIndex = 4;
            this.cbStatus.ValueMember = "status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Status";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.cbStatus);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(56, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 188);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Contract Detail";
            // 
            // iTP4915DataSet
            // 
            this.iTP4915DataSet.DataSetName = "ITP4915DataSet";
            this.iTP4915DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // purchaseorderBindingSource
            // 
            this.purchaseorderBindingSource.DataMember = "purchase_order";
            this.purchaseorderBindingSource.DataSource = this.iTP4915DataSet;
            // 
            // purchase_orderTableAdapter
            // 
            this.purchase_orderTableAdapter.ClearBeforeFill = true;
            // 
            // iTP4915DataSet1
            // 
            this.iTP4915DataSet1.DataSetName = "ITP4915DataSet1";
            this.iTP4915DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // purchaseorderBindingSource1
            // 
            this.purchaseorderBindingSource1.DataMember = "purchase_order";
            this.purchaseorderBindingSource1.DataSource = this.iTP4915DataSet1;
            // 
            // purchase_orderTableAdapter1
            // 
            this.purchase_orderTableAdapter1.ClearBeforeFill = true;
            // 
            // RequestProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "RequestProcessing";
            this.Text = "RequestProcessing";
            this.Load += new System.EventHandler(this.RequestProcessing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iTP4915DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseorderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iTP4915DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.purchaseorderBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private ITP4915DataSet iTP4915DataSet;
        private System.Windows.Forms.BindingSource purchaseorderBindingSource;
        private ITP4915DataSetTableAdapters.purchase_orderTableAdapter purchase_orderTableAdapter;
        private ITP4915DataSet1 iTP4915DataSet1;
        private System.Windows.Forms.BindingSource purchaseorderBindingSource1;
        private ITP4915DataSet1TableAdapters.purchase_orderTableAdapter purchase_orderTableAdapter1;
    }
}