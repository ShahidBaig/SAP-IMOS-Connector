namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using SAPbobsCOM;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using static System.Runtime.CompilerServices.RuntimeHelpers;

    public class frmArticleAssemblyParts : Form
    {
        private List<Item> items;
        private List<Item> query;
        private List<Item> itemsTemp;
        private List<MatFolder> mfs;
        private List<IMOSVariable> vlist;
        private List<IMOSVariable> vlistTemp;
        private List<MapItem> mlst;
        private List<MapItem> tlst;
        private List<MapItem> templst;
        private List<MapItem> queryMap;
        private List<IMOSItem> iis;
        private List<IMOSItem> temp;
        private List<SaleQuotationLineItem> LineItems;
        private List<IMOSItem> sq;
        private IMOSItem CurrentIMOSItem;
        private Item CurrentSAPItem;
        private IMOSItem selectedImos;
        private IContainer components;
        private Button btnMap;
        private Label label6;
        private Label label5;
        private Label label4;
        private TextBox txtSearchFrgnName;
        private TextBox txtSearchItemName;
        private Label label2;
        private TextBox txtSearchItemCode;
        private DataGridView dgvItemSync;
        private DataGridView dgvAssemblyItemsIMOS;
        private Label lblOrderId;
        private Label lblAssemblyItems;
        private Label lblVariables;
        private Label lblMappedItemsList;
        private DataGridView dgvSalesLinesSAPQuotation;
        private Button btnPostSAP;
        private Label lblVariableValues;
        private Label lblSAPSaleQuotation;
        private Label lblComments;
        private TextBox txtSearchAssemblyItems;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem removeToolStripMenuItem;
        private TextBox txtSearchMapIMOSItem;
        private TextBox txtSearchIMOSItemVariable;
        private TextBox txtSearchIMOSVariableValue;
        private TextBox txtSearchMapSAPItem;
        private Label label1;
        private Label label3;
        private Label label7;
        private Label label8;
        private TextBox txtSearchSalUnitMsr;
        private Label lblSalUnitMsr;
        private Button btnMapComplete;
        private TextBox txtSearchVariables;
        private ListBox clbxAssemblyItemsfromIMOS;
        private ListBox clbxVariables;
        private ListBox clbxVariableValues;
        private Button btnExit;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem removeToolStripMenuItem1;

        private Company oCompany = ((Company)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));

        public frmArticleAssemblyParts()
        {
            this.items = new List<Item>();
            this.query = new List<Item>();
            this.itemsTemp = new List<Item>();
            this.mfs = new List<MatFolder>();
            this.mlst = new List<MapItem>();
            this.tlst = new List<MapItem>();
            this.templst = new List<MapItem>();
            this.queryMap = new List<MapItem>();
            this.iis = new List<IMOSItem>();
            this.temp = new List<IMOSItem>();
            this.LineItems = new List<SaleQuotationLineItem>();
            this.sq = new List<IMOSItem>();
            this.CurrentIMOSItem = null;
            this.CurrentSAPItem = null;
            this.selectedImos = null;
            this.components = null;
            this.InitializeComponent();
        }

        public frmArticleAssemblyParts(string OrderId)
        {
            this.items = new List<Item>();
            this.query = new List<Item>();
            this.itemsTemp = new List<Item>();
            this.mfs = new List<MatFolder>();
            this.mlst = new List<MapItem>();
            this.tlst = new List<MapItem>();
            this.templst = new List<MapItem>();
            this.queryMap = new List<MapItem>();
            this.iis = new List<IMOSItem>();
            this.temp = new List<IMOSItem>();
            this.LineItems = new List<SaleQuotationLineItem>();
            this.sq = new List<IMOSItem>();
            this.CurrentIMOSItem = null;
            this.CurrentSAPItem = null;
            this.selectedImos = null;
            this.components = null;
            this.InitializeComponent();
            this.OrderId = OrderId;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            MapItem item = new MapItem();
            IMOSItem item2 = this.temp[this.clbxAssemblyItemsfromIMOS.SelectedIndex];
            Item item3 = this.itemsTemp[this.dgvItemSync.CurrentCell.RowIndex];
            IMOSItemVariable variable = new IMOSItemVariable();
            item.IMOSItem = item2.Name;
            item.Length = item2.Length;
            item.Width = item2.Width;
            item.Thickness = item2.Thickness;
            item.ArticleNo = item2.ArticleID;
            if (this.clbxVariables.SelectedItem != null)
            {
                item.IMOSItemVariable = ((IMOSVariable)this.clbxVariables.SelectedItem).Name;
                item.IMOSItemVariableValue = ((IMOSItemVariable)this.clbxVariableValues.SelectedItem).VariableValue;
            }
            else
            {
                item.IMOSItemVariable = string.Empty;
                item.IMOSItemVariableValue = string.Empty;
            }
            item.SAPItem = item3.ItemCode;
            if (this.mlst.Contains(item))
            {
                this.lblComments.ForeColor = Color.Red;
                this.lblComments.Text = $" {"Item already mapped"} - {item.ToString()}";
            }
            else if (new ItemsDAL().MapItem(item, this.OrderId))
            {
                this.lblComments.ForeColor = Color.Green;
                this.lblComments.Text = item.ToString() + " Map Successfully!";
                this.LoadMapItems();
            }
        }

        private void btnMapComplete_Click(object sender, EventArgs e)
        {
            IMOSItem selectedItem = (IMOSItem)this.clbxAssemblyItemsfromIMOS.SelectedItem;
        }

        private void btnMapComplete_Click_1(object sender, EventArgs e)
        {
            this.btnMap_Click(sender, e);
            this.CurrentIMOSItem = (IMOSItem)this.clbxAssemblyItemsfromIMOS.SelectedItem;
            this.CurrentSAPItem = this.itemsTemp[this.dgvItemSync.CurrentCell.RowIndex];
        }

        private void btnPostSAP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You want to Update Sales Quotation to SAP", "Update SQ SAP", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new SaleQuotationDAL().SaveSaleQuotationtoSAP(this.DocEntry, this.LineItems);
                this.lblComments.ForeColor = Color.Navy;
                this.lblComments.Text = "Congratulations! - Sales Quoatation Update to SAP";
                this.LineItems = new List<SaleQuotationLineItem>();
                this.dgvSalesLinesSAPQuotation.DataSource = null;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void clbxAssemblyItemsfromIMOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedImos = (IMOSItem)this.clbxAssemblyItemsfromIMOS.SelectedItem;
            if (!ReferenceEquals(this.selectedImos, null))
            {
                this.LoadVariablesforAssemblyItem(this.selectedImos);
            }
        }

        private void clbxVariables_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void DisplayLineItems()
        {
            this.dgvSalesLinesSAPQuotation.DataSource = null;
            this.dgvSalesLinesSAPQuotation.DataSource = this.LineItems;
            this.dgvSalesLinesSAPQuotation.Columns["DocEntry"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["TargetType"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["TrgetEntry"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["BaseRef"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["BaseType"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["BaseEntry"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["TargetEntry"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["BaseLine"].Visible = false;
            this.dgvSalesLinesSAPQuotation.Columns["LineStatus"].Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmArticleAssemblyParts_Load(object sender, EventArgs e)
        {
            this.LoadItemsSAPItems();
            this.LoadIMOSItems();
            this.lblOrderId.Text = $"Order Id : {this.OrderId}";
            this.DocEntry = new SaleQuotationDAL().GetDocEntry(this.OrderId);
            this.LoadPreMappedFG();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnMap = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchFrgnName = new System.Windows.Forms.TextBox();
            this.txtSearchItemName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearchItemCode = new System.Windows.Forms.TextBox();
            this.dgvItemSync = new System.Windows.Forms.DataGridView();
            this.dgvAssemblyItemsIMOS = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblAssemblyItems = new System.Windows.Forms.Label();
            this.lblVariables = new System.Windows.Forms.Label();
            this.lblMappedItemsList = new System.Windows.Forms.Label();
            this.dgvSalesLinesSAPQuotation = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPostSAP = new System.Windows.Forms.Button();
            this.lblVariableValues = new System.Windows.Forms.Label();
            this.lblSAPSaleQuotation = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();
            this.txtSearchAssemblyItems = new System.Windows.Forms.TextBox();
            this.txtSearchMapIMOSItem = new System.Windows.Forms.TextBox();
            this.txtSearchIMOSItemVariable = new System.Windows.Forms.TextBox();
            this.txtSearchIMOSVariableValue = new System.Windows.Forms.TextBox();
            this.txtSearchMapSAPItem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSearchSalUnitMsr = new System.Windows.Forms.TextBox();
            this.lblSalUnitMsr = new System.Windows.Forms.Label();
            this.btnMapComplete = new System.Windows.Forms.Button();
            this.txtSearchVariables = new System.Windows.Forms.TextBox();
            this.clbxAssemblyItemsfromIMOS = new System.Windows.Forms.ListBox();
            this.clbxVariables = new System.Windows.Forms.ListBox();
            this.clbxVariableValues = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSync)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssemblyItemsIMOS)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesLinesSAPQuotation)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(659, -2);
            this.btnMap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(109, 34);
            this.btnMap.TabIndex = 23;
            this.btnMap.Text = "&Map";
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Visible = false;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(846, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 22;
            this.label6.Text = "FrgnName";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(738, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 21;
            this.label5.Text = "ItemName";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(672, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "ItemCode";
            this.label4.Visible = false;
            // 
            // txtSearchFrgnName
            // 
            this.txtSearchFrgnName.Location = new System.Drawing.Point(850, 61);
            this.txtSearchFrgnName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchFrgnName.Name = "txtSearchFrgnName";
            this.txtSearchFrgnName.Size = new System.Drawing.Size(116, 23);
            this.txtSearchFrgnName.TabIndex = 19;
            this.txtSearchFrgnName.Visible = false;
            this.txtSearchFrgnName.TextChanged += new System.EventHandler(this.txtSearchFrgnName_TextChanged);
            // 
            // txtSearchItemName
            // 
            this.txtSearchItemName.Location = new System.Drawing.Point(741, 61);
            this.txtSearchItemName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchItemName.Name = "txtSearchItemName";
            this.txtSearchItemName.Size = new System.Drawing.Size(104, 23);
            this.txtSearchItemName.TabIndex = 18;
            this.txtSearchItemName.Visible = false;
            this.txtSearchItemName.TextChanged += new System.EventHandler(this.txtSearchFrgnName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(629, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Search ";
            this.label2.Visible = false;
            // 
            // txtSearchItemCode
            // 
            this.txtSearchItemCode.Location = new System.Drawing.Point(631, 60);
            this.txtSearchItemCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchItemCode.Name = "txtSearchItemCode";
            this.txtSearchItemCode.Size = new System.Drawing.Size(106, 23);
            this.txtSearchItemCode.TabIndex = 14;
            this.txtSearchItemCode.Visible = false;
            this.txtSearchItemCode.TextChanged += new System.EventHandler(this.txtSearchFrgnName_TextChanged);
            // 
            // dgvItemSync
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItemSync.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItemSync.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItemSync.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItemSync.Location = new System.Drawing.Point(626, 85);
            this.dgvItemSync.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvItemSync.Name = "dgvItemSync";
            this.dgvItemSync.RowHeadersVisible = false;
            this.dgvItemSync.Size = new System.Drawing.Size(454, 9);
            this.dgvItemSync.TabIndex = 13;
            this.dgvItemSync.Visible = false;
            // 
            // dgvAssemblyItemsIMOS
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAssemblyItemsIMOS.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAssemblyItemsIMOS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssemblyItemsIMOS.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAssemblyItemsIMOS.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAssemblyItemsIMOS.Location = new System.Drawing.Point(632, 168);
            this.dgvAssemblyItemsIMOS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvAssemblyItemsIMOS.Name = "dgvAssemblyItemsIMOS";
            this.dgvAssemblyItemsIMOS.Size = new System.Drawing.Size(552, 14);
            this.dgvAssemblyItemsIMOS.TabIndex = 24;
            this.dgvAssemblyItemsIMOS.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // lblOrderId
            // 
            this.lblOrderId.AutoSize = true;
            this.lblOrderId.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblOrderId.Location = new System.Drawing.Point(11, 2);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(97, 26);
            this.lblOrderId.TabIndex = 25;
            this.lblOrderId.Text = "OrderId :";
            // 
            // lblAssemblyItems
            // 
            this.lblAssemblyItems.AutoSize = true;
            this.lblAssemblyItems.Location = new System.Drawing.Point(10, 28);
            this.lblAssemblyItems.Name = "lblAssemblyItems";
            this.lblAssemblyItems.Size = new System.Drawing.Size(93, 15);
            this.lblAssemblyItems.TabIndex = 28;
            this.lblAssemblyItems.Text = "Assembly Items ";
            // 
            // lblVariables
            // 
            this.lblVariables.AutoSize = true;
            this.lblVariables.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblVariables.Location = new System.Drawing.Point(212, 28);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(215, 15);
            this.lblVariables.TabIndex = 29;
            this.lblVariables.Text = "FG Codes - One to One Missing Articles";
            // 
            // lblMappedItemsList
            // 
            this.lblMappedItemsList.AutoSize = true;
            this.lblMappedItemsList.Location = new System.Drawing.Point(629, 107);
            this.lblMappedItemsList.Name = "lblMappedItemsList";
            this.lblMappedItemsList.Size = new System.Drawing.Size(104, 15);
            this.lblMappedItemsList.TabIndex = 30;
            this.lblMappedItemsList.Text = "Mapped Items List";
            this.lblMappedItemsList.Visible = false;
            // 
            // dgvSalesLinesSAPQuotation
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSalesLinesSAPQuotation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSalesLinesSAPQuotation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesLinesSAPQuotation.ContextMenuStrip = this.contextMenuStrip2;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSalesLinesSAPQuotation.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSalesLinesSAPQuotation.Location = new System.Drawing.Point(8, 211);
            this.dgvSalesLinesSAPQuotation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSalesLinesSAPQuotation.Name = "dgvSalesLinesSAPQuotation";
            this.dgvSalesLinesSAPQuotation.Size = new System.Drawing.Size(1062, 220);
            this.dgvSalesLinesSAPQuotation.TabIndex = 32;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
            // 
            // btnPostSAP
            // 
            this.btnPostSAP.Location = new System.Drawing.Point(123, 435);
            this.btnPostSAP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPostSAP.Name = "btnPostSAP";
            this.btnPostSAP.Size = new System.Drawing.Size(116, 34);
            this.btnPostSAP.TabIndex = 33;
            this.btnPostSAP.Text = "&Post SAP";
            this.btnPostSAP.UseVisualStyleBackColor = true;
            this.btnPostSAP.Click += new System.EventHandler(this.btnPostSAP_Click);
            // 
            // lblVariableValues
            // 
            this.lblVariableValues.AutoSize = true;
            this.lblVariableValues.Location = new System.Drawing.Point(420, 49);
            this.lblVariableValues.Name = "lblVariableValues";
            this.lblVariableValues.Size = new System.Drawing.Size(89, 15);
            this.lblVariableValues.TabIndex = 35;
            this.lblVariableValues.Text = "Variables Values";
            this.lblVariableValues.Visible = false;
            // 
            // lblSAPSaleQuotation
            // 
            this.lblSAPSaleQuotation.AutoSize = true;
            this.lblSAPSaleQuotation.Location = new System.Drawing.Point(9, 193);
            this.lblSAPSaleQuotation.Name = "lblSAPSaleQuotation";
            this.lblSAPSaleQuotation.Size = new System.Drawing.Size(109, 15);
            this.lblSAPSaleQuotation.TabIndex = 36;
            this.lblSAPSaleQuotation.Text = "SAP Sale Quotation";
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblComments.Location = new System.Drawing.Point(420, 126);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(26, 18);
            this.lblComments.TabIndex = 40;
            this.lblComments.Text = "...";
            // 
            // txtSearchAssemblyItems
            // 
            this.txtSearchAssemblyItems.Location = new System.Drawing.Point(8, 46);
            this.txtSearchAssemblyItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchAssemblyItems.Name = "txtSearchAssemblyItems";
            this.txtSearchAssemblyItems.Size = new System.Drawing.Size(202, 23);
            this.txtSearchAssemblyItems.TabIndex = 41;
            this.txtSearchAssemblyItems.TextChanged += new System.EventHandler(this.txtSearchAssemblyItems_TextChanged);
            // 
            // txtSearchMapIMOSItem
            // 
            this.txtSearchMapIMOSItem.Location = new System.Drawing.Point(632, 143);
            this.txtSearchMapIMOSItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchMapIMOSItem.Name = "txtSearchMapIMOSItem";
            this.txtSearchMapIMOSItem.Size = new System.Drawing.Size(140, 23);
            this.txtSearchMapIMOSItem.TabIndex = 43;
            this.txtSearchMapIMOSItem.Visible = false;
            this.txtSearchMapIMOSItem.TextChanged += new System.EventHandler(this.txtSearchMapIMOSItem_TextChanged);
            // 
            // txtSearchIMOSItemVariable
            // 
            this.txtSearchIMOSItemVariable.Location = new System.Drawing.Point(774, 143);
            this.txtSearchIMOSItemVariable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchIMOSItemVariable.Name = "txtSearchIMOSItemVariable";
            this.txtSearchIMOSItemVariable.Size = new System.Drawing.Size(140, 23);
            this.txtSearchIMOSItemVariable.TabIndex = 44;
            this.txtSearchIMOSItemVariable.Visible = false;
            this.txtSearchIMOSItemVariable.TextChanged += new System.EventHandler(this.txtSearchMapIMOSItem_TextChanged);
            // 
            // txtSearchIMOSVariableValue
            // 
            this.txtSearchIMOSVariableValue.Location = new System.Drawing.Point(915, 143);
            this.txtSearchIMOSVariableValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchIMOSVariableValue.Name = "txtSearchIMOSVariableValue";
            this.txtSearchIMOSVariableValue.Size = new System.Drawing.Size(120, 23);
            this.txtSearchIMOSVariableValue.TabIndex = 45;
            this.txtSearchIMOSVariableValue.Visible = false;
            this.txtSearchIMOSVariableValue.TextChanged += new System.EventHandler(this.txtSearchMapIMOSItem_TextChanged);
            // 
            // txtSearchMapSAPItem
            // 
            this.txtSearchMapSAPItem.Location = new System.Drawing.Point(1040, 143);
            this.txtSearchMapSAPItem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchMapSAPItem.Name = "txtSearchMapSAPItem";
            this.txtSearchMapSAPItem.Size = new System.Drawing.Size(120, 23);
            this.txtSearchMapSAPItem.TabIndex = 46;
            this.txtSearchMapSAPItem.Visible = false;
            this.txtSearchMapSAPItem.TextChanged += new System.EventHandler(this.txtSearchMapIMOSItem_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(629, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "IMOS Item";
            this.label1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(771, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 48;
            this.label3.Text = "IMOS Item Variable";
            this.label3.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(919, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 15);
            this.label7.TabIndex = 49;
            this.label7.Text = "Variable Value";
            this.label7.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1038, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 50;
            this.label8.Text = "SAP Item";
            this.label8.Visible = false;
            // 
            // txtSearchSalUnitMsr
            // 
            this.txtSearchSalUnitMsr.Location = new System.Drawing.Point(970, 61);
            this.txtSearchSalUnitMsr.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchSalUnitMsr.Name = "txtSearchSalUnitMsr";
            this.txtSearchSalUnitMsr.Size = new System.Drawing.Size(111, 23);
            this.txtSearchSalUnitMsr.TabIndex = 51;
            this.txtSearchSalUnitMsr.Visible = false;
            this.txtSearchSalUnitMsr.TextChanged += new System.EventHandler(this.txtSearchFrgnName_TextChanged);
            // 
            // lblSalUnitMsr
            // 
            this.lblSalUnitMsr.AutoSize = true;
            this.lblSalUnitMsr.Location = new System.Drawing.Point(970, 44);
            this.lblSalUnitMsr.Name = "lblSalUnitMsr";
            this.lblSalUnitMsr.Size = new System.Drawing.Size(70, 15);
            this.lblSalUnitMsr.TabIndex = 52;
            this.lblSalUnitMsr.Text = "Sal Unit Msr";
            this.lblSalUnitMsr.Visible = false;
            // 
            // btnMapComplete
            // 
            this.btnMapComplete.Location = new System.Drawing.Point(774, -2);
            this.btnMapComplete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMapComplete.Name = "btnMapComplete";
            this.btnMapComplete.Size = new System.Drawing.Size(109, 34);
            this.btnMapComplete.TabIndex = 53;
            this.btnMapComplete.Text = "Map &Complete";
            this.btnMapComplete.UseVisualStyleBackColor = true;
            this.btnMapComplete.Visible = false;
            this.btnMapComplete.Click += new System.EventHandler(this.btnMapComplete_Click_1);
            // 
            // txtSearchVariables
            // 
            this.txtSearchVariables.Location = new System.Drawing.Point(214, 46);
            this.txtSearchVariables.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchVariables.Name = "txtSearchVariables";
            this.txtSearchVariables.Size = new System.Drawing.Size(200, 23);
            this.txtSearchVariables.TabIndex = 54;
            this.txtSearchVariables.Visible = false;
            this.txtSearchVariables.TextChanged += new System.EventHandler(this.txtSearchVariables_TextChanged);
            // 
            // clbxAssemblyItemsfromIMOS
            // 
            this.clbxAssemblyItemsfromIMOS.FormattingEnabled = true;
            this.clbxAssemblyItemsfromIMOS.ItemHeight = 15;
            this.clbxAssemblyItemsfromIMOS.Location = new System.Drawing.Point(8, 71);
            this.clbxAssemblyItemsfromIMOS.Name = "clbxAssemblyItemsfromIMOS";
            this.clbxAssemblyItemsfromIMOS.Size = new System.Drawing.Size(202, 109);
            this.clbxAssemblyItemsfromIMOS.TabIndex = 55;
            this.clbxAssemblyItemsfromIMOS.SelectedIndexChanged += new System.EventHandler(this.clbxAssemblyItemsfromIMOS_SelectedIndexChanged);
            // 
            // clbxVariables
            // 
            this.clbxVariables.FormattingEnabled = true;
            this.clbxVariables.ItemHeight = 15;
            this.clbxVariables.Location = new System.Drawing.Point(214, 71);
            this.clbxVariables.Name = "clbxVariables";
            this.clbxVariables.Size = new System.Drawing.Size(200, 109);
            this.clbxVariables.TabIndex = 56;
            this.clbxVariables.SelectedIndexChanged += new System.EventHandler(this.clbxVariables_SelectedIndexChanged);
            // 
            // clbxVariableValues
            // 
            this.clbxVariableValues.FormattingEnabled = true;
            this.clbxVariableValues.ItemHeight = 15;
            this.clbxVariableValues.Location = new System.Drawing.Point(419, 71);
            this.clbxVariableValues.Name = "clbxVariableValues";
            this.clbxVariableValues.Size = new System.Drawing.Size(200, 19);
            this.clbxVariableValues.TabIndex = 57;
            this.clbxVariableValues.Visible = false;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(9, 435);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 34);
            this.btnExit.TabIndex = 58;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmArticleAssemblyParts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 485);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.clbxVariableValues);
            this.Controls.Add(this.clbxVariables);
            this.Controls.Add(this.clbxAssemblyItemsfromIMOS);
            this.Controls.Add(this.txtSearchVariables);
            this.Controls.Add(this.btnMapComplete);
            this.Controls.Add(this.lblSalUnitMsr);
            this.Controls.Add(this.txtSearchSalUnitMsr);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearchMapSAPItem);
            this.Controls.Add(this.txtSearchIMOSVariableValue);
            this.Controls.Add(this.txtSearchIMOSItemVariable);
            this.Controls.Add(this.txtSearchMapIMOSItem);
            this.Controls.Add(this.txtSearchAssemblyItems);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.lblSAPSaleQuotation);
            this.Controls.Add(this.lblVariableValues);
            this.Controls.Add(this.btnPostSAP);
            this.Controls.Add(this.dgvSalesLinesSAPQuotation);
            this.Controls.Add(this.lblMappedItemsList);
            this.Controls.Add(this.lblVariables);
            this.Controls.Add(this.lblAssemblyItems);
            this.Controls.Add(this.lblOrderId);
            this.Controls.Add(this.dgvAssemblyItemsIMOS);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearchFrgnName);
            this.Controls.Add(this.txtSearchItemName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearchItemCode);
            this.Controls.Add(this.dgvItemSync);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmArticleAssemblyParts";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SAP Sales Quotation - Manage Article Assembly Parts";
            this.Load += new System.EventHandler(this.frmArticleAssemblyParts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSync)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssemblyItemsIMOS)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesLinesSAPQuotation)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void listBox1s_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void listBox1s_SelectedIndexChanged_1(object sender, EventArgs e)
        {
        }

        private void LoadIMOSItems()
        {
            this.iis = new SaleQuotationDAL().GetAssemblyItemsfromIMOS(this.OrderId);
            this.clbxAssemblyItemsfromIMOS.DataSource = this.iis;
            this.temp = this.iis;
        }

        private void LoadItemsSAPItems()
        {
            this.items = new List<Item>();
            this.query = new List<Item>();
            this.itemsTemp = new List<Item>();
            this.items = new ItemsDAL().GetItems();

            List<Item> query = items.Where(i => !mfs.Select(m => m.Name).Contains(i.FrgnName)).ToList();

            this.dgvItemSync.DataSource = this.query;
            this.itemsTemp = this.query;
            this.dgvItemSync.Columns["U_InCharges"].Visible = false;
            this.dgvItemSync.Columns["Price"].Visible = false;
            this.dgvItemSync.Columns["DfltWH"].Visible = false;
            this.dgvItemSync.Columns["VatGourpSa"].Visible = false;
        }

        private void LoadMapItems()
        {
            this.mlst = new ItemsDAL().GetMapItems();
            MapItem mi = new MapItem();
            IMOSItem imosItem = (IMOSItem)this.clbxAssemblyItemsfromIMOS.SelectedItem;
            if (!ReferenceEquals(imosItem, null))
            {
                mi.IMOSItem = imosItem.Name;
                this.tlst = (from ml in this.mlst
                             where ((ml.IMOSItem == mi.IMOSItem) && ((ml.Length == imosItem.Length) && (ml.Width == imosItem.Width))) && (ml.Thickness == imosItem.Thickness)
                             select ml).ToList<MapItem>();
                this.dgvAssemblyItemsIMOS.DataSource = null;
                this.dgvAssemblyItemsIMOS.DataSource = this.tlst;
                this.dgvAssemblyItemsIMOS.Columns["IMOSItem"].Visible = false;
                this.dgvAssemblyItemsIMOS.Columns["Length"].Visible = false;
                this.dgvAssemblyItemsIMOS.Columns["Width"].Visible = false;
                this.dgvAssemblyItemsIMOS.Columns["Thickness"].Visible = false;
                this.dgvAssemblyItemsIMOS.Columns["ArticleNo"].Visible = false;
                this.templst = this.mlst;
                this.queryMap = this.mlst;
            }
        }

        private void LoadPreMappedFG()
        {
            this.LoadSQLineItems();
        }

        private void LoadSQLineItems()
        {
            SAPItemGroups l_SAPItemGroups = new SAPItemGroups();
            DataTable l_Data = new DataTable();
			DataTable l_ISCData = new DataTable();
			DataTable l_FormulasData = new DataTable();
			SaleQuotationDAL l_SaleQuotationDAL = new SaleQuotationDAL();
            int l_ItemQty = 0;

            if(l_SaleQuotationDAL.GetSalesQuotationItemList(this.lblOrderId.Text, ref l_Data))
            {
                l_SaleQuotationDAL.ClearIMOSMappedTablesforSQ(this.lblOrderId.Text);

				if (l_SaleQuotationDAL.AddTablesDatafromIMOStoISC("SalesQuotationItemsList", l_Data))
                {
					if (l_SaleQuotationDAL.GetSalesQuotationforSAPItems(this.lblOrderId.Text, ref l_ISCData))
					{
						foreach (DataRow l_Row in l_ISCData.Rows)
						{
							SaleQuotationLineItem item = new SaleQuotationLineItem
							{
								LineNum = l_Row["Line_No"].ToString(),
								DocEntry = this.DocEntry,
								ItemCode = l_Row["ItemCode"].ToString(),
								WhsCode = l_Row["DfltWH"].ToString(),
								TaxCode = l_Row["VatGourpSa"].ToString(),
								Price = Convert.ToDouble(l_Row["Price"].ToString()),
								Dscription = l_Row["ItemName"].ToString(),
								UOM = l_Row["SalUnitMsr"].ToString(),
								Quantity = Convert.ToDouble(l_Row["Quantity"].ToString()),
							};

							this.LineItems.Add(item);
						}
					}
				}
                else
                {
                    MessageBox.Show("Unable to setup Sales Quotation Detail");
                    return;
                }
            }

			this.DisplayLineItems();
			this.clbxAssemblyItemsfromIMOS.DataSource = null;
			this.clbxAssemblyItemsfromIMOS.DataSource = this.iis;
			this.temp = this.iis;
		}

        private SAPItemGroups GetGroupsforSAPItem(string p_ItemCode)
        {
            SAPItemGroups l_SAPItemGroups = new SAPItemGroups();

			this.oCompany = new SAPDAL().ConnectSAP();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            recordset.DoQuery($"SELECT ItemCode, U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name from OITM Where ItemCode = {p_ItemCode}");

            while (!recordset.EoF)
            {
                l_SAPItemGroups.ItemCode = recordset.Fields.Item(0).Value.ToString();
				l_SAPItemGroups.U_Grp1Name = recordset.Fields.Item(1).Value.ToString();
				l_SAPItemGroups.U_Grp1Name = recordset.Fields.Item(2).Value.ToString();
				l_SAPItemGroups.U_Grp1Name = recordset.Fields.Item(3).Value.ToString();
				l_SAPItemGroups.U_Grp1Name = recordset.Fields.Item(4).Value.ToString();

				recordset.MoveNext();
            }

            return l_SAPItemGroups;

		}

		private bool LoadVariablesforAssemblyItem(IMOSItem i)
        {
            this.vlist = new SaleQuotationDAL().GetFGOne2OnefromIMOS(i);
            this.vlistTemp = this.vlist;
            return (this.vlist.Count != 0);
        }

        private void LoadVariablesValuesforAssemblyItem(IMOSItem i, string VariableName)
        {
            List<IMOSItemVariable> assemblyItemsVariablesValuesfromIMOS = new SaleQuotationDAL().GetAssemblyItemsVariablesValuesfromIMOS(i, VariableName);
            this.clbxVariableValues.DataSource = assemblyItemsVariablesValuesfromIMOS;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapItem mi = this.tlst[this.dgvAssemblyItemsIMOS.CurrentCell.RowIndex];
            if (new ItemsDAL().UnMapItem(mi))
            {
                MessageBox.Show($"Deleted Successfully - {mi.ToString()}");
                this.LoadMapItems();
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.dgvSalesLinesSAPQuotation.CurrentCell.RowIndex < 0)
            {
                MessageBox.Show("Please select the rows for deletion");
            }
            else
            {
                this.LineItems.RemoveAt(this.dgvSalesLinesSAPQuotation.CurrentCell.RowIndex);
                this.DisplayLineItems();
            }
        }

        private void txtSearchAssemblyItems_TextChanged(object sender, EventArgs e)
        {
            this.temp = (from itm in this.iis
                where itm.Name.ToUpper().StartsWith(this.txtSearchAssemblyItems.Text.ToUpper())
                select itm).ToList<IMOSItem>();
            this.clbxAssemblyItemsfromIMOS.DataSource = null;
            this.clbxAssemblyItemsfromIMOS.DataSource = this.temp;
        }

        private void txtSearchFrgnName_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtSearchItemCode.Text;
            string str2 = this.txtSearchItemName.Text;
            string str3 = this.txtSearchFrgnName.Text;
            this.itemsTemp = new List<Item>();
            this.itemsTemp = (from i in this.query
                where (i.ItemCode.ToUpper().StartsWith(this.txtSearchItemCode.Text.ToUpper()) && (i.ItemName.ToUpper().StartsWith(this.txtSearchItemName.Text.ToUpper()) && i.FrgnName.ToUpper().StartsWith(this.txtSearchFrgnName.Text.ToUpper()))) && i.SalUnitMsr.ToUpper().StartsWith(this.txtSearchSalUnitMsr.Text.ToUpper())
                select i).ToList<Item>();
            this.dgvItemSync.DataSource = null;
            this.dgvItemSync.DataSource = this.itemsTemp;
        }

        private void txtSearchMapIMOSItem_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtSearchMapIMOSItem.Text;
            string str2 = this.txtSearchIMOSItemVariable.Text;
            string str3 = this.txtSearchIMOSVariableValue.Text;
            string str4 = this.txtSearchMapSAPItem.Text;
            this.templst = new List<MapItem>();
            this.templst = (from i in this.queryMap
                where (i.IMOSItem.ToUpper().StartsWith(this.txtSearchMapIMOSItem.Text.ToUpper()) && (i.SAPItem.ToUpper().StartsWith(this.txtSearchMapSAPItem.Text.ToUpper()) && i.IMOSItemVariable.ToUpper().StartsWith(this.txtSearchIMOSItemVariable.Text.ToUpper()))) && i.IMOSItemVariableValue.ToUpper().StartsWith(this.txtSearchIMOSVariableValue.Text.ToUpper())
                select i).ToList<MapItem>();
            this.dgvAssemblyItemsIMOS.DataSource = null;
            this.dgvAssemblyItemsIMOS.DataSource = this.templst;
        }

        private void txtSearchVariables_TextChanged(object sender, EventArgs e)
        {
        }

        public string OrderId { get; set; }

        public string DocEntry { get; set; }

        //[Serializable, CompilerGenerated]
        //private sealed class <>c
        //{
        //    public static readonly frmArticleAssemblyParts.<>c <>9 = new frmArticleAssemblyParts.<>c();
        //    public static Func<MatFolder, string> <>9__17_1;

        //    internal string <LoadItemsSAPItems>b__17_1(MatFolder m) => 
        //        m.Name;
        //}
    }
}

