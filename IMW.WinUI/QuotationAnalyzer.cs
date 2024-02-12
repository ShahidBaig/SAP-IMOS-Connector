using IMW.DAL;
using IMW.DB;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IMW.WinUI
{
    public partial class QuotationAnalyzer : Form
    {
        private Company oCompany = new SAPbobsCOM.Company();
        private DBConnector dbConnect = new DBConnector(HelperDAL.ISCConnectionString);

        public QuotationAnalyzer()
        {
            this.InitializeComponent();
            DateTime l_FromDate = DateTime.Today.AddDays(-7);
            DateTime l_ToDate = DateTime.Today;

            this.l_chkboxFromDate.CheckedChanged += l_chkboxFromDate_CheckedChanged;
            this.l_chkboxToDate.CheckedChanged += l_chkboxToDate_CheckedChanged;
            this.l_SearchData.Click += l_SearchData_Click;
            this.btn_Clear.Click += btn_Clear_Click;

            l_chkboxToDate.Checked = true;
            l_chkboxFromDate.Checked = true;
            l_frmDate_dateTimePicker.Value = (l_FromDate);
            l_ToDate_dateTimePicker.Value = (l_ToDate);
        }

        private void l_chkboxFromDate_CheckedChanged(object sender, EventArgs e)
        {
            if (l_chkboxFromDate.Checked == false)
            {
                l_frmDate_dateTimePicker.Enabled = false;
            }
            else
            {
                l_frmDate_dateTimePicker.Enabled = true;

            }
        }

        private void l_chkboxToDate_CheckedChanged(object sender, EventArgs e)
        {
            if (l_chkboxToDate.Checked == false)
            {
                l_ToDate_dateTimePicker.Enabled = false;
            }
            else
            {
                l_ToDate_dateTimePicker.Enabled = true;
            }
        }

        private void l_SearchData_Click(object sender, EventArgs e)
        {

            DataTable l_Data = new DataTable();
            string l_SQL = string.Empty;
            string l_Param = string.Empty;
            int l_DocNo = 0;
            string l_Status = string.Empty;
            l_dataGridView.DataSource = null;
            string l_FromDate = string.Empty;
            string l_ToDate = string.Empty;

            if (l_chkboxFromDate.Checked)
            {
                l_FromDate = l_frmDate_dateTimePicker.Value.ToString();
            }

            if (l_chkboxToDate.Checked)
            {
                l_ToDate = l_ToDate_dateTimePicker.Value.ToString();
            }

            l_DocNo = Convert.ToInt32(l_DocNoUpDown.Text);
            l_Status = l_StatusCombobox.Text;

            l_SQL = "EXEC SAP_QuotationAnalyzer ";

            PublicFunctions.FieldToParam(l_DocNo, ref l_Param, Declarations.FieldTypes.Number);
            l_SQL = l_SQL + l_Param;

            PublicFunctions.FieldToParam(l_FromDate, ref l_Param, Declarations.FieldTypes.String);
            l_SQL = l_SQL + " , " + l_Param;

            PublicFunctions.FieldToParam(l_ToDate, ref l_Param, Declarations.FieldTypes.String);
            l_SQL = l_SQL + " , " + l_Param;

            PublicFunctions.FieldToParam(l_Status, ref l_Param, Declarations.FieldTypes.String);
            l_SQL = l_SQL + " , " + l_Param;

            this.dbConnect.GetData(l_SQL, ref l_Data);

            if (l_Data.Rows.Count > 0)
            {
                l_dataGridView.DataSource = l_Data;
                l_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                l_dataGridView.DataSource = null;
                MessageBox.Show("No Data found against the selected filters.");
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            l_frmDate_dateTimePicker.Value = DateTime.Today.AddDays(-7);
            l_ToDate_dateTimePicker.Value = DateTime.Today;
            l_DocNoUpDown.Text = "0";
            l_StatusCombobox.Text = "";
            l_dataGridView.DataSource = null;
        }
    }
}
