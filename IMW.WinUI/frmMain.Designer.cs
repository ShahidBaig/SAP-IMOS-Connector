namespace IMW.WinUI
{
    partial class frmMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            sAPConnectionToolStripMenuItem = new ToolStripMenuItem();
            salesCentersToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            userManagerMenu = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            syncToolStripMenuItem = new ToolStripMenuItem();
            itemMasterToolStripMenuItem = new ToolStripMenuItem();
            mnuSyncSettings = new ToolStripMenuItem();
            salesQuotationToolStripMenuItem = new ToolStripMenuItem();
            qtyConversionForSAPToolStripMenuItem = new ToolStripMenuItem();
            QuotationAnalyzerToolStripMenuItem = new ToolStripMenuItem();
            sAPItemIdentificationConfigToolStripMenuItem = new ToolStripMenuItem();
            sAPToolStripMenuItem = new ToolStripMenuItem();
            sAPQueryToolStripMenuItem = new ToolStripMenuItem();
            helpMenu = new ToolStripMenuItem();
            contentsToolStripMenuItem = new ToolStripMenuItem();
            indexToolStripMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            newToolStripButton = new ToolStripButton();
            openToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            printToolStripButton = new ToolStripButton();
            printPreviewToolStripButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            helpToolStripButton = new ToolStripButton();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolTip = new ToolTip(components);
            menuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, syncToolStripMenuItem, salesQuotationToolStripMenuItem, sAPToolStripMenuItem, helpMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(737, 24);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "MenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sAPConnectionToolStripMenuItem, salesCentersToolStripMenuItem, toolStripSeparator4, userManagerMenu, toolStripSeparator3, exitToolStripMenuItem });
            fileToolStripMenuItem.ImageTransparentColor = SystemColors.ActiveBorder;
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // sAPConnectionToolStripMenuItem
            // 
            sAPConnectionToolStripMenuItem.Image = (Image)resources.GetObject("sAPConnectionToolStripMenuItem.Image");
            sAPConnectionToolStripMenuItem.ImageTransparentColor = Color.Black;
            sAPConnectionToolStripMenuItem.Name = "sAPConnectionToolStripMenuItem";
            sAPConnectionToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            sAPConnectionToolStripMenuItem.Size = new Size(186, 22);
            sAPConnectionToolStripMenuItem.Text = "&Connections";
            sAPConnectionToolStripMenuItem.Click += sAPConnectionToolStripMenuItem_Click;
            // 
            // salesCentersToolStripMenuItem
            // 
            salesCentersToolStripMenuItem.Image = (Image)resources.GetObject("salesCentersToolStripMenuItem.Image");
            salesCentersToolStripMenuItem.ImageTransparentColor = Color.Black;
            salesCentersToolStripMenuItem.Name = "salesCentersToolStripMenuItem";
            salesCentersToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            salesCentersToolStripMenuItem.Size = new Size(186, 22);
            salesCentersToolStripMenuItem.Text = "&Sales Centers";
            salesCentersToolStripMenuItem.Click += salesCentersToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(183, 6);
            // 
            // userManagerMenu
            // 
            userManagerMenu.Name = "userManagerMenu";
            userManagerMenu.Size = new Size(186, 22);
            userManagerMenu.Text = "&User Manager";
            userManagerMenu.Click += userManagerMenu_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(183, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(186, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += ExitToolsStripMenuItem_Click;
            // 
            // syncToolStripMenuItem
            // 
            syncToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { itemMasterToolStripMenuItem, mnuSyncSettings });
            syncToolStripMenuItem.Name = "syncToolStripMenuItem";
            syncToolStripMenuItem.Size = new Size(44, 20);
            syncToolStripMenuItem.Text = "&Sync";
            // 
            // itemMasterToolStripMenuItem
            // 
            itemMasterToolStripMenuItem.Name = "itemMasterToolStripMenuItem";
            itemMasterToolStripMenuItem.Size = new Size(137, 22);
            itemMasterToolStripMenuItem.Text = "Item Master";
            itemMasterToolStripMenuItem.Click += itemMasterToolStripMenuItem_Click;
            // 
            // mnuSyncSettings
            // 
            mnuSyncSettings.Name = "mnuSyncSettings";
            mnuSyncSettings.Size = new Size(137, 22);
            mnuSyncSettings.Text = "Settings";
            mnuSyncSettings.Click += mnuSyncSettings_Click;
            // 
            // salesQuotationToolStripMenuItem
            // 
            salesQuotationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { qtyConversionForSAPToolStripMenuItem, QuotationAnalyzerToolStripMenuItem, sAPItemIdentificationConfigToolStripMenuItem });
            salesQuotationToolStripMenuItem.Name = "salesQuotationToolStripMenuItem";
            salesQuotationToolStripMenuItem.Size = new Size(102, 20);
            salesQuotationToolStripMenuItem.Text = "Sales &Quotation";
            sAPItemIdentificationConfigToolStripMenuItem.Click += sAPItemIdentificationConfigToolStripMenuItem_Click;
            // 
            // qtyConversionForSAPToolStripMenuItem
            // 
            qtyConversionForSAPToolStripMenuItem.Name = "qtyConversionForSAPToolStripMenuItem";
            qtyConversionForSAPToolStripMenuItem.Size = new Size(234, 22);
            qtyConversionForSAPToolStripMenuItem.Text = "&Qty Conversion for SAP";
            qtyConversionForSAPToolStripMenuItem.Click += qtyConversionForSAPToolStripMenuItem_Click;
            // 
            // QuotationAnalyzerToolStripMenuItem
            // 
            QuotationAnalyzerToolStripMenuItem.Name = "QuotationAnalyzerToolStripMenuItem";
            QuotationAnalyzerToolStripMenuItem.Size = new Size(234, 22);
            QuotationAnalyzerToolStripMenuItem.Text = "Quotation &Analyzer";
            QuotationAnalyzerToolStripMenuItem.Click += QuotationAnalyzer_Click;
            // 
            // sAPItemIdentificationConfigToolStripMenuItem
            // 
            sAPItemIdentificationConfigToolStripMenuItem.Name = "sAPItemIdentificationConfigToolStripMenuItem";
            sAPItemIdentificationConfigToolStripMenuItem.Size = new Size(234, 22);
            sAPItemIdentificationConfigToolStripMenuItem.Text = "SAP Item Identification Config";
            // 
            // sAPToolStripMenuItem
            // 
            sAPToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sAPQueryToolStripMenuItem });
            sAPToolStripMenuItem.Name = "sAPToolStripMenuItem";
            sAPToolStripMenuItem.Size = new Size(40, 20);
            sAPToolStripMenuItem.Text = "S&AP";
            // 
            // sAPQueryToolStripMenuItem
            // 
            sAPQueryToolStripMenuItem.Name = "sAPQueryToolStripMenuItem";
            sAPQueryToolStripMenuItem.Size = new Size(130, 22);
            sAPQueryToolStripMenuItem.Text = "SAP &Query";
            sAPQueryToolStripMenuItem.Click += sAPQueryToolStripMenuItem_Click;
            // 
            // helpMenu
            // 
            helpMenu.DropDownItems.AddRange(new ToolStripItem[] { contentsToolStripMenuItem, indexToolStripMenuItem, searchToolStripMenuItem, toolStripSeparator8, aboutToolStripMenuItem });
            helpMenu.Name = "helpMenu";
            helpMenu.Size = new Size(44, 20);
            helpMenu.Text = "&Help";
            helpMenu.Visible = false;
            // 
            // contentsToolStripMenuItem
            // 
            contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            contentsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F1;
            contentsToolStripMenuItem.Size = new Size(180, 22);
            contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            indexToolStripMenuItem.Image = (Image)resources.GetObject("indexToolStripMenuItem.Image");
            indexToolStripMenuItem.ImageTransparentColor = Color.Black;
            indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            indexToolStripMenuItem.Size = new Size(180, 22);
            indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.Image = (Image)resources.GetObject("searchToolStripMenuItem.Image");
            searchToolStripMenuItem.ImageTransparentColor = Color.Black;
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(180, 22);
            searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(177, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(180, 22);
            aboutToolStripMenuItem.Text = "&About ... ...";
            // 
            // toolStrip
            // 
            toolStrip.Items.AddRange(new ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, toolStripSeparator1, printToolStripButton, printPreviewToolStripButton, toolStripSeparator2, helpToolStripButton });
            toolStrip.Location = new Point(0, 24);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(737, 25);
            toolStrip.TabIndex = 1;
            toolStrip.Text = "ToolStrip";
            toolStrip.Visible = false;
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = (Image)resources.GetObject("newToolStripButton.Image");
            newToolStripButton.ImageTransparentColor = Color.Black;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Size = new Size(23, 22);
            newToolStripButton.Text = "New";
            newToolStripButton.Click += sAPConnectionToolStripMenuItem_Click;
            // 
            // openToolStripButton
            // 
            openToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            openToolStripButton.Image = (Image)resources.GetObject("openToolStripButton.Image");
            openToolStripButton.ImageTransparentColor = Color.Black;
            openToolStripButton.Name = "openToolStripButton";
            openToolStripButton.Size = new Size(23, 22);
            openToolStripButton.Text = "Open";
            openToolStripButton.Click += salesCentersToolStripMenuItem_Click;
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = (Image)resources.GetObject("saveToolStripButton.Image");
            saveToolStripButton.ImageTransparentColor = Color.Black;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(23, 22);
            saveToolStripButton.Text = "Save";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Black;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(23, 22);
            printToolStripButton.Text = "Print";
            // 
            // printPreviewToolStripButton
            // 
            printPreviewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printPreviewToolStripButton.Image = (Image)resources.GetObject("printPreviewToolStripButton.Image");
            printPreviewToolStripButton.ImageTransparentColor = Color.Black;
            printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            printPreviewToolStripButton.Size = new Size(23, 22);
            printPreviewToolStripButton.Text = "Print Preview";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // helpToolStripButton
            // 
            helpToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            helpToolStripButton.Image = (Image)resources.GetObject("helpToolStripButton.Image");
            helpToolStripButton.ImageTransparentColor = Color.Black;
            helpToolStripButton.Name = "helpToolStripButton";
            helpToolStripButton.Size = new Size(23, 22);
            helpToolStripButton.Text = "Help";
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 501);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(737, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(39, 17);
            toolStripStatusLabel.Text = "Status";
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 523);
            Controls.Add(statusStrip);
            Controls.Add(toolStrip);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "frmMain";
            Text = "ISC";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmMain_FormClosing;
            FormClosed += FrmMain_FormClosed;
            Load += frmMain_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sAPConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salesCentersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolTip toolTip;
        private ToolStripMenuItem syncToolStripMenuItem;
        private ToolStripMenuItem itemMasterToolStripMenuItem;
        private ToolStripMenuItem mnuSyncSettings;
        private ToolStripMenuItem salesQuotationToolStripMenuItem;
        private ToolStripMenuItem qtyConversionForSAPToolStripMenuItem;
        private ToolStripMenuItem userManagerMenu;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem sAPToolStripMenuItem;
        private ToolStripMenuItem sAPQueryToolStripMenuItem;
        private ToolStripMenuItem QuotationAnalyzerToolStripMenuItem;
        private ToolStripMenuItem sAPItemIdentificationConfigToolStripMenuItem;
    }
}



