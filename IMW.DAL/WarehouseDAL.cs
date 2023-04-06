namespace IMW.DAL
{
    using IMW.Common;
    using Microsoft.CSharp.RuntimeBinder;
    using SAPbobsCOM;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class WarehouseDAL
    {
        private Company oCompany = ((Company) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));

        public List<Warehouse> GetWarehouses()
        {
            List<Warehouse> list = new List<Warehouse>();
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            this.oCompany = new SAPDAL().ConnectSAP();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            recordset.DoQuery(string.Format("SELECT * FROM OWHS;", new object[0]));
            while (true)
            {
                if (recordset.EoF)
                {
                    transaction.Commit();
                    connection.Close();
                    return list;
                }
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    Transaction = transaction
                };
                Warehouse item = new Warehouse();
                item.WhsCode = recordset.Fields.Item(0).Value;
                item.WhsName = recordset.Fields.Item(1).Value;
                command.CommandText = $"Insert into OWHs(WhsCode, WhsName,Building,CreateDate,UpdateDate) values ('{recordset.Fields.Item(0).Value}','{recordset.Fields.Item(1).Value}','{recordset.Fields.Item("Building").Value}','{recordset.Fields.Item("CreateDate").Value}','{recordset.Fields.Item("UpdateDate").Value}')";
                command.ExecuteNonQuery();
                recordset.MoveNext();
                list.Add(item);
            }
            return list;
        }
    }
}

