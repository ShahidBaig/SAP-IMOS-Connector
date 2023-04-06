namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmProductionOrderCompleted : Form
    {
        private IContainer components = null;
        private ListBox lbxCompletedPO;
        private Button btnLoadSAP;
        private Button btnMarkComplete;
        private Button btnExit;
        private Label lblCompletedPO;
        private ListBox lbxSAPPO;
        private Label lblSAPPO;
        private Label lblLoadingStatus;
        private Button btnLoadSAPBOM;
        private Button btnLoadSAPPO;

        public frmProductionOrderCompleted()
        {
            this.InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnLoadSAP_Click(object sender, EventArgs e)
        {
            if (this.lbxSAPPO.SelectedItem == null)
            {
                MessageBox.Show("No Item Selected to Transfer to SAP");
            }
            else
            {
                new frmArticleAssemblyParts(((SaleQuotation) this.lbxSAPPO.SelectedItem).DocNum).ShowDialog();
            }
        }

        private void btnMarkComplete_Click(object sender, EventArgs e)
        {
            if (this.lbxCompletedPO.SelectedItems.Count == 0)
            {
                MessageBox.Show("No Item Selected to mark as complete");
            }
            else
            {
                SaleQuotation selectedItem = (SaleQuotation) this.lbxCompletedPO.SelectedItem;
                if (new SaleQuotationDAL().MarkPOCompleteInIMOS(selectedItem.DocEntry))
                {
                    string path = Application.StartupPath + @"\Sample_IMOS_Production_Order.csv";
                    new SaleQuotationDAL().LoadSampleCompleteDatatoIMOS(path, selectedItem.IMOS_PO_ID);
                    this.LoadPOUnderDesign();
                }
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

        private void frmProductionOrderCompleted_Load(object sender, EventArgs e)
        {
            this.LoadPOUnderDesign();
        }

        private void InitializeComponent()
        {
            this.lbxCompletedPO = new System.Windows.Forms.ListBox();
            this.btnLoadSAP = new System.Windows.Forms.Button();
            this.btnMarkComplete = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblCompletedPO = new System.Windows.Forms.Label();
            this.lbxSAPPO = new System.Windows.Forms.ListBox();
            this.lblSAPPO = new System.Windows.Forms.Label();
            this.lblLoadingStatus = new System.Windows.Forms.Label();
            this.btnLoadSAPBOM = new System.Windows.Forms.Button();
            this.btnLoadSAPPO = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbxCompletedPO
            // 
            this.lbxCompletedPO.FormattingEnabled = true;
            this.lbxCompletedPO.ItemHeight = 15;
            this.lbxCompletedPO.Location = new System.Drawing.Point(10, 54);
            this.lbxCompletedPO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxCompletedPO.Name = "lbxCompletedPO";
            this.lbxCompletedPO.Size = new System.Drawing.Size(252, 244);
            this.lbxCompletedPO.TabIndex = 0;
            // 
            // btnLoadSAP
            // 
            this.btnLoadSAP.Location = new System.Drawing.Point(267, 133);
            this.btnLoadSAP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadSAP.Name = "btnLoadSAP";
            this.btnLoadSAP.Size = new System.Drawing.Size(140, 32);
            this.btnLoadSAP.TabIndex = 1;
            this.btnLoadSAP.Text = "Load SAP Qoutation";
            this.btnLoadSAP.UseVisualStyleBackColor = true;
            this.btnLoadSAP.Click += new System.EventHandler(this.btnLoadSAP_Click);
            // 
            // btnMarkComplete
            // 
            this.btnMarkComplete.Location = new System.Drawing.Point(267, 95);
            this.btnMarkComplete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMarkComplete.Name = "btnMarkComplete";
            this.btnMarkComplete.Size = new System.Drawing.Size(140, 32);
            this.btnMarkComplete.TabIndex = 2;
            this.btnMarkComplete.Text = "Mark Complete";
            this.btnMarkComplete.UseVisualStyleBackColor = true;
            this.btnMarkComplete.Click += new System.EventHandler(this.btnMarkComplete_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(267, 241);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(140, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblCompletedPO
            // 
            this.lblCompletedPO.AutoSize = true;
            this.lblCompletedPO.Location = new System.Drawing.Point(10, 36);
            this.lblCompletedPO.Name = "lblCompletedPO";
            this.lblCompletedPO.Size = new System.Drawing.Size(176, 15);
            this.lblCompletedPO.TabIndex = 4;
            this.lblCompletedPO.Text = "Production Order Under Design:";
            // 
            // lbxSAPPO
            // 
            this.lbxSAPPO.FormattingEnabled = true;
            this.lbxSAPPO.ItemHeight = 15;
            this.lbxSAPPO.Location = new System.Drawing.Point(412, 54);
            this.lbxSAPPO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lbxSAPPO.Name = "lbxSAPPO";
            this.lbxSAPPO.Size = new System.Drawing.Size(252, 244);
            this.lbxSAPPO.TabIndex = 5;
            // 
            // lblSAPPO
            // 
            this.lblSAPPO.AutoSize = true;
            this.lblSAPPO.Location = new System.Drawing.Point(410, 36);
            this.lblSAPPO.Name = "lblSAPPO";
            this.lblSAPPO.Size = new System.Drawing.Size(203, 15);
            this.lblSAPPO.TabIndex = 6;
            this.lblSAPPO.Text = "Production Order Design Completed:";
            // 
            // lblLoadingStatus
            // 
            this.lblLoadingStatus.AutoSize = true;
            this.lblLoadingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLoadingStatus.ForeColor = System.Drawing.Color.Navy;
            this.lblLoadingStatus.Location = new System.Drawing.Point(410, 308);
            this.lblLoadingStatus.Name = "lblLoadingStatus";
            this.lblLoadingStatus.Size = new System.Drawing.Size(64, 13);
            this.lblLoadingStatus.TabIndex = 7;
            this.lblLoadingStatus.Text = "Loading...";
            // 
            // btnLoadSAPBOM
            // 
            this.btnLoadSAPBOM.Location = new System.Drawing.Point(267, 169);
            this.btnLoadSAPBOM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadSAPBOM.Name = "btnLoadSAPBOM";
            this.btnLoadSAPBOM.Size = new System.Drawing.Size(140, 32);
            this.btnLoadSAPBOM.TabIndex = 8;
            this.btnLoadSAPBOM.Text = "Load SAP BOM";
            this.btnLoadSAPBOM.UseVisualStyleBackColor = true;
            // 
            // btnLoadSAPPO
            // 
            this.btnLoadSAPPO.Location = new System.Drawing.Point(267, 204);
            this.btnLoadSAPPO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoadSAPPO.Name = "btnLoadSAPPO";
            this.btnLoadSAPPO.Size = new System.Drawing.Size(140, 32);
            this.btnLoadSAPPO.TabIndex = 9;
            this.btnLoadSAPPO.Text = "Load SAP PO";
            this.btnLoadSAPPO.UseVisualStyleBackColor = true;
            // 
            // frmProductionOrderCompleted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 332);
            this.Controls.Add(this.btnLoadSAPPO);
            this.Controls.Add(this.btnLoadSAPBOM);
            this.Controls.Add(this.lblLoadingStatus);
            this.Controls.Add(this.lblSAPPO);
            this.Controls.Add(this.lbxSAPPO);
            this.Controls.Add(this.lblCompletedPO);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnMarkComplete);
            this.Controls.Add(this.btnLoadSAP);
            this.Controls.Add(this.lbxCompletedPO);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmProductionOrderCompleted";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Production Order for Design in IMOS";
            this.Load += new System.EventHandler(this.frmProductionOrderCompleted_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoadPOUnderDesign()
        {
            this.lbxCompletedPO.DataSource = new SaleQuotationDAL().GetSaleQuotationFromIMOS();
            this.lbxSAPPO.DataSource = new SaleQuotationDAL().GetSaleQuotationToSAP();
        }
    }
}

