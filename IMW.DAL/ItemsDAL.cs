namespace IMW.DAL
{
    using IMW.Common;
    using Microsoft.CSharp.RuntimeBinder;
    using SAPbobsCOM;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ItemsDAL
    {
        private Company oCompany = ((Company)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));

        public List<MatFolder> GetIMOSHierarchy()
        {
            List<MatFolder> list = new List<MatFolder>();
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = "WITH generation as ( SELECT Dir_ID, name, parent_id, type, 0 AS generation_number FROM matfolder WHERE parent_id = 0 UNION ALL SELECT child.dir_id, child.name, child.parent_id, child.type, generation_number + 1 AS generation_number FROM matfolder child JOIN generation g ON g.dir_id = child.parent_id) SELECT Dir_Id, name, Parent_Id, type, generation_number FROM generation order by generation_number; "
            }.ExecuteReader();
            while (reader.Read())
            {
                MatFolder item = new MatFolder
                {
                    Dir_Id = Convert.ToInt32(reader["DIR_ID"]),
                    Name = reader["Name"].ToString(),
                    Type = reader["Type"].ToString(),
                    Parent_Id = Convert.ToInt32(reader["Parent_Id"].ToString()),
                    Generation_Number = Convert.ToInt32(reader["generation_number"])
                };
                list.Add(item);
            }
            return list;
        }

        public List<Item> GetItems()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<Item> list = new List<Item>();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = "select t1.ItemCode,t1.ItemName,t1.FrgnName,t1.VatGourpSa,t1.SalUnitMsr,t1.DfltWH, t2.pricelist,t2.price from oitm t1,itm1 t2 where t1.ItemCode=t2.itemcode"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                Item item = new Item
                {
                    ItemCode = reader["ItemCode"].ToString(),
                    ItemName = reader["ItemName"].ToString(),
                    FrgnName = reader["FrgnName"].ToString(),
                    SalUnitMsr = reader["SalUnitMsr"].ToString(),
                    Price = Convert.ToDouble(reader["Price"].ToString()),
                    DfltWH = reader["DfltWH"].ToString(),
                    VatGourpSa = reader["VatGourpSa"].ToString()
                };
                list.Add(item);
            }
        }

        public Item GetItems(IMW.Common.MapItem mi)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<Item> list = new List<Item>();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = string.Format("select t1.ItemCode,t1.ItemName,t1.FrgnName,t1.VatGourpSa,t1.SalUnitMsr,t1.DfltWH, t2.pricelist,t2.price from oitm t1,itm1 t2 where t1.ItemCode=t2.itemcode and (FrgnName like '{0}%' or 8t1.ItemCode = '{0}')", mi.SAPItem)
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list[0];
                }
                Item item = new Item
                {
                    ItemCode = reader["ItemCode"].ToString(),
                    ItemName = reader["ItemName"].ToString(),
                    FrgnName = reader["FrgnName"].ToString(),
                    SalUnitMsr = reader["SalUnitMsr"].ToString(),
                    Price = Convert.ToDouble(reader["Price"].ToString()),
                    DfltWH = reader["DfltWH"].ToString(),
                    VatGourpSa = reader["VatGourpSa"].ToString()
                };
                list.Add(item);
            }
        }

        private DateTime GetLastItemCreateDateFSAP()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select isnull(max(createdate),0) LastDate from oitm"
            };
            DateTime time = Convert.ToDateTime(command.ExecuteScalar());
            connection.Close();
            command.Dispose();
            return time;
        }

        public string GetLastItemFSAP(DateTime CreateDate)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Sp_ItemCodes_CSV"
            };
            command.Parameters.AddWithValue("@CreateDate", CreateDate.Date);
            string str = Convert.ToString(command.ExecuteScalar());
            connection.Close();
            command.Dispose();
            return str;
        }

        public List<IMW.Common.MapItem> GetMapItems()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<IMW.Common.MapItem> list = new List<IMW.Common.MapItem>();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = "select * from MapIMOSSAP"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                IMW.Common.MapItem item = new IMW.Common.MapItem
                {
                    IMOSItem = reader["IMOSItem"].ToString(),
                    IMOSItemVariable = reader["IMOSItemVar"].ToString(),
                    IMOSItemVariableValue = reader["IMOSItemVarValue"].ToString(),
                    SAPItem = reader["SAPItem"].ToString(),
                    Length = (reader["Length"] == DBNull.Value) ? 0.0 : Convert.ToDouble(reader["Length"]),
                    Width = (reader["Width"] == DBNull.Value) ? 0.0 : Convert.ToDouble(reader["Width"]),
                    Thickness = (reader["Thickness"] == DBNull.Value) ? 0.0 : Convert.ToDouble(reader["Thickness"]),
                    ArticleNo = (reader["ArticleNo"] == DBNull.Value) ? string.Empty : reader["ArticleNo"].ToString()
                };
                list.Add(item);
            }
        }

        public bool LoadItem()
        {
            DateTime lastItemCreateDateFSAP = this.GetLastItemCreateDateFSAP();
            string lastItemFSAP = this.GetLastItemFSAP(lastItemCreateDateFSAP);
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            this.oCompany = new SAPDAL().ConnectSAP();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset recordset2 = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            if (lastItemFSAP.Trim() == string.Empty)
            {
                recordset.DoQuery("SELECT * FROM OITM");
            }
            else
            {
                recordset.DoQuery($"SELECT * FROM OITM where CreateDate >= '{lastItemCreateDateFSAP.ToString("yyyy/MM/dd")}'");
            }
            while (true)
            {
                if (recordset.EoF)
                {
                    connection.Close();
                    return true;
                }
                if (lastItemFSAP.Contains(recordset.Fields.Item(0).Value))
                {
                    recordset.MoveNext();
                }
                else
                {
                    recordset2.DoQuery($"Select * from ITM1 where \"ItemCode\"='{recordset.Fields.Item("ItemCode").Value}'");
                    SqlTransaction transaction = connection.BeginTransaction();
                    SqlCommand command = new SqlCommand();
                    SqlCommand command2 = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandText = $"Insert into OITM(ItemCode,ItemName,FrgnName,ExitPrice,AvgPrice, PriceUnit,CreateDate,SalUnitMsr,UGPEntry,DfltWH,VatGourpSa,BHeight1,U_Grp1Name, U_Grp2Name, U_Grp3Name, U_Grp4Name) values ('{recordset.Fields.Item("ItemCode").Value}','{recordset.Fields.Item("ItemName").Value.Replace("'", "''")}','{recordset.Fields.Item("FrgnName").Value.Replace("'", "''")}','{recordset.Fields.Item("ExitPrice").Value}','{recordset.Fields.Item("AvgPrice").Value}','{recordset.Fields.Item("PriceUnit").Value}','{recordset.Fields.Item("CreateDate").Value}','{recordset.Fields.Item("SalUnitMsr").Value}','{recordset.Fields.Item("UgpEntry").Value}','{recordset.Fields.Item("DfltWH").Value}','{recordset.Fields.Item("VatGourpSa").Value}','{recordset.Fields.Item("BHeight1").Value}','{recordset.Fields.Item("U_Grp1Name").Value}','{recordset.Fields.Item("U_Grp2Name").Value}','{recordset.Fields.Item("U_Grp3Name").Value}','{recordset.Fields.Item("U_Grp4Name").Value}')";
                    command2.Connection = connection;
                    command2.Transaction = transaction;
                    command2.CommandText = $"INSERT INTO ITM1 (ItemCode, PriceList, Price) VALUES ('{recordset2.Fields.Item("ItemCode").Value}', {recordset2.Fields.Item("PriceList").Value}, {recordset2.Fields.Item("Price").Value});";
                    command.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                    transaction.Commit();
                    LogConsumerDAL.Instance.Write($"{recordset.Fields.Item("ItemCode").Value} - Loaded to ISC Item Master Data");
                    recordset.MoveNext();
                }
            }
            return false;
        }

    public bool LoadItemToIMOSfromSAP(string IMOS_Parent, string SAP_ItemFrgnName, string SAP_ItemName, ItemTargetTable table)
    {
        SqlConnection connection = new SqlConnection
        {
            ConnectionString = HelperDAL.IMOSConnectionString
        };
        connection.Open();
        SqlCommand command = new SqlCommand
        {
            Connection = connection
        };
        SqlTransaction transaction = connection.BeginTransaction();
        command.Transaction = transaction;
        command.CommandText = $"Insert into matfolder(Name,Type,Parent_ID) values ('{SAP_ItemFrgnName}','100',{IMOS_Parent})";
        command.ExecuteNonQuery();
        switch (table)
        {
            case ItemTargetTable.Material:
                command.CommandText = $"Insert into Mat(Name,TEXT) values ('{SAP_ItemFrgnName}','{SAP_ItemName}')";
                command.ExecuteNonQuery();
                break;

            case ItemTargetTable.Profil:
                command.CommandText = $"Insert into PROFIL(Name,TEXT) values ('{SAP_ItemFrgnName}','{SAP_ItemName}')";
                command.ExecuteNonQuery();
                break;

            case ItemTargetTable.Surface:
                command.CommandText = $"Insert into surf(Name,TEXT) values ('{SAP_ItemFrgnName}','{SAP_ItemName}')";
                command.ExecuteNonQuery();
                break;

            default:
                break;
        }
        transaction.Commit();
        connection.Close();
        return true;
    }

    public bool MapItem(IMW.Common.MapItem mi, string DocNum)
    {
            bool l_Result;
        SqlConnection connection = new SqlConnection
        {
            ConnectionString = HelperDAL.ISCConnectionString
        };
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        this.oCompany = new SAPDAL().ConnectSAP();
        SqlCommand command = new SqlCommand
        {
            CommandText = $"Select isnull(count(IMOSItem),0) total from MapIMOSSAP where IMOSItem='{mi.IMOSItem}' and IMOSItemVar='{mi.IMOSItemVariable}' and IMOSItemVarValue='{mi.IMOSItemVariableValue}' and SAPItem='{mi.SAPItem}' and DocNum='{DocNum}' and Length='{mi.Length}' and Width='{mi.Width}' and Thickness='{mi.Thickness}'",
            Connection = connection,
            Transaction = transaction
        };
        if (Convert.ToInt32(command.ExecuteScalar()) != 0)
        {
            connection.Close();
                l_Result = false;
        }
        else
        {
            command = new SqlCommand();
            object[] args = new object[9];
            args[0] = mi.IMOSItem;
            args[1] = mi.IMOSItemVariable;
            args[2] = mi.IMOSItemVariableValue;
            args[3] = mi.SAPItem;
            args[4] = DocNum;
            args[5] = mi.Length;
            args[6] = mi.Width;
            args[7] = mi.Thickness;
            args[8] = mi.ArticleNo;
            command.CommandText = string.Format("Insert into MapIMOSSAP(IMOSItem,IMOSItemVar,IMOSItemVarValue,SAPItem,DocNum,Length,Width,Thickness,ArticleNo) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", args);
            command.Connection = connection;
            command.Transaction = transaction;
            command.ExecuteNonQuery();
            transaction.Commit();
            connection.Close();
                l_Result = true;
        }
            return l_Result;
    }

    public bool UnMapItem(IMW.Common.MapItem mi)
    {
        SqlConnection connection = new SqlConnection
        {
            ConnectionString = HelperDAL.ISCConnectionString
        };
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        this.oCompany = new SAPDAL().ConnectSAP();
        new SqlCommand
        {
            Connection = connection,
            Transaction = transaction,
            CommandText = $"delete from MapIMOSSAP where IMOSItem='{mi.IMOSItem}' and IMOSItemVar='{mi.IMOSItemVariable}' and IMOSItemVarValue='{mi.IMOSItemVariableValue}' and SAPItem='{mi.SAPItem}'"
        }.ExecuteNonQuery();
        transaction.Commit();
        connection.Close();
        return true;
    }
}
}

