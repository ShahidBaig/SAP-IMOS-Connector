namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class frmItemMasterSync : Form
    {
        private List<Item> items = new List<Item>();
        private List<Item> query = new List<Item>();
        private List<Item> itemsTemp = new List<Item>();
        private List<MatFolder> mfs = new List<MatFolder>();
        private IContainer components = null;
        private DataGridView dgvItemSync;
        private TextBox txtSearchItemCode;
        private ComboBox cbxItemTables;
        private TreeView tvTargetLocationIMOS;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtSearchItemName;
        private TextBox txtSearchFrgnName;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btnLoadtoIMOS;

        public frmItemMasterSync()
        {
            this.InitializeComponent();
        }

        private void btnLoadtoIMOS_Click(object sender, EventArgs e)
        {
            ItemTargetTable table = (ItemTargetTable)Enum.Parse(typeof(ItemTargetTable), this.cbxItemTables.SelectedItem.ToString());
            if (new ItemsDAL().LoadItemToIMOSfromSAP(this.tvTargetLocationIMOS.SelectedNode.Tag.ToString(), this.itemsTemp[this.dgvItemSync.CurrentCell.RowIndex].FrgnName, this.itemsTemp[this.dgvItemSync.CurrentCell.RowIndex].ItemName, table))
            {
                MessageBox.Show(" Congratulations -" + this.itemsTemp[this.dgvItemSync.CurrentCell.RowIndex].FrgnName + " Successfully Loaded To " + this.tvTargetLocationIMOS.SelectedNode.Text);
                this.LoadItems();
                this.PopulateTree();
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

        private void frmItemMasterSync_Load(object sender, EventArgs e)
        {
            this.cbxItemTables.DataSource = Enum.GetNames(typeof(ItemTargetTable));
            this.PopulateTree();
            this.LoadItems();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvItemSync = new System.Windows.Forms.DataGridView();
            this.txtSearchItemCode = new System.Windows.Forms.TextBox();
            this.cbxItemTables = new System.Windows.Forms.ComboBox();
            this.tvTargetLocationIMOS = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchItemName = new System.Windows.Forms.TextBox();
            this.txtSearchFrgnName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLoadtoIMOS = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSync)).BeginInit();
            this.SuspendLayout();
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
            this.dgvItemSync.Location = new System.Drawing.Point(10, 63);
            this.dgvItemSync.Name = "dgvItemSync";
            this.dgvItemSync.RowHeadersWidth = 51;
            this.dgvItemSync.Size = new System.Drawing.Size(586, 187);
            this.dgvItemSync.TabIndex = 0;
            // 
            // txtSearchItemCode
            // 
            this.txtSearchItemCode.Location = new System.Drawing.Point(10, 37);
            this.txtSearchItemCode.Name = "txtSearchItemCode";
            this.txtSearchItemCode.Size = new System.Drawing.Size(140, 23);
            this.txtSearchItemCode.TabIndex = 1;
            this.txtSearchItemCode.TextChanged += txtSearchItemCode_TextChanged;
            // 
            // cbxItemTables
            // 
            this.cbxItemTables.FormattingEnabled = true;
            this.cbxItemTables.Location = new System.Drawing.Point(443, 36);
            this.cbxItemTables.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbxItemTables.Name = "cbxItemTables";
            this.cbxItemTables.Size = new System.Drawing.Size(155, 23);
            this.cbxItemTables.TabIndex = 2;
            // 
            // tvTargetLocationIMOS
            // 
            this.tvTargetLocationIMOS.Location = new System.Drawing.Point(12, 298);
            this.tvTargetLocationIMOS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tvTargetLocationIMOS.Name = "tvTargetLocationIMOS";
            this.tvTargetLocationIMOS.Size = new System.Drawing.Size(586, 310);
            this.tvTargetLocationIMOS.TabIndex = 3;
            this.tvTargetLocationIMOS.DoubleClick += tvTargetLocationIMOS_DoubleClick;
            this.tvTargetLocationIMOS.AfterSelect += tvTargetLocationIMOS_AfterSelect;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 280);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Target Location IMOS:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(439, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Target Table";
            // 
            // txtSearchItemName
            // 
            this.txtSearchItemName.Location = new System.Drawing.Point(154, 37);
            this.txtSearchItemName.Name = "txtSearchItemName";
            this.txtSearchItemName.Size = new System.Drawing.Size(140, 23);
            this.txtSearchItemName.TabIndex = 7;
            // 
            // txtSearchFrgnName
            // 
            this.txtSearchFrgnName.Location = new System.Drawing.Point(298, 37);
            this.txtSearchFrgnName.Name = "txtSearchFrgnName";
            this.txtSearchFrgnName.Size = new System.Drawing.Size(140, 23);
            this.txtSearchFrgnName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "ItemCode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "ItemName";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(294, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "FrgnName";
            // 
            // btnLoadtoIMOS
            // 
            this.btnLoadtoIMOS.Location = new System.Drawing.Point(228, 255);
            this.btnLoadtoIMOS.Name = "btnLoadtoIMOS";
            this.btnLoadtoIMOS.Size = new System.Drawing.Size(116, 34);
            this.btnLoadtoIMOS.TabIndex = 12;
            this.btnLoadtoIMOS.Text = "Load to IMOS";
            this.btnLoadtoIMOS.UseVisualStyleBackColor = true;
            this.btnLoadtoIMOS.Click += btnLoadtoIMOS_Click;
            // 
            // frmItemMasterSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 626);
            this.Controls.Add(this.btnLoadtoIMOS);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSearchFrgnName);
            this.Controls.Add(this.txtSearchItemName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvTargetLocationIMOS);
            this.Controls.Add(this.cbxItemTables);
            this.Controls.Add(this.txtSearchItemCode);
            this.Controls.Add(this.dgvItemSync);
            this.Name = "frmItemMasterSync";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Item Master Sync";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemSync)).EndInit();
            this.Load += frmItemMasterSync_Load;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadChilds(int selected)
        {
            this.tvTargetLocationIMOS.SelectedNode.Nodes.Clear();
            foreach (MatFolder folder in (from a in this.mfs
                                          where a.Parent_Id == selected
                                          select a).ToList<MatFolder>())
            {
                TreeNode node = new TreeNode
                {
                    Text = folder.Name,
                    Tag = folder.Dir_Id
                };
                this.tvTargetLocationIMOS.SelectedNode.Nodes.Add(node);
            }
        }

        private void LoadItems()
        {
            this.items = new List<Item>();
            this.query = new List<Item>();
            this.itemsTemp = new List<Item>();
            this.items = new ItemsDAL().GetItems();
            this.query = this.items.Where(i => !this.mfs.Select(m => m.Name).Contains(i.FrgnName)).ToList();
            this.dgvItemSync.DataSource = this.query;
            this.itemsTemp = this.query;
        }

        public void PopulateTree()
        {
            try
            {
                this.mfs = new ItemsDAL().GetIMOSHierarchy();
                this.tvTargetLocationIMOS.Nodes.Clear();
                TreeNode node = new TreeNode
                {
                    Text = this.mfs[0].Name,
                    Tag = this.mfs[0].Dir_Id
                };
                this.tvTargetLocationIMOS.Nodes.Add(node);
                this.tvTargetLocationIMOS.SelectedNode = node;
                foreach (MatFolder folder in (from a in this.mfs
                                              where a.Parent_Id == this.mfs[0].Dir_Id
                                              select a).ToList<MatFolder>())
                {
                    node = new TreeNode
                    {
                        Text = folder.Name,
                        Tag = folder.Dir_Id
                    };
                    this.tvTargetLocationIMOS.SelectedNode.Nodes.Add(node);
                }
                this.tvTargetLocationIMOS.CollapseAll();
                this.tvTargetLocationIMOS.SelectedNode = this.tvTargetLocationIMOS.Nodes[0];
            }
            catch (Exception exception1)
            {
                MessageBox.Show(exception1.Message, "Categories PopulateTree", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void tvTargetLocationIMOS_DoubleClick(object sender, EventArgs e)
        {
            if (tvTargetLocationIMOS.SelectedNode != null && int.TryParse(tvTargetLocationIMOS.SelectedNode.Tag.ToString(), out int selectedNodeId))
            {
                MatFolder selectedFolder = mfs.FirstOrDefault(f => f.Dir_Id == selectedNodeId);
                if (selectedFolder != null)
                {
                    LoadChilds(selectedFolder.Dir_Id);
                }
            }
        }

        private void txtSearchItemCode_TextChanged(object sender, EventArgs e)
        {
            string text = this.txtSearchItemCode.Text;
            string str2 = this.txtSearchItemName.Text;
            string str3 = this.txtSearchFrgnName.Text;
            this.itemsTemp = new List<Item>();
            this.itemsTemp = (from i in this.query
                              where (i.ItemCode.ToUpper().StartsWith(this.txtSearchItemCode.Text.ToUpper()) && i.ItemName.ToUpper().StartsWith(this.txtSearchItemName.Text.ToUpper())) && i.FrgnName.ToUpper().StartsWith(this.txtSearchFrgnName.Text.ToUpper())
                              select i).ToList<Item>();
            this.dgvItemSync.DataSource = null;
            this.dgvItemSync.DataSource = this.itemsTemp;
        }

        private void tvTargetLocationIMOS_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        [Serializable, CompilerGenerated]
        private sealed class ItemMasterSync
        {
            public static readonly ItemMasterSync Instance = new ItemMasterSync();

            public string GetName(MatFolder m) => m.Name;

            public int GetDirectoryId(MatFolder a) => a.Dir_Id;
        }
    }
}

