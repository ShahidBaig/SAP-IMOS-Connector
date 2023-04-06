namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmSalesUnitConnections : Form
    {
        private List<SalesCenters> scs = new List<SalesCenters>();
        private bool isUpdate = false;
        private SalesCenters sc = new SalesCenters();
        private IContainer components = null;
        private Label lblMachineAddress;
        private TextBox txtWDBPassword;
        private Label lblWDbName;
        private TextBox txtWDbUserName;
        private Label lblWDbPassword;
        private TextBox txtWDatabaseName;
        private Label lblWDbUserName;
        private TextBox txtWSQLServer;
        private Label lblWSQLServer;
        private TextBox txtMachineAddress;
        private Label lblFirstOrder;
        private TextBox txtFirstOrder;
        private Label lblWName;
        private TextBox txtWName;
        private DataGridView dgvSalesCenters;
        private Button btnClose;
        private Button btnConnect;
        private Button btnSave;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem editToolStripMenuItem;

        public frmSalesUnitConnections()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (new SalesCenterDAL().ConnectSalesCenters(this.sc))
            {
                MessageBox.Show("Congratulations - Connected", "IMOS Sales Center");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SalesCenters sc = new SalesCenters {
                Name = this.txtWName.Text,
                FirstOrder = this.txtFirstOrder.Text,
                MachineAddress = this.txtMachineAddress.Text,
                SQLServer = this.txtWSQLServer.Text,
                DatabaseName = this.txtWDatabaseName.Text,
                DbUserName = this.txtWDbUserName.Text,
                DBPassword = this.txtWDBPassword.Text
            };
            if (new SalesCenterDAL().SaveSalesCenter(sc, this.isUpdate))
            {
                MessageBox.Show("Congratulations - Sales Center Saved Successfully", "Save Completed");
                this.scs = new SalesCenterDAL().GetSalesCenters();
                this.dgvSalesCenters.DataSource = null;
                this.dgvSalesCenters.DataSource = this.scs;
                this.isUpdate = false;
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

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sc = this.scs[this.dgvSalesCenters.CurrentCell.RowIndex];
            this.txtFirstOrder.Text = this.sc.FirstOrder;
            this.txtWName.Text = this.sc.Name;
            this.txtFirstOrder.Text = this.sc.FirstOrder;
            this.txtMachineAddress.Text = this.sc.MachineAddress;
            this.txtWDatabaseName.Text = this.sc.DatabaseName;
            this.txtWDbUserName.Text = this.sc.DbUserName;
            this.txtWDBPassword.Text = this.sc.DBPassword;
            this.txtWSQLServer.Text = this.sc.SQLServer;
            this.isUpdate = true;
        }

        private void frmSalesUnitConnections_Load(object sender, EventArgs e)
        {
            this.scs = new SalesCenterDAL().GetSalesCenters();
            this.dgvSalesCenters.DataSource = null;
            this.dgvSalesCenters.DataSource = this.scs;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblMachineAddress = new System.Windows.Forms.Label();
            this.txtWDBPassword = new System.Windows.Forms.TextBox();
            this.lblWDbName = new System.Windows.Forms.Label();
            this.txtWDbUserName = new System.Windows.Forms.TextBox();
            this.lblWDbPassword = new System.Windows.Forms.Label();
            this.txtWDatabaseName = new System.Windows.Forms.TextBox();
            this.lblWDbUserName = new System.Windows.Forms.Label();
            this.txtWSQLServer = new System.Windows.Forms.TextBox();
            this.lblWSQLServer = new System.Windows.Forms.Label();
            this.txtMachineAddress = new System.Windows.Forms.TextBox();
            this.lblFirstOrder = new System.Windows.Forms.Label();
            this.txtFirstOrder = new System.Windows.Forms.TextBox();
            this.lblWName = new System.Windows.Forms.Label();
            this.txtWName = new System.Windows.Forms.TextBox();
            this.dgvSalesCenters = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesCenters)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMachineAddress
            // 
            this.lblMachineAddress.AutoSize = true;
            this.lblMachineAddress.Location = new System.Drawing.Point(27, 76);
            this.lblMachineAddress.Name = "lblMachineAddress";
            this.lblMachineAddress.Size = new System.Drawing.Size(98, 15);
            this.lblMachineAddress.TabIndex = 63;
            this.lblMachineAddress.Text = "Machine Address";
            // 
            // txtWDBPassword
            // 
            this.txtWDBPassword.Location = new System.Drawing.Point(134, 181);
            this.txtWDBPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtWDBPassword.Name = "txtWDBPassword";
            this.txtWDBPassword.Size = new System.Drawing.Size(228, 23);
            this.txtWDBPassword.TabIndex = 61;
            // 
            // lblWDbName
            // 
            this.lblWDbName.AutoSize = true;
            this.lblWDbName.Location = new System.Drawing.Point(27, 134);
            this.lblWDbName.Name = "lblWDbName";
            this.lblWDbName.Size = new System.Drawing.Size(90, 15);
            this.lblWDbName.TabIndex = 62;
            this.lblWDbName.Text = "Database Name";
            // 
            // txtWDbUserName
            // 
            this.txtWDbUserName.Location = new System.Drawing.Point(134, 154);
            this.txtWDbUserName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtWDbUserName.Name = "txtWDbUserName";
            this.txtWDbUserName.Size = new System.Drawing.Size(228, 23);
            this.txtWDbUserName.TabIndex = 59;
            // 
            // lblWDbPassword
            // 
            this.lblWDbPassword.AutoSize = true;
            this.lblWDbPassword.Location = new System.Drawing.Point(27, 185);
            this.lblWDbPassword.Name = "lblWDbPassword";
            this.lblWDbPassword.Size = new System.Drawing.Size(72, 15);
            this.lblWDbPassword.TabIndex = 60;
            this.lblWDbPassword.Text = "DbPassword";
            // 
            // txtWDatabaseName
            // 
            this.txtWDatabaseName.Location = new System.Drawing.Point(134, 126);
            this.txtWDatabaseName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtWDatabaseName.Name = "txtWDatabaseName";
            this.txtWDatabaseName.Size = new System.Drawing.Size(228, 23);
            this.txtWDatabaseName.TabIndex = 57;
            // 
            // lblWDbUserName
            // 
            this.lblWDbUserName.AutoSize = true;
            this.lblWDbUserName.Location = new System.Drawing.Point(27, 158);
            this.lblWDbUserName.Name = "lblWDbUserName";
            this.lblWDbUserName.Size = new System.Drawing.Size(77, 15);
            this.lblWDbUserName.TabIndex = 58;
            this.lblWDbUserName.Text = "DbUserName";
            // 
            // txtWSQLServer
            // 
            this.txtWSQLServer.Location = new System.Drawing.Point(134, 96);
            this.txtWSQLServer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtWSQLServer.Name = "txtWSQLServer";
            this.txtWSQLServer.Size = new System.Drawing.Size(228, 23);
            this.txtWSQLServer.TabIndex = 55;
            // 
            // lblWSQLServer
            // 
            this.lblWSQLServer.AutoSize = true;
            this.lblWSQLServer.Location = new System.Drawing.Point(27, 104);
            this.lblWSQLServer.Name = "lblWSQLServer";
            this.lblWSQLServer.Size = new System.Drawing.Size(63, 15);
            this.lblWSQLServer.TabIndex = 56;
            this.lblWSQLServer.Text = "SQL Server";
            // 
            // txtMachineAddress
            // 
            this.txtMachineAddress.Location = new System.Drawing.Point(134, 68);
            this.txtMachineAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMachineAddress.Name = "txtMachineAddress";
            this.txtMachineAddress.Size = new System.Drawing.Size(228, 23);
            this.txtMachineAddress.TabIndex = 53;
            // 
            // lblFirstOrder
            // 
            this.lblFirstOrder.AutoSize = true;
            this.lblFirstOrder.Location = new System.Drawing.Point(27, 46);
            this.lblFirstOrder.Name = "lblFirstOrder";
            this.lblFirstOrder.Size = new System.Drawing.Size(62, 15);
            this.lblFirstOrder.TabIndex = 54;
            this.lblFirstOrder.Text = "First Order";
            // 
            // txtFirstOrder
            // 
            this.txtFirstOrder.Location = new System.Drawing.Point(134, 40);
            this.txtFirstOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFirstOrder.Name = "txtFirstOrder";
            this.txtFirstOrder.Size = new System.Drawing.Size(228, 23);
            this.txtFirstOrder.TabIndex = 51;
            // 
            // lblWName
            // 
            this.lblWName.AutoSize = true;
            this.lblWName.Location = new System.Drawing.Point(27, 22);
            this.lblWName.Name = "lblWName";
            this.lblWName.Size = new System.Drawing.Size(39, 15);
            this.lblWName.TabIndex = 52;
            this.lblWName.Text = "Name";
            // 
            // txtWName
            // 
            this.txtWName.Location = new System.Drawing.Point(134, 13);
            this.txtWName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtWName.Name = "txtWName";
            this.txtWName.Size = new System.Drawing.Size(228, 23);
            this.txtWName.TabIndex = 50;
            // 
            // dgvSalesCenters
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSalesCenters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSalesCenters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesCenters.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSalesCenters.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSalesCenters.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvSalesCenters.Location = new System.Drawing.Point(0, 249);
            this.dgvSalesCenters.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSalesCenters.Name = "dgvSalesCenters";
            this.dgvSalesCenters.Size = new System.Drawing.Size(591, 158);
            this.dgvSalesCenters.TabIndex = 64;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(95, 26);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(466, 167);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 32);
            this.btnClose.TabIndex = 67;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(466, 130);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(77, 32);
            this.btnConnect.TabIndex = 66;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(466, 90);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 32);
            this.btnSave.TabIndex = 65;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSalesUnitConnections
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 407);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvSalesCenters);
            this.Controls.Add(this.lblMachineAddress);
            this.Controls.Add(this.txtWDBPassword);
            this.Controls.Add(this.lblWDbName);
            this.Controls.Add(this.txtWDbUserName);
            this.Controls.Add(this.lblWDbPassword);
            this.Controls.Add(this.txtWDatabaseName);
            this.Controls.Add(this.lblWDbUserName);
            this.Controls.Add(this.txtWSQLServer);
            this.Controls.Add(this.lblWSQLServer);
            this.Controls.Add(this.txtMachineAddress);
            this.Controls.Add(this.lblFirstOrder);
            this.Controls.Add(this.txtFirstOrder);
            this.Controls.Add(this.lblWName);
            this.Controls.Add(this.txtWName);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSalesUnitConnections";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IMOS Sales Center Connection";
            this.Load += new System.EventHandler(this.frmSalesUnitConnections_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesCenters)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

