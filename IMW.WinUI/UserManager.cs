using IMW.Common;
using IMW.DAL;
using IMW.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMW.WinUI
{
    public partial class UserManager : Form
    {
        DBConnector m_Connection = new DBConnector(HelperDAL.ISCConnectionString);
        int m_UserNo = 0;

        public UserManager()
        {
            InitializeComponent();
            FirstName.ReadOnly = true;
            LastName.ReadOnly = true;
            Password.ReadOnly = true;
            IsAdmin.Enabled = false;
            Blocked.Enabled = false;
            Modify.Enabled = false;
            Save.Enabled = false;
            Cancel.Enabled = false;
            Delete.Enabled = false;
            New.Enabled = true;
        }

        private void UserManager_Load(object sender, EventArgs e)
        {

        }

        private void UserIDKeyPress(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == 13 && Save.Enabled == false && Cancel.Enabled == false)
            {
                DataTable l_Data = new DataTable();
                string l_Query = string.Empty;
                string l_Param = string.Empty;
                string l_UserID = string.Empty;

                l_UserID = UserID.Text;

                PublicFunctions.FieldToParam(l_UserID, ref l_Param, Declarations.FieldTypes.String);
                l_Query = "SELECT * FROM Users WITH (NOLOCK) WHERE UserID = " + l_Param;

                if (this.m_Connection.GetData(l_Query, ref l_Data))
                {
                    if (l_Data.Rows.Count > 0)
                    {
                        //if 
                        m_UserNo = Convert.ToInt32(l_Data.Rows[0]["UserNo"]);
                        FirstName.Text = Convert.ToString(l_Data.Rows[0]["FirstName"]);
                        LastName.Text = Convert.ToString(l_Data.Rows[0]["LastName"]);
                        Password.Text = Encrypt(l_Data.Rows[0]["Password"].ToString());
                        IsAdmin.Checked = Convert.ToBoolean(l_Data.Rows[0]["IsAdmin"]);
                        Blocked.Checked = Convert.ToBoolean(l_Data.Rows[0]["Blocked"]);
                    }

                    PublicFunctions.FieldToParam(m_UserNo, ref l_Param, Declarations.FieldTypes.String);
                    l_Query = "SELECT MenuNo,MenuID, Caption AS Menu, AllowAcess FROM VW_UserMenus WHERE UserNo = " + l_Param;

                    if (this.m_Connection.GetData(l_Query, ref l_Data))
                    {
                        if (l_Data.Rows.Count > 0)
                        {
                            this.menuDetailGrid.DataSource = null;
                            menuDetailGrid.DataSource = l_Data;

                            this.menuDetailGrid.AllowUserToAddRows = false;
                            this.menuDetailGrid.RowHeadersVisible = false;
                            this.menuDetailGrid.AllowUserToAddRows = false;
                            this.menuDetailGrid.RowHeadersVisible = false;
                            this.menuDetailGrid.Columns[0].Visible = false;
                            this.menuDetailGrid.Columns[1].Visible = false;
                        }
                    }

                    FirstName.ReadOnly = true;
                    LastName.ReadOnly = true;
                    Password.ReadOnly = true;
                    IsAdmin.Enabled = false;
                    Blocked.Enabled = false;
                    Modify.Enabled = true;
                    Delete.Enabled = true;
                    New.Enabled = true;
                    Save.Enabled = false;
                    Cancel.Enabled = false;
                }
                else
                {
                    MessageBox.Show("User doesn't exists");
                    return;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void IsAdmin_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Modify_Click(object sender, EventArgs e)
        {
            UserID.ReadOnly = true;
            FirstName.ReadOnly = false;
            LastName.ReadOnly = false;
            Password.ReadOnly = false;
            IsAdmin.Enabled = true;
            Blocked.Enabled = true;
            Modify.Enabled = false;
            Delete.Enabled = false;
            New.Enabled = false;
            Cancel.Enabled = true;
            Save.Enabled = true;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                string l_Query = string.Empty;
                string l_Param = string.Empty;
                DataTable l_Data = new DataTable();

                int l_UserNo = 0;
                string l_UserID = UserID.Text;
                string l_Password = Encrypt(Password.Text);
                string l_FirstName = FirstName.Text;
                string l_LastName = LastName.Text;
                Boolean l_Admin = IsAdmin.Checked;
                Boolean l_Blocked = Blocked.Checked;

                if (l_UserID == "")
                {
                    MessageBox.Show("Please provide user ID");
                    return;
                }

                if (l_Password == "")
                {
                    MessageBox.Show("Please provide Password");
                    return;
                }

                if (l_UserID != "" && m_UserNo == 0)
                {
                    PublicFunctions.FieldToParam(l_UserID, ref l_Param, Declarations.FieldTypes.String);

                    l_Query = "SELECT * FROM Users WITH (NOLOCK) WHERE UserID = " + l_Param;

                    if (this.m_Connection.GetData(l_Query, ref l_Data))
                    {
                        if (l_Data.Rows.Count > 0)
                        {
                            MessageBox.Show("User ID already exists");
                            l_Data = new DataTable();
                            return;
                        }
                    }
                }

                if (m_UserNo != 0)
                {
                    l_UserNo = m_UserNo;

                    m_Connection.BeginTransaction();

                    PublicFunctions.FieldToParam(l_UserNo, ref l_Param, Declarations.FieldTypes.Number);

                    l_Query = "DELETE FROM UserMenus WHERE UserNo = " + l_Param;

                    if (this.m_Connection.Execute(l_Query))
                    {
                        l_Query = "DELETE FROM Users WHERE UserNo = " + l_Param;

                        if (this.m_Connection.Execute(l_Query))
                        {
                            m_Connection.CommitTransaction();
                        }
                        else
                        {
                            m_Connection.RollbackTransaction();
                        }
                    }
                    else
                    {
                        m_Connection.RollbackTransaction();
                    }
                }
                else
                {
                    l_Query = "SELECT ISNULL(MAX(UserNo),0) + 1 AS UserNo FROM Users WITH (NOLOCK)";

                    if (this.m_Connection.GetData(l_Query, ref l_Data))
                    {
                        l_UserNo = Convert.ToInt32(l_Data.Rows[0]["UserNo"]);
                    }
                }

                m_Connection.BeginTransaction();

                l_Query = "INSERT INTO Users (UserNo, UserID,Password, FirstName, LastName, IsAdmin, Blocked) VALUES ( ";

                PublicFunctions.FieldToParam(l_UserNo, ref l_Param, Declarations.FieldTypes.Number);
                l_Query += l_Param + ", ";

                PublicFunctions.FieldToParam(l_UserID, ref l_Param, Declarations.FieldTypes.String);
                l_Query += l_Param + ", ";

                PublicFunctions.FieldToParam(l_Password, ref l_Param, Declarations.FieldTypes.String);
                l_Query += l_Param + ", ";

                PublicFunctions.FieldToParam(l_FirstName, ref l_Param, Declarations.FieldTypes.String);
                l_Query += l_Param + ", ";

                PublicFunctions.FieldToParam(l_LastName, ref l_Param, Declarations.FieldTypes.String);
                l_Query += l_Param + ", ";

                PublicFunctions.FieldToParam(l_Admin, ref l_Param, Declarations.FieldTypes.Boolean);
                l_Query += l_Param + ", ";

                PublicFunctions.FieldToParam(l_Blocked, ref l_Param, Declarations.FieldTypes.Boolean);
                l_Query += l_Param + ")";

                if (this.m_Connection.Execute(l_Query))
                {
                    foreach (DataGridViewRow row in menuDetailGrid.Rows)
                    {
                        int l_MenuNo = 0;
                        Boolean l_AllowAccess = false;

                        l_MenuNo = Convert.ToInt32(row.Cells["MenuNo"].Value);
                        l_AllowAccess = Convert.ToBoolean(row.Cells["AllowAcess"].Value);

                        l_Query = "INSERT INTO UserMenus (UserNo, MenuNo,AllowAcess) VALUES ( ";

                        PublicFunctions.FieldToParam(l_UserNo, ref l_Param, Declarations.FieldTypes.Number);
                        l_Query += l_Param + ", ";

                        PublicFunctions.FieldToParam(l_MenuNo, ref l_Param, Declarations.FieldTypes.String);
                        l_Query += l_Param + ", ";

                        PublicFunctions.FieldToParam(l_AllowAccess, ref l_Param, Declarations.FieldTypes.Boolean);
                        l_Query += l_Param + ")";

                        this.m_Connection.Execute(l_Query);
                    }
                    MessageBox.Show("User create saved successfully");
                    m_Connection.CommitTransaction();
                }
                else
                {
                    MessageBox.Show("Failed to save User");
                    m_Connection.RollbackTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
                m_Connection.RollbackTransaction();
            }

            FirstName.ReadOnly = true;
            LastName.ReadOnly = true;
            Password.ReadOnly = true;
            IsAdmin.Enabled = false;
            Blocked.Enabled = false;
            Modify.Enabled = true;
            Delete.Enabled = true;
            New.Enabled = true;
            Cancel.Enabled = false;
            Save.Enabled = false;

        }

        public static string Encrypt(string strInput)
        {
            string strKey = "v";
            int lngPtr = 0;
            char[] input = strInput.ToCharArray();

            for (int iCount = 0; iCount < strInput.Length; iCount++)
            {
                if (strInput.Substring(0, iCount) != strKey)
                {
                    input[iCount] = (char)(input[iCount] ^ strKey[lngPtr]);
                    lngPtr = (lngPtr + 1) % strKey.Length;
                }
            }

            return new string(input);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                string l_Query = string.Empty;
                string l_Param = string.Empty;

                m_Connection.BeginTransaction();

                PublicFunctions.FieldToParam(m_UserNo, ref l_Param, Declarations.FieldTypes.Number);

                l_Query = "DELETE FROM UserMenus WHERE UserNo = " + l_Param;

                if (this.m_Connection.Execute(l_Query))
                {
                    l_Query = "DELETE FROM Users WHERE UserNo = " + l_Param;

                    if (this.m_Connection.Execute(l_Query))
                    {
                        MessageBox.Show("User Deleted Successfully.");
                        m_Connection.CommitTransaction();

                        UserID.Text = "";
                        FirstName.Text = "";
                        LastName.Text = "";
                        Password.Text = "";
                        IsAdmin.Checked = false;
                        Blocked.Checked = false;

                        FirstName.ReadOnly = false;
                        LastName.ReadOnly = false;
                        Password.ReadOnly = false;
                        IsAdmin.Enabled = true;
                        Blocked.Enabled = true;
                        Modify.Enabled = false;
                        Delete.Enabled = false;
                        New.Enabled = true;
                        Cancel.Enabled = false;
                        Save.Enabled = false;

                        //DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                        this.menuDetailGrid.DataSource = null;

                        return;
                    }
                    else
                    {
                        MessageBox.Show("Unable to delete User.");
                        m_Connection.RollbackTransaction();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Unable to delete delete User.");
                    m_Connection.RollbackTransaction();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
                m_Connection.RollbackTransaction();
            }
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            m_UserNo = 0;
            UserID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            Password.Text = "";
            IsAdmin.Checked = false;
            Blocked.Checked = false;

            FirstName.ReadOnly = true;
            LastName.ReadOnly = true;
            Password.ReadOnly = true;
            IsAdmin.Enabled = false;
            Blocked.Enabled = false;
            Modify.Enabled = false;
            Delete.Enabled = false;
            New.Enabled = true;
            Cancel.Enabled = false;
            Save.Enabled = false;

            //DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            this.menuDetailGrid.DataSource = null;
        }

        private void New_Click(object sender, EventArgs e)
        {
            m_UserNo = 0;
            UserID.Text = "";
            FirstName.Text = "";
            LastName.Text = "";
            Password.Text = "";
            IsAdmin.Checked = false;
            Blocked.Checked = false;

            FirstName.ReadOnly = false;
            LastName.ReadOnly = false;
            Password.ReadOnly = false;
            IsAdmin.Enabled = true;
            Blocked.Enabled = true;
            Modify.Enabled = false;
            Delete.Enabled = false;
            New.Enabled = false;
            Cancel.Enabled = true;
            Save.Enabled = true;

            //DataGridViewButtonColumn col = new DataGridViewButtonColumn();
            this.menuDetailGrid.DataSource = null;

            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            string l_UserID = string.Empty;

            l_Query = "SELECT MenuNo,MenuID, Caption AS Menu, AllowAcess FROM VW_Menu";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                if (l_Data.Rows.Count > 0)
                {
                    menuDetailGrid.DataSource = l_Data;

                    this.menuDetailGrid.AllowUserToAddRows = false;
                    this.menuDetailGrid.RowHeadersVisible = false;
                    this.menuDetailGrid.Columns[0].Visible = false;
                    this.menuDetailGrid.Columns[1].Visible = false;
                }
            }

        }
    }
}
