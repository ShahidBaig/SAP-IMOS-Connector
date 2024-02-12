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
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
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
            SalesCenters sc = new SalesCenters
            {
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
            components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            lblMachineAddress = new Label();
            txtWDBPassword = new TextBox();
            lblWDbName = new Label();
            txtWDbUserName = new TextBox();
            lblWDbPassword = new Label();
            txtWDatabaseName = new TextBox();
            lblWDbUserName = new Label();
            txtWSQLServer = new TextBox();
            lblWSQLServer = new Label();
            txtMachineAddress = new TextBox();
            lblFirstOrder = new Label();
            txtFirstOrder = new TextBox();
            lblWName = new Label();
            txtWName = new TextBox();
            dgvSalesCenters = new DataGridView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            editToolStripMenuItem = new ToolStripMenuItem();
            btnClose = new Button();
            btnConnect = new Button();
            btnSave = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            ((ISupportInitialize)dgvSalesCenters).BeginInit();
            contextMenuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lblMachineAddress
            // 
            lblMachineAddress.AutoSize = true;
            lblMachineAddress.Location = new Point(31, 99);
            lblMachineAddress.Name = "lblMachineAddress";
            lblMachineAddress.Size = new Size(122, 20);
            lblMachineAddress.TabIndex = 63;
            lblMachineAddress.Text = "Machine Address";
            // 
            // txtWDBPassword
            // 
            txtWDBPassword.Location = new Point(159, 239);
            txtWDBPassword.Name = "txtWDBPassword";
            txtWDBPassword.Size = new Size(260, 27);
            txtWDBPassword.TabIndex = 61;
            // 
            // lblWDbName
            // 
            lblWDbName.AutoSize = true;
            lblWDbName.Location = new Point(37, 177);
            lblWDbName.Name = "lblWDbName";
            lblWDbName.Size = new Size(116, 20);
            lblWDbName.TabIndex = 62;
            lblWDbName.Text = "Database Name";
            // 
            // txtWDbUserName
            // 
            txtWDbUserName.Location = new Point(159, 203);
            txtWDbUserName.Name = "txtWDbUserName";
            txtWDbUserName.Size = new Size(260, 27);
            txtWDbUserName.TabIndex = 59;
            // 
            // lblWDbPassword
            // 
            lblWDbPassword.AutoSize = true;
            lblWDbPassword.Location = new Point(63, 245);
            lblWDbPassword.Name = "lblWDbPassword";
            lblWDbPassword.Size = new Size(90, 20);
            lblWDbPassword.TabIndex = 60;
            lblWDbPassword.Text = "DbPassword";
            // 
            // txtWDatabaseName
            // 
            txtWDatabaseName.Location = new Point(159, 166);
            txtWDatabaseName.Name = "txtWDatabaseName";
            txtWDatabaseName.Size = new Size(260, 27);
            txtWDatabaseName.TabIndex = 57;
            // 
            // lblWDbUserName
            // 
            lblWDbUserName.AutoSize = true;
            lblWDbUserName.Location = new Point(55, 209);
            lblWDbUserName.Name = "lblWDbUserName";
            lblWDbUserName.Size = new Size(98, 20);
            lblWDbUserName.TabIndex = 58;
            lblWDbUserName.Text = "DbUserName";
            // 
            // txtWSQLServer
            // 
            txtWSQLServer.Location = new Point(159, 126);
            txtWSQLServer.Name = "txtWSQLServer";
            txtWSQLServer.Size = new Size(260, 27);
            txtWSQLServer.TabIndex = 55;
            // 
            // lblWSQLServer
            // 
            lblWSQLServer.AutoSize = true;
            lblWSQLServer.Location = new Point(73, 137);
            lblWSQLServer.Name = "lblWSQLServer";
            lblWSQLServer.Size = new Size(80, 20);
            lblWSQLServer.TabIndex = 56;
            lblWSQLServer.Text = "SQL Server";
            // 
            // txtMachineAddress
            // 
            txtMachineAddress.Location = new Point(159, 89);
            txtMachineAddress.Name = "txtMachineAddress";
            txtMachineAddress.Size = new Size(260, 27);
            txtMachineAddress.TabIndex = 53;
            // 
            // lblFirstOrder
            // 
            lblFirstOrder.AutoSize = true;
            lblFirstOrder.Location = new Point(75, 59);
            lblFirstOrder.Name = "lblFirstOrder";
            lblFirstOrder.Size = new Size(78, 20);
            lblFirstOrder.TabIndex = 54;
            lblFirstOrder.Text = "First Order";
            // 
            // txtFirstOrder
            // 
            txtFirstOrder.Location = new Point(159, 51);
            txtFirstOrder.Name = "txtFirstOrder";
            txtFirstOrder.Size = new Size(260, 27);
            txtFirstOrder.TabIndex = 51;
            // 
            // lblWName
            // 
            lblWName.AutoSize = true;
            lblWName.Location = new Point(104, 27);
            lblWName.Name = "lblWName";
            lblWName.Size = new Size(49, 20);
            lblWName.TabIndex = 52;
            lblWName.Text = "Name";
            // 
            // txtWName
            // 
            txtWName.Location = new Point(159, 15);
            txtWName.Name = "txtWName";
            txtWName.Size = new Size(260, 27);
            txtWName.TabIndex = 50;
            // 
            // dgvSalesCenters
            // 
            dgvSalesCenters.BackgroundColor = Color.White;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvSalesCenters.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvSalesCenters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSalesCenters.ContextMenuStrip = contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvSalesCenters.DefaultCellStyle = dataGridViewCellStyle2;
            dgvSalesCenters.Dock = DockStyle.Fill;
            dgvSalesCenters.Location = new Point(0, 286);
            dgvSalesCenters.Margin = new Padding(5, 5, 5, 5);
            dgvSalesCenters.Name = "dgvSalesCenters";
            dgvSalesCenters.RowHeadersWidth = 51;
            dgvSalesCenters.Size = new Size(675, 335);
            dgvSalesCenters.TabIndex = 64;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { editToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(105, 28);
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(104, 24);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(58, 166);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 43);
            btnClose.TabIndex = 67;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(58, 116);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(88, 43);
            btnConnect.TabIndex = 66;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(58, 63);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 43);
            btnSave.TabIndex = 65;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(675, 286);
            panel1.TabIndex = 68;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblMachineAddress);
            panel2.Controls.Add(txtWDBPassword);
            panel2.Controls.Add(lblWDbName);
            panel2.Controls.Add(txtWDbUserName);
            panel2.Controls.Add(lblWDbPassword);
            panel2.Controls.Add(txtWDatabaseName);
            panel2.Controls.Add(lblWDbUserName);
            panel2.Controls.Add(txtWSQLServer);
            panel2.Controls.Add(lblWSQLServer);
            panel2.Controls.Add(txtMachineAddress);
            panel2.Controls.Add(lblFirstOrder);
            panel2.Controls.Add(txtFirstOrder);
            panel2.Controls.Add(lblWName);
            panel2.Controls.Add(txtWName);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(468, 286);
            panel2.TabIndex = 68;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnClose);
            panel3.Controls.Add(btnConnect);
            panel3.Controls.Add(btnSave);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(468, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(207, 286);
            panel3.TabIndex = 69;
            // 
            // frmSalesUnitConnections
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(675, 621);
            Controls.Add(dgvSalesCenters);
            Controls.Add(panel1);
            Margin = new Padding(5, 5, 5, 5);
            Name = "frmSalesUnitConnections";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "IMOS Sales Center Connection";
            Load += frmSalesUnitConnections_Load;
            ((ISupportInitialize)dgvSalesCenters).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}

