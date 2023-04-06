namespace IMW.WinUI
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSalesOrder : Form
    {
        private IContainer components = null;
        private TextBox txtCustomerRefNo;
        private TextBox txtContactPerson;
        private TextBox txtCustomerName;
        private TextBox txtCustomerCode;
        private Label lblCustomerCode;
        private Label lblName;
        private Label lblContactPerson;
        private Label lblCustomerRefNo;
        private ComboBox cbxLocalCurrency;
        private Label lblLocalCurrency;
        private Label lblDocumentDate;
        private ComboBox cbxNo;
        private Label lblDeliveryDate;
        private Label lblPostingDate;
        private Label lblStatus;
        private Label lblNo;
        private TextBox txtNo;
        private TextBox txtStatus;
        private DateTimePicker dtpDocumentDate;
        private DateTimePicker dtpDeliveryDate;
        private DateTimePicker dtpPostingDate;
        private DataGridView dgvSalesLines;
        private DataGridViewTextBoxColumn dgcSr;
        private DataGridViewTextBoxColumn dgcItemNo;
        private DataGridViewTextBoxColumn dgcQuantity;
        private DataGridViewTextBoxColumn dgcUnitPrice;
        private DataGridViewTextBoxColumn dgcDisc;
        private DataGridViewTextBoxColumn dgcTaxCode;
        private DataGridViewTextBoxColumn dgcLineTotal;
        private Label lblSalesEmployee;
        private ComboBox cbxSalesEmployee;
        private Label lblOwner;
        private TextBox txtOwner;
        private Label lblRemarks;
        private TextBox txtRemarks;
        private Label lblTotal;
        private ComboBox cbxTotal;
        private Label lblTax;
        private Label lblDiscount;
        private Label lblTotalBeforeDiscount;
        private TextBox txtTotalBeforeDiscount;
        private TextBox txtDiscount;
        private TextBox txtRounding;
        private TextBox txtTax;
        private TextBox txtDiscountPercentage;
        private Label lblPercentage;
        private CheckBox chxRounding;
        private Button btnClose;
        private Button btnAdd;
        private Button btnLoadCustomer;

        public frmSalesOrder()
        {
            this.InitializeComponent();
        }

        private void btnLoadCustomer_Click(object sender, EventArgs e)
        {
            frmSearchCustomer customer = new frmSearchCustomer();
            customer.ShowDialog();
            if (customer.DialogResult == DialogResult.OK)
            {
                this.txtCustomerCode.Text = customer.SelectedCustomer.CustomerCode;
                this.txtCustomerName.Text = customer.SelectedCustomer.CustomerName;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSalesOrder_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            this.txtCustomerRefNo = new System.Windows.Forms.TextBox();
            this.txtContactPerson = new System.Windows.Forms.TextBox();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.txtCustomerCode = new System.Windows.Forms.TextBox();
            this.lblCustomerCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblContactPerson = new System.Windows.Forms.Label();
            this.lblCustomerRefNo = new System.Windows.Forms.Label();
            this.cbxLocalCurrency = new System.Windows.Forms.ComboBox();
            this.lblLocalCurrency = new System.Windows.Forms.Label();
            this.lblDocumentDate = new System.Windows.Forms.Label();
            this.cbxNo = new System.Windows.Forms.ComboBox();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.lblPostingDate = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.dtpDocumentDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.dtpPostingDate = new System.Windows.Forms.DateTimePicker();
            this.dgvSalesLines = new System.Windows.Forms.DataGridView();
            this.dgcSr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcItemNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcUnitPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcDisc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcTaxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgcLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblSalesEmployee = new System.Windows.Forms.Label();
            this.cbxSalesEmployee = new System.Windows.Forms.ComboBox();
            this.lblOwner = new System.Windows.Forms.Label();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.cbxTotal = new System.Windows.Forms.ComboBox();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotalBeforeDiscount = new System.Windows.Forms.Label();
            this.txtTotalBeforeDiscount = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtRounding = new System.Windows.Forms.TextBox();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.txtDiscountPercentage = new System.Windows.Forms.TextBox();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.chxRounding = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnLoadCustomer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesLines)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCustomerRefNo
            // 
            this.txtCustomerRefNo.Location = new System.Drawing.Point(133, 102);
            this.txtCustomerRefNo.Name = "txtCustomerRefNo";
            this.txtCustomerRefNo.Size = new System.Drawing.Size(190, 23);
            this.txtCustomerRefNo.TabIndex = 0;
            // 
            // txtContactPerson
            // 
            this.txtContactPerson.Location = new System.Drawing.Point(133, 76);
            this.txtContactPerson.Name = "txtContactPerson";
            this.txtContactPerson.Size = new System.Drawing.Size(190, 23);
            this.txtContactPerson.TabIndex = 1;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(133, 50);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(190, 23);
            this.txtCustomerName.TabIndex = 2;
            // 
            // txtCustomerCode
            // 
            this.txtCustomerCode.Location = new System.Drawing.Point(133, 23);
            this.txtCustomerCode.Name = "txtCustomerCode";
            this.txtCustomerCode.Size = new System.Drawing.Size(190, 23);
            this.txtCustomerCode.TabIndex = 3;
            // 
            // lblCustomerCode
            // 
            this.lblCustomerCode.AutoSize = true;
            this.lblCustomerCode.Location = new System.Drawing.Point(10, 23);
            this.lblCustomerCode.Name = "lblCustomerCode";
            this.lblCustomerCode.Size = new System.Drawing.Size(59, 15);
            this.lblCustomerCode.TabIndex = 4;
            this.lblCustomerCode.Text = "Customer";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(10, 54);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 5;
            this.lblName.Text = "Name";
            // 
            // lblContactPerson
            // 
            this.lblContactPerson.AutoSize = true;
            this.lblContactPerson.Location = new System.Drawing.Point(10, 81);
            this.lblContactPerson.Name = "lblContactPerson";
            this.lblContactPerson.Size = new System.Drawing.Size(88, 15);
            this.lblContactPerson.TabIndex = 6;
            this.lblContactPerson.Text = "Contact Person";
            // 
            // lblCustomerRefNo
            // 
            this.lblCustomerRefNo.AutoSize = true;
            this.lblCustomerRefNo.Location = new System.Drawing.Point(10, 107);
            this.lblCustomerRefNo.Name = "lblCustomerRefNo";
            this.lblCustomerRefNo.Size = new System.Drawing.Size(107, 15);
            this.lblCustomerRefNo.TabIndex = 7;
            this.lblCustomerRefNo.Text = "Customer. Ref. No.";
            // 
            // cbxLocalCurrency
            // 
            this.cbxLocalCurrency.FormattingEnabled = true;
            this.cbxLocalCurrency.Location = new System.Drawing.Point(133, 128);
            this.cbxLocalCurrency.Name = "cbxLocalCurrency";
            this.cbxLocalCurrency.Size = new System.Drawing.Size(190, 23);
            this.cbxLocalCurrency.TabIndex = 8;
            // 
            // lblLocalCurrency
            // 
            this.lblLocalCurrency.AutoSize = true;
            this.lblLocalCurrency.Location = new System.Drawing.Point(10, 135);
            this.lblLocalCurrency.Name = "lblLocalCurrency";
            this.lblLocalCurrency.Size = new System.Drawing.Size(86, 15);
            this.lblLocalCurrency.TabIndex = 9;
            this.lblLocalCurrency.Text = "Local Currency";
            // 
            // lblDocumentDate
            // 
            this.lblDocumentDate.AutoSize = true;
            this.lblDocumentDate.Location = new System.Drawing.Point(348, 135);
            this.lblDocumentDate.Name = "lblDocumentDate";
            this.lblDocumentDate.Size = new System.Drawing.Size(90, 15);
            this.lblDocumentDate.TabIndex = 19;
            this.lblDocumentDate.Text = "Document Date";
            // 
            // cbxNo
            // 
            this.cbxNo.FormattingEnabled = true;
            this.cbxNo.Location = new System.Drawing.Point(393, 21);
            this.cbxNo.Name = "cbxNo";
            this.cbxNo.Size = new System.Drawing.Size(68, 23);
            this.cbxNo.TabIndex = 18;
            // 
            // lblDeliveryDate
            // 
            this.lblDeliveryDate.AutoSize = true;
            this.lblDeliveryDate.Location = new System.Drawing.Point(348, 107);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(76, 15);
            this.lblDeliveryDate.TabIndex = 17;
            this.lblDeliveryDate.Text = "Delivery Date";
            // 
            // lblPostingDate
            // 
            this.lblPostingDate.AutoSize = true;
            this.lblPostingDate.Location = new System.Drawing.Point(348, 81);
            this.lblPostingDate.Name = "lblPostingDate";
            this.lblPostingDate.Size = new System.Drawing.Size(74, 15);
            this.lblPostingDate.TabIndex = 16;
            this.lblPostingDate.Text = "Posting Date";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(348, 54);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 15);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "Status";
            // 
            // lblNo
            // 
            this.lblNo.AutoSize = true;
            this.lblNo.Location = new System.Drawing.Point(348, 23);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(26, 15);
            this.lblNo.TabIndex = 14;
            this.lblNo.Text = "No.";
            // 
            // txtNo
            // 
            this.txtNo.Location = new System.Drawing.Point(472, 23);
            this.txtNo.Name = "txtNo";
            this.txtNo.Size = new System.Drawing.Size(154, 23);
            this.txtNo.TabIndex = 13;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(472, 50);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(154, 23);
            this.txtStatus.TabIndex = 12;
            // 
            // dtpDocumentDate
            // 
            this.dtpDocumentDate.CustomFormat = "";
            this.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDocumentDate.Location = new System.Drawing.Point(472, 130);
            this.dtpDocumentDate.Name = "dtpDocumentDate";
            this.dtpDocumentDate.Size = new System.Drawing.Size(138, 23);
            this.dtpDocumentDate.TabIndex = 20;
            // 
            // dtpDeliveryDate
            // 
            this.dtpDeliveryDate.CustomFormat = "";
            this.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDeliveryDate.Location = new System.Drawing.Point(472, 100);
            this.dtpDeliveryDate.Name = "dtpDeliveryDate";
            this.dtpDeliveryDate.Size = new System.Drawing.Size(138, 23);
            this.dtpDeliveryDate.TabIndex = 21;
            // 
            // dtpPostingDate
            // 
            this.dtpPostingDate.CustomFormat = "";
            this.dtpPostingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPostingDate.Location = new System.Drawing.Point(472, 74);
            this.dtpPostingDate.Name = "dtpPostingDate";
            this.dtpPostingDate.Size = new System.Drawing.Size(138, 23);
            this.dtpPostingDate.TabIndex = 22;
            // 
            // dgvSalesLines
            // 
            this.dgvSalesLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcSr,
            this.dgcItemNo,
            this.dgcQuantity,
            this.dgcUnitPrice,
            this.dgcDisc,
            this.dgcTaxCode,
            this.dgcLineTotal});
            this.dgvSalesLines.Location = new System.Drawing.Point(10, 157);
            this.dgvSalesLines.Name = "dgvSalesLines";
            this.dgvSalesLines.RowHeadersVisible = false;
            this.dgvSalesLines.RowTemplate.Height = 24;
            this.dgvSalesLines.Size = new System.Drawing.Size(648, 176);
            this.dgvSalesLines.TabIndex = 23;
            // 
            // dgcSr
            // 
            this.dgcSr.HeaderText = "#";
            this.dgcSr.Name = "dgcSr";
            // 
            // dgcItemNo
            // 
            this.dgcItemNo.HeaderText = "Item No";
            this.dgcItemNo.Name = "dgcItemNo";
            // 
            // dgcQuantity
            // 
            this.dgcQuantity.HeaderText = "Quantity";
            this.dgcQuantity.Name = "dgcQuantity";
            // 
            // dgcUnitPrice
            // 
            this.dgcUnitPrice.HeaderText = "Unit Price";
            this.dgcUnitPrice.Name = "dgcUnitPrice";
            // 
            // dgcDisc
            // 
            this.dgcDisc.HeaderText = "Disc";
            this.dgcDisc.Name = "dgcDisc";
            // 
            // dgcTaxCode
            // 
            this.dgcTaxCode.HeaderText = "Tax Code";
            this.dgcTaxCode.Name = "dgcTaxCode";
            // 
            // dgcLineTotal
            // 
            this.dgcLineTotal.HeaderText = "Total (LC)";
            this.dgcLineTotal.Name = "dgcLineTotal";
            // 
            // lblSalesEmployee
            // 
            this.lblSalesEmployee.AutoSize = true;
            this.lblSalesEmployee.Location = new System.Drawing.Point(10, 345);
            this.lblSalesEmployee.Name = "lblSalesEmployee";
            this.lblSalesEmployee.Size = new System.Drawing.Size(88, 15);
            this.lblSalesEmployee.TabIndex = 25;
            this.lblSalesEmployee.Text = "Sales Employee";
            // 
            // cbxSalesEmployee
            // 
            this.cbxSalesEmployee.FormattingEnabled = true;
            this.cbxSalesEmployee.Location = new System.Drawing.Point(110, 338);
            this.cbxSalesEmployee.Name = "cbxSalesEmployee";
            this.cbxSalesEmployee.Size = new System.Drawing.Size(190, 23);
            this.cbxSalesEmployee.TabIndex = 24;
            // 
            // lblOwner
            // 
            this.lblOwner.AutoSize = true;
            this.lblOwner.Location = new System.Drawing.Point(10, 367);
            this.lblOwner.Name = "lblOwner";
            this.lblOwner.Size = new System.Drawing.Size(42, 15);
            this.lblOwner.TabIndex = 27;
            this.lblOwner.Text = "Owner";
            // 
            // txtOwner
            // 
            this.txtOwner.Location = new System.Drawing.Point(110, 367);
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(190, 23);
            this.txtOwner.TabIndex = 26;
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(10, 393);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(52, 15);
            this.lblRemarks.TabIndex = 29;
            this.lblRemarks.Text = "Remarks";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(110, 393);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(190, 57);
            this.txtRemarks.TabIndex = 28;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(310, 450);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(32, 15);
            this.lblTotal.TabIndex = 39;
            this.lblTotal.Text = "Total";
            // 
            // cbxTotal
            // 
            this.cbxTotal.FormattingEnabled = true;
            this.cbxTotal.Location = new System.Drawing.Point(442, 443);
            this.cbxTotal.Name = "cbxTotal";
            this.cbxTotal.Size = new System.Drawing.Size(190, 23);
            this.cbxTotal.TabIndex = 38;
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Location = new System.Drawing.Point(310, 422);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(24, 15);
            this.lblTax.TabIndex = 37;
            this.lblTax.Text = "Tax";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(310, 369);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(54, 15);
            this.lblDiscount.TabIndex = 35;
            this.lblDiscount.Text = "Discount";
            // 
            // lblTotalBeforeDiscount
            // 
            this.lblTotalBeforeDiscount.AutoSize = true;
            this.lblTotalBeforeDiscount.Location = new System.Drawing.Point(310, 338);
            this.lblTotalBeforeDiscount.Name = "lblTotalBeforeDiscount";
            this.lblTotalBeforeDiscount.Size = new System.Drawing.Size(119, 15);
            this.lblTotalBeforeDiscount.TabIndex = 34;
            this.lblTotalBeforeDiscount.Text = "Total Before Discount";
            // 
            // txtTotalBeforeDiscount
            // 
            this.txtTotalBeforeDiscount.Location = new System.Drawing.Point(442, 338);
            this.txtTotalBeforeDiscount.Name = "txtTotalBeforeDiscount";
            this.txtTotalBeforeDiscount.Size = new System.Drawing.Size(190, 23);
            this.txtTotalBeforeDiscount.TabIndex = 33;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(442, 365);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(190, 23);
            this.txtDiscount.TabIndex = 32;
            // 
            // txtRounding
            // 
            this.txtRounding.Location = new System.Drawing.Point(442, 391);
            this.txtRounding.Name = "txtRounding";
            this.txtRounding.Size = new System.Drawing.Size(190, 23);
            this.txtRounding.TabIndex = 31;
            // 
            // txtTax
            // 
            this.txtTax.Location = new System.Drawing.Point(442, 417);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(190, 23);
            this.txtTax.TabIndex = 30;
            // 
            // txtDiscountPercentage
            // 
            this.txtDiscountPercentage.Location = new System.Drawing.Point(370, 367);
            this.txtDiscountPercentage.Name = "txtDiscountPercentage";
            this.txtDiscountPercentage.Size = new System.Drawing.Size(50, 23);
            this.txtDiscountPercentage.TabIndex = 40;
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Location = new System.Drawing.Point(424, 369);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(17, 15);
            this.lblPercentage.TabIndex = 41;
            this.lblPercentage.Text = "%";
            // 
            // chxRounding
            // 
            this.chxRounding.AutoSize = true;
            this.chxRounding.Location = new System.Drawing.Point(312, 393);
            this.chxRounding.Name = "chxRounding";
            this.chxRounding.Size = new System.Drawing.Size(78, 19);
            this.chxRounding.TabIndex = 42;
            this.chxRounding.Text = "Rounding";
            this.chxRounding.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(133, 475);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 35);
            this.btnClose.TabIndex = 43;
            this.btnClose.Text = "Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(39, 475);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(77, 35);
            this.btnAdd.TabIndex = 44;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnLoadCustomer
            // 
            this.btnLoadCustomer.Location = new System.Drawing.Point(318, 22);
            this.btnLoadCustomer.Name = "btnLoadCustomer";
            this.btnLoadCustomer.Size = new System.Drawing.Size(27, 22);
            this.btnLoadCustomer.TabIndex = 45;
            this.btnLoadCustomer.Text = "...";
            this.btnLoadCustomer.UseVisualStyleBackColor = true;
            this.btnLoadCustomer.Click += new System.EventHandler(this.btnLoadCustomer_Click);
            // 
            // frmSalesOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 568);
            this.Controls.Add(this.btnLoadCustomer);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.chxRounding);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.txtDiscountPercentage);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.cbxTotal);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblTotalBeforeDiscount);
            this.Controls.Add(this.txtTotalBeforeDiscount);
            this.Controls.Add(this.txtDiscount);
            this.Controls.Add(this.txtRounding);
            this.Controls.Add(this.txtTax);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.txtRemarks);
            this.Controls.Add(this.lblOwner);
            this.Controls.Add(this.txtOwner);
            this.Controls.Add(this.lblSalesEmployee);
            this.Controls.Add(this.cbxSalesEmployee);
            this.Controls.Add(this.dgvSalesLines);
            this.Controls.Add(this.dtpPostingDate);
            this.Controls.Add(this.dtpDeliveryDate);
            this.Controls.Add(this.dtpDocumentDate);
            this.Controls.Add(this.lblDocumentDate);
            this.Controls.Add(this.cbxNo);
            this.Controls.Add(this.lblDeliveryDate);
            this.Controls.Add(this.lblPostingDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblNo);
            this.Controls.Add(this.txtNo);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.lblLocalCurrency);
            this.Controls.Add(this.cbxLocalCurrency);
            this.Controls.Add(this.lblCustomerRefNo);
            this.Controls.Add(this.lblContactPerson);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblCustomerCode);
            this.Controls.Add(this.txtCustomerCode);
            this.Controls.Add(this.txtCustomerName);
            this.Controls.Add(this.txtContactPerson);
            this.Controls.Add(this.txtCustomerRefNo);
            this.Name = "frmSalesOrder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Order";
            this.Load += new System.EventHandler(this.frmSalesOrder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

