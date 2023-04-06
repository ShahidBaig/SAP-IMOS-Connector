using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMW.DAL;
using IMW.DB;

namespace IMW.WinUI
{

	public partial class AddFormulaInfo : Form
	{
		public string m_LabelTitle = string.Empty;
		private string m_TableName = string.Empty;
		DBConnector m_Connection = new DBConnector(HelperDAL.IMOSConnectionString);

		public AddFormulaInfo()
		{
			InitializeComponent();
		}

		private void AddFormulaInfo_Load(object sender, EventArgs e)
		{
			try
			{
				this.FormClosing += new FormClosingEventHandler(AddFormulaInfo_FormClosing);

				this.lblTitle.Text = "Add " + m_LabelTitle;
				
				if (m_LabelTitle == "Group 1")
				{
					m_TableName = "Group1";
				}
				else if (m_LabelTitle == "Group 2")
				{
					m_TableName = "Group2";
				}
				else if (m_LabelTitle == "Group 3")
				{
					m_TableName = "Group3";
				}
				else if (m_LabelTitle == "Group 4")
				{
					m_TableName = "Group4";
				}
				else if (m_LabelTitle == "Formula")
				{
					m_TableName = "Formulas";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.InnerException.ToString());
			}
		}

		private void btnInfo_Click(object sender, EventArgs e)
		{
			try
			{
				if (string.IsNullOrEmpty(this.txtInfo.Text))
				{
					MessageBox.Show("Please provide valid information");
					return;
				}

				string l_Query = string.Empty;
				string l_Param = string.Empty;

				l_Query = "INSERT INTO " + this.m_TableName + " VALUES (";

				PublicFunctions.FieldToParam(this.txtInfo.Text, ref l_Param, Declarations.FieldTypes.String);
				l_Query += l_Param + ")";

				if(this.m_Connection.Execute(l_Query))
				{
					MessageBox.Show("Information added successfully.");
					this.Close();
				}
				else
				{
					MessageBox.Show("Unable to save data");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.InnerException.ToString());
			}
		}

		private void AddFormulaInfo_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}
	}
}
