using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMW.DB;
using IMW.DAL;

namespace IMW.WinUI
{
	public partial class SAPItemIdentifier : Form
	{
		DBConnector m_Connection = new DBConnector(HelperDAL.IMOSConnectionString);

		public SAPItemIdentifier()
		{
			InitializeComponent();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string l_Query = string.Empty;
			string l_Param = string.Empty;

			try
			{
				string l_ItemIDentificationCode = this.txtItemCode.Text;

				if(string.IsNullOrEmpty(l_ItemIDentificationCode))
				{
					MessageBox.Show("Please provide SAP Item Identification Code");
					return;
				}

				if(!this.CheckIdentificationCode())
				{
					MessageBox.Show("Item Identification Code already exists in system.");

					return;
				}

				l_Query = "INSERT INTO SAPItemIdentityConfig VALUES (";

				PublicFunctions.FieldToParam(l_ItemIDentificationCode, ref l_Param, Declarations.FieldTypes.String);
				l_Query += l_Param + ")";

				if(this.m_Connection.Execute(l_Query))
				{
					this.txtItemCode.Text = string.Empty;
					this.LoadData();
				}
				else
				{
					MessageBox.Show("Unable to add Item Identification Code");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.InnerException.ToString());
			}
		}

		private void SAPItemIdentifier_Load(object sender, EventArgs e)
		{
			try
			{
				this.dgvInitialCodes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInitialCodes_CellClick);
				this.LoadData();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.InnerException.ToString());
			}
		}

		public void LoadData()
		{
			string l_Query = string.Empty;
			DataTable l_Data = new DataTable();

			l_Query = "SELECT SerialNo, ProductCode FROM SAPItemIdentityConfig";

			if (this.m_Connection.GetData(l_Query, ref l_Data))
			{
				this.dgvInitialCodes.DataSource = l_Data;

				if (dgvInitialCodes.Columns.Contains("Remove"))
				{
					this.dgvInitialCodes.Columns.Remove("Remove");
				}

				DataGridViewButtonColumn col = new DataGridViewButtonColumn();
				col.UseColumnTextForButtonValue = true;
				col.Text = "Remove";
				col.HeaderText = "";
				col.Name = "Remove";
				dgvInitialCodes.Columns.Add(col);

				this.dgvInitialCodes.AllowUserToAddRows = false;
				this.dgvInitialCodes.RowHeadersVisible = false;

				this.dgvInitialCodes.Columns[0].Visible = false;

				this.dgvInitialCodes.Columns[1].Width = 150;

				this.dgvInitialCodes.DefaultCellStyle.Font = new Font("Segoe UI", 12);
				this.dgvInitialCodes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
			}
			else
			{
				this.dgvInitialCodes.DataSource = null;
				this.dgvInitialCodes.Rows.Clear();
				this.dgvInitialCodes.Refresh();
			}
		}

		public bool CheckIdentificationCode()
		{
			string l_Query = string.Empty;
			string l_Param = string.Empty;

			DataTable l_Data = new DataTable();

			l_Query = "SELECT ProductCode FROM SAPItemIdentityConfig WHERE ProductCode = ";

			PublicFunctions.FieldToParam(this.txtItemCode.Text, ref l_Param, Declarations.FieldTypes.String);
			l_Query += l_Param;

			if (this.m_Connection.GetData(l_Query, ref l_Data))
			{
				return false;
			}

			return true;
		}

		private void dgvInitialCodes_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				if (e.ColumnIndex == dgvInitialCodes.Columns["Remove"].Index && dgvInitialCodes.Rows.Count > 0)
				{
					DataTable l_Data = new DataTable();
					string l_Query = string.Empty;

					l_Query = "DELETE FROM SAPItemIdentityConfig WHERE SerialNo = " + this.dgvInitialCodes.Rows[e.RowIndex].Cells[0].Value.ToString();

					if (this.m_Connection.Execute(l_Query))
					{
						MessageBox.Show("Item Identification Code removed successfully.");
						this.LoadData();
					}
					else
					{
						MessageBox.Show("Unable to delete Item Identification Code.");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, ex.InnerException.ToString());
			}

		}

	}
}
