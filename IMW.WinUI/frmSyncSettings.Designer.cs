namespace IMW.WinUI
{
    partial class frmSyncSettings
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
            this.chkSyncItems = new System.Windows.Forms.CheckBox();
            this.chkSyncCustomers = new System.Windows.Forms.CheckBox();
            this.chkTransferSQFromSAPToISC = new System.Windows.Forms.CheckBox();
            this.chkTransferSQFromISCToIMOS = new System.Windows.Forms.CheckBox();
            this.chkTransferSQFromISCToSAP = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadItems = new System.Windows.Forms.Button();
            this.btnLoadCustomers = new System.Windows.Forms.Button();
            this.btnTransferSQfromSAPtoISC = new System.Windows.Forms.Button();
            this.btnTransferSQfromISCtoIMOS = new System.Windows.Forms.Button();
            this.btnTransferSQfromISCtoSAP = new System.Windows.Forms.Button();
            this.btnTransferSQFromIMOSToISC = new System.Windows.Forms.Button();
            this.chkTransferSQFromIMOSToISC = new System.Windows.Forms.CheckBox();
            this.btnInstallSyncService = new System.Windows.Forms.Button();
            this.btnStartService = new System.Windows.Forms.Button();
            this.btnUnInstallSyncService = new System.Windows.Forms.Button();
            this.btnStopService = new System.Windows.Forms.Button();
            this.btnCreateSQInSAPForEachOpportunity = new System.Windows.Forms.Button();
            this.chkCreateSQInSAPForEachOpportunity = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkSyncItems
            // 
            this.chkSyncItems.AutoSize = true;
            this.chkSyncItems.Location = new System.Drawing.Point(240, 28);
            this.chkSyncItems.Name = "chkSyncItems";
            this.chkSyncItems.Size = new System.Drawing.Size(55, 19);
            this.chkSyncItems.TabIndex = 43;
            this.chkSyncItems.Text = "Items";
            this.chkSyncItems.UseVisualStyleBackColor = true;
            this.chkSyncItems.CheckedChanged += new System.EventHandler(this.chkSyncItems_CheckedChanged);
            // 
            // chkSyncCustomers
            // 
            this.chkSyncCustomers.AutoSize = true;
            this.chkSyncCustomers.Location = new System.Drawing.Point(240, 69);
            this.chkSyncCustomers.Name = "chkSyncCustomers";
            this.chkSyncCustomers.Size = new System.Drawing.Size(83, 19);
            this.chkSyncCustomers.TabIndex = 44;
            this.chkSyncCustomers.Text = "Customers";
            this.chkSyncCustomers.UseVisualStyleBackColor = true;
            this.chkSyncCustomers.CheckedChanged += new System.EventHandler(this.chkSyncCustomers_CheckedChanged);
            // 
            // chkTransferSQFromSAPToISC
            // 
            this.chkTransferSQFromSAPToISC.AutoSize = true;
            this.chkTransferSQFromSAPToISC.Location = new System.Drawing.Point(240, 152);
            this.chkTransferSQFromSAPToISC.Name = "chkTransferSQFromSAPToISC";
            this.chkTransferSQFromSAPToISC.Size = new System.Drawing.Size(172, 19);
            this.chkTransferSQFromSAPToISC.TabIndex = 45;
            this.chkTransferSQFromSAPToISC.Text = "Transfer SQ from SAP to ISC";
            this.chkTransferSQFromSAPToISC.UseVisualStyleBackColor = true;
            this.chkTransferSQFromSAPToISC.CheckedChanged += new System.EventHandler(this.chkTransferSQFromSAPToISC_CheckedChanged);
            // 
            // chkTransferSQFromISCToIMOS
            // 
            this.chkTransferSQFromISCToIMOS.AutoSize = true;
            this.chkTransferSQFromISCToIMOS.Location = new System.Drawing.Point(240, 193);
            this.chkTransferSQFromISCToIMOS.Name = "chkTransferSQFromISCToIMOS";
            this.chkTransferSQFromISCToIMOS.Size = new System.Drawing.Size(180, 19);
            this.chkTransferSQFromISCToIMOS.TabIndex = 46;
            this.chkTransferSQFromISCToIMOS.Text = "Transfer SQ from ISC to IMOS";
            this.chkTransferSQFromISCToIMOS.UseVisualStyleBackColor = true;
            this.chkTransferSQFromISCToIMOS.CheckedChanged += new System.EventHandler(this.chkTransferSQFromISCToIMOS_CheckedChanged);
            // 
            // chkTransferSQFromISCToSAP
            // 
            this.chkTransferSQFromISCToSAP.AutoSize = true;
            this.chkTransferSQFromISCToSAP.Location = new System.Drawing.Point(240, 276);
            this.chkTransferSQFromISCToSAP.Name = "chkTransferSQFromISCToSAP";
            this.chkTransferSQFromISCToSAP.Size = new System.Drawing.Size(172, 19);
            this.chkTransferSQFromISCToSAP.TabIndex = 47;
            this.chkTransferSQFromISCToSAP.Text = "Transfer SQ from ISC to SAP";
            this.chkTransferSQFromISCToSAP.UseVisualStyleBackColor = true;
            this.chkTransferSQFromISCToSAP.CheckedChanged += new System.EventHandler(this.chkTransferSQFromISCToSAP_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.Location = new System.Drawing.Point(581, 28);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(172, 241);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoadItems
            // 
            this.btnLoadItems.Location = new System.Drawing.Point(111, 12);
            this.btnLoadItems.Name = "btnLoadItems";
            this.btnLoadItems.Size = new System.Drawing.Size(123, 35);
            this.btnLoadItems.TabIndex = 49;
            this.btnLoadItems.Text = "Load Items";
            this.btnLoadItems.UseVisualStyleBackColor = true;
            this.btnLoadItems.Click += new System.EventHandler(this.btnLoadItems_Click);
            // 
            // btnLoadCustomers
            // 
            this.btnLoadCustomers.Location = new System.Drawing.Point(79, 53);
            this.btnLoadCustomers.Name = "btnLoadCustomers";
            this.btnLoadCustomers.Size = new System.Drawing.Size(155, 35);
            this.btnLoadCustomers.TabIndex = 50;
            this.btnLoadCustomers.Text = "Load Customers";
            this.btnLoadCustomers.UseVisualStyleBackColor = true;
            this.btnLoadCustomers.Click += new System.EventHandler(this.btnLoadCustomers_Click);
            // 
            // btnTransferSQfromSAPtoISC
            // 
            this.btnTransferSQfromSAPtoISC.Location = new System.Drawing.Point(12, 136);
            this.btnTransferSQfromSAPtoISC.Name = "btnTransferSQfromSAPtoISC";
            this.btnTransferSQfromSAPtoISC.Size = new System.Drawing.Size(222, 35);
            this.btnTransferSQfromSAPtoISC.TabIndex = 51;
            this.btnTransferSQfromSAPtoISC.Text = "Transfer SQ from SAP to ISC";
            this.btnTransferSQfromSAPtoISC.UseVisualStyleBackColor = true;
            this.btnTransferSQfromSAPtoISC.Click += new System.EventHandler(this.btnTransferSQfromSAPtoISC_Click);
            // 
            // btnTransferSQfromISCtoIMOS
            // 
            this.btnTransferSQfromISCtoIMOS.Location = new System.Drawing.Point(12, 177);
            this.btnTransferSQfromISCtoIMOS.Name = "btnTransferSQfromISCtoIMOS";
            this.btnTransferSQfromISCtoIMOS.Size = new System.Drawing.Size(222, 35);
            this.btnTransferSQfromISCtoIMOS.TabIndex = 52;
            this.btnTransferSQfromISCtoIMOS.Text = "Transfer SQ from ISC to IMOS";
            this.btnTransferSQfromISCtoIMOS.UseVisualStyleBackColor = true;
            this.btnTransferSQfromISCtoIMOS.Click += new System.EventHandler(this.btnTransferSQfromISCtoIMOS_Click);
            // 
            // btnTransferSQfromISCtoSAP
            // 
            this.btnTransferSQfromISCtoSAP.Location = new System.Drawing.Point(12, 260);
            this.btnTransferSQfromISCtoSAP.Name = "btnTransferSQfromISCtoSAP";
            this.btnTransferSQfromISCtoSAP.Size = new System.Drawing.Size(222, 35);
            this.btnTransferSQfromISCtoSAP.TabIndex = 53;
            this.btnTransferSQfromISCtoSAP.Text = "Transfer SQ from ISC to SAP";
            this.btnTransferSQfromISCtoSAP.UseVisualStyleBackColor = true;
            this.btnTransferSQfromISCtoSAP.Click += new System.EventHandler(this.btnTransferSQfromISCtoSAP_Click);
            // 
            // btnTransferSQFromIMOSToISC
            // 
            this.btnTransferSQFromIMOSToISC.Location = new System.Drawing.Point(12, 219);
            this.btnTransferSQFromIMOSToISC.Name = "btnTransferSQFromIMOSToISC";
            this.btnTransferSQFromIMOSToISC.Size = new System.Drawing.Size(222, 35);
            this.btnTransferSQFromIMOSToISC.TabIndex = 55;
            this.btnTransferSQFromIMOSToISC.Text = "Transfer SQ from IMOS to ISC";
            this.btnTransferSQFromIMOSToISC.UseVisualStyleBackColor = true;
            this.btnTransferSQFromIMOSToISC.Click += new System.EventHandler(this.btnTransferSQFromIMOSToISC_Click);
            // 
            // chkTransferSQFromIMOSToISC
            // 
            this.chkTransferSQFromIMOSToISC.AutoSize = true;
            this.chkTransferSQFromIMOSToISC.Location = new System.Drawing.Point(240, 235);
            this.chkTransferSQFromIMOSToISC.Name = "chkTransferSQFromIMOSToISC";
            this.chkTransferSQFromIMOSToISC.Size = new System.Drawing.Size(183, 19);
            this.chkTransferSQFromIMOSToISC.TabIndex = 54;
            this.chkTransferSQFromIMOSToISC.Text = "Transfer SQ from IMOS to ISC ";
            this.chkTransferSQFromIMOSToISC.UseVisualStyleBackColor = true;
            this.chkTransferSQFromIMOSToISC.CheckedChanged += new System.EventHandler(this.chkTransferSQFromIMOSToISC_CheckedChanged);
            // 
            // btnInstallSyncService
            // 
            this.btnInstallSyncService.Location = new System.Drawing.Point(91, 350);
            this.btnInstallSyncService.Name = "btnInstallSyncService";
            this.btnInstallSyncService.Size = new System.Drawing.Size(149, 35);
            this.btnInstallSyncService.TabIndex = 56;
            this.btnInstallSyncService.Text = "Install Sync Service";
            this.btnInstallSyncService.UseVisualStyleBackColor = true;
            this.btnInstallSyncService.Click += new System.EventHandler(this.btnInstallSyncService_Click);
            // 
            // btnStartService
            // 
            this.btnStartService.Location = new System.Drawing.Point(465, 350);
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(102, 35);
            this.btnStartService.TabIndex = 57;
            this.btnStartService.Text = "Start Service";
            this.btnStartService.UseVisualStyleBackColor = true;
            this.btnStartService.Click += new System.EventHandler(this.btnStartService_Click);
            // 
            // btnUnInstallSyncService
            // 
            this.btnUnInstallSyncService.Location = new System.Drawing.Point(246, 350);
            this.btnUnInstallSyncService.Name = "btnUnInstallSyncService";
            this.btnUnInstallSyncService.Size = new System.Drawing.Size(149, 35);
            this.btnUnInstallSyncService.TabIndex = 58;
            this.btnUnInstallSyncService.Text = "Un-Install Sync Service";
            this.btnUnInstallSyncService.UseVisualStyleBackColor = true;
            this.btnUnInstallSyncService.Click += new System.EventHandler(this.btnUnInstallSyncService_Click);
            // 
            // btnStopService
            // 
            this.btnStopService.Location = new System.Drawing.Point(573, 350);
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(102, 35);
            this.btnStopService.TabIndex = 59;
            this.btnStopService.Text = "Stop Service";
            this.btnStopService.UseVisualStyleBackColor = true;
            this.btnStopService.Click += new System.EventHandler(this.btnStopService_Click);
            // 
            // btnCreateSQInSAPForEachOpportunity
            // 
            this.btnCreateSQInSAPForEachOpportunity.Location = new System.Drawing.Point(12, 94);
            this.btnCreateSQInSAPForEachOpportunity.Name = "btnCreateSQInSAPForEachOpportunity";
            this.btnCreateSQInSAPForEachOpportunity.Size = new System.Drawing.Size(222, 35);
            this.btnCreateSQInSAPForEachOpportunity.TabIndex = 61;
            this.btnCreateSQInSAPForEachOpportunity.Text = "Create SQ in SAP for each Opportunity";
            this.btnCreateSQInSAPForEachOpportunity.UseVisualStyleBackColor = true;
            this.btnCreateSQInSAPForEachOpportunity.Click += new System.EventHandler(this.btnCreateSQInSAPForEachOpportunity_Click);
            // 
            // chkCreateSQInSAPForEachOpportunity
            // 
            this.chkCreateSQInSAPForEachOpportunity.AutoSize = true;
            this.chkCreateSQInSAPForEachOpportunity.Location = new System.Drawing.Point(240, 110);
            this.chkCreateSQInSAPForEachOpportunity.Name = "chkCreateSQInSAPForEachOpportunity";
            this.chkCreateSQInSAPForEachOpportunity.Size = new System.Drawing.Size(229, 19);
            this.chkCreateSQInSAPForEachOpportunity.TabIndex = 60;
            this.chkCreateSQInSAPForEachOpportunity.Text = "Create SQ in SAP for each Opportunity";
            this.chkCreateSQInSAPForEachOpportunity.UseVisualStyleBackColor = true;
            this.chkCreateSQInSAPForEachOpportunity.CheckedChanged += new System.EventHandler(this.chkCreateSQInSAPForEachOpportunity_CheckedChanged);
            // 
            // frmSyncSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 409);
            this.Controls.Add(this.btnCreateSQInSAPForEachOpportunity);
            this.Controls.Add(this.chkCreateSQInSAPForEachOpportunity);
            this.Controls.Add(this.btnStopService);
            this.Controls.Add(this.btnUnInstallSyncService);
            this.Controls.Add(this.btnStartService);
            this.Controls.Add(this.btnInstallSyncService);
            this.Controls.Add(this.btnTransferSQFromIMOSToISC);
            this.Controls.Add(this.chkTransferSQFromIMOSToISC);
            this.Controls.Add(this.btnTransferSQfromISCtoSAP);
            this.Controls.Add(this.btnTransferSQfromISCtoIMOS);
            this.Controls.Add(this.btnTransferSQfromSAPtoISC);
            this.Controls.Add(this.btnLoadCustomers);
            this.Controls.Add(this.btnLoadItems);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkTransferSQFromISCToSAP);
            this.Controls.Add(this.chkTransferSQFromISCToIMOS);
            this.Controls.Add(this.chkTransferSQFromSAPToISC);
            this.Controls.Add(this.chkSyncCustomers);
            this.Controls.Add(this.chkSyncItems);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSyncSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ISC Sync Settings";
            this.Load += new System.EventHandler(this.frmSyncSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox chkSyncItems;
        private CheckBox chkSyncCustomers;
        private CheckBox chkTransferSQFromSAPToISC;
        private CheckBox chkTransferSQFromISCToIMOS;
        private CheckBox chkTransferSQFromISCToSAP;
        private Button btnSave;
        private Button btnLoadItems;
        private Button btnLoadCustomers;
        private Button btnTransferSQfromSAPtoISC;
        private Button btnTransferSQfromISCtoIMOS;
        private Button btnTransferSQfromISCtoSAP;
        private Button btnTransferSQFromIMOSToISC;
        private CheckBox chkTransferSQFromIMOSToISC;
        private Button btnInstallSyncService;
        private Button btnStartService;
        private Button btnUnInstallSyncService;
        private Button btnStopService;
        private Button btnCreateSQInSAPForEachOpportunity;
        private CheckBox chkCreateSQInSAPForEachOpportunity;
    }
}