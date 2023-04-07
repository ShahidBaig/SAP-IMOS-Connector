using IMW.DAL;
using IMW.DB;
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
    public partial class frmLogin : Form
    {
        DBConnector m_Connection = new DBConnector(HelperDAL.ISCConnectionString);

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtPassword.Clear();
                txtUserName.Clear();

                txtUserName.Focus();
            }
            catch (Exception)
            {

            }
        }
    }
}
