using IMW.Common;
using IMW.DAL;
using IMW.DB;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMW.WinUI
{
    public partial class frmMain : Form
    {
        private int childFormNumber = 0;

        public frmMain()
        {
            InitializeComponent();
            this.Hide();

            new frmSplash().ShowDialog(this);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
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
            IMW.DB.Common l_Common = new IMW.DB.Common();
            string l_Query = string.Empty;
            DataTable l_Data = new DataTable();
            string l_ScriptFile = string.Empty;

            try
            {
                foreach (SalesCenters item in new SalesCenterDAL().GetSalesCenters())
                {
                    Declarations.g_ConnectionString = (new SalesCenterDAL().GetConnectionString(item)) + ";TrustServerCertificate=True;";

                    Microsoft.Data.SqlClient.SqlConnection l_Conn = new Microsoft.Data.SqlClient.SqlConnection(Declarations.g_ConnectionString);
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
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

        private void salesCentersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSalesUnitConnections().Show(this);
        }

        private void sAPConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSAPConnection().Show(this);
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
