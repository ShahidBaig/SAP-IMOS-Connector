namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using IMW.DB;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using Microsoft.SqlServer.Management.Smo;
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.Extensions.Configuration;

    public class frmMainScreen : Form
    {
        private IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem aboutMiddleWareToolStripMenuItem;
        private ToolStripMenuItem exitMiddleWareToolStripMenuItem;
        private ToolStripMenuItem sAPConnectionToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem syncToolStripMenuItem;
        private ToolStripMenuItem itemMasterToolStripMenuItem;
        private ToolStripMenuItem salesCentersToolStripMenuItem;
        private ToolStripMenuItem sAPToISCToolStripMenuItem;
        private ToolStripMenuItem tsmSIPause;
        private ToolStripMenuItem tsmSIStop;
        private ToolStripMenuItem tsmSIStart;
        private ToolStripMenuItem iSCToIMOSToolStripMenuItem;
        private ToolStripMenuItem tsmISPause;
        private ToolStripMenuItem tsmISStop;
        private ToolStripMenuItem tsmISStart;
        private ToolStripMenuItem qtyConversionForSAPToolStripMenuItem;
        private ToolStripMenuItem userManagerMenu;
        private ToolStripMenuItem sAPQueryToolStripMenuItem;
        private ToolStripMenuItem sAPItemIdentificationConfigToolStripMenuItem;
        private ToolStripMenuItem QuotationAnalyzerToolStripMenuItem;
        private ToolStripMenuItem mnuSyncSettings;
        private ImageList imageList1;

        public frmMainScreen()
        {
            this.InitializeComponent();
            this.Hide();

            new frmSplash().ShowDialog();
        }

        private void aboutMiddleWareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().Show(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void exitMiddleWareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if !DEBUG
            frmSAPConnection conn = new frmSAPConnection();

            conn.btnClose.Visible = false;
            conn.main = this;

            conn.Show(this);
            conn.btnConnect_Click(sender, e);
#else
            this.PerformLogin();
#endif            
        }

        public void PerformLogin()
        {
            frmLogin login = new frmLogin();

            login.ShowDialog(this);
            if (login.DialogResult == DialogResult.OK)
            {
                login.Close();

                this.LoadMenus();

                this.ExecuteIMOSScripts();
            }
        }

        private void ExecuteIMOSScripts()
        {
            Common l_Common = new Common();
            string l_Query = string.Empty;
            DataTable l_Data = new DataTable();
            string l_ScriptFile = string.Empty;

            try
            {
                foreach (SalesCenters item in new SalesCenterDAL().GetSalesCenters())
                {
                    Declarations.g_ConnectionString = (new SalesCenterDAL().GetConnectionString(item)) + ";TrustServerCertificate=True;";

                    SqlConnection l_Conn = new SqlConnection(Declarations.g_ConnectionString);
                    Server l_Server = new Server(new ServerConnection(l_Conn));

                    l_Query = "SELECT name FROM sys.tables WHERE name = 'ISCVersions'";
                    if (!l_Common.GetList(l_Query, ref l_Data))
                    {
                        l_Query = File.ReadAllText(Path.Combine(Application.StartupPath, "Scripts\\Versions\\Versions.sql"));
                        l_Server.ConnectionContext.ExecuteNonQuery(l_Query);
                    }

                    l_Data.Dispose();
                    l_Data = new DataTable();

                    l_Query = "SELECT VersionNo FROM ISCVersions ORDER BY VersionNo";
                    l_Common.GetList(l_Query, ref l_Data);

                    foreach (string folder in Directory.GetDirectories(Path.Combine(Application.StartupPath, "Scripts"), "2*"))
                    {
                        if (l_Data.Select("VersionNo = '" + folder.Replace(Path.GetDirectoryName(folder) + "\\", string.Empty) + "'").Length == 0)
                        {
                            foreach (string file in Directory.GetFiles(Path.Combine(folder, "Deploy"), "*.sql"))
                            {
                                l_ScriptFile = Path.GetFileName(file);

                                l_Query = File.ReadAllText(file);
                                l_Server.ConnectionContext.ExecuteNonQuery(l_Query);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show($"Script execution failed for [{l_ScriptFile}]", "IMOS Scripts", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMainScreen_Resize(object sender, EventArgs e)
        {
            if (base.WindowState == FormWindowState.Minimized)
            {
                base.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmMainScreen));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            sAPConnectionToolStripMenuItem = new ToolStripMenuItem();
            salesCentersToolStripMenuItem = new ToolStripMenuItem();
            aboutMiddleWareToolStripMenuItem = new ToolStripMenuItem();
            exitMiddleWareToolStripMenuItem = new ToolStripMenuItem();
            syncToolStripMenuItem = new ToolStripMenuItem();
            itemMasterToolStripMenuItem = new ToolStripMenuItem();
            mnuSyncSettings = new System.Windows.Forms.ToolStripMenuItem();
            qtyConversionForSAPToolStripMenuItem = new ToolStripMenuItem();
            userManagerMenu = new ToolStripMenuItem();
            sAPQueryToolStripMenuItem = new ToolStripMenuItem();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            sAPToISCToolStripMenuItem = new ToolStripMenuItem();
            tsmSIPause = new ToolStripMenuItem();
            tsmSIStop = new ToolStripMenuItem();
            tsmSIStart = new ToolStripMenuItem();
            iSCToIMOSToolStripMenuItem = new ToolStripMenuItem();
            tsmISPause = new ToolStripMenuItem();
            tsmISStop = new ToolStripMenuItem();
            tsmISStart = new ToolStripMenuItem();
            imageList1 = new ImageList(components);
            this.sAPItemIdentificationConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.QuotationAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, syncToolStripMenuItem, qtyConversionForSAPToolStripMenuItem, userManagerMenu, sAPQueryToolStripMenuItem, this.QuotationAnalyzerToolStripMenuItem,
            this.sAPItemIdentificationConfigToolStripMenuItem});
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 3, 0, 3);
            menuStrip1.Size = new Size(1157, 30);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sAPConnectionToolStripMenuItem, salesCentersToolStripMenuItem, aboutMiddleWareToolStripMenuItem, exitMiddleWareToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            fileToolStripMenuItem.Visible = false;
            // 
            // sAPConnectionToolStripMenuItem
            // 
            sAPConnectionToolStripMenuItem.Name = "sAPConnectionToolStripMenuItem";
            sAPConnectionToolStripMenuItem.Size = new Size(179, 26);
            sAPConnectionToolStripMenuItem.Text = "Connections";
            sAPConnectionToolStripMenuItem.Click += sAPConnectionToolStripMenuItem_Click;
            sAPConnectionToolStripMenuItem.Visible = false;
            // 
            // salesCentersToolStripMenuItem
            // 
            salesCentersToolStripMenuItem.Name = "salesCentersToolStripMenuItem";
            salesCentersToolStripMenuItem.Size = new Size(179, 26);
            salesCentersToolStripMenuItem.Text = "Sales Centers";
            salesCentersToolStripMenuItem.Click += salesCentersToolStripMenuItem_Click;
            salesCentersToolStripMenuItem.Visible = false;
            // 
            // aboutMiddleWareToolStripMenuItem
            // 
            aboutMiddleWareToolStripMenuItem.Name = "aboutMiddleWareToolStripMenuItem";
            aboutMiddleWareToolStripMenuItem.Size = new Size(179, 26);
            aboutMiddleWareToolStripMenuItem.Text = "About ISC";
            aboutMiddleWareToolStripMenuItem.Click += aboutMiddleWareToolStripMenuItem_Click;
            // 
            // exitMiddleWareToolStripMenuItem
            // 
            exitMiddleWareToolStripMenuItem.Name = "exitMiddleWareToolStripMenuItem";
            exitMiddleWareToolStripMenuItem.Size = new Size(179, 26);
            exitMiddleWareToolStripMenuItem.Text = "Exit ISC";
            exitMiddleWareToolStripMenuItem.Click += exitMiddleWareToolStripMenuItem_Click;
            // 
            this.QuotationAnalyzerToolStripMenuItem.Name = "QuotationAnalyzerToolStripMenuItem";
            this.QuotationAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.QuotationAnalyzerToolStripMenuItem.Text = "Quotation Analyzer";
            this.QuotationAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.QuotationAnalyzer_Click);
            this.QuotationAnalyzerToolStripMenuItem.Visible = false;
            // syncToolStripMenuItem
            // 
            this.syncToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemMasterToolStripMenuItem,
            this.mnuSyncSettings});
            syncToolStripMenuItem.Name = "syncToolStripMenuItem";
            syncToolStripMenuItem.Size = new Size(53, 24);
            syncToolStripMenuItem.Text = "Sync";
            syncToolStripMenuItem.Visible = false;
            // 
            // itemMasterToolStripMenuItem
            // 
            itemMasterToolStripMenuItem.Name = "itemMasterToolStripMenuItem";
            itemMasterToolStripMenuItem.Size = new Size(224, 26);
            itemMasterToolStripMenuItem.Text = "Item Master";
            itemMasterToolStripMenuItem.Click += itemMasterToolStripMenuItem_Click;
            // 
            // mnuSyncSettings
            // 
            this.mnuSyncSettings.Name = "mnuSyncSettings";
            this.mnuSyncSettings.Size = new System.Drawing.Size(180, 22);
            this.mnuSyncSettings.Text = "Settings";
            this.mnuSyncSettings.Click += new System.EventHandler(this.mnuSyncSettings_Click);
            // 
            // qtyConversionForSAPToolStripMenuItem
            // 
            qtyConversionForSAPToolStripMenuItem.Name = "qtyConversionForSAPToolStripMenuItem";
            qtyConversionForSAPToolStripMenuItem.Size = new Size(176, 24);
            qtyConversionForSAPToolStripMenuItem.Text = "Qty Conversion for SAP";
            qtyConversionForSAPToolStripMenuItem.Click += qtyConversionForSAPToolStripMenuItem_Click;
            qtyConversionForSAPToolStripMenuItem.Visible = false;
            // 
            // userManagerMenu
            // 
            userManagerMenu.Name = "userManagerMenu";
            userManagerMenu.Size = new Size(176, 24);
            userManagerMenu.Text = "User Manager";
            userManagerMenu.Click += userManagerMenu_Click;
            userManagerMenu.Visible = false;
            // 

            // sAPQueryToolStripMenuItem
            // 
            sAPQueryToolStripMenuItem.Name = "sAPQueryToolStripMenuItem";
            sAPQueryToolStripMenuItem.Size = new Size(92, 24);
            sAPQueryToolStripMenuItem.Text = "SAP Query";
            sAPQueryToolStripMenuItem.Click += sAPQueryToolStripMenuItem_Click;
            sAPQueryToolStripMenuItem.Visible = false;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Text = "ISC - IMOS and SAP Communicator";
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { sAPToISCToolStripMenuItem, iSCToIMOSToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(163, 52);
            // 
            // sAPToISCToolStripMenuItem
            // 
            sAPToISCToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmSIPause, tsmSIStop, tsmSIStart });
            sAPToISCToolStripMenuItem.Name = "sAPToISCToolStripMenuItem";
            sAPToISCToolStripMenuItem.Size = new Size(162, 24);
            sAPToISCToolStripMenuItem.Text = "SAP to IMOS";
            // 
            // tsmSIPause
            // 
            tsmSIPause.Name = "tsmSIPause";
            tsmSIPause.Size = new Size(129, 26);
            tsmSIPause.Text = "Pause";
            // 
            // tsmSIStop
            // 
            tsmSIStop.Name = "tsmSIStop";
            tsmSIStop.Size = new Size(129, 26);
            tsmSIStop.Text = "Stop";
            // 
            // tsmSIStart
            // 
            tsmSIStart.Name = "tsmSIStart";
            tsmSIStart.Size = new Size(129, 26);
            tsmSIStart.Text = "Start";
            // 
            // iSCToIMOSToolStripMenuItem
            // 
            iSCToIMOSToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmISPause, tsmISStop, tsmISStart });
            iSCToIMOSToolStripMenuItem.Name = "iSCToIMOSToolStripMenuItem";
            iSCToIMOSToolStripMenuItem.Size = new Size(162, 24);
            iSCToIMOSToolStripMenuItem.Text = "IMOS to SAP";
            // 
            // tsmISPause
            // 
            tsmISPause.Name = "tsmISPause";
            tsmISPause.Size = new Size(129, 26);
            tsmISPause.Text = "Pause";
            // 
            // tsmISStop
            // 
            tsmISStop.Name = "tsmISStop";
            tsmISStop.Size = new Size(129, 26);
            tsmISStop.Text = "Stop";
            // 
            // tsmISStart
            // 
            tsmISStart.Name = "tsmISStart";
            tsmISStart.Size = new Size(129, 26);
            tsmISStart.Text = "Start";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // sAPItemIdentificationConfigToolStripMenuItem
            // 
            this.sAPItemIdentificationConfigToolStripMenuItem.Name = "sAPItemIdentificationConfigToolStripMenuItem";
            this.sAPItemIdentificationConfigToolStripMenuItem.Size = new System.Drawing.Size(179, 20);
            this.sAPItemIdentificationConfigToolStripMenuItem.Text = "SAP Item Identification Config";
            this.sAPItemIdentificationConfigToolStripMenuItem.Click += new System.EventHandler(this.sAPItemIdentificationConfigToolStripMenuItem_Click);
            this.sAPItemIdentificationConfigToolStripMenuItem.Visible = false;
            // 
            // frmMainScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1157, 783);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            MainMenuStrip = menuStrip1;
            Name = "frmMainScreen";
            Text = "ISC - IMOS and SAP Communicator";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            Resize += frmMainScreen_Resize;
            FormClosed += FrmMainScreen_FormClosed;
            FormClosing += FrmMainScreen_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void FrmMainScreen_FormClosing(object? sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (Form frm in this.MdiChildren)
                {
                    frm.Close();
                }
            }
            catch (Exception)
            { }
        }

        private void FrmMainScreen_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void itemMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmItemMasterSync().Show(this);
        }

        private void LoadMenus()
        {
            sAPConnectionToolStripMenuItem.Visible = Declarations.g_Connections;
            salesCentersToolStripMenuItem.Visible = Declarations.g_SalesCenters;
            syncToolStripMenuItem.Visible = Declarations.g_Sync;
            qtyConversionForSAPToolStripMenuItem.Visible = Declarations.g_QtyConversionSAP;
            sAPQueryToolStripMenuItem.Visible = Declarations.g_SAPQuery;
            mnuSyncSettings.Visible = Declarations.g_SyncSettings;
            userManagerMenu.Visible = Declarations.g_UserManager;
            fileToolStripMenuItem.Visible = Declarations.g_File;
            sAPItemIdentificationConfigToolStripMenuItem.Visible = Declarations.g_ItemIdentification;
            QuotationAnalyzerToolStripMenuItem.Visible = Declarations.g_QuotationAnalyzer;
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            base.Show();
            base.WindowState = FormWindowState.Maximized;
            this.notifyIcon1.Visible = false;
        }

        private void salesCentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSalesUnitConnections().Show(this);
        }

        private void sAPConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSAPConnection().Show(this);
        }

        private void warehouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmWarehouses().Show(this);
        }

        private void QuotationAnalyzer_Click(object sender, EventArgs e)
        {
            new QuotationAnalyzer().Show(this);
        }

        private void qtyConversionForSAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmQtyConversionFormula l_frmQtyConversionFormula = new frmQtyConversionFormula();
                l_frmQtyConversionFormula.Show(this);
            }
            catch (Exception ex)
            {

            }
        }

        private void userManagerMenu_Click(object sender, EventArgs e)
        {
            try
            {
                UserManager l_UserManager = new UserManager();
                l_UserManager.Show(this);
            }
            catch (Exception ex)
            {

            }
        }

        private void sAPQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                SAPConnectivity l_SAPConnectivity = new SAPConnectivity();
                l_SAPConnectivity.Show(this);
            }
            catch (Exception ex)
            {

            }
        }
   
        private void sAPItemIdentificationConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SAPItemIdentifier l_SAPItemIdentifier = new SAPItemIdentifier();

                l_SAPItemIdentifier.Show(this);
            }
            catch (Exception ex)
            {

            }
        }

        private void mnuSyncSettings_Click(object sender, EventArgs e)
        {
            try
            {
                frmSyncSettings l_frmSyncSettings = new frmSyncSettings();
                
                l_frmSyncSettings.Show(this);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

