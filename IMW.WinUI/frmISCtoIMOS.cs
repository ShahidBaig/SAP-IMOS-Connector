namespace IMW.WinUI
{
    using IMW.DAL;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmISCtoIMOS : Form
    {
        private IContainer components = null;
        private Button btnTransferSalesQuotations;
        private Label lblComments;

        public frmISCtoIMOS()
        {
            this.InitializeComponent();
        }

        private void btnTransferSalesQuotations_Click(object sender, EventArgs e)
        {
            new SaleQuotationDAL().TransferSQFromISCToIMOS();
            MessageBox.Show("Congratulations Sales Qoutation Transferred to IMOS- Successfully!");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnTransferSalesQuotations = new System.Windows.Forms.Button();
            this.lblComments = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnTransferSalesQuotations
            // 
            this.btnTransferSalesQuotations.Location = new System.Drawing.Point(42, 86);
            this.btnTransferSalesQuotations.Name = "btnTransferSalesQuotations";
            this.btnTransferSalesQuotations.Size = new System.Drawing.Size(165, 37);
            this.btnTransferSalesQuotations.TabIndex = 8;
            this.btnTransferSalesQuotations.Text = "Transfer Sales Quotations";
            this.btnTransferSalesQuotations.UseVisualStyleBackColor = true;
            this.btnTransferSalesQuotations.Click += new System.EventHandler(this.btnTransferSalesQuotations_Click);
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(39, 126);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(16, 15);
            this.lblComments.TabIndex = 12;
            this.lblComments.Text = "...";
            // 
            // frmISCtoIMOS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 383);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.btnTransferSalesQuotations);
            this.Name = "frmISCtoIMOS";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Load data from ISC to IMOS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

