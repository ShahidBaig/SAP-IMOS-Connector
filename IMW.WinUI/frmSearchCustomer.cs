namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSearchCustomer : Form
    {
        private List<Customer> customers;
        public Customer SelectedCustomer = new Customer();
        private IContainer components = null;
        internal Button cmdSelect;
        private Label lblComments;
        private DataGridView dataGridView1;
        internal Button btnClose;

        public frmSearchCustomer()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void cmdSelect_Click(object sender, EventArgs e)
        {
            this.SelectedCustomer = this.customers[this.dataGridView1.CurrentCell.RowIndex];
            base.DialogResult = DialogResult.OK;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSearchCustomer_Load(object sender, EventArgs e)
        {
            this.customers = new CustomerDAL().GetCustomers();
            this.dataGridView1.DataSource = this.customers;
        }

        private void InitializeComponent()
        {
            this.cmdSelect = new System.Windows.Forms.Button();
            this.lblComments = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSelect
            // 
            this.cmdSelect.Location = new System.Drawing.Point(330, 234);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(79, 24);
            this.cmdSelect.TabIndex = 59;
            this.cmdSelect.Text = "Choose";
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(37, 8);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(10, 15);
            this.lblComments.TabIndex = 60;
            this.lblComments.Text = ".";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(21, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(472, 141);
            this.dataGridView1.TabIndex = 61;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(414, 234);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 24);
            this.btnClose.TabIndex = 62;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmSearchCustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 382);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.cmdSelect);
            this.Name = "frmSearchCustomer";
            this.ShowInTaskbar = false;
            this.Text = "frmSearchCustomer";
            this.Load += new System.EventHandler(this.frmSearchCustomer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

