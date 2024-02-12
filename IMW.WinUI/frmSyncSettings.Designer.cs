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
            chkSyncItems = new CheckBox();
            chkSyncCustomers = new CheckBox();
            chkTransferSQFromSAPToISC = new CheckBox();
            chkTransferSQFromISCToIMOS = new CheckBox();
            chkTransferSQFromISCToSAP = new CheckBox();
            btnSave = new Button();
            btnLoadItems = new Button();
            btnLoadCustomers = new Button();
            btnTransferSQfromSAPtoISC = new Button();
            btnTransferSQfromISCtoIMOS = new Button();
            btnTransferSQfromISCtoSAP = new Button();
            btnTransferSQFromIMOSToISC = new Button();
            chkTransferSQFromIMOSToISC = new CheckBox();
            btnInstallSyncService = new Button();
            btnStartService = new Button();
            btnUnInstallSyncService = new Button();
            btnStopService = new Button();
            btnCreateSQInSAPForEachOpportunity = new Button();
            chkCreateSQInSAPForEachOpportunity = new CheckBox();
            btnSyncBoM = new Button();
            chkSyncBoM = new CheckBox();
            chkItemsItems = new CheckBox();
            chkItemsPrices = new CheckBox();
            chkItemsGroups = new CheckBox();
            chkItemsResources = new CheckBox();
            SuspendLayout();
            // 
            // chkSyncItems
            // 
            chkSyncItems.AutoSize = true;
            chkSyncItems.Location = new Point(326, 37);
            chkSyncItems.Margin = new Padding(3, 4, 3, 4);
            chkSyncItems.Name = "chkSyncItems";
            chkSyncItems.Size = new Size(67, 24);
            chkSyncItems.TabIndex = 43;
            chkSyncItems.Text = "Items";
            chkSyncItems.UseVisualStyleBackColor = true;
            chkSyncItems.CheckedChanged += chkSyncItems_CheckedChanged;
            // 
            // chkSyncCustomers
            // 
            chkSyncCustomers.AutoSize = true;
            chkSyncCustomers.Location = new Point(326, 92);
            chkSyncCustomers.Margin = new Padding(3, 4, 3, 4);
            chkSyncCustomers.Name = "chkSyncCustomers";
            chkSyncCustomers.Size = new Size(100, 24);
            chkSyncCustomers.TabIndex = 44;
            chkSyncCustomers.Text = "Customers";
            chkSyncCustomers.UseVisualStyleBackColor = true;
            chkSyncCustomers.CheckedChanged += chkSyncCustomers_CheckedChanged;
            // 
            // chkTransferSQFromSAPToISC
            // 
            chkTransferSQFromSAPToISC.AutoSize = true;
            chkTransferSQFromSAPToISC.Location = new Point(326, 203);
            chkTransferSQFromSAPToISC.Margin = new Padding(3, 4, 3, 4);
            chkTransferSQFromSAPToISC.Name = "chkTransferSQFromSAPToISC";
            chkTransferSQFromSAPToISC.Size = new Size(215, 24);
            chkTransferSQFromSAPToISC.TabIndex = 45;
            chkTransferSQFromSAPToISC.Text = "Transfer SQ from SAP to ISC";
            chkTransferSQFromSAPToISC.UseVisualStyleBackColor = true;
            chkTransferSQFromSAPToISC.CheckedChanged += chkTransferSQFromSAPToISC_CheckedChanged;
            // 
            // chkTransferSQFromISCToIMOS
            // 
            chkTransferSQFromISCToIMOS.AutoSize = true;
            chkTransferSQFromISCToIMOS.Location = new Point(326, 257);
            chkTransferSQFromISCToIMOS.Margin = new Padding(3, 4, 3, 4);
            chkTransferSQFromISCToIMOS.Name = "chkTransferSQFromISCToIMOS";
            chkTransferSQFromISCToIMOS.Size = new Size(225, 24);
            chkTransferSQFromISCToIMOS.TabIndex = 46;
            chkTransferSQFromISCToIMOS.Text = "Transfer SQ from ISC to IMOS";
            chkTransferSQFromISCToIMOS.UseVisualStyleBackColor = true;
            chkTransferSQFromISCToIMOS.CheckedChanged += chkTransferSQFromISCToIMOS_CheckedChanged;
            // 
            // chkTransferSQFromISCToSAP
            // 
            chkTransferSQFromISCToSAP.AutoSize = true;
            chkTransferSQFromISCToSAP.Location = new Point(326, 368);
            chkTransferSQFromISCToSAP.Margin = new Padding(3, 4, 3, 4);
            chkTransferSQFromISCToSAP.Name = "chkTransferSQFromISCToSAP";
            chkTransferSQFromISCToSAP.Size = new Size(215, 24);
            chkTransferSQFromISCToSAP.TabIndex = 47;
            chkTransferSQFromISCToSAP.Text = "Transfer SQ from ISC to SAP";
            chkTransferSQFromISCToSAP.UseVisualStyleBackColor = true;
            chkTransferSQFromISCToSAP.CheckedChanged += chkTransferSQFromISCToSAP_CheckedChanged;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnSave.Location = new Point(665, 87);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(197, 321);
            btnSave.TabIndex = 48;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnLoadItems
            // 
            btnLoadItems.Location = new Point(127, 16);
            btnLoadItems.Margin = new Padding(3, 4, 3, 4);
            btnLoadItems.Name = "btnLoadItems";
            btnLoadItems.Size = new Size(193, 47);
            btnLoadItems.TabIndex = 49;
            btnLoadItems.Text = "Load Items";
            btnLoadItems.UseVisualStyleBackColor = true;
            btnLoadItems.Click += btnLoadItems_Click;
            // 
            // btnLoadCustomers
            // 
            btnLoadCustomers.Location = new Point(90, 71);
            btnLoadCustomers.Margin = new Padding(3, 4, 3, 4);
            btnLoadCustomers.Name = "btnLoadCustomers";
            btnLoadCustomers.Size = new Size(229, 47);
            btnLoadCustomers.TabIndex = 50;
            btnLoadCustomers.Text = "Load Customers";
            btnLoadCustomers.UseVisualStyleBackColor = true;
            btnLoadCustomers.Click += btnLoadCustomers_Click;
            // 
            // btnTransferSQfromSAPtoISC
            // 
            btnTransferSQfromSAPtoISC.Location = new Point(14, 181);
            btnTransferSQfromSAPtoISC.Margin = new Padding(3, 4, 3, 4);
            btnTransferSQfromSAPtoISC.Name = "btnTransferSQfromSAPtoISC";
            btnTransferSQfromSAPtoISC.Size = new Size(306, 47);
            btnTransferSQfromSAPtoISC.TabIndex = 51;
            btnTransferSQfromSAPtoISC.Text = "Transfer SQ from SAP to ISC";
            btnTransferSQfromSAPtoISC.UseVisualStyleBackColor = true;
            btnTransferSQfromSAPtoISC.Click += btnTransferSQfromSAPtoISC_Click;
            // 
            // btnTransferSQfromISCtoIMOS
            // 
            btnTransferSQfromISCtoIMOS.Location = new Point(14, 236);
            btnTransferSQfromISCtoIMOS.Margin = new Padding(3, 4, 3, 4);
            btnTransferSQfromISCtoIMOS.Name = "btnTransferSQfromISCtoIMOS";
            btnTransferSQfromISCtoIMOS.Size = new Size(306, 47);
            btnTransferSQfromISCtoIMOS.TabIndex = 52;
            btnTransferSQfromISCtoIMOS.Text = "Transfer SQ from ISC to IMOS";
            btnTransferSQfromISCtoIMOS.UseVisualStyleBackColor = true;
            btnTransferSQfromISCtoIMOS.Click += btnTransferSQfromISCtoIMOS_Click;
            // 
            // btnTransferSQfromISCtoSAP
            // 
            btnTransferSQfromISCtoSAP.Location = new Point(14, 347);
            btnTransferSQfromISCtoSAP.Margin = new Padding(3, 4, 3, 4);
            btnTransferSQfromISCtoSAP.Name = "btnTransferSQfromISCtoSAP";
            btnTransferSQfromISCtoSAP.Size = new Size(306, 47);
            btnTransferSQfromISCtoSAP.TabIndex = 53;
            btnTransferSQfromISCtoSAP.Text = "Transfer SQ from ISC to SAP";
            btnTransferSQfromISCtoSAP.UseVisualStyleBackColor = true;
            btnTransferSQfromISCtoSAP.Click += btnTransferSQfromISCtoSAP_Click;
            // 
            // btnTransferSQFromIMOSToISC
            // 
            btnTransferSQFromIMOSToISC.Location = new Point(14, 292);
            btnTransferSQFromIMOSToISC.Margin = new Padding(3, 4, 3, 4);
            btnTransferSQFromIMOSToISC.Name = "btnTransferSQFromIMOSToISC";
            btnTransferSQFromIMOSToISC.Size = new Size(306, 47);
            btnTransferSQFromIMOSToISC.TabIndex = 55;
            btnTransferSQFromIMOSToISC.Text = "Transfer SQ from IMOS to ISC";
            btnTransferSQFromIMOSToISC.UseVisualStyleBackColor = true;
            btnTransferSQFromIMOSToISC.Click += btnTransferSQFromIMOSToISC_Click;
            // 
            // chkTransferSQFromIMOSToISC
            // 
            chkTransferSQFromIMOSToISC.AutoSize = true;
            chkTransferSQFromIMOSToISC.Location = new Point(326, 313);
            chkTransferSQFromIMOSToISC.Margin = new Padding(3, 4, 3, 4);
            chkTransferSQFromIMOSToISC.Name = "chkTransferSQFromIMOSToISC";
            chkTransferSQFromIMOSToISC.Size = new Size(229, 24);
            chkTransferSQFromIMOSToISC.TabIndex = 54;
            chkTransferSQFromIMOSToISC.Text = "Transfer SQ from IMOS to ISC ";
            chkTransferSQFromIMOSToISC.UseVisualStyleBackColor = true;
            chkTransferSQFromIMOSToISC.CheckedChanged += chkTransferSQFromIMOSToISC_CheckedChanged;
            // 
            // btnInstallSyncService
            // 
            btnInstallSyncService.Location = new Point(106, 485);
            btnInstallSyncService.Margin = new Padding(3, 4, 3, 4);
            btnInstallSyncService.Name = "btnInstallSyncService";
            btnInstallSyncService.Size = new Size(170, 47);
            btnInstallSyncService.TabIndex = 56;
            btnInstallSyncService.Text = "Install Sync Service";
            btnInstallSyncService.UseVisualStyleBackColor = true;
            btnInstallSyncService.Click += btnInstallSyncService_Click;
            // 
            // btnStartService
            // 
            btnStartService.Location = new Point(533, 485);
            btnStartService.Margin = new Padding(3, 4, 3, 4);
            btnStartService.Name = "btnStartService";
            btnStartService.Size = new Size(117, 47);
            btnStartService.TabIndex = 57;
            btnStartService.Text = "Start Service";
            btnStartService.UseVisualStyleBackColor = true;
            btnStartService.Click += btnStartService_Click;
            // 
            // btnUnInstallSyncService
            // 
            btnUnInstallSyncService.Location = new Point(283, 485);
            btnUnInstallSyncService.Margin = new Padding(3, 4, 3, 4);
            btnUnInstallSyncService.Name = "btnUnInstallSyncService";
            btnUnInstallSyncService.Size = new Size(170, 47);
            btnUnInstallSyncService.TabIndex = 58;
            btnUnInstallSyncService.Text = "Un-Install Sync Service";
            btnUnInstallSyncService.UseVisualStyleBackColor = true;
            btnUnInstallSyncService.Click += btnUnInstallSyncService_Click;
            // 
            // btnStopService
            // 
            btnStopService.Location = new Point(657, 485);
            btnStopService.Margin = new Padding(3, 4, 3, 4);
            btnStopService.Name = "btnStopService";
            btnStopService.Size = new Size(117, 47);
            btnStopService.TabIndex = 59;
            btnStopService.Text = "Stop Service";
            btnStopService.UseVisualStyleBackColor = true;
            btnStopService.Click += btnStopService_Click;
            // 
            // btnCreateSQInSAPForEachOpportunity
            // 
            btnCreateSQInSAPForEachOpportunity.Location = new Point(14, 125);
            btnCreateSQInSAPForEachOpportunity.Margin = new Padding(3, 4, 3, 4);
            btnCreateSQInSAPForEachOpportunity.Name = "btnCreateSQInSAPForEachOpportunity";
            btnCreateSQInSAPForEachOpportunity.Size = new Size(306, 47);
            btnCreateSQInSAPForEachOpportunity.TabIndex = 61;
            btnCreateSQInSAPForEachOpportunity.Text = "Create SQ in SAP for each Opportunity";
            btnCreateSQInSAPForEachOpportunity.UseVisualStyleBackColor = true;
            btnCreateSQInSAPForEachOpportunity.Click += btnCreateSQInSAPForEachOpportunity_Click;
            // 
            // chkCreateSQInSAPForEachOpportunity
            // 
            chkCreateSQInSAPForEachOpportunity.AutoSize = true;
            chkCreateSQInSAPForEachOpportunity.Location = new Point(326, 147);
            chkCreateSQInSAPForEachOpportunity.Margin = new Padding(3, 4, 3, 4);
            chkCreateSQInSAPForEachOpportunity.Name = "chkCreateSQInSAPForEachOpportunity";
            chkCreateSQInSAPForEachOpportunity.Size = new Size(285, 24);
            chkCreateSQInSAPForEachOpportunity.TabIndex = 60;
            chkCreateSQInSAPForEachOpportunity.Text = "Create SQ in SAP for each Opportunity";
            chkCreateSQInSAPForEachOpportunity.UseVisualStyleBackColor = true;
            chkCreateSQInSAPForEachOpportunity.CheckedChanged += chkCreateSQInSAPForEachOpportunity_CheckedChanged;
            // 
            // btnSyncBoM
            // 
            btnSyncBoM.Location = new Point(12, 402);
            btnSyncBoM.Margin = new Padding(3, 4, 3, 4);
            btnSyncBoM.Name = "btnSyncBoM";
            btnSyncBoM.Size = new Size(306, 47);
            btnSyncBoM.TabIndex = 63;
            btnSyncBoM.Text = "Sync BoM";
            btnSyncBoM.UseVisualStyleBackColor = true;
            btnSyncBoM.Click += btnSyncBoM_Click;
            // 
            // chkSyncBoM
            // 
            chkSyncBoM.AutoSize = true;
            chkSyncBoM.Location = new Point(324, 423);
            chkSyncBoM.Margin = new Padding(3, 4, 3, 4);
            chkSyncBoM.Name = "chkSyncBoM";
            chkSyncBoM.Size = new Size(96, 24);
            chkSyncBoM.TabIndex = 62;
            chkSyncBoM.Text = "Sync BoM";
            chkSyncBoM.UseVisualStyleBackColor = true;
            chkSyncBoM.CheckedChanged += chkSyncBoM_CheckedChanged;
            // 
            // chkItemsItems
            // 
            chkItemsItems.AutoSize = true;
            chkItemsItems.Checked = true;
            chkItemsItems.CheckState = CheckState.Checked;
            chkItemsItems.Location = new Point(419, 37);
            chkItemsItems.Margin = new Padding(3, 4, 3, 4);
            chkItemsItems.Name = "chkItemsItems";
            chkItemsItems.Size = new Size(67, 24);
            chkItemsItems.TabIndex = 64;
            chkItemsItems.Text = "Items";
            chkItemsItems.UseVisualStyleBackColor = true;
            // 
            // chkItemsPrices
            // 
            chkItemsPrices.AutoSize = true;
            chkItemsPrices.Checked = true;
            chkItemsPrices.CheckState = CheckState.Checked;
            chkItemsPrices.Location = new Point(492, 37);
            chkItemsPrices.Margin = new Padding(3, 4, 3, 4);
            chkItemsPrices.Name = "chkItemsPrices";
            chkItemsPrices.Size = new Size(69, 24);
            chkItemsPrices.TabIndex = 65;
            chkItemsPrices.Text = "Prices";
            chkItemsPrices.UseVisualStyleBackColor = true;
            // 
            // chkItemsGroups
            // 
            chkItemsGroups.AutoSize = true;
            chkItemsGroups.Checked = true;
            chkItemsGroups.CheckState = CheckState.Checked;
            chkItemsGroups.Location = new Point(567, 37);
            chkItemsGroups.Margin = new Padding(3, 4, 3, 4);
            chkItemsGroups.Name = "chkItemsGroups";
            chkItemsGroups.Size = new Size(78, 24);
            chkItemsGroups.TabIndex = 66;
            chkItemsGroups.Text = "Groups";
            chkItemsGroups.UseVisualStyleBackColor = true;
            // 
            // chkItemsResources
            // 
            chkItemsResources.AutoSize = true;
            chkItemsResources.Checked = true;
            chkItemsResources.CheckState = CheckState.Checked;
            chkItemsResources.Location = new Point(651, 37);
            chkItemsResources.Margin = new Padding(3, 4, 3, 4);
            chkItemsResources.Name = "chkItemsResources";
            chkItemsResources.Size = new Size(97, 24);
            chkItemsResources.TabIndex = 67;
            chkItemsResources.Text = "Resources";
            chkItemsResources.UseVisualStyleBackColor = true;
            // 
            // frmSyncSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(874, 545);
            Controls.Add(chkItemsResources);
            Controls.Add(chkItemsGroups);
            Controls.Add(chkItemsPrices);
            Controls.Add(chkItemsItems);
            Controls.Add(btnSyncBoM);
            Controls.Add(chkSyncBoM);
            Controls.Add(btnCreateSQInSAPForEachOpportunity);
            Controls.Add(chkCreateSQInSAPForEachOpportunity);
            Controls.Add(btnStopService);
            Controls.Add(btnUnInstallSyncService);
            Controls.Add(btnStartService);
            Controls.Add(btnInstallSyncService);
            Controls.Add(btnTransferSQFromIMOSToISC);
            Controls.Add(chkTransferSQFromIMOSToISC);
            Controls.Add(btnTransferSQfromISCtoSAP);
            Controls.Add(btnTransferSQfromISCtoIMOS);
            Controls.Add(btnTransferSQfromSAPtoISC);
            Controls.Add(btnLoadCustomers);
            Controls.Add(btnLoadItems);
            Controls.Add(btnSave);
            Controls.Add(chkTransferSQFromISCToSAP);
            Controls.Add(chkTransferSQFromISCToIMOS);
            Controls.Add(chkTransferSQFromSAPToISC);
            Controls.Add(chkSyncCustomers);
            Controls.Add(chkSyncItems);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSyncSettings";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ISC Sync Settings";
            Load += frmSyncSettings_Load;
            ResumeLayout(false);
            PerformLayout();
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
        private Button btnSyncBoM;
        private CheckBox chkSyncBoM;
        private CheckBox chkItemsItems;
        private CheckBox chkItemsPrices;
        private CheckBox chkItemsGroups;
        private CheckBox chkItemsResources;
    }
}