using IMW.Common;
using IMW.DAL;
using IMW.DB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IMW.WinUI
{
    public partial class frmQtyConversionFormula : Form
    {
        DBConnector m_Connection = new DBConnector(HelperDAL.ISCConnectionString);
        public frmQtyConversionFormula()
        {
            InitializeComponent();
        }

        private void frmQtyConversionFormula_Load(object sender, EventArgs e)
        {
            try
            {
                this.dgvSavedFormulas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSavedFormulas_CellClick);
                this.LoadData();
            }
            catch (Exception ex)
            {

            }
        }

        public void LoadGroup1()
        {
            Dictionary<int, string> l_List = new Dictionary<int, string>();
            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;

            l_List.Add(-1, "Select Group 1");

            l_Query = "Select * FROM Group1";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                foreach (DataRow l_Row in l_Data.Rows)
                {
                    l_List.Add(Convert.ToInt32(l_Row["SerialNo"].ToString()), l_Row["GrpName"].ToString());
                }
            }

            this.cmbGroup1.DataSource = l_List.ToList();
            this.cmbGroup1.ValueMember = "Key";
            this.cmbGroup1.DisplayMember = "Value";
            this.cmbGroup1.SelectedIndex = 0;
            this.cmbGroup1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void LoadGroup2()
        {
            Dictionary<int, string> l_List = new Dictionary<int, string>();
            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;

            l_List.Add(-1, "Select Group 2");

            l_Query = "Select * FROM Group2";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                foreach (DataRow l_Row in l_Data.Rows)
                {
                    l_List.Add(Convert.ToInt32(l_Row["SerialNo"].ToString()), l_Row["GrpName"].ToString());
                }
            }

            this.cmbGroup2.DataSource = l_List.ToList();
            this.cmbGroup2.ValueMember = "Key";
            this.cmbGroup2.DisplayMember = "Value";
            this.cmbGroup2.SelectedIndex = 0;
            this.cmbGroup2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void LoadGroup3()
        {
            Dictionary<int, string> l_List = new Dictionary<int, string>();
            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;

            l_List.Add(-1, "Select Group 3");

            l_Query = "Select * FROM Group3";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                foreach (DataRow l_Row in l_Data.Rows)
                {
                    l_List.Add(Convert.ToInt32(l_Row["SerialNo"].ToString()), l_Row["GrpName"].ToString());
                }
            }

            this.cmbGroup3.DataSource = l_List.ToList();
            this.cmbGroup3.ValueMember = "Key";
            this.cmbGroup3.DisplayMember = "Value";
            this.cmbGroup3.SelectedIndex = 0;
            this.cmbGroup3.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void LoadGroup4()
        {
            Dictionary<int, string> l_List = new Dictionary<int, string>();
            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;

            l_List.Add(-1, "Select Group 4");

            l_Query = "Select * FROM Group4";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                foreach (DataRow l_Row in l_Data.Rows)
                {
                    l_List.Add(Convert.ToInt32(l_Row["SerialNo"].ToString()), l_Row["GrpName"].ToString());
                }
            }

            this.cmbGroup4.DataSource = l_List.ToList();
            this.cmbGroup4.ValueMember = "Key";
            this.cmbGroup4.DisplayMember = "Value";
            this.cmbGroup4.SelectedIndex = 0;
            this.cmbGroup4.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void LoadFormulas()
        {
            Dictionary<int, string> l_List = new Dictionary<int, string>();
            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;

            l_List.Add(-1, "Select Formula");

            l_Query = "Select * FROM Formulas";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                foreach (DataRow l_Row in l_Data.Rows)
                {
                    l_List.Add(Convert.ToInt32(l_Row["FormulaNo"].ToString()), l_Row["FormulaDesc"].ToString());
                }
            }

            this.cmbFormula.DataSource = l_List.ToList();
            this.cmbFormula.ValueMember = "Key";
            this.cmbFormula.DisplayMember = "Value";
            this.cmbFormula.SelectedIndex = 0;
            this.cmbFormula.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void LoadSavedFormulas()
        {
            Dictionary<int, string> l_List = new Dictionary<int, string>();
            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;

            l_Query = "Select * FROM VW_QtyConversionDetail";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                this.dgvSavedFormulas.DataSource = l_Data;

                if (dgvSavedFormulas.Columns.Contains("Remove"))
                {
                    this.dgvSavedFormulas.Columns.Remove("Remove");
                }

                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.UseColumnTextForButtonValue = true;
                col.Text = "Remove";
                col.HeaderText = "";
                col.Name = "Remove";
                dgvSavedFormulas.Columns.Add(col);

                this.dgvSavedFormulas.AllowUserToAddRows = false;
                this.dgvSavedFormulas.RowHeadersVisible = false;

                this.dgvSavedFormulas.Columns[0].Visible = false;
                this.dgvSavedFormulas.Columns[7].Visible = false;

                this.dgvSavedFormulas.Columns[1].Width = 108;
                this.dgvSavedFormulas.Columns[2].Width = 200;
                this.dgvSavedFormulas.Columns[3].Width = 130;
                this.dgvSavedFormulas.Columns[4].Width = 180;
                this.dgvSavedFormulas.Columns[5].Width = 350;
                this.dgvSavedFormulas.Columns[6].Width = 100;

                this.dgvSavedFormulas.Columns[1].HeaderText = "Group 1";
                this.dgvSavedFormulas.Columns[2].HeaderText = "Group 2";
                this.dgvSavedFormulas.Columns[3].HeaderText = "Group 3";
                this.dgvSavedFormulas.Columns[4].HeaderText = "Group 4";
                this.dgvSavedFormulas.Columns[5].HeaderText = "Formula";
                this.dgvSavedFormulas.Columns[6].HeaderText = "Sq. No";

                this.dgvSavedFormulas.DefaultCellStyle.Font = new Font("Segoe UI", 12);
                this.dgvSavedFormulas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            }

        }

        private void dgvSavedFormulas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvSavedFormulas.Columns["Remove"].Index)
                {
                    DataTable l_Data = new DataTable();
                    string l_Query = string.Empty;

                    l_Query = "DELETE FROM QtyConversionDetail WHERE QtyFormulaNo = " + this.dgvSavedFormulas.Rows[e.RowIndex].Cells[0].Value.ToString();

                    if (this.m_Connection.Execute(l_Query))
                    {
                        MessageBox.Show("Qty Formula removed successfully.");
                        this.LoadSavedFormulas();
                    }
                    else
                    {
                        MessageBox.Show("Unable to delete Qty Formula.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
            }

        }

        public void LoadData()
        {
            this.LoadGroup1();
            this.LoadGroup2();
            this.LoadGroup3();
            this.LoadGroup4();
            this.LoadFormulas();
            this.LoadSavedFormulas();
        }

        private void btnAddformula_Click(object sender, EventArgs e)
        {
            try
            {
                string l_Query = string.Empty;
                string l_Param = string.Empty;
                bool l_process = false;
                DataTable l_Data = new DataTable();

                int l_Group1 = Convert.ToInt32(this.cmbGroup1.SelectedValue.ToString());
                int l_Group2 = Convert.ToInt32(this.cmbGroup2.SelectedValue.ToString());
                int l_Group3 = Convert.ToInt32(this.cmbGroup3.SelectedValue.ToString());
                int l_Group4 = Convert.ToInt32(this.cmbGroup4.SelectedValue.ToString());
                int l_FormulaNo = Convert.ToInt32(this.cmbFormula.SelectedValue.ToString());
                double l_SequenceNo = Convert.ToDouble(this.SequenceNoUpDown.Text);

                if (l_Group1 == -1)
                {
                    MessageBox.Show("Please select Group 1");
                    return;
                }

                if (l_Group2 == -1 && l_Group3 != -1)
                {
                    MessageBox.Show("Please select Group 2");
                    return;
                }

                if (l_Group4 != -1 && l_Group2 == -1)
                {
                    MessageBox.Show("Please select Group 2");
                    return;
                }

                if (l_Group4 != -1 && l_Group3 == -1)
                {
                    MessageBox.Show("Please select Group 3");
                    return;
                }

                if (l_FormulaNo == -1 || l_SequenceNo == 0)
                {
                    MessageBox.Show("Please select Formula or provide Sequence No");
                    return;
                }

                l_Query = "EXEC SAP_InsertQtyConversionDetail ";

                PublicFunctions.FieldToParam(l_Group1, ref l_Param, Declarations.FieldTypes.Number);
                l_Query = l_Query + l_Param;

                PublicFunctions.FieldToParam(l_Group2, ref l_Param, Declarations.FieldTypes.Number);
                l_Query = l_Query + " , " + l_Param;

                PublicFunctions.FieldToParam(l_Group3, ref l_Param, Declarations.FieldTypes.Number);
                l_Query = l_Query + " , " + l_Param;

                PublicFunctions.FieldToParam(l_Group4, ref l_Param, Declarations.FieldTypes.Number);
                l_Query = l_Query + " , " + l_Param;

                PublicFunctions.FieldToParam(l_FormulaNo, ref l_Param, Declarations.FieldTypes.Number);
                l_Query = l_Query + " , " + l_Param;

                PublicFunctions.FieldToParam(l_SequenceNo, ref l_Param, Declarations.FieldTypes.Number);
                l_Query = l_Query + " , " + l_Param;

                l_process = this.m_Connection.GetDataSP(l_Query, ref l_Data);

                if (l_process == true)
                {
                    MessageBox.Show(Convert.ToString(l_Data.Rows[0]["Description"]));
                    this.LoadSavedFormulas();
                }
                else
                {
                    MessageBox.Show("Failed to save formula");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
            }
        }

        private void btnAddGroup1_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormulaInfo l_AddFormulaInfo = new AddFormulaInfo();
                l_AddFormulaInfo.m_LabelTitle = "Group 1";

                if (l_AddFormulaInfo.ShowDialog() == DialogResult.OK)
                {
                    this.LoadGroup1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
            }
        }

        private void btnAddGroup2_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormulaInfo l_AddFormulaInfo = new AddFormulaInfo();
                l_AddFormulaInfo.m_LabelTitle = "Group 2";

                if (l_AddFormulaInfo.ShowDialog() == DialogResult.OK)
                {
                    this.LoadGroup2();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
            }
        }

        private void btnAddGroup3_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormulaInfo l_AddFormulaInfo = new AddFormulaInfo();
                l_AddFormulaInfo.m_LabelTitle = "Group 3";

                if (l_AddFormulaInfo.ShowDialog() == DialogResult.OK)
                {
                    this.LoadGroup3();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
            }
        }

        private void btnAddGroup4_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormulaInfo l_AddFormulaInfo = new AddFormulaInfo();
                l_AddFormulaInfo.m_LabelTitle = "Group 4";

                if (l_AddFormulaInfo.ShowDialog() == DialogResult.OK)
                {
                    this.LoadGroup4();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
            }
        }

        private void btnAddFormulas_Click(object sender, EventArgs e)
        {
            try
            {
                AddFormulaInfo l_AddFormulaInfo = new AddFormulaInfo();
                l_AddFormulaInfo.m_LabelTitle = "Formula";

                if (l_AddFormulaInfo.ShowDialog() == DialogResult.OK)
                {
                    this.LoadFormulas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.ToString());
            }
        }
    }
}
