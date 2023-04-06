using IMW.Common;
using IMW.DAL;
using IMW.DB;
using Microsoft.CSharp.RuntimeBinder;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Transactions;
using System.Xml.Linq;

namespace IMW.WinUI
{
	public partial class SAPConnectivity : Form
	{
		private Company oCompany = ((Company)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));

		public SAPConnectivity()
		{
			InitializeComponent();
		}

		private void SAPConnectivity_Load(object sender, EventArgs e)
		{

		}

		private void btnAdd_Click_1(object sender, EventArgs e)
		{
			this.oCompany = new SAPDAL().ConnectSAP();
			string l_Query = string.Empty;

			l_Query = this.txtData.Text;

			Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
			recordset.DoQuery(l_Query);

			this.lblCount.Text = "Record Count:" + recordset.RecordCount.ToString();

			if (recordset.RecordCount > 0)
			{
				DataTable dt = new DataTable();

				// Add columns to the DataTable based on the fields in the Recordset
				for (int i = 0; i < recordset.Fields.Count; i++)
				{
					dt.Columns.Add(recordset.Fields.Item(i).Name);
				}

				// Add rows to the DataTable based on the data in the Recordset
				while (!recordset.EoF)
				{
					DataRow dr = dt.NewRow();

					for (int i = 0; i < recordset.Fields.Count; i++)
					{
						dr[i] = recordset.Fields.Item(i).Value;
					}

					dt.Rows.Add(dr);

					recordset.MoveNext();
				}

				this.dgvQueryData.DataSource = dt;

				this.dgvQueryData.AllowUserToAddRows = false;
				this.dgvQueryData.RowHeadersVisible = false;
			}
		}

		private void btnGetGroups_Click(object sender, EventArgs e)
		{
			DBConnector l_DBConnector = new DBConnector(HelperDAL.ISCConnectionString);
			DateTime l_CreateDate = DateTime.MinValue;
			string l_Query = string.Empty;
			string l_Param = string.Empty;
			string l_SAPQuery = string.Empty;
			int l_Index = 0;

			SqlConnection connection = new SqlConnection
			{
				ConnectionString = HelperDAL.ISCConnectionString
			};
			connection.Open();

			LogConsumerDAL.Instance.Write("Connecting SAP");

			this.oCompany = new SAPDAL().ConnectSAP();
			Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

			LogConsumerDAL.Instance.Write("Connected SAP");

			l_SAPQuery = this.txtData.Text;

			recordset.DoQuery(l_SAPQuery);

			while (true)
			{
				if (recordset.EoF)
				{
					connection.Close();
					return;
				}
				else
				{
					try
					{
						SqlCommand command = new SqlCommand();
						command.Connection = connection;
						command.CommandText = $"Insert into OITM_ItemGroups(ItemCode,ItemName,FrgnName,ExitPrice,AvgPrice, PriceUnit,CreateDate,SalUnitMsr,DfltWH,VatGourpSa,U_Grp1Name,U_Grp2Name,U_Grp3Name,U_Grp4Name,BHeight1,UgpEntry) values ('{recordset.Fields.Item("ItemCode").Value}','{recordset.Fields.Item("ItemName").Value}','{recordset.Fields.Item("FrgnName").Value}','{recordset.Fields.Item("ExitPrice").Value}','{recordset.Fields.Item("AvgPrice").Value}','{recordset.Fields.Item("PriceUnit").Value}','{recordset.Fields.Item("CreateDate").Value}','{recordset.Fields.Item("SalUnitMsr").Value}','{recordset.Fields.Item("DfltWH").Value}','{recordset.Fields.Item("VatGourpSa").Value}','{recordset.Fields.Item("U_Grp1Name").Value}','{recordset.Fields.Item("U_Grp2Name").Value}','{recordset.Fields.Item("U_Grp3Name").Value}','{recordset.Fields.Item("U_Grp4Name").Value}','{recordset.Fields.Item("BHeight1").Value}','{recordset.Fields.Item("UgpEntry").Value}')";
						command.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
					}

					recordset.MoveNext();
				}
			}
		}

        private void btnForceSyncOpportunity_Click(object sender, EventArgs e)
        {
            DataTable l_Data = new DataTable();
            
			try
            {
                DBConnector l_Connection = new DBConnector();
                string l_Query = string.Empty;
                string l_Param = string.Empty;

                l_Connection = new DBConnector(HelperDAL.ISCConnectionString);

                this.oCompany = new SAPDAL().ConnectSAP();
                Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                l_Query = "DELETE FROM OOPR";
                l_Connection.Execute(l_Query);

                recordset.DoQuery($"SELECT \"OpprId\",\"CardCode\" FROM OOPR");

                while (!recordset.EoF)
                {
                    l_Query = $"INSERT INTO OOPR(OpprId, CardCode, CardName, UPSAP) VALUES ('{recordset.Fields.Item("OPPRID").Value}', '{recordset.Fields.Item("CardCode").Value}', '{recordset.Fields.Item("CardCode").Value}', '{true}')";

                    l_Connection.Execute(l_Query);

                    recordset.MoveNext();
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
    }
}
