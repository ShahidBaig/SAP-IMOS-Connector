namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.DAL;
    using IMW.DB;
    using Microsoft.Extensions.Configuration;
    using SAPbobsCOM;
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Windows.Forms;

    public class frmSAPConnection : Form
    {
        private CustomError ce = new CustomError();
        private SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
        private IContainer components = null;
        public Label lblComments;
        private Button btnSave;
        private Button btnConnect;
        public Button btnClose;
        private GroupBox gbxSAPConnection;
        private CheckBox chxUseTrusted;
        private ComboBox cbxDBServerType;
        private Label lblDBServerType;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblUserName;
        private Label lblDBPassword;
        private Label lblDBUserName;
        private Label lblSLDServer;
        private Label lblLicenseServer;
        private Label lblServer;
        private Label lblCompanyDB;
        private TextBox txtCompanyDB;
        private TextBox txtServer;
        private TextBox txtLicenseServer;
        private TextBox txtSLDServer;
        private TextBox txtDBUserName;
        private TextBox txtDBPassword;
        private TextBox txtUserName;
        private GroupBox gbxISCConnection;
        public Label lblISCComments;
        private Label lblISCDbPassword;
        private Label lblISCDbUserName;
        private Label lblISCServer;
        private Label lblISCCompanyDB;
        private TextBox txtISCCompanyDB;
        private TextBox txtISCServer;
        private TextBox txtISCDbUserName;
        private TextBox txtISCDbPassword;
        private GroupBox gbxIMOSConnection;
        public Label lblIMOSComments;
        private Label lblIMOSDbPassword;
        private Label lblIMOSDbUserName;
        private Label lblIMOSServer;
        private Label lblIMOSCompanyDB;
        private TextBox txtIMOSCompanyDB;
        private TextBox txtIMOSServer;
        private TextBox txtIMOSDbUserName;
        private TextBox txtIMOSDbPassword;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        public frmMain main;

        public frmSAPConnection()
        {
            this.InitializeComponent();
        }

        public void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        public void btnConnect_Click(object sender, EventArgs e)
        {
            this.ConnectSAP();
            SAPSettings.oCompany = this.oCompany;

            this.ConnectISC();
            this.ConnectIMOS();

#if !DEBUG
            if (this.lblComments.Text.Contains("Connected") &&
                this.lblISCComments.Text.Contains("Connected") &&
                this.lblIMOSComments.Text.Contains("Connected"))
            {
                if(this.main != null)
                {
                    this.btnSave_Click(sender, e);

                    this.Hide();
                    this.main.PerformLogin();

                    this.btnClose_Click(sender, e);
                }
            }
#endif
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SetSettings();
            this.SetSettingsISC();
            this.SetSettingsIMOS();
        }

        private void ConnectIMOS()
        {
            try
            {
                var appSettings = AppConfiguration.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

                SqlConnection connection = new SqlConnection
                {
                    ConnectionString = appSettings.imosConn
                };
                connection.Open();
                this.lblIMOSComments.ForeColor = Color.Green;
                this.lblIMOSComments.Text = "IMOS Connected Successfully!";
                connection.Close();
            }
            catch (Exception exception)
            {
                this.lblIMOSComments.ForeColor = Color.Red;
                this.lblIMOSComments.Text = exception.Message;
            }
        }

        private void ConnectISC()
        {
            try
            {
                var appSettings = AppConfiguration.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

                SqlConnection connection = new SqlConnection
                {
                    ConnectionString = appSettings.iscConn
                };
                connection.Open();
                this.lblISCComments.ForeColor = Color.Green;
                this.lblISCComments.Text = "ISC Connected Successfully!";
                connection.Close();
            }
            catch (Exception exception)
            {
                this.lblISCComments.ForeColor = Color.Red;
                this.lblISCComments.Text = exception.Message;
            }
        }

        private void ConnectSAP()
        {
            try
            {
                var appSettings = HelperDAL.GetSettings();

                this.oCompany.CompanyDB = appSettings.CompanyDB;
                this.oCompany.Server = appSettings.Server;
                this.oCompany.LicenseServer = appSettings.LicenseServer;
                this.oCompany.SLDServer = appSettings.SLDServer;
                this.oCompany.DbUserName = appSettings.DbUserName;
                this.oCompany.DbPassword = appSettings.DbPassword;
                this.oCompany.UserName = appSettings.UserName;
                this.oCompany.Password = appSettings.Password;

                if (appSettings.DbServerType == "dst_MSSQL2014")
                {
                    this.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                }
                else if (appSettings.DbServerType == "dst_HANADB")
                {
                    this.oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                }
                if (appSettings.UseTrusted == "True")
                {
                    this.oCompany.UseTrusted = true;
                }
                else if (appSettings.UseTrusted == "False")
                {
                    this.oCompany.UseTrusted = false;
                }

                int num = this.oCompany.Connect();
                string lastErrorDescription = this.oCompany.GetLastErrorDescription();

                if (this.oCompany.GetLastErrorCode() != 0)
                {
                    this.lblComments.ForeColor = Color.Red;
                    this.lblComments.Text = lastErrorDescription;
                }
                else
                {
                    this.lblComments.ForeColor = Color.Green;
                    this.lblComments.Text = "Connected to SAP Company : " + this.oCompany.CompanyName;
                }
            }
            catch (Exception exception)
            {
                this.lblComments.ForeColor = Color.Red;
                this.lblComments.Text = exception.ToString();
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

        private void frmConnection_Load(object sender, EventArgs e)
        {
            this.cbxDBServerType.DataSource = Enum.GetValues(typeof(SAPbobsCOM.BoDataServerTypes));
            this.GetSettings();
            this.GetSettingsISC();
            this.GetSettingsIMOS();
        }

        private void GetSettings()
        {
            var appSettings = HelperDAL.GetSettings();

            this.txtCompanyDB.Text = appSettings.CompanyDB;
            this.txtServer.Text = appSettings.Server;
            this.txtSLDServer.Text = appSettings.SLDServer;
            this.txtLicenseServer.Text = appSettings.LicenseServer;
            this.txtDBUserName.Text = appSettings.DbUserName;
            this.txtDBPassword.Text = appSettings.DbPassword;
            this.txtUserName.Text = appSettings.UserName;
            this.txtPassword.Text = appSettings.Password;
            this.cbxDBServerType.Text = appSettings.DbServerType;
            this.chxUseTrusted.Checked = appSettings.UseTrusted == "True";
        }

        private void GetSettingsIMOS()
        {
            var appSettings = AppConfiguration.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            string connectionString = appSettings.imosConn;

            char[] separator = new char[] { ';' };
            char[] chArray2 = new char[] { '=' };
            this.txtIMOSCompanyDB.Text = connectionString.Split(separator).GetValue(1).ToString().Split(chArray2).GetValue(1).ToString();
            char[] chArray3 = new char[] { ';' };
            char[] chArray4 = new char[] { '=' };
            this.txtIMOSServer.Text = connectionString.Split(chArray3).GetValue(0).ToString().Split(chArray4).GetValue(1).ToString();
            char[] chArray5 = new char[] { ';' };
            char[] chArray6 = new char[] { '=' };
            this.txtIMOSDbUserName.Text = connectionString.Split(chArray5).GetValue(2).ToString().Split(chArray6).GetValue(1).ToString();
            char[] chArray7 = new char[] { ';' };
            char[] chArray8 = new char[] { '=' };
            this.txtIMOSDbPassword.Text = connectionString.Split(chArray7).GetValue(3).ToString().ToString().Split(chArray8).GetValue(1).ToString();
        }

        private void GetSettingsISC()
        {
            var appSettings = AppConfiguration.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            string connectionString = appSettings.iscConn;

            char[] separator = new char[] { ';' };
            char[] chArray2 = new char[] { '=' };
            this.txtISCCompanyDB.Text = connectionString.Split(separator).GetValue(1).ToString().Split(chArray2).GetValue(1).ToString();
            char[] chArray3 = new char[] { ';' };
            char[] chArray4 = new char[] { '=' };
            this.txtISCServer.Text = connectionString.Split(chArray3).GetValue(0).ToString().Split(chArray4).GetValue(1).ToString();
            char[] chArray5 = new char[] { ';' };
            char[] chArray6 = new char[] { '=' };
            this.txtISCDbUserName.Text = connectionString.Split(chArray5).GetValue(2).ToString().Split(chArray6).GetValue(1).ToString();
            char[] chArray7 = new char[] { ';' };
            char[] chArray8 = new char[] { '=' };
            this.txtISCDbPassword.Text = connectionString.Split(chArray7).GetValue(3).ToString().ToString().Split(chArray8).GetValue(1).ToString();
        }

        private void InitializeComponent()
        {
            lblComments = new Label();
            btnSave = new Button();
            btnConnect = new Button();
            btnClose = new Button();
            gbxSAPConnection = new GroupBox();
            chxUseTrusted = new CheckBox();
            cbxDBServerType = new ComboBox();
            lblDBServerType = new Label();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblUserName = new Label();
            lblDBPassword = new Label();
            lblDBUserName = new Label();
            lblSLDServer = new Label();
            lblLicenseServer = new Label();
            lblServer = new Label();
            lblCompanyDB = new Label();
            txtCompanyDB = new TextBox();
            txtServer = new TextBox();
            txtLicenseServer = new TextBox();
            txtSLDServer = new TextBox();
            txtDBUserName = new TextBox();
            txtDBPassword = new TextBox();
            txtUserName = new TextBox();
            gbxISCConnection = new GroupBox();
            lblISCComments = new Label();
            lblISCDbPassword = new Label();
            lblISCDbUserName = new Label();
            lblISCServer = new Label();
            lblISCCompanyDB = new Label();
            txtISCCompanyDB = new TextBox();
            txtISCServer = new TextBox();
            txtISCDbUserName = new TextBox();
            txtISCDbPassword = new TextBox();
            gbxIMOSConnection = new GroupBox();
            lblIMOSComments = new Label();
            lblIMOSDbPassword = new Label();
            lblIMOSDbUserName = new Label();
            lblIMOSServer = new Label();
            lblIMOSCompanyDB = new Label();
            txtIMOSCompanyDB = new TextBox();
            txtIMOSServer = new TextBox();
            txtIMOSDbUserName = new TextBox();
            txtIMOSDbPassword = new TextBox();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            gbxSAPConnection.SuspendLayout();
            gbxISCConnection.SuspendLayout();
            gbxIMOSConnection.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lblComments
            // 
            lblComments.AutoSize = true;
            lblComments.Location = new Point(10, 36);
            lblComments.Margin = new Padding(2, 0, 2, 0);
            lblComments.Name = "lblComments";
            lblComments.Size = new Size(12, 20);
            lblComments.TabIndex = 13;
            lblComments.Text = ".";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(250, 6);
            btnSave.Margin = new Padding(2, 3, 2, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 43);
            btnSave.TabIndex = 23;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(344, 6);
            btnConnect.Margin = new Padding(2, 3, 2, 3);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(88, 43);
            btnConnect.TabIndex = 24;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(438, 6);
            btnClose.Margin = new Padding(2, 3, 2, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 43);
            btnClose.TabIndex = 25;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // gbxSAPConnection
            // 
            gbxSAPConnection.Controls.Add(chxUseTrusted);
            gbxSAPConnection.Controls.Add(lblComments);
            gbxSAPConnection.Controls.Add(cbxDBServerType);
            gbxSAPConnection.Controls.Add(lblDBServerType);
            gbxSAPConnection.Controls.Add(lblPassword);
            gbxSAPConnection.Controls.Add(txtPassword);
            gbxSAPConnection.Controls.Add(lblUserName);
            gbxSAPConnection.Controls.Add(lblDBPassword);
            gbxSAPConnection.Controls.Add(lblDBUserName);
            gbxSAPConnection.Controls.Add(lblSLDServer);
            gbxSAPConnection.Controls.Add(lblLicenseServer);
            gbxSAPConnection.Controls.Add(lblServer);
            gbxSAPConnection.Controls.Add(lblCompanyDB);
            gbxSAPConnection.Controls.Add(txtCompanyDB);
            gbxSAPConnection.Controls.Add(txtServer);
            gbxSAPConnection.Controls.Add(txtLicenseServer);
            gbxSAPConnection.Controls.Add(txtSLDServer);
            gbxSAPConnection.Controls.Add(txtDBUserName);
            gbxSAPConnection.Controls.Add(txtDBPassword);
            gbxSAPConnection.Controls.Add(txtUserName);
            gbxSAPConnection.Dock = DockStyle.Left;
            gbxSAPConnection.Location = new Point(0, 0);
            gbxSAPConnection.Margin = new Padding(2, 3, 2, 3);
            gbxSAPConnection.Name = "gbxSAPConnection";
            gbxSAPConnection.Padding = new Padding(2, 3, 2, 3);
            gbxSAPConnection.Size = new Size(390, 509);
            gbxSAPConnection.TabIndex = 26;
            gbxSAPConnection.TabStop = false;
            gbxSAPConnection.Text = "SAP B1 9.3 PL 12 Connection";
            // 
            // chxUseTrusted
            // 
            chxUseTrusted.AutoSize = true;
            chxUseTrusted.Location = new Point(122, 400);
            chxUseTrusted.Margin = new Padding(2, 3, 2, 3);
            chxUseTrusted.Name = "chxUseTrusted";
            chxUseTrusted.Size = new Size(103, 24);
            chxUseTrusted.TabIndex = 41;
            chxUseTrusted.Text = "UseTrusted";
            chxUseTrusted.UseVisualStyleBackColor = true;
            // 
            // cbxDBServerType
            // 
            cbxDBServerType.FormattingEnabled = true;
            cbxDBServerType.Location = new Point(122, 357);
            cbxDBServerType.Margin = new Padding(2, 3, 2, 3);
            cbxDBServerType.Name = "cbxDBServerType";
            cbxDBServerType.Size = new Size(260, 28);
            cbxDBServerType.TabIndex = 40;
            // 
            // lblDBServerType
            // 
            lblDBServerType.AutoSize = true;
            lblDBServerType.Location = new Point(10, 364);
            lblDBServerType.Margin = new Padding(2, 0, 2, 0);
            lblDBServerType.Name = "lblDBServerType";
            lblDBServerType.Size = new Size(101, 20);
            lblDBServerType.TabIndex = 39;
            lblDBServerType.Text = "DBServerType";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(10, 329);
            lblPassword.Margin = new Padding(2, 0, 2, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(70, 20);
            lblPassword.TabIndex = 38;
            lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(122, 323);
            txtPassword.Margin = new Padding(2, 3, 2, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(260, 27);
            txtPassword.TabIndex = 37;
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(10, 296);
            lblUserName.Margin = new Padding(2, 0, 2, 0);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(78, 20);
            lblUserName.TabIndex = 36;
            lblUserName.Text = "UserName";
            // 
            // lblDBPassword
            // 
            lblDBPassword.AutoSize = true;
            lblDBPassword.Location = new Point(10, 260);
            lblDBPassword.Margin = new Padding(2, 0, 2, 0);
            lblDBPassword.Name = "lblDBPassword";
            lblDBPassword.Size = new Size(90, 20);
            lblDBPassword.TabIndex = 35;
            lblDBPassword.Text = "DbPassword";
            // 
            // lblDBUserName
            // 
            lblDBUserName.AutoSize = true;
            lblDBUserName.Location = new Point(10, 224);
            lblDBUserName.Margin = new Padding(2, 0, 2, 0);
            lblDBUserName.Name = "lblDBUserName";
            lblDBUserName.Size = new Size(98, 20);
            lblDBUserName.TabIndex = 34;
            lblDBUserName.Text = "DbUserName";
            // 
            // lblSLDServer
            // 
            lblSLDServer.AutoSize = true;
            lblSLDServer.Location = new Point(10, 191);
            lblSLDServer.Margin = new Padding(2, 0, 2, 0);
            lblSLDServer.Name = "lblSLDServer";
            lblSLDServer.Size = new Size(76, 20);
            lblSLDServer.TabIndex = 33;
            lblSLDServer.Text = "SLDServer";
            // 
            // lblLicenseServer
            // 
            lblLicenseServer.AutoSize = true;
            lblLicenseServer.Location = new Point(10, 152);
            lblLicenseServer.Margin = new Padding(2, 0, 2, 0);
            lblLicenseServer.Name = "lblLicenseServer";
            lblLicenseServer.Size = new Size(98, 20);
            lblLicenseServer.TabIndex = 32;
            lblLicenseServer.Text = "LicenseServer";
            // 
            // lblServer
            // 
            lblServer.AutoSize = true;
            lblServer.Location = new Point(10, 120);
            lblServer.Margin = new Padding(2, 0, 2, 0);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(50, 20);
            lblServer.TabIndex = 31;
            lblServer.Text = "Server";
            // 
            // lblCompanyDB
            // 
            lblCompanyDB.AutoSize = true;
            lblCompanyDB.Location = new Point(10, 77);
            lblCompanyDB.Margin = new Padding(2, 0, 2, 0);
            lblCompanyDB.Name = "lblCompanyDB";
            lblCompanyDB.Size = new Size(92, 20);
            lblCompanyDB.TabIndex = 30;
            lblCompanyDB.Text = "ComapnyDB";
            // 
            // txtCompanyDB
            // 
            txtCompanyDB.Location = new Point(122, 73);
            txtCompanyDB.Margin = new Padding(2, 3, 2, 3);
            txtCompanyDB.Name = "txtCompanyDB";
            txtCompanyDB.Size = new Size(260, 27);
            txtCompanyDB.TabIndex = 29;
            // 
            // txtServer
            // 
            txtServer.Location = new Point(122, 113);
            txtServer.Margin = new Padding(2, 3, 2, 3);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(260, 27);
            txtServer.TabIndex = 28;
            // 
            // txtLicenseServer
            // 
            txtLicenseServer.Location = new Point(122, 149);
            txtLicenseServer.Margin = new Padding(2, 3, 2, 3);
            txtLicenseServer.Name = "txtLicenseServer";
            txtLicenseServer.Size = new Size(260, 27);
            txtLicenseServer.TabIndex = 27;
            // 
            // txtSLDServer
            // 
            txtSLDServer.Location = new Point(122, 183);
            txtSLDServer.Margin = new Padding(2, 3, 2, 3);
            txtSLDServer.Name = "txtSLDServer";
            txtSLDServer.Size = new Size(260, 27);
            txtSLDServer.TabIndex = 26;
            // 
            // txtDBUserName
            // 
            txtDBUserName.Location = new Point(122, 219);
            txtDBUserName.Margin = new Padding(2, 3, 2, 3);
            txtDBUserName.Name = "txtDBUserName";
            txtDBUserName.Size = new Size(260, 27);
            txtDBUserName.TabIndex = 25;
            // 
            // txtDBPassword
            // 
            txtDBPassword.Location = new Point(122, 253);
            txtDBPassword.Margin = new Padding(2, 3, 2, 3);
            txtDBPassword.Name = "txtDBPassword";
            txtDBPassword.PasswordChar = '*';
            txtDBPassword.Size = new Size(260, 27);
            txtDBPassword.TabIndex = 24;
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(122, 289);
            txtUserName.Margin = new Padding(2, 3, 2, 3);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(260, 27);
            txtUserName.TabIndex = 23;
            // 
            // gbxISCConnection
            // 
            gbxISCConnection.Controls.Add(lblISCComments);
            gbxISCConnection.Controls.Add(lblISCDbPassword);
            gbxISCConnection.Controls.Add(lblISCDbUserName);
            gbxISCConnection.Controls.Add(lblISCServer);
            gbxISCConnection.Controls.Add(lblISCCompanyDB);
            gbxISCConnection.Controls.Add(txtISCCompanyDB);
            gbxISCConnection.Controls.Add(txtISCServer);
            gbxISCConnection.Controls.Add(txtISCDbUserName);
            gbxISCConnection.Controls.Add(txtISCDbPassword);
            gbxISCConnection.Dock = DockStyle.Top;
            gbxISCConnection.Location = new Point(0, 0);
            gbxISCConnection.Margin = new Padding(2, 3, 2, 3);
            gbxISCConnection.Name = "gbxISCConnection";
            gbxISCConnection.Padding = new Padding(2, 3, 2, 3);
            gbxISCConnection.Size = new Size(417, 231);
            gbxISCConnection.TabIndex = 27;
            gbxISCConnection.TabStop = false;
            gbxISCConnection.Text = "ISC Connection";
            // 
            // lblISCComments
            // 
            lblISCComments.AutoSize = true;
            lblISCComments.Location = new Point(8, 23);
            lblISCComments.Margin = new Padding(2, 0, 2, 0);
            lblISCComments.Name = "lblISCComments";
            lblISCComments.Size = new Size(12, 20);
            lblISCComments.TabIndex = 69;
            lblISCComments.Text = ".";
            // 
            // lblISCDbPassword
            // 
            lblISCDbPassword.AutoSize = true;
            lblISCDbPassword.Location = new Point(8, 183);
            lblISCDbPassword.Margin = new Padding(2, 0, 2, 0);
            lblISCDbPassword.Name = "lblISCDbPassword";
            lblISCDbPassword.Size = new Size(90, 20);
            lblISCDbPassword.TabIndex = 68;
            lblISCDbPassword.Text = "DbPassword";
            // 
            // lblISCDbUserName
            // 
            lblISCDbUserName.AutoSize = true;
            lblISCDbUserName.Location = new Point(8, 149);
            lblISCDbUserName.Margin = new Padding(2, 0, 2, 0);
            lblISCDbUserName.Name = "lblISCDbUserName";
            lblISCDbUserName.Size = new Size(98, 20);
            lblISCDbUserName.TabIndex = 67;
            lblISCDbUserName.Text = "DbUserName";
            // 
            // lblISCServer
            // 
            lblISCServer.AutoSize = true;
            lblISCServer.Location = new Point(8, 108);
            lblISCServer.Margin = new Padding(2, 0, 2, 0);
            lblISCServer.Name = "lblISCServer";
            lblISCServer.Size = new Size(50, 20);
            lblISCServer.TabIndex = 66;
            lblISCServer.Text = "Server";
            // 
            // lblISCCompanyDB
            // 
            lblISCCompanyDB.AutoSize = true;
            lblISCCompanyDB.Location = new Point(8, 64);
            lblISCCompanyDB.Margin = new Padding(2, 0, 2, 0);
            lblISCCompanyDB.Name = "lblISCCompanyDB";
            lblISCCompanyDB.Size = new Size(92, 20);
            lblISCCompanyDB.TabIndex = 65;
            lblISCCompanyDB.Text = "ComapnyDB";
            // 
            // txtISCCompanyDB
            // 
            txtISCCompanyDB.Location = new Point(121, 61);
            txtISCCompanyDB.Margin = new Padding(2, 3, 2, 3);
            txtISCCompanyDB.Name = "txtISCCompanyDB";
            txtISCCompanyDB.Size = new Size(260, 27);
            txtISCCompanyDB.TabIndex = 64;
            // 
            // txtISCServer
            // 
            txtISCServer.Location = new Point(121, 101);
            txtISCServer.Margin = new Padding(2, 3, 2, 3);
            txtISCServer.Name = "txtISCServer";
            txtISCServer.Size = new Size(260, 27);
            txtISCServer.TabIndex = 63;
            // 
            // txtISCDbUserName
            // 
            txtISCDbUserName.Location = new Point(121, 143);
            txtISCDbUserName.Margin = new Padding(2, 3, 2, 3);
            txtISCDbUserName.Name = "txtISCDbUserName";
            txtISCDbUserName.Size = new Size(260, 27);
            txtISCDbUserName.TabIndex = 62;
            // 
            // txtISCDbPassword
            // 
            txtISCDbPassword.Location = new Point(121, 177);
            txtISCDbPassword.Margin = new Padding(2, 3, 2, 3);
            txtISCDbPassword.Name = "txtISCDbPassword";
            txtISCDbPassword.PasswordChar = '*';
            txtISCDbPassword.Size = new Size(260, 27);
            txtISCDbPassword.TabIndex = 61;
            // 
            // gbxIMOSConnection
            // 
            gbxIMOSConnection.Controls.Add(lblIMOSComments);
            gbxIMOSConnection.Controls.Add(lblIMOSDbPassword);
            gbxIMOSConnection.Controls.Add(lblIMOSDbUserName);
            gbxIMOSConnection.Controls.Add(lblIMOSServer);
            gbxIMOSConnection.Controls.Add(lblIMOSCompanyDB);
            gbxIMOSConnection.Controls.Add(txtIMOSCompanyDB);
            gbxIMOSConnection.Controls.Add(txtIMOSServer);
            gbxIMOSConnection.Controls.Add(txtIMOSDbUserName);
            gbxIMOSConnection.Controls.Add(txtIMOSDbPassword);
            gbxIMOSConnection.Dock = DockStyle.Fill;
            gbxIMOSConnection.Location = new Point(0, 231);
            gbxIMOSConnection.Margin = new Padding(2, 3, 2, 3);
            gbxIMOSConnection.Name = "gbxIMOSConnection";
            gbxIMOSConnection.Padding = new Padding(2, 3, 2, 3);
            gbxIMOSConnection.Size = new Size(417, 278);
            gbxIMOSConnection.TabIndex = 28;
            gbxIMOSConnection.TabStop = false;
            gbxIMOSConnection.Text = "IMOS Connection";
            // 
            // lblIMOSComments
            // 
            lblIMOSComments.AutoSize = true;
            lblIMOSComments.Location = new Point(16, 32);
            lblIMOSComments.Margin = new Padding(2, 0, 2, 0);
            lblIMOSComments.Name = "lblIMOSComments";
            lblIMOSComments.Size = new Size(12, 20);
            lblIMOSComments.TabIndex = 57;
            lblIMOSComments.Text = ".";
            // 
            // lblIMOSDbPassword
            // 
            lblIMOSDbPassword.AutoSize = true;
            lblIMOSDbPassword.Location = new Point(16, 193);
            lblIMOSDbPassword.Margin = new Padding(2, 0, 2, 0);
            lblIMOSDbPassword.Name = "lblIMOSDbPassword";
            lblIMOSDbPassword.Size = new Size(90, 20);
            lblIMOSDbPassword.TabIndex = 56;
            lblIMOSDbPassword.Text = "DbPassword";
            // 
            // lblIMOSDbUserName
            // 
            lblIMOSDbUserName.AutoSize = true;
            lblIMOSDbUserName.Location = new Point(16, 159);
            lblIMOSDbUserName.Margin = new Padding(2, 0, 2, 0);
            lblIMOSDbUserName.Name = "lblIMOSDbUserName";
            lblIMOSDbUserName.Size = new Size(98, 20);
            lblIMOSDbUserName.TabIndex = 55;
            lblIMOSDbUserName.Text = "DbUserName";
            // 
            // lblIMOSServer
            // 
            lblIMOSServer.AutoSize = true;
            lblIMOSServer.Location = new Point(16, 117);
            lblIMOSServer.Margin = new Padding(2, 0, 2, 0);
            lblIMOSServer.Name = "lblIMOSServer";
            lblIMOSServer.Size = new Size(50, 20);
            lblIMOSServer.TabIndex = 54;
            lblIMOSServer.Text = "Server";
            // 
            // lblIMOSCompanyDB
            // 
            lblIMOSCompanyDB.AutoSize = true;
            lblIMOSCompanyDB.Location = new Point(16, 76);
            lblIMOSCompanyDB.Margin = new Padding(2, 0, 2, 0);
            lblIMOSCompanyDB.Name = "lblIMOSCompanyDB";
            lblIMOSCompanyDB.Size = new Size(92, 20);
            lblIMOSCompanyDB.TabIndex = 53;
            lblIMOSCompanyDB.Text = "ComapnyDB";
            // 
            // txtIMOSCompanyDB
            // 
            txtIMOSCompanyDB.Location = new Point(121, 71);
            txtIMOSCompanyDB.Margin = new Padding(2, 3, 2, 3);
            txtIMOSCompanyDB.Name = "txtIMOSCompanyDB";
            txtIMOSCompanyDB.Size = new Size(260, 27);
            txtIMOSCompanyDB.TabIndex = 52;
            // 
            // txtIMOSServer
            // 
            txtIMOSServer.Location = new Point(121, 111);
            txtIMOSServer.Margin = new Padding(2, 3, 2, 3);
            txtIMOSServer.Name = "txtIMOSServer";
            txtIMOSServer.Size = new Size(260, 27);
            txtIMOSServer.TabIndex = 51;
            // 
            // txtIMOSDbUserName
            // 
            txtIMOSDbUserName.Location = new Point(121, 152);
            txtIMOSDbUserName.Margin = new Padding(2, 3, 2, 3);
            txtIMOSDbUserName.Name = "txtIMOSDbUserName";
            txtIMOSDbUserName.Size = new Size(260, 27);
            txtIMOSDbUserName.TabIndex = 50;
            // 
            // txtIMOSDbPassword
            // 
            txtIMOSDbPassword.Location = new Point(121, 188);
            txtIMOSDbPassword.Margin = new Padding(2, 3, 2, 3);
            txtIMOSDbPassword.Name = "txtIMOSDbPassword";
            txtIMOSDbPassword.PasswordChar = '*';
            txtIMOSDbPassword.Size = new Size(260, 27);
            txtIMOSDbPassword.TabIndex = 49;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnConnect);
            panel1.Controls.Add(btnSave);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 509);
            panel1.Name = "panel1";
            panel1.Size = new Size(807, 52);
            panel1.TabIndex = 29;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(gbxSAPConnection);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(807, 509);
            panel2.TabIndex = 30;
            // 
            // panel3
            // 
            panel3.Controls.Add(gbxIMOSConnection);
            panel3.Controls.Add(gbxISCConnection);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(390, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(417, 509);
            panel3.TabIndex = 29;
            // 
            // frmSAPConnection
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(807, 561);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2, 3, 2, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmSAPConnection";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Connection SAP B1 9.3 PL 12";
            Load += frmConnection_Load;
            gbxSAPConnection.ResumeLayout(false);
            gbxSAPConnection.PerformLayout();
            gbxISCConnection.ResumeLayout(false);
            gbxISCConnection.PerformLayout();
            gbxIMOSConnection.ResumeLayout(false);
            gbxIMOSConnection.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        private void SetSettings()
        {
            try
            {
                HelperDAL.SaveTagValue("CompanyDB", txtCompanyDB.Text);
                HelperDAL.SaveTagValue("Server", txtServer.Text);
                HelperDAL.SaveTagValue("SLDServer", txtSLDServer.Text);
                HelperDAL.SaveTagValue("LicenseServer", txtLicenseServer.Text);
                HelperDAL.SaveTagValue("DbUserName", txtDBUserName.Text);
                HelperDAL.SaveTagValue("DbPassword", txtDBPassword.Text);
                HelperDAL.SaveTagValue("UserName", txtUserName.Text);
                HelperDAL.SaveTagValue("Password", txtPassword.Text);
                HelperDAL.SaveTagValue("DbServerType", cbxDBServerType.Text);
                HelperDAL.SaveTagValue("UseTrusted", !this.chxUseTrusted.Checked ? "False" : "True");

                SaleQuotationDAL.appSettings = HelperDAL.GetSettings();
                BOMDAL.appSettings = HelperDAL.GetSettings();

                this.lblComments.ForeColor = Color.Blue;
                this.lblComments.Text = "Configuration Saved Successfully!";
            }
            catch (Exception ex)
            {

            }
        }

        private void SetSettingsIMOS()
        {
            Common l_Common = new Common();

            var configJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

            var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
            var node = JsonNode.Parse(configJson, jsonNodeOptions);
            var options = new JsonSerializerOptions { WriteIndented = true };

            node["ConnectionStrings"]["imosConn"] = $"Server={this.txtIMOSServer.Text};Database={this.txtIMOSCompanyDB.Text};user id={this.txtIMOSDbUserName.Text};password={this.txtIMOSDbPassword.Text}";

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), node.ToJsonString(options));

            l_Common.UseConnection(HelperDAL.ISCConnectionString);

            l_Common.Execut($"UPDATE IMOSCenters SET SQLServer = '{this.txtIMOSServer.Text}', DatabaseName = '{this.txtIMOSCompanyDB.Text}', DBUserName = '{this.txtIMOSDbUserName.Text}', DBPassword = '{this.txtIMOSDbPassword.Text}'");

            this.lblComments.ForeColor = Color.Blue;
            this.lblComments.Text = "Configuration Saved Successfully!";
        }

        private void SetSettingsISC()
        {
            var configJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

            var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
            var node = JsonNode.Parse(configJson, jsonNodeOptions);
            var options = new JsonSerializerOptions { WriteIndented = true };

            node["ConnectionStrings"]["iscConn"] = $"Server={this.txtISCServer.Text};Database={this.txtISCCompanyDB.Text};user id={this.txtISCDbUserName.Text};password={this.txtISCDbPassword.Text}";

            Declarations.g_ConnectionString = $"Server={this.txtISCServer.Text};Database={this.txtISCCompanyDB.Text};user id={this.txtISCDbUserName.Text};password={this.txtISCDbPassword.Text}";

            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), node.ToJsonString(options));

            this.lblComments.ForeColor = Color.Blue;
            this.lblComments.Text = "Configuration Saved Successfully!";
        }
    }
}

