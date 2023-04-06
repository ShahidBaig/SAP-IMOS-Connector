namespace IMW.WinUI
{
    using IMW.DAL;
    using IMW.DB;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmLogin : Form
    {
        DBConnector m_Connection = new DBConnector(HelperDAL.ISCConnectionString);

        private IContainer components = null;
        private TextBox txtUserName;
        private Label lblUserName;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnCancel;
        private Button btnLogin;
        private Label lblComments;

        public frmLogin()
        {
            this.InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //if ((this.txtUserName.Text == "IWD") && (this.txtPassword.Text == "sapimos"))
            //{
            //    base.DialogResult = DialogResult.OK;
            //}
            //else
            //{
            //    this.lblComments.ForeColor = Color.Red;
            //    this.lblComments.Text = "Incorrect Username or Password!";
            //    this.lblUserName.Focus();
            //}

            string l_Param = string.Empty;
            string l_UserID = string.Empty;
            string l_Query = string.Empty;
            string l_Password = String.Empty;
            string l_Menu = string.Empty;
            bool l_AllowAccess = false;
            int l_UserNo = 0;
            Boolean l_Admin = false;
            Boolean l_Blocked = false;
            DataTable l_Data = new DataTable();

            try
            {
                if ((this.txtUserName.Text == "") || (this.txtPassword.Text == ""))
                {
                    this.lblComments.ForeColor = Color.Red;
                    this.lblComments.Text = "Please Provide User Name or Password!";
                    this.lblUserName.Focus();
                    return;
                }
                else
                {
                    l_UserID = this.txtUserName.Text;
                    l_Password = this.txtPassword.Text;

                    PublicFunctions.FieldToParam(l_UserID, ref l_Param, Declarations.FieldTypes.String);
                    l_Query = "SELECT ISNULL(IsAdmin,0) AS AdminUser, ISNULL(Blocked,0) AS Blocked, UserNo FROM Users WITH (NOLOCK) WHERE UserID = " + l_Param;

                    PublicFunctions.FieldToParam(UserManager.Encrypt(l_Password), ref l_Param, Declarations.FieldTypes.String);
                    l_Query += "AND Password = " + l_Param;

                    if (this.m_Connection.GetData(l_Query, ref l_Data))
                    {
                        if (l_Data.Rows.Count > 0)
                        {
                            l_Admin = Convert.ToBoolean(l_Data.Rows[0]["AdminUser"]);
                            l_Blocked = Convert.ToBoolean(l_Data.Rows[0]["Blocked"]);
                            l_UserNo = Convert.ToInt32(l_Data.Rows[0]["UserNo"]);

                            l_Data = new DataTable();

                            if (l_Blocked == true)
                            {
                                this.lblComments.ForeColor = Color.Red;
                                this.lblComments.Text = "User is Blocked";
                                this.lblUserName.Focus();
                                return;
                            }
                            else
                            {
                                PublicFunctions.FieldToParam(l_UserNo, ref l_Param, Declarations.FieldTypes.Number);
                                l_Query = "SELECT * FROM VW_UserMenus WITH (NOLOCK) WHERE UserNo = " + l_Param;

                                if (this.m_Connection.GetData(l_Query, ref l_Data))
                                {
                                    foreach (DataRow row in l_Data.Rows)
                                    {
                                        l_AllowAccess = Convert.ToBoolean(row["AllowAcess"]);
                                        l_Menu = Convert.ToString(row["Caption"]);

                                        if (l_Menu == "Connections" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_Connections = true;
                                        }
                                        else if (l_Menu == "Sales Centers" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_SalesCenters = true;
                                        }
                                        else if (l_Menu == "Sync" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_Sync = true;
                                        }
                                        else if (l_Menu == "Qty Conversion for SAP" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_QtyConversionSAP = true;
                                        }
                                        else if (l_Menu == "SAP Query" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_SAPQuery = true;
                                        }
                                        else if (l_Menu == "Sync Settings" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_SyncSettings = true;
                                        }
                                        else if (l_Menu == "Item Identification Configuration" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_ItemIdentification = true;
                                        }
                                        else if (l_Menu == "Quotation Analyzer" && l_AllowAccess == true)
                                        {
                                            IMW.DB.Declarations.g_QuotationAnalyzer = true;
                                        }
                                    }

                                    if (l_Admin == true)
                                    {
                                        IMW.DB.Declarations.g_UserManager = true;
                                    }

                                    IMW.DB.Declarations.g_File = true;
                                }
                            }

                            base.DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        this.lblComments.ForeColor = Color.Red;
                        this.lblComments.Text = "User ID or Password Incorrect";
                        this.lblUserName.Focus();
                        return;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                l_Data.Dispose();
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

        private void frmLogin_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblComments = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(120, 64);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(245, 23);
            this.txtUserName.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(59, 68);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(44, 15);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "&User ID";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(46, 113);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "&Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(120, 109);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(245, 23);
            this.txtPassword.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(244, 154);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 32);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(136, 154);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(83, 32);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // lblComments
            // 
            this.lblComments.AutoSize = true;
            this.lblComments.Location = new System.Drawing.Point(10, 22);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(10, 15);
            this.lblComments.TabIndex = 14;
            this.lblComments.Text = ".";
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 223);
            this.ControlBox = false;
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login ISC";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Clear();
                txtUserName.Clear();

                txtUserName.Focus();
            }
            catch(Exception)
            {

            }
        }
    }
}

