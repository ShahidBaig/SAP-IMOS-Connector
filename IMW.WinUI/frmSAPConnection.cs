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

				SqlConnection connection = new SqlConnection {
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

				SqlConnection connection = new SqlConnection {
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
            this.lblComments = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbxSAPConnection = new System.Windows.Forms.GroupBox();
            this.chxUseTrusted = new System.Windows.Forms.CheckBox();
            this.cbxDBServerType = new System.Windows.Forms.ComboBox();
            this.lblDBServerType = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblDBPassword = new System.Windows.Forms.Label();
            this.lblDBUserName = new System.Windows.Forms.Label();
            this.lblSLDServer = new System.Windows.Forms.Label();
            this.lblLicenseServer = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblCompanyDB = new System.Windows.Forms.Label();
            this.txtCompanyDB = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtLicenseServer = new System.Windows.Forms.TextBox();
            this.txtSLDServer = new System.Windows.Forms.TextBox();
            this.txtDBUserName = new System.Windows.Forms.TextBox();
            this.txtDBPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.gbxISCConnection = new System.Windows.Forms.GroupBox();
            this.lblISCComments = new System.Windows.Forms.Label();
            this.lblISCDbPassword = new System.Windows.Forms.Label();
            this.lblISCDbUserName = new System.Windows.Forms.Label();
            this.lblISCServer = new System.Windows.Forms.Label();
            this.lblISCCompanyDB = new System.Windows.Forms.Label();
            this.txtISCCompanyDB = new System.Windows.Forms.TextBox();
            this.txtISCServer = new System.Windows.Forms.TextBox();
            this.txtISCDbUserName = new System.Windows.Forms.TextBox();
            this.txtISCDbPassword = new System.Windows.Forms.TextBox();
            this.gbxIMOSConnection = new System.Windows.Forms.GroupBox();
            this.lblIMOSComments = new System.Windows.Forms.Label();
            this.lblIMOSDbPassword = new System.Windows.Forms.Label();
            this.lblIMOSDbUserName = new System.Windows.Forms.Label();
            this.lblIMOSServer = new System.Windows.Forms.Label();
            this.lblIMOSCompanyDB = new System.Windows.Forms.Label();
            this.txtIMOSCompanyDB = new System.Windows.Forms.TextBox();
            this.txtIMOSServer = new System.Windows.Forms.TextBox();
            this.txtIMOSDbUserName = new System.Windows.Forms.TextBox();
            this.txtIMOSDbPassword = new System.Windows.Forms.TextBox();
            this.gbxSAPConnection.SuspendLayout();
            this.gbxISCConnection.SuspendLayout();
            this.gbxIMOSConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(9, 27);
            this.lblComments.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(10, 15);
            this.lblComments.TabIndex = 13;
            this.lblComments.Text = ".";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(232, 377);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 32);
            this.btnSave.TabIndex = 23;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(314, 377);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(77, 32);
            this.btnConnect.TabIndex = 24;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(397, 377);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 32);
            this.btnClose.TabIndex = 25;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gbxSAPConnection
            // 
            this.gbxSAPConnection.Controls.Add(this.chxUseTrusted);
            this.gbxSAPConnection.Controls.Add(this.lblComments);
            this.gbxSAPConnection.Controls.Add(this.cbxDBServerType);
            this.gbxSAPConnection.Controls.Add(this.lblDBServerType);
            this.gbxSAPConnection.Controls.Add(this.lblPassword);
            this.gbxSAPConnection.Controls.Add(this.txtPassword);
            this.gbxSAPConnection.Controls.Add(this.lblUserName);
            this.gbxSAPConnection.Controls.Add(this.lblDBPassword);
            this.gbxSAPConnection.Controls.Add(this.lblDBUserName);
            this.gbxSAPConnection.Controls.Add(this.lblSLDServer);
            this.gbxSAPConnection.Controls.Add(this.lblLicenseServer);
            this.gbxSAPConnection.Controls.Add(this.lblServer);
            this.gbxSAPConnection.Controls.Add(this.lblCompanyDB);
            this.gbxSAPConnection.Controls.Add(this.txtCompanyDB);
            this.gbxSAPConnection.Controls.Add(this.txtServer);
            this.gbxSAPConnection.Controls.Add(this.txtLicenseServer);
            this.gbxSAPConnection.Controls.Add(this.txtSLDServer);
            this.gbxSAPConnection.Controls.Add(this.txtDBUserName);
            this.gbxSAPConnection.Controls.Add(this.txtDBPassword);
            this.gbxSAPConnection.Controls.Add(this.txtUserName);
            this.gbxSAPConnection.Location = new System.Drawing.Point(10, 12);
            this.gbxSAPConnection.Margin = new System.Windows.Forms.Padding(2);
            this.gbxSAPConnection.Name = "gbxSAPConnection";
            this.gbxSAPConnection.Padding = new System.Windows.Forms.Padding(2);
            this.gbxSAPConnection.Size = new System.Drawing.Size(341, 360);
            this.gbxSAPConnection.TabIndex = 26;
            this.gbxSAPConnection.TabStop = false;
            this.gbxSAPConnection.Text = "SAP B1 9.3 PL 12 Connection";
            // 
            // chxUseTrusted
            // 
            this.chxUseTrusted.AutoSize = true;
            this.chxUseTrusted.Location = new System.Drawing.Point(107, 300);
            this.chxUseTrusted.Margin = new System.Windows.Forms.Padding(2);
            this.chxUseTrusted.Name = "chxUseTrusted";
            this.chxUseTrusted.Size = new System.Drawing.Size(83, 19);
            this.chxUseTrusted.TabIndex = 41;
            this.chxUseTrusted.Text = "UseTrusted";
            this.chxUseTrusted.UseVisualStyleBackColor = true;
            // 
            // cbxDBServerType
            // 
            this.cbxDBServerType.FormattingEnabled = true;
            this.cbxDBServerType.Location = new System.Drawing.Point(107, 268);
            this.cbxDBServerType.Margin = new System.Windows.Forms.Padding(2);
            this.cbxDBServerType.Name = "cbxDBServerType";
            this.cbxDBServerType.Size = new System.Drawing.Size(228, 23);
            this.cbxDBServerType.TabIndex = 40;
            // 
            // lblDBServerType
            // 
            this.lblDBServerType.AutoSize = true;
            this.lblDBServerType.Location = new System.Drawing.Point(9, 273);
            this.lblDBServerType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDBServerType.Name = "lblDBServerType";
            this.lblDBServerType.Size = new System.Drawing.Size(78, 15);
            this.lblDBServerType.TabIndex = 39;
            this.lblDBServerType.Text = "DBServerType";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(9, 247);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 38;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(107, 242);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(228, 23);
            this.txtPassword.TabIndex = 37;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(9, 222);
            this.lblUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(62, 15);
            this.lblUserName.TabIndex = 36;
            this.lblUserName.Text = "UserName";
            // 
            // lblDBPassword
            // 
            this.lblDBPassword.AutoSize = true;
            this.lblDBPassword.Location = new System.Drawing.Point(9, 195);
            this.lblDBPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDBPassword.Name = "lblDBPassword";
            this.lblDBPassword.Size = new System.Drawing.Size(72, 15);
            this.lblDBPassword.TabIndex = 35;
            this.lblDBPassword.Text = "DbPassword";
            // 
            // lblDBUserName
            // 
            this.lblDBUserName.AutoSize = true;
            this.lblDBUserName.Location = new System.Drawing.Point(9, 168);
            this.lblDBUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDBUserName.Name = "lblDBUserName";
            this.lblDBUserName.Size = new System.Drawing.Size(77, 15);
            this.lblDBUserName.TabIndex = 34;
            this.lblDBUserName.Text = "DbUserName";
            // 
            // lblSLDServer
            // 
            this.lblSLDServer.AutoSize = true;
            this.lblSLDServer.Location = new System.Drawing.Point(9, 143);
            this.lblSLDServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSLDServer.Name = "lblSLDServer";
            this.lblSLDServer.Size = new System.Drawing.Size(59, 15);
            this.lblSLDServer.TabIndex = 33;
            this.lblSLDServer.Text = "SLDServer";
            // 
            // lblLicenseServer
            // 
            this.lblLicenseServer.AutoSize = true;
            this.lblLicenseServer.Location = new System.Drawing.Point(9, 114);
            this.lblLicenseServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLicenseServer.Name = "lblLicenseServer";
            this.lblLicenseServer.Size = new System.Drawing.Size(78, 15);
            this.lblLicenseServer.TabIndex = 32;
            this.lblLicenseServer.Text = "LicenseServer";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(9, 90);
            this.lblServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(39, 15);
            this.lblServer.TabIndex = 31;
            this.lblServer.Text = "Server";
            // 
            // lblCompanyDB
            // 
            this.lblCompanyDB.AutoSize = true;
            this.lblCompanyDB.Location = new System.Drawing.Point(9, 58);
            this.lblCompanyDB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCompanyDB.Name = "lblCompanyDB";
            this.lblCompanyDB.Size = new System.Drawing.Size(74, 15);
            this.lblCompanyDB.TabIndex = 30;
            this.lblCompanyDB.Text = "ComapnyDB";
            // 
            // txtCompanyDB
            // 
            this.txtCompanyDB.Location = new System.Drawing.Point(107, 55);
            this.txtCompanyDB.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyDB.Name = "txtCompanyDB";
            this.txtCompanyDB.Size = new System.Drawing.Size(228, 23);
            this.txtCompanyDB.TabIndex = 29;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(107, 85);
            this.txtServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(228, 23);
            this.txtServer.TabIndex = 28;
            // 
            // txtLicenseServer
            // 
            this.txtLicenseServer.Location = new System.Drawing.Point(107, 112);
            this.txtLicenseServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtLicenseServer.Name = "txtLicenseServer";
            this.txtLicenseServer.Size = new System.Drawing.Size(228, 23);
            this.txtLicenseServer.TabIndex = 27;
            // 
            // txtSLDServer
            // 
            this.txtSLDServer.Location = new System.Drawing.Point(107, 137);
            this.txtSLDServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtSLDServer.Name = "txtSLDServer";
            this.txtSLDServer.Size = new System.Drawing.Size(228, 23);
            this.txtSLDServer.TabIndex = 26;
            // 
            // txtDBUserName
            // 
            this.txtDBUserName.Location = new System.Drawing.Point(107, 164);
            this.txtDBUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtDBUserName.Name = "txtDBUserName";
            this.txtDBUserName.Size = new System.Drawing.Size(228, 23);
            this.txtDBUserName.TabIndex = 25;
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Location = new System.Drawing.Point(107, 190);
            this.txtDBPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.PasswordChar = '*';
            this.txtDBPassword.Size = new System.Drawing.Size(228, 23);
            this.txtDBPassword.TabIndex = 24;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(107, 217);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(228, 23);
            this.txtUserName.TabIndex = 23;
            // 
            // gbxISCConnection
            // 
            this.gbxISCConnection.Controls.Add(this.lblISCComments);
            this.gbxISCConnection.Controls.Add(this.lblISCDbPassword);
            this.gbxISCConnection.Controls.Add(this.lblISCDbUserName);
            this.gbxISCConnection.Controls.Add(this.lblISCServer);
            this.gbxISCConnection.Controls.Add(this.lblISCCompanyDB);
            this.gbxISCConnection.Controls.Add(this.txtISCCompanyDB);
            this.gbxISCConnection.Controls.Add(this.txtISCServer);
            this.gbxISCConnection.Controls.Add(this.txtISCDbUserName);
            this.gbxISCConnection.Controls.Add(this.txtISCDbPassword);
            this.gbxISCConnection.Location = new System.Drawing.Point(356, 12);
            this.gbxISCConnection.Margin = new System.Windows.Forms.Padding(2);
            this.gbxISCConnection.Name = "gbxISCConnection";
            this.gbxISCConnection.Padding = new System.Windows.Forms.Padding(2);
            this.gbxISCConnection.Size = new System.Drawing.Size(341, 173);
            this.gbxISCConnection.TabIndex = 27;
            this.gbxISCConnection.TabStop = false;
            this.gbxISCConnection.Text = "ISC Connection";
            // 
            // lblISCComments
            // 
            this.lblISCComments.AutoSize = true;
            this.lblISCComments.Location = new System.Drawing.Point(7, 17);
            this.lblISCComments.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblISCComments.Name = "lblISCComments";
            this.lblISCComments.Size = new System.Drawing.Size(10, 15);
            this.lblISCComments.TabIndex = 69;
            this.lblISCComments.Text = ".";
            // 
            // lblISCDbPassword
            // 
            this.lblISCDbPassword.AutoSize = true;
            this.lblISCDbPassword.Location = new System.Drawing.Point(7, 137);
            this.lblISCDbPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblISCDbPassword.Name = "lblISCDbPassword";
            this.lblISCDbPassword.Size = new System.Drawing.Size(72, 15);
            this.lblISCDbPassword.TabIndex = 68;
            this.lblISCDbPassword.Text = "DbPassword";
            // 
            // lblISCDbUserName
            // 
            this.lblISCDbUserName.AutoSize = true;
            this.lblISCDbUserName.Location = new System.Drawing.Point(7, 112);
            this.lblISCDbUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblISCDbUserName.Name = "lblISCDbUserName";
            this.lblISCDbUserName.Size = new System.Drawing.Size(77, 15);
            this.lblISCDbUserName.TabIndex = 67;
            this.lblISCDbUserName.Text = "DbUserName";
            // 
            // lblISCServer
            // 
            this.lblISCServer.AutoSize = true;
            this.lblISCServer.Location = new System.Drawing.Point(7, 81);
            this.lblISCServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblISCServer.Name = "lblISCServer";
            this.lblISCServer.Size = new System.Drawing.Size(39, 15);
            this.lblISCServer.TabIndex = 66;
            this.lblISCServer.Text = "Server";
            // 
            // lblISCCompanyDB
            // 
            this.lblISCCompanyDB.AutoSize = true;
            this.lblISCCompanyDB.Location = new System.Drawing.Point(7, 48);
            this.lblISCCompanyDB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblISCCompanyDB.Name = "lblISCCompanyDB";
            this.lblISCCompanyDB.Size = new System.Drawing.Size(74, 15);
            this.lblISCCompanyDB.TabIndex = 65;
            this.lblISCCompanyDB.Text = "ComapnyDB";
            // 
            // txtISCCompanyDB
            // 
            this.txtISCCompanyDB.Location = new System.Drawing.Point(106, 46);
            this.txtISCCompanyDB.Margin = new System.Windows.Forms.Padding(2);
            this.txtISCCompanyDB.Name = "txtISCCompanyDB";
            this.txtISCCompanyDB.Size = new System.Drawing.Size(228, 23);
            this.txtISCCompanyDB.TabIndex = 64;
            // 
            // txtISCServer
            // 
            this.txtISCServer.Location = new System.Drawing.Point(106, 76);
            this.txtISCServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtISCServer.Name = "txtISCServer";
            this.txtISCServer.Size = new System.Drawing.Size(228, 23);
            this.txtISCServer.TabIndex = 63;
            // 
            // txtISCDbUserName
            // 
            this.txtISCDbUserName.Location = new System.Drawing.Point(106, 107);
            this.txtISCDbUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtISCDbUserName.Name = "txtISCDbUserName";
            this.txtISCDbUserName.Size = new System.Drawing.Size(228, 23);
            this.txtISCDbUserName.TabIndex = 62;
            // 
            // txtISCDbPassword
            // 
            this.txtISCDbPassword.Location = new System.Drawing.Point(106, 133);
            this.txtISCDbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtISCDbPassword.Name = "txtISCDbPassword";
            this.txtISCDbPassword.PasswordChar = '*';
            this.txtISCDbPassword.Size = new System.Drawing.Size(228, 23);
            this.txtISCDbPassword.TabIndex = 61;
            // 
            // gbxIMOSConnection
            // 
            this.gbxIMOSConnection.Controls.Add(this.lblIMOSComments);
            this.gbxIMOSConnection.Controls.Add(this.lblIMOSDbPassword);
            this.gbxIMOSConnection.Controls.Add(this.lblIMOSDbUserName);
            this.gbxIMOSConnection.Controls.Add(this.lblIMOSServer);
            this.gbxIMOSConnection.Controls.Add(this.lblIMOSCompanyDB);
            this.gbxIMOSConnection.Controls.Add(this.txtIMOSCompanyDB);
            this.gbxIMOSConnection.Controls.Add(this.txtIMOSServer);
            this.gbxIMOSConnection.Controls.Add(this.txtIMOSDbUserName);
            this.gbxIMOSConnection.Controls.Add(this.txtIMOSDbPassword);
            this.gbxIMOSConnection.Location = new System.Drawing.Point(356, 192);
            this.gbxIMOSConnection.Margin = new System.Windows.Forms.Padding(2);
            this.gbxIMOSConnection.Name = "gbxIMOSConnection";
            this.gbxIMOSConnection.Padding = new System.Windows.Forms.Padding(2);
            this.gbxIMOSConnection.Size = new System.Drawing.Size(341, 180);
            this.gbxIMOSConnection.TabIndex = 28;
            this.gbxIMOSConnection.TabStop = false;
            this.gbxIMOSConnection.Text = "IMOS Connection";
            // 
            // lblIMOSComments
            // 
            this.lblIMOSComments.AutoSize = true;
            this.lblIMOSComments.Location = new System.Drawing.Point(14, 24);
            this.lblIMOSComments.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIMOSComments.Name = "lblIMOSComments";
            this.lblIMOSComments.Size = new System.Drawing.Size(10, 15);
            this.lblIMOSComments.TabIndex = 57;
            this.lblIMOSComments.Text = ".";
            // 
            // lblIMOSDbPassword
            // 
            this.lblIMOSDbPassword.AutoSize = true;
            this.lblIMOSDbPassword.Location = new System.Drawing.Point(14, 145);
            this.lblIMOSDbPassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIMOSDbPassword.Name = "lblIMOSDbPassword";
            this.lblIMOSDbPassword.Size = new System.Drawing.Size(72, 15);
            this.lblIMOSDbPassword.TabIndex = 56;
            this.lblIMOSDbPassword.Text = "DbPassword";
            // 
            // lblIMOSDbUserName
            // 
            this.lblIMOSDbUserName.AutoSize = true;
            this.lblIMOSDbUserName.Location = new System.Drawing.Point(14, 119);
            this.lblIMOSDbUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIMOSDbUserName.Name = "lblIMOSDbUserName";
            this.lblIMOSDbUserName.Size = new System.Drawing.Size(77, 15);
            this.lblIMOSDbUserName.TabIndex = 55;
            this.lblIMOSDbUserName.Text = "DbUserName";
            // 
            // lblIMOSServer
            // 
            this.lblIMOSServer.AutoSize = true;
            this.lblIMOSServer.Location = new System.Drawing.Point(14, 88);
            this.lblIMOSServer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIMOSServer.Name = "lblIMOSServer";
            this.lblIMOSServer.Size = new System.Drawing.Size(39, 15);
            this.lblIMOSServer.TabIndex = 54;
            this.lblIMOSServer.Text = "Server";
            // 
            // lblIMOSCompanyDB
            // 
            this.lblIMOSCompanyDB.AutoSize = true;
            this.lblIMOSCompanyDB.Location = new System.Drawing.Point(14, 57);
            this.lblIMOSCompanyDB.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblIMOSCompanyDB.Name = "lblIMOSCompanyDB";
            this.lblIMOSCompanyDB.Size = new System.Drawing.Size(74, 15);
            this.lblIMOSCompanyDB.TabIndex = 53;
            this.lblIMOSCompanyDB.Text = "ComapnyDB";
            // 
            // txtIMOSCompanyDB
            // 
            this.txtIMOSCompanyDB.Location = new System.Drawing.Point(106, 53);
            this.txtIMOSCompanyDB.Margin = new System.Windows.Forms.Padding(2);
            this.txtIMOSCompanyDB.Name = "txtIMOSCompanyDB";
            this.txtIMOSCompanyDB.Size = new System.Drawing.Size(228, 23);
            this.txtIMOSCompanyDB.TabIndex = 52;
            // 
            // txtIMOSServer
            // 
            this.txtIMOSServer.Location = new System.Drawing.Point(106, 83);
            this.txtIMOSServer.Margin = new System.Windows.Forms.Padding(2);
            this.txtIMOSServer.Name = "txtIMOSServer";
            this.txtIMOSServer.Size = new System.Drawing.Size(228, 23);
            this.txtIMOSServer.TabIndex = 51;
            // 
            // txtIMOSDbUserName
            // 
            this.txtIMOSDbUserName.Location = new System.Drawing.Point(106, 114);
            this.txtIMOSDbUserName.Margin = new System.Windows.Forms.Padding(2);
            this.txtIMOSDbUserName.Name = "txtIMOSDbUserName";
            this.txtIMOSDbUserName.Size = new System.Drawing.Size(228, 23);
            this.txtIMOSDbUserName.TabIndex = 50;
            // 
            // txtIMOSDbPassword
            // 
            this.txtIMOSDbPassword.Location = new System.Drawing.Point(106, 141);
            this.txtIMOSDbPassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtIMOSDbPassword.Name = "txtIMOSDbPassword";
            this.txtIMOSDbPassword.PasswordChar = '*';
            this.txtIMOSDbPassword.Size = new System.Drawing.Size(228, 23);
            this.txtIMOSDbPassword.TabIndex = 49;
            // 
            // frmSAPConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 421);
            this.ControlBox = false;
            this.Controls.Add(this.gbxIMOSConnection);
            this.Controls.Add(this.gbxISCConnection);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.gbxSAPConnection);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSAPConnection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection SAP B1 9.3 PL 12";
            this.Load += new System.EventHandler(this.frmConnection_Load);
            this.gbxSAPConnection.ResumeLayout(false);
            this.gbxSAPConnection.PerformLayout();
            this.gbxISCConnection.ResumeLayout(false);
            this.gbxISCConnection.PerformLayout();
            this.gbxIMOSConnection.ResumeLayout(false);
            this.gbxIMOSConnection.PerformLayout();
            this.ResumeLayout(false);

        }

        private void SetSettings()
        {
            try
			{
				var configJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

				var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
				var node = JsonNode.Parse(configJson, jsonNodeOptions);
				var options = new JsonSerializerOptions { WriteIndented = true };

				node["AppSettings"]["CompanyDB"] = this.txtCompanyDB.Text;
				node["AppSettings"]["Server"] = this.txtServer.Text;
				node["AppSettings"]["SLDServer"] = this.txtSLDServer.Text;
				node["AppSettings"]["LicenseServer"] = this.txtLicenseServer.Text;
				node["AppSettings"]["DbUserName"] = this.txtDBUserName.Text;
				node["AppSettings"]["DbPassword"] = this.txtDBPassword.Text;
				node["AppSettings"]["UserName"] = this.txtUserName.Text;
				node["AppSettings"]["Password"] = this.txtPassword.Text;
				node["AppSettings"]["DbServerType"] = this.cbxDBServerType.Text;
				node["AppSettings"]["UseTrusted"] = !this.chxUseTrusted.Checked ? "False" : "True";

				File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), node.ToJsonString(options));
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

