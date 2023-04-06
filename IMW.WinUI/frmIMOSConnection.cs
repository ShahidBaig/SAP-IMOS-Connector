namespace IMW.WinUI
{
    using IMW.Common;
    using IMW.WinUI.Properties;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using System.Windows.Forms;

    public class frmIMOSConnection : Form
    {
        private IContainer components = null;
        private Button btnClose;
        private Button btnConnect;
        private Button btnSave;
        private Label lblComments;
        private Label lblDBPassword;
        private Label lblDBUserName;
        private Label lblServer;
        private Label lblCompanyDB;
        private TextBox txtCompanyDB;
        private TextBox txtServer;
        private TextBox txtDBUserName;
        private TextBox txtDBPassword;

        public frmIMOSConnection()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.ConnectIMOS();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
                this.lblComments.ForeColor = Color.Green;
                this.lblComments.Text = "IMOS Connected Successfully!";
                connection.Close();
            }
            catch (Exception exception)
            {
                this.lblComments.ForeColor = Color.Red;
                this.lblComments.Text = exception.Message;
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

        private void frmIMOSConnection_Load(object sender, EventArgs e)
        {
            this.GetSettingsIMOS();
        }

        private void GetSettingsIMOS()
        {
			var appSettings = AppConfiguration.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

			string connectionString = appSettings.imosConn;
			char[] separator = new char[] { ';' };
            char[] chArray2 = new char[] { '=' };
            this.txtCompanyDB.Text = connectionString.Split(separator).GetValue(1).ToString().Split(chArray2).GetValue(1).ToString();
            char[] chArray3 = new char[] { ';' };
            char[] chArray4 = new char[] { '=' };
            this.txtServer.Text = connectionString.Split(chArray3).GetValue(0).ToString().Split(chArray4).GetValue(1).ToString();
            char[] chArray5 = new char[] { ';' };
            char[] chArray6 = new char[] { '=' };
            this.txtDBUserName.Text = connectionString.Split(chArray5).GetValue(2).ToString().Split(chArray6).GetValue(1).ToString();
            char[] chArray7 = new char[] { ';' };
            char[] chArray8 = new char[] { '=' };
            this.txtDBPassword.Text = connectionString.Split(chArray7).GetValue(3).ToString().ToString().Split(chArray8).GetValue(1).ToString();
        }

        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblComments = new System.Windows.Forms.Label();
            this.lblDBPassword = new System.Windows.Forms.Label();
            this.lblDBUserName = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblCompanyDB = new System.Windows.Forms.Label();
            this.txtCompanyDB = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.txtDBUserName = new System.Windows.Forms.TextBox();
            this.txtDBPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(293, 113);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(77, 32);
            this.btnClose.TabIndex = 48;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(293, 76);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(77, 32);
            this.btnConnect.TabIndex = 47;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(293, 36);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 32);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(11, 8);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(10, 15);
            this.lblComments.TabIndex = 39;
            this.lblComments.Text = ".";
            // 
            // lblDBPassword
            // 
            this.lblDBPassword.AutoSize = true;
            this.lblDBPassword.Location = new System.Drawing.Point(11, 129);
            this.lblDBPassword.Name = "lblDBPassword";
            this.lblDBPassword.Size = new System.Drawing.Size(72, 15);
            this.lblDBPassword.TabIndex = 38;
            this.lblDBPassword.Text = "DbPassword";
            // 
            // lblDBUserName
            // 
            this.lblDBUserName.AutoSize = true;
            this.lblDBUserName.Location = new System.Drawing.Point(11, 103);
            this.lblDBUserName.Name = "lblDBUserName";
            this.lblDBUserName.Size = new System.Drawing.Size(77, 15);
            this.lblDBUserName.TabIndex = 37;
            this.lblDBUserName.Text = "DbUserName";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(11, 72);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(39, 15);
            this.lblServer.TabIndex = 34;
            this.lblServer.Text = "Server";
            // 
            // lblCompanyDB
            // 
            this.lblCompanyDB.AutoSize = true;
            this.lblCompanyDB.Location = new System.Drawing.Point(11, 40);
            this.lblCompanyDB.Name = "lblCompanyDB";
            this.lblCompanyDB.Size = new System.Drawing.Size(74, 15);
            this.lblCompanyDB.TabIndex = 33;
            this.lblCompanyDB.Text = "ComapnyDB";
            // 
            // txtCompanyDB
            // 
            this.txtCompanyDB.Location = new System.Drawing.Point(110, 38);
            this.txtCompanyDB.Name = "txtCompanyDB";
            this.txtCompanyDB.Size = new System.Drawing.Size(155, 23);
            this.txtCompanyDB.TabIndex = 32;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(110, 68);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(155, 23);
            this.txtServer.TabIndex = 31;
            // 
            // txtDBUserName
            // 
            this.txtDBUserName.Location = new System.Drawing.Point(110, 98);
            this.txtDBUserName.Name = "txtDBUserName";
            this.txtDBUserName.Size = new System.Drawing.Size(155, 23);
            this.txtDBUserName.TabIndex = 28;
            // 
            // txtDBPassword
            // 
            this.txtDBPassword.Location = new System.Drawing.Point(110, 125);
            this.txtDBPassword.Name = "txtDBPassword";
            this.txtDBPassword.Size = new System.Drawing.Size(155, 23);
            this.txtDBPassword.TabIndex = 27;
            // 
            // frmIMOSConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 187);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.lblDBPassword);
            this.Controls.Add(this.lblDBUserName);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblCompanyDB);
            this.Controls.Add(this.txtCompanyDB);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.txtDBUserName);
            this.Controls.Add(this.txtDBPassword);
            this.Name = "frmIMOSConnection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connection IMOS 19";
            this.Load += new System.EventHandler(this.frmIMOSConnection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SetSettingsIMOS()
		{
			var configJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

			var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
			var node = JsonNode.Parse(configJson, jsonNodeOptions);
			var options = new JsonSerializerOptions { WriteIndented = true };

			node["ConnectionStrings"]["imosConn"] = $"Server={this.txtServer.Text};Database={this.txtCompanyDB.Text};user id={this.txtDBUserName.Text};password={this.txtDBPassword.Text}";

			File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), node.ToJsonString(options));
			this.lblComments.ForeColor = Color.Blue;
			this.lblComments.Text = "Configuration Saved Successfully!";
		}
    }
}

