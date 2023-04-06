namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Drawing;
    using System.Text.Json;
	using System.Text.Json.Nodes;
	using System.Windows.Forms;

    public class frmSAPtoISC : Form
    {
        private IContainer components = null;
        private Button btnLoadItems;
        private Button btnLoadSalesQuotations;
        private TextBox txtTSI;
        private Label label1;
        private Button btnSaveTSI;
        private Label lblComments;

        public frmSAPtoISC()
        {
            this.InitializeComponent();
        }

        private void btnLoadItems_Click(object sender, EventArgs e)
        {
            new ItemsDAL().LoadItem();
            MessageBox.Show("Item Transferred Successfully from SAP to ISC ");
        }

        private void btnLoadSalesQuotations_Click(object sender, EventArgs e)
        {
            new SaleQuotationDAL().TransferSQFromoSAPToISC();
            MessageBox.Show("Sales Qoutation Transferred Successfully from SAP to ISC ");
        }

        private void btnSaveTSI_Click(object sender, EventArgs e)
        {
            this.SetSettings();
            this.GetSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmSAPtoISC_Load(object sender, EventArgs e)
        {
        }

        private void GetSettings()
		{
			var appSettings = AppConfiguration.Configuration.GetSection("AppSettings").Get<AppSettings>();
			this.txtTSI.Text = appSettings.TSI;
        }

        private void InitializeComponent()
        {
            this.btnLoadItems = new System.Windows.Forms.Button();
            this.btnLoadSalesQuotations = new System.Windows.Forms.Button();
            this.txtTSI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveTSI = new System.Windows.Forms.Button();
            this.lblComments = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLoadItems
            // 
            this.btnLoadItems.Location = new System.Drawing.Point(58, 69);
            this.btnLoadItems.Name = "btnLoadItems";
            this.btnLoadItems.Size = new System.Drawing.Size(153, 37);
            this.btnLoadItems.TabIndex = 1;
            this.btnLoadItems.Text = "Load Items";
            this.btnLoadItems.UseVisualStyleBackColor = true;
            this.btnLoadItems.Click += new System.EventHandler(this.btnLoadItems_Click);
            // 
            // btnLoadSalesQuotations
            // 
            this.btnLoadSalesQuotations.Location = new System.Drawing.Point(58, 111);
            this.btnLoadSalesQuotations.Name = "btnLoadSalesQuotations";
            this.btnLoadSalesQuotations.Size = new System.Drawing.Size(153, 37);
            this.btnLoadSalesQuotations.TabIndex = 2;
            this.btnLoadSalesQuotations.Text = "Load Sales Quotations";
            this.btnLoadSalesQuotations.UseVisualStyleBackColor = true;
            this.btnLoadSalesQuotations.Click += new System.EventHandler(this.btnLoadSalesQuotations_Click);
            // 
            // txtTSI
            // 
            this.txtTSI.Location = new System.Drawing.Point(58, 207);
            this.txtTSI.Name = "txtTSI";
            this.txtTSI.Size = new System.Drawing.Size(110, 23);
            this.txtTSI.TabIndex = 3;
            this.txtTSI.Text = "60";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Load Time (Seconds)";
            // 
            // btnSaveTSI
            // 
            this.btnSaveTSI.Location = new System.Drawing.Point(58, 233);
            this.btnSaveTSI.Name = "btnSaveTSI";
            this.btnSaveTSI.Size = new System.Drawing.Size(90, 37);
            this.btnSaveTSI.TabIndex = 5;
            this.btnSaveTSI.Text = "Save ";
            this.btnSaveTSI.UseVisualStyleBackColor = true;
            this.btnSaveTSI.Click += new System.EventHandler(this.btnSaveTSI_Click);
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(55, 273);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(16, 15);
            this.lblComments.TabIndex = 6;
            this.lblComments.Text = "...";
            // 
            // frmSAPtoISC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 383);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.btnSaveTSI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTSI);
            this.Controls.Add(this.btnLoadSalesQuotations);
            this.Controls.Add(this.btnLoadItems);
            this.Name = "frmSAPtoISC";
            this.ShowInTaskbar = false;
            this.Text = "Load data from SAP to ISC";
            this.Load += new System.EventHandler(this.frmSAPtoISC_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SetSettings()
        {
			var configJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

			var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
			var node = JsonNode.Parse(configJson, jsonNodeOptions);
			var options = new JsonSerializerOptions { WriteIndented = true };

			node["AppSettings"]["TSI"] = this.txtTSI.Text;

			File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), node.ToJsonString(options));
			this.lblComments.ForeColor = Color.Blue;
			this.lblComments.Text = "Configuration Saved Successfully!";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }
    }
}

