namespace IMW.DAL
{
    using IMW.Common;
    using IMW.DB;
    using Microsoft.CSharp.RuntimeBinder;
    using Microsoft.Extensions.Configuration;
    using SAPbobsCOM;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;

    public class SaleQuotationDAL
    {
        private Company oCompany = ((Company)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));
        DBConnector m_Connection = new DBConnector();

        public List<IMOSItem> GetAssemblyItems(string IMOS_PO_ID)
        {
            List<IMOSItem> list = new List<IMOSItem>();
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select * from IDBEXT where ORDERID='" + IMOS_PO_ID + "' and Typ = 1"
            };
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    return list;
                }
                IMOSItem item = new IMOSItem
                {
                    Name = reader["Name"].ToString(),
                    Name2 = reader["Name2"].ToString(),
                    Typ = (IMOSItemType)Convert.ToInt32(reader["Typ"]),
                    Price = Convert.ToDouble(reader["Price"].ToString()),
                    Quantity = Convert.ToInt32(reader["Quantity"].ToString())
                };
                list.Add(item);
            }
        }

        public List<IMOSItem> GetAssemblyItemsfromIMOS(string OrderId)
        {
            List<IMOSItem> list = new List<IMOSItem>();
            SqlConnection connection = new SqlConnection();
            SalesCenters salesCenter = new SalesCenterDAL().GetSalesCenter(OrderId);
            connection.ConnectionString = new SalesCenterDAL().GetConnectionString(salesCenter);
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select * from IDBORDEREXT where ORDERID='" + OrderId + "'"
            };
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    return list;
                }
                IMOSItem item = new IMOSItem
                {
                    ArticleID = reader["ID"].ToString(),
                    OrderId = reader["OrderId"].ToString(),
                    Name = reader["Name"].ToString(),
                    Typ = (IMOSItemType)Convert.ToInt32(reader["Typ"]),
                    Price = Convert.ToDouble(reader["Price"].ToString()),
                    Length = Convert.ToDouble(reader["Depth"]),
                    Width = Convert.ToDouble(reader["Width"]),
                    Thickness = Convert.ToDouble(reader["Height"])
                };
                list.Add(item);
            }
        }

        public List<IMOSVariable> GetAssemblyItemsVariablesfromIMOS(IMOSItem i)
        {
            List<IMOSVariable> list = new List<IMOSVariable>();
            SqlConnection connection = new SqlConnection();
            SalesCenters salesCenter = new SalesCenterDAL().GetSalesCenter(i.OrderId);
            connection.ConnectionString = new SalesCenterDAL().GetConnectionString(salesCenter);
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection
            };
            string[] textArray1 = new string[] { "select distinct Name from IDBVAREXT where ORDERID='", i.OrderId, "' and articleId = '", i.ArticleID, "'" };
            command.CommandText = string.Concat(textArray1);
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    return list;
                }
                IMOSVariable item = new IMOSVariable
                {
                    OrderId = i.OrderId,
                    Name = reader["Name"].ToString()
                };
                list.Add(item);
            }
        }

        public List<IMOSItemVariable> GetAssemblyItemsVariablesValuesfromIMOS(IMOSItem i, string VariableName)
        {
            List<IMOSItemVariable> list = new List<IMOSItemVariable>();
            SqlConnection connection = new SqlConnection();
            SalesCenters salesCenter = new SalesCenterDAL().GetSalesCenter(i.OrderId);
            connection.ConnectionString = new SalesCenterDAL().GetConnectionString(salesCenter);
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection
            };
            string[] textArray1 = new string[] { "select Name, Value from IDBVAREXT where ORDERID='", i.OrderId, "' and articleId = '", i.ArticleID, "' and Name ='", VariableName, "'" };
            command.CommandText = string.Concat(textArray1);
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    return list;
                }
                IMOSItemVariable item = new IMOSItemVariable
                {
                    VariableName = reader["Name"].ToString(),
                    VariableValue = reader["Value"].ToString()
                };
                list.Add(item);
            }
        }

        public string GetDocEntry(string orderId)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = $"SELECT DocEntry FROM OQUT where DocNum='{orderId}'"
            };
            string str = command.ExecuteScalar().ToString();
            connection.Close();
            command.Dispose();
            return str;
        }

        public List<IMOSVariable> GetFGOne2OnefromIMOS(IMOSItem i)
        {
            List<IMOSVariable> list = new List<IMOSVariable>();
            SqlConnection connection = new SqlConnection();
            SalesCenters salesCenter = new SalesCenterDAL().GetSalesCenter(i.OrderId);
            connection.ConnectionString = new SalesCenterDAL().GetConnectionString(salesCenter);
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection
            };
            string[] textArray1 = new string[] { "Select ORDERID,ID,SubString(PVARSTRING,Charindex('FG_Codes_Carcass:=',PVARSTRING)+18,11)CarcasCode,SubString(PVARSTRING, Charindex('FG_Codes_Front:=', PVARSTRING) + 16, 11)FrontCode,SubString(PVARSTRING, Charindex('Handle_Type_KI:=', PVARSTRING) + 16, 10)HandelCode,PVARSTRING From IDBVERSO where ORDERID ='", i.OrderId, "' and Id = '", i.ArticleID, "'" };
            command.CommandText = string.Concat(textArray1);
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    return list;
                }
                IMOSVariable item = new IMOSVariable
                {
                    OrderId = i.OrderId,
                    Name = reader["CarcasCode"].ToString()
                };
                list.Add(item);
                item = new IMOSVariable
                {
                    OrderId = i.OrderId,
                    Name = reader["FrontCode"].ToString()
                };
                list.Add(item);
                item = new IMOSVariable
                {
                    OrderId = i.OrderId,
                    Name = reader["HandelCode"].ToString()
                };
                list.Add(item);
            }
        }

        public string GetLastSalesQuotationFSAP(DateTime LastDate, SalesCenters sc)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<SaleQuotation> list = new List<SaleQuotation>();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "Sp_SaleQuotationCodes_CSV"
            };
            command.Parameters.AddWithValue("@CreateDate", LastDate.Date);
            command.Parameters.AddWithValue("@DocNum", sc.FirstOrder.Substring(0, 4));
            string str = Convert.ToString(command.ExecuteScalar());
            connection.Close();
            command.Dispose();
            return str;
        }

        private DateTime GetLastSQCreateDateFSAP(SalesCenters sc)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = $"select isnull(max(docdate),0) LastDate from oqut where DocNum like '{sc.FirstOrder.Substring(0, 4)}%'"
            };
            DateTime time = Convert.ToDateTime(command.ExecuteScalar());
            connection.Close();
            command.Dispose();
            return time;
        }

        public List<MapItem> GetMapItemsForSale(string orderId)
        {
            List<MapItem> list = new List<MapItem>();
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection
            };
            char[] separator = new char[] { ':' };
            command.CommandText = $"select * from MAPIMOSSAP where docnum = {orderId.Split(separator)[1].Trim()}";
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                MapItem item = new MapItem
                {
                    IMOSItem = reader["IMOSItem"].ToString(),
                    SAPItem = reader["SAPItem"].ToString(),
                    Length = (reader["Length"] == DBNull.Value) ? 0.0 : Convert.ToDouble(reader["Length"].ToString()),
                    Width = (reader["Width"] == DBNull.Value) ? 0.0 : Convert.ToDouble(reader["Width"].ToString()),
                    Thickness = (reader["Thickness"] == DBNull.Value) ? 0.0 : Convert.ToDouble(reader["Thickness"].ToString())
                };
                list.Add(item);
            }
        }

        public List<MapItem> GetMapItemsOne2OneForSaleArticle(string orderId)
        {
            List<MapItem> list = new List<MapItem>();
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection
            };
            char[] separator = new char[] { ':' };
            command.CommandText = $"Select ORDERID,ID,RIGHT(F.VALUE,11) ITEMCODE From(Select orderid, id, f.value PvarString From(SELECT orderid, id, f.value As Pvarstring FROM idbverso AS s CROSS APPLY STRING_SPLIT(s.PVARSTRING, '|') as f )T0 CROSS APPLY STRING_SPLIT(PVARSTRING, '=') as f Where T0.Pvarstring Like '%FG01%')A CROSS APPLY STRING_SPLIT(PVARSTRING, ',') as f Where pvarstring Like '%FG01%' And ORDERID = '{orderId.Split(separator)[1].Trim()}' And PATINDEX('%[A-Z]%', f.value) = 1 Order by id";
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                MapItem item = new MapItem
                {
                    IMOSItem = reader["ID"].ToString(),
                    SAPItem = reader["ItemCode"].ToString()
                };
                list.Add(item);
            }
        }

        public int GetItemsQty(string p_OrderID, string p_ItemCode, string p_ItemName, double p_Length, double p_Width, double p_Thickness, SAPItemGroups p_SAPItemGroups, ref DataTable p_Data)
        {
            DBConnector l_Connection = new DBConnector(HelperDAL.IMOSConnectionString);
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            DataTable l_Data = new DataTable();


            l_Query = "EXEC sp_GetSAPItemQty ";

            PublicFunctions.FieldToParam(p_OrderID, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_ItemCode, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_ItemName, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_Length, ref l_Param, Declarations.FieldTypes.Decimal);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_Width, ref l_Param, Declarations.FieldTypes.Decimal);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_Thickness, ref l_Param, Declarations.FieldTypes.Decimal);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_SAPItemGroups.U_Grp1Name, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_SAPItemGroups.U_Grp2Name, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_SAPItemGroups.U_Grp3Name, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param + ", ";

            PublicFunctions.FieldToParam(p_SAPItemGroups.U_Grp4Name, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;


            if (l_Connection.GetData(l_Query, ref l_Data))
            {
                return Convert.ToInt32(l_Data.Rows[0]["Qty"].ToString());
            }
            else
            {
                return 0;
            }
        }

        public List<MapItem> GetMapItemsOne2OneForSaleDrawer(string orderId)
        {
            List<MapItem> list = new List<MapItem>();
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection
            };
            char[] separator = new char[] { ':' };
            command.CommandText = $"Select ORDERID,ID,RIGHT(F.VALUE,11) ITEMCODE,LEFT(F.VALUE,1)QTY From( Select orderid, id, f.value PvarString From( SELECT orderid, id, f.value As Pvarstring FROM idbverso AS s CROSS APPLY STRING_SPLIT(s.PVARSTRING, '|') as f )T0 CROSS APPLY STRING_SPLIT(PVARSTRING, '=') as f  Where T0.Pvarstring Like '%FG01%')A CROSS APPLY STRING_SPLIT(PVARSTRING, ',') as f Where pvarstring Like '%FG01%' And ORDERID = '{orderId.Split(separator)[1].Trim()}' And PATINDEX('%[A-Z]%', f.value) <> 1 Order by id;";
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                MapItem item = new MapItem
                {
                    IMOSItem = reader["ID"].ToString(),
                    SAPItem = reader["ItemCode"].ToString(),
                    Quantity = Convert.ToDouble(reader["Qty"])
                };
                list.Add(item);
            }
        }

        public List<SaleQuotation> GetSaleQuotationFromIMOS()
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            string l_OrderID = string.Empty;
            DataTable l_Data = new DataTable();
            DataTable l_IMOSData = new DataTable();
            DateTime l_LogDate = DateTime.MinValue;
            DateTime l_IMOSLogDate = DateTime.MinValue;
            List<SaleQuotation> list = new List<SaleQuotation>();

            this.m_Connection.ConnectionString = HelperDAL.ISCConnectionString;

            l_Query = "EXEC sp_GetSaleQuotationFromIMOS";

            if (this.m_Connection.GetData(l_Query, ref l_Data))
            {
                this.m_Connection.ConnectionString = HelperDAL.IMOSConnectionString;

                foreach (DataRow l_Row in l_Data.Rows)
                {
                    l_OrderID = l_Row["DocNum"].ToString();
                    l_LogDate = Convert.ToDateTime(PublicFunctions.ConvertNull(l_Row["LogDate"], DateTime.MinValue));

                    l_Query = "SELECT MAX(LogDate) LogDate From OrderLogging WHERE FUNCTION_KEY = 'Process Data Transfer' AND ORDERID = ";

                    PublicFunctions.FieldToParam(l_OrderID, ref l_Param, Declarations.FieldTypes.String);
                    l_Query += l_Param;

                    if (this.m_Connection.GetData(l_Query, ref l_IMOSData))
                    {
                        l_IMOSLogDate = Convert.ToDateTime(PublicFunctions.ConvertNull(l_IMOSData.Rows[0]["LogDate"], DateTime.MinValue));

                        if (l_IMOSLogDate > DateTime.MinValue || l_IMOSLogDate > l_LogDate)
                        {
                            SaleQuotation item = new SaleQuotation
                            {
                                DocEntry = l_Row["DocEntry"].ToString(),
                                DocNum = l_Row["DocNum"].ToString(),
                                DocType = l_Row["DocType"].ToString(),
                                Canceled = l_Row["CANCELED"].ToString(),
                                DocStatus = l_Row["DocStatus"].ToString(),
                                InvntSttus = l_Row["InvntSttus"].ToString(),
                                Transfered = l_Row["Transfered"].ToString(),
                                ObjType = l_Row["ObjType"].ToString(),
                                DocDate = Convert.ToDateTime(l_Row["DocDate"].ToString()),
                                DocDueDate = l_IMOSLogDate,
                                CardCode = l_Row["CardCode"].ToString(),
                                CardName = l_Row["CardName"].ToString(),
                                IMOS_PO_ID = l_Row["IMOS_PO_ID"].ToString(),
                                U_Type1 = l_Row["U_Type1"].ToString(),
                            };

                            list.Add(item);
                        }
                    }
                }
            }

            return list;
        }

        public SaleQuotation GetSaleQuotationLines(SaleQuotation sq)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = $"SELECT DocEntry, LineNum, TargetType, TrgetEntry, BaseRef, BaseType, BaseEntry, BaseLine, LineStatus, ItemCode, Dscription, Quantity FROM QUT1 where DocEntry='{sq.DocEntry}'"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return sq;
                }
                SaleQuotationLineItem item = new SaleQuotationLineItem
                {
                    DocEntry = reader["DocEntry"].ToString(),
                    LineNum = reader["LineNum"].ToString(),
                    TargetType = reader["TargetType"].ToString(),
                    TrgetEntry = reader["TrgetEntry"].ToString(),
                    BaseRef = reader["BaseRef"].ToString(),
                    BaseType = reader["BaseType"].ToString(),
                    BaseEntry = reader["BaseEntry"].ToString(),
                    BaseLine = reader["BaseLine"].ToString(),
                    LineStatus = reader["LineStatus"].ToString(),
                    ItemCode = reader["ItemCode"].ToString(),
                    Dscription = reader["Dscription"].ToString(),
                    Quantity = Convert.ToDouble(reader["Quantity"].ToString())
                };
                sq.Lines.Add(item);
            }
        }

        public List<SaleQuotation> GetSaleQuotationToIMOS()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<SaleQuotation> list = new List<SaleQuotation>();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT DocEntry, DocNum, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1, IMOS_PO_ID FROM OQUT where Posted_IMOS=0;"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                SaleQuotation item = new SaleQuotation
                {
                    DocEntry = reader["DocEntry"].ToString(),
                    DocNum = reader["DocNum"].ToString(),
                    DocType = reader["DocType"].ToString(),
                    Canceled = reader["CANCELED"].ToString(),
                    DocStatus = reader["DocStatus"].ToString(),
                    InvntSttus = reader["InvntSttus"].ToString(),
                    Transfered = reader["Transfered"].ToString(),
                    ObjType = reader["ObjType"].ToString(),
                    DocDate = Convert.ToDateTime(reader["DocDate"].ToString()),
                    DocDueDate = Convert.ToDateTime(reader["DocDueDate"].ToString()),
                    CardCode = reader["CardCode"].ToString(),
                    CardName = reader["CardName"].ToString(),
                    IMOS_PO_ID = reader["IMOS_PO_ID"].ToString(),
                    U_Type1 = reader["U_Type1"].ToString(),
                };
                list.Add(item);
            }
        }

        public List<SaleQuotation> GetSaleQuotationTOSAP()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<SaleQuotation> list = new List<SaleQuotation>();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT DocEntry, DocNum, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1,IMOS_PO_ID FROM OQUT where Posted_IMOS=1 and Completed_IMOS=1 and Posted_SAP = 0;"
			}.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                SaleQuotation item = new SaleQuotation
                {
                    DocEntry = reader["DocEntry"].ToString(),
                    DocNum = reader["DocNum"].ToString(),
                    DocType = reader["DocType"].ToString(),
                    Canceled = reader["CANCELED"].ToString(),
                    DocStatus = reader["DocStatus"].ToString(),
                    InvntSttus = reader["InvntSttus"].ToString(),
                    Transfered = reader["Transfered"].ToString(),
                    ObjType = reader["ObjType"].ToString(),
                    DocDate = Convert.ToDateTime(reader["DocDate"].ToString()),
                    DocDueDate = Convert.ToDateTime(reader["DocDueDate"].ToString()),
                    CardCode = reader["CardCode"].ToString(),
                    CardName = reader["CardName"].ToString(),
                    IMOS_PO_ID = reader["IMOS_PO_ID"].ToString(),
                    U_Type1 = reader["U_Type1"].ToString(),
                };
                list.Add(item);
            }
        }

        public bool GetSalesQuotationItemList(string p_OrderID, ref DataTable p_Data)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;

            this.m_Connection.ConnectionString = HelperDAL.IMOSConnectionString;

            l_Query = "EXEC sp_GetSAPQuotationItems ";

            PublicFunctions.FieldToParam(p_OrderID, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;

            return this.m_Connection.GetData(l_Query, ref p_Data);

        }

        public bool GetSalesQuotationforSAPItems(string p_OrderID, ref DataTable p_Data)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;

            this.m_Connection.ConnectionString = HelperDAL.ISCConnectionString;

            l_Query = "EXEC sp_GetSAPQuotationItemsISC ";

            PublicFunctions.FieldToParam(p_OrderID, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;

            return this.m_Connection.GetData(l_Query, ref p_Data);
        }

        public bool ClearIMOSMappedTablesforSQ(string p_OrderID)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;

            this.m_Connection.ConnectionString = HelperDAL.ISCConnectionString;

            l_Query = "EXEC sp_ClearIMOSMappedTablesforSQ ";

            PublicFunctions.FieldToParam(p_OrderID, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;

            return this.m_Connection.Execute(l_Query);
        }

        public bool GetQtyConversionFormulas(ref DataTable p_Data)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;

            this.m_Connection.ConnectionString = HelperDAL.IMOSConnectionString;

            l_Query = "EXEC Select QtyFormulaNo, Grp1Name, Grp2Name, Grp3Name, Grp4Name, FormulaDesc FROM VW_QtyConversionDetail ";

            return this.m_Connection.GetData(l_Query, ref p_Data);

        }

        public bool AddTablesDatafromIMOStoISC(string p_TableName, DataTable p_Data)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;

            this.m_Connection.ConnectionString = HelperDAL.ISCConnectionString;

            return this.m_Connection.BulkInsert(p_TableName, p_Data);

        }

        public object GetSaleQuotationToSAP()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<SaleQuotation> list = new List<SaleQuotation>();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT DocEntry, DocNum, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1,IMOS_PO_ID FROM OQUT where Posted_IMOS=1 and Completed_IMOS=1 and Posted_SAP = 0;"
			}.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                SaleQuotation item = new SaleQuotation
                {
                    DocEntry = reader["DocEntry"].ToString(),
                    DocNum = reader["DocNum"].ToString(),
                    DocType = reader["DocType"].ToString(),
                    Canceled = reader["CANCELED"].ToString(),
                    DocStatus = reader["DocStatus"].ToString(),
                    InvntSttus = reader["InvntSttus"].ToString(),
                    Transfered = reader["Transfered"].ToString(),
                    ObjType = reader["ObjType"].ToString(),
                    DocDate = Convert.ToDateTime(reader["DocDate"].ToString()),
                    DocDueDate = Convert.ToDateTime(reader["DocDueDate"].ToString()),
                    CardCode = reader["CardCode"].ToString(),
                    CardName = reader["CardName"].ToString(),
                    IMOS_PO_ID = reader["IMOS_PO_ID"].ToString(),
                    U_Type1 = reader["U_Type1"].ToString()
                };
                list.Add(item);
            }
        }

        public List<string> LoadParentItems(string IMOS_PO_ID)
        {
            List<string> list = new List<string>();
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select distinct PARENTID from IDBEXT where ORDERID='" + IMOS_PO_ID + "' and parentid != ''"
            };
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    command.Dispose();
                    return list;
                }
                list.Add(reader["ParentID"].ToString());
            }
        }

        public bool LoadSampleCompleteDatatoIMOS(string path, string IMOS_PO_ID)
        {
            string[] strArray = File.ReadAllLines(path);
            char[] separator = new char[] { ',' };
            int length = strArray[0].Split(separator).GetLength(0);
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            foreach (string str in strArray)
            {
                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    Transaction = transaction
                };
                string[] textArray1 = new string[] { "INSERT INTO [dbo].[IDBEXT] ([ORDERID],[TYP],[PARTTYPE],[NAME],[SEQUENCE],[NAME2],[COST],[COST2],[COSTCENTER],[STEPTIME],[LENGTH],[WIDTH],[THICKNESS],[ID],[PARENTID],[CNT],[QUANTITY],[ARTICLE_ID],[BARCODE],[ISPEC],[GRID],[GROR],[EDGE_ID],[EDGE_TRANS],[SURF_TRANS],[ORDERPOS],[PRICE],[WEIGHT],[ID_SERIE],[ID_TEXT],[ID_NCNO],[NC_FLAG],[MPE_TYPE],[BOM_FLAG],[CUT_FLAG],[INFO1],[INFO2],[INFO3],[INFO4],[INFO5],[CHECKSUM2],[COLOR1],[COLOR2],[ORDER_ID],[SUPPLIER]) VALUES ('", IMOS_PO_ID, "','", str.Replace(",", "','"), "')" };
                string str2 = string.Concat(textArray1);
                command.CommandText = str2;
                command.ExecuteNonQuery();
            }
            transaction.Commit();
            connection.Close();
            return true;
        }

        public bool MarkPOCompleteInIMOS(string DocEntry)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<SaleQuotation> list = new List<SaleQuotation>();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = $"Update OQUT set Completed_IMOS='true' where DocEntry='{DocEntry}'"
            };
            command.ExecuteNonQuery();
            connection.Close();
            command.Dispose();
            return true;
        }

        public bool SaveBOMToSAP(string IMOS_PO_ID)
        {
            List<string> list = this.LoadParentItems(IMOS_PO_ID);
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            this.oCompany = new SAPDAL().ConnectSAP();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select * from IDBEXT where ORDERID='" + IMOS_PO_ID + "'"
            };
            SqlDataReader reader = command.ExecuteReader();
            string str = string.Empty;
            int num = 0;
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    return true;
                }
                if (reader["ParentID"].ToString() != string.Empty)
                {
                    if (str != reader["ParentID"].ToString())
                    {
                        num = 0;
                    }
                    recordset.DoQuery($"INSERT INTO ITT1 (Father,ChildNum,VisOrder, Code, Quantity, Warehouse) VALUES ('{reader["ParentID"].ToString()}', {num}, {num}, '{reader["ID"].ToString()}','{1}','{"01"}');");
                    num++;
                    str = reader["ParentID"].ToString();
                }
            }
            return false;
        }

        public bool SaveItemsToSAP(string IMOS_PO_ID)
        {
            List<string> list = this.LoadParentItems(IMOS_PO_ID);
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.IMOSConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select * from IDBEXT where ORDERID='" + IMOS_PO_ID + "'"
            };
            SqlDataReader reader = command.ExecuteReader();
            this.oCompany = new SAPDAL().ConnectSAP();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    return true;
                }
                string format = "INSERT INTO OITM (ItemCode, ItemName, FrgnName) VALUES ('{0}', '{1}', '{2}');";
                recordset.DoQuery(string.Format(format, reader["ID"].ToString(), reader["Name"].ToString(), reader["ID"].ToString() + "_" + reader["Typ"].ToString()));
                if (list.Contains(reader["ParentID"].ToString()))
                {
                    object[] objArray1 = new object[11];
                    objArray1[0] = "INSERT INTO OITT(Code, TreeType, PriceList, Qauntity, CreateDate, UpdateDate, Transfered, DataSource, UserSign, SCNCounter, DispCurr, ToWH, Object, LogInstac, UserSign2, OcrCode, HideComp, OcrCode2, OcrCode3, OcrCode4, OcrCode5, UpdateTime, Project, PlAvgSize, Name) VALUES('";
                    objArray1[1] = reader["ID"];
                    objArray1[2] = "', 'P', '1', '1', '";
                    objArray1[3] = DateTime.Now.Date;
                    objArray1[4] = "', '";
                    objArray1[5] = DateTime.Now.Date;
                    objArray1[6] = "', 'N', 'I', '1', '0', '', '01', '66', '0', '0', '', 'N', '', '', '', '', '0', '', '1', '";
                    objArray1[7] = Convert.ToInt16(reader["Typ"].ToString()).ToString();
                    objArray1[8] = "_";
                    objArray1[9] = reader["ID"];
                    objArray1[10] = "');";
                    recordset.DoQuery(string.Concat(objArray1));
                    list.Remove(reader["ParentID"].ToString());
                }
            }
            return false;
        }

        public bool SaveProductionOrdertoSAP(string DocEntry, string IMOS_PO_ID)
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
            int num = 1;
            foreach (string str in this.LoadParentItems(IMOS_PO_ID))
            {
                string str2 = DocEntry + "0" + num;
                string[] textArray1 = new string[] { "select * from IDBEXT where ORDERID='", IMOS_PO_ID, "' and ParentID ='", str, "'" };
                command.CommandText = string.Concat(textArray1);
                SqlDataReader reader = command.ExecuteReader();
                this.oCompany = new SAPDAL().ConnectSAP();
                Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                recordset.DoQuery($"Delete from OWOR where DocEntry='{str2}'");
                object[] args = new object[10];
                args[0] = str2;
                args[1] = DocEntry;
                args[2] = str;
                args[3] = "P";
                args[4] = "S";
                args[5] = 1;
                args[6] = 0;
                args[7] = 0;
                args[8] = DateTime.Now.Date;
                args[9] = DateTime.Now.Date;
                recordset.DoQuery(string.Format("INSERT INTO OWOR (DocEntry,DocNum, ItemCode, Status, Type, PlannedQty, CmpltQty, RjctQty, PostDate, DueDate) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}')", args));
                int num2 = 0;
                num++;
                while (true)
                {
                    if (!reader.Read())
                    {
                        reader.Close();
                        break;
                    }
                    recordset.DoQuery($"Delete from WOR1 where DocEntry={str2}");
                    string format = "INSERT INTO WOR1 (DocEntry, LineNum, ItemCode, BaseQty, PlannedQty, IssuedQty, IssueType, wareHouse, VisOrder) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}');";
                    object[] objArray2 = new object[9];
                    objArray2[0] = str2;
                    objArray2[1] = num2;
                    objArray2[2] = reader["ID"].ToString();
                    objArray2[3] = 1;
                    objArray2[4] = 1;
                    objArray2[5] = 0;
                    objArray2[6] = "B";
                    objArray2[7] = "01";
                    objArray2[8] = num2;
                    recordset.DoQuery(string.Format(format, objArray2));
                    num2++;
                }
            }
            connection.Close();
            command.Dispose();
            return true;
        }

        public bool SaveSaleQuotationtoSAP(string DocEntry, List<SaleQuotationLineItem> LineItems)
        {
            this.oCompany = new SAPDAL().ConnectSAP();
            bool l_Update = false;
            Common l_ISCCommon = new Common();
            string l_Param = string.Empty;
            string l_Query = string.Empty;
            string iscConnectionString = HelperDAL.ISCConnectionString;
			Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
			int num = 1;
            bool l_ObjectUpdate = false;
            Dictionary<string, double> taxRates = new Dictionary<string, double>();
			Dictionary<string, int> uomCodes = new Dictionary<string, int>();

			try
			{
				SAPbobsCOM.Documents oSQ = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations) as SAPbobsCOM.Documents;
				oSQ.GetByKey(Convert.ToInt32(DocEntry));

				foreach (SaleQuotationLineItem item in LineItems)
				{
					item.Updated = 0;

					for (int i = 0; i < oSQ.Lines.Count; i++)
					{
						oSQ.Lines.SetCurrentLine(i);

						try
						{
							if (item.ItemCode == oSQ.Lines.ItemCode)
							{
								oSQ.Lines.Quantity = item.Quantity;

                                item.Updated = 1;

								LogConsumerDAL.Instance.Write(oSQ.Lines.ItemCode + " Updated line");
							}
						}
						catch (Exception ex)
						{
							LogConsumerDAL.Instance.Write(ex.Message);
						}
					}
				}

				oSQ.Update();

				foreach (SaleQuotationLineItem item in LineItems)
				{
					if (item.Updated == 0)
					{
						try
						{

							string format = "INSERT INTO QUT1 (\"DocEntry\", \"LineNum\", \"ItemCode\", \"Dscription\", \"Quantity\",\"DocDate\", \"OpenQty\", \"Price\", \"Currency\", \"Rate\", \"DiscPrcnt\", \"LineTotal\", \"TotalFrgn\",\"VatGroup\",\"VatPrcnt\",\"WhsCode\",\"UomEntry\",\"UomCode\") VALUES ({0}, {1}, '{2}', '{3}', {4}, '{5}', {6}, {7}, '{8}', {9}, {10}, {11},{12},'{13}','{14}','{15}','{16}','{17}');";
							object[] args = new object[18];

                            if (!string.IsNullOrEmpty(item.TaxCode))
                            {
                                if (taxRates.ContainsKey(item.TaxCode))
                                {
                                    item.TaxRate = taxRates[item.TaxCode];
                                }
                                else
                                {
									Recordset recordset1 = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
									recordset1.DoQuery($"SELECT \"Rate\" FROM AVT1 WHERE \"Code\" = '{item.TaxCode}'");

									while (!recordset1.EoF)
									{
                                        item.TaxRate = recordset1.Fields.Item("Rate").Value;

										recordset1.MoveNext();
									}

									//taxRates.Add(item.TaxCode, item.TaxRate);
								}
							}

                            if (!string.IsNullOrEmpty(item.UOM))
                            {
                                if (uomCodes.ContainsKey(item.UOM))
                                {
                                    item.UomEntry = uomCodes[item.UOM];
                                }
                                else
                                {
                                    Recordset recordset1 = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                                    recordset1.DoQuery($"SELECT \"UgpCode\" FROM OUGP WHERE \"UgpEntry\" = '{item.UgpEntry}'");

                                    while (!recordset1.EoF)
                                    {
                                        item.UOM = recordset1.Fields.Item("UgpCode").Value;

                                        recordset1.MoveNext();
                                    }

                                    //uomCodes.Add(item.UOM, item.UgpEntry);
                                }
                            }

                            args[0] = DocEntry;
							args[1] = num++;
							args[2] = item.ItemCode;
							args[3] = item.Dscription;
							args[4] = item.Quantity;
							args[5] = DateTime.Now.Date.ToString("yyyy/MM/dd");
							args[6] = 0;
							args[7] = item.Price;
							args[8] = "";
							args[9] = 0;
							args[10] = 0;
							args[11] = 0;
							args[12] = 0;
							args[13] = item.TaxCode;
							args[14] = item.TaxRate;
							args[15] = item.WhsCode;
							args[16] = item.UgpEntry;
							args[17] = item.UOM;

							recordset.DoQuery(string.Format(format, args));

							//oSQ.Lines.ItemCode = item.ItemCode;
							//oSQ.Lines.Quantity = item.Quantity;
							//oSQ.Lines.ItemDescription = item.Dscription;
							//oSQ.Lines.Price = item.Price;
							//oSQ.Lines.LineTotal = item.GetLineTotal();
							//oSQ.Lines.VatGroup = item.TaxCode;
							//oSQ.Lines.WarehouseCode = item.WhsCode;
							//oSQ.Lines.UoMEntry = item.UgpEntry;

							//oSQ.Lines.Add();

							LogConsumerDAL.Instance.Write(item.ItemCode + " Added new line");
						}
						catch (Exception ex)
						{
							LogConsumerDAL.Instance.Write(item.ItemCode + ex.Message);
						}
					}
				}

				SAPbobsCOM.Documents oSQ1 = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations) as SAPbobsCOM.Documents;
				oSQ1.GetByKey(Convert.ToInt32(DocEntry));

				foreach (SaleQuotationLineItem item in LineItems)
				{
					if (item.Updated == 0)
					{
						try
						{
							for (int i = 0; i < oSQ1.Lines.Count; i++)
							{
								oSQ1.Lines.SetCurrentLine(i);

								try
								{
									if (item.ItemCode == oSQ1.Lines.ItemCode)
									{
										oSQ1.Lines.Quantity = item.Quantity;
                                        l_ObjectUpdate = true;

										LogConsumerDAL.Instance.Write(oSQ1.Lines.ItemCode + " Updated line");
									}
								}
								catch (Exception ex)
								{
									LogConsumerDAL.Instance.Write(ex.Message);
								}
							}
						}
						catch (Exception ex)
						{
							LogConsumerDAL.Instance.Write(item.ItemCode + ex.Message);
						}
					}
				}

                if(l_ObjectUpdate)
				{
					oSQ1.Update();
				}
			}
			catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write(ex.Message + ex.InnerException.ToString());
            }

			l_Query = $"UPDATE OQUT SET Posted_SAP=1 WHERE DocEntry = ";

            PublicFunctions.FieldToParam(DocEntry, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;

            l_ISCCommon.UseConnection(iscConnectionString);
            l_ISCCommon.Execut(l_Query);

            return true;
        }

        public bool TransferSQFromoSAPToISC()
        {
            List<SalesCenters> salesCenters = new SalesCenterDAL().GetSalesCenters();
            this.oCompany = new SAPDAL().ConnectSAP();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            foreach (SalesCenters centers in salesCenters)
            {
                SqlConnection connection = new SqlConnection
                {
                    ConnectionString = HelperDAL.ISCConnectionString
                };
                try
                {
                    connection.Open();
                }
                catch (Exception exception)
                {
                    LogConsumerDAL.Instance.Write($"Connection Failed for Sales Center :{centers.Name}, SQL Server Eror : {exception.Message}");
                    continue;
                }
                SqlTransaction transaction = connection.BeginTransaction();
                DateTime lastSQCreateDateFSAP = this.GetLastSQCreateDateFSAP(centers);
                string lastSalesQuotationFSAP = this.GetLastSalesQuotationFSAP(lastSQCreateDateFSAP, centers);
                char[] separator = new char[] { ',' };
                char[] chArray2 = new char[] { ',' };
                string str2 = lastSalesQuotationFSAP.Split(separator)[lastSalesQuotationFSAP.Split(chArray2).Count<string>() - 1];
                LogConsumerDAL.Instance.Write($"Exporting Sale Qoutations for Sales Center :{centers.Name} from SAP");
                recordset.DoQuery($"SELECT * FROM OQUT where \"DocDate\" >= '{$"{lastSQCreateDateFSAP.Year}/{lastSQCreateDateFSAP.Month}/{lastSQCreateDateFSAP.Day}"}' and \"DocNum\" like '{centers.FirstOrder.Substring(0, 4)}%'");
                LogConsumerDAL.Instance.Write($"Picked Sale Qoutations for Sales Center :{centers.Name} from SAP");
                while (true)
                {
                    if (recordset.EoF)
                    {
                        SqlCommand command = new SqlCommand
                        {
                            Connection = connection,
                            Transaction = transaction,
                            CommandText = "SP_LastSalesQuotation",
                            CommandType = CommandType.StoredProcedure
                        };
                        command.Parameters.AddWithValue("@LastOrder", str2);
                        command.Parameters.AddWithValue("@LastOrderDate", lastSQCreateDateFSAP);
                        command.Parameters.AddWithValue("@Name", centers.Name);
                        command.ExecuteNonQuery();
                        transaction.Commit();
                        command.Dispose();
                        connection.Close();

                        LogConsumerDAL.Instance.Write($"Completed Sale Qoutations for Sales Center :{centers.Name} from SAP");
                        break;
                    }
                    string str3 = recordset.Fields.Item("DocEntry").Value.ToString();
                    DateTime time2 = recordset.Fields.Item("DocDate").Value;
                    if (lastSalesQuotationFSAP.Contains(str3.Trim()))
                    {
                        recordset.MoveNext();
                    }
                    else
                    {
                        SqlCommand command2 = new SqlCommand
                        {
                            Connection = connection,
                            Transaction = transaction
                        };
                        LogConsumerDAL.Instance.Write($"Loading Sale Qoutations :{recordset.Fields.Item("DocNum").Value} for Sales Center :{centers.Name} from SAP to ISC");
                        command2.CommandText = $"Insert into OQUT(DocEntry, DocNum, Series, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1, IMOS_PO_ID,Posted_IMOS,Posted_SAP,Completed_IMOS) values ('{recordset.Fields.Item("DocEntry").Value.ToString()}','{recordset.Fields.Item("DocNum").Value}','{recordset.Fields.Item("Series").Value}','{recordset.Fields.Item("DocType").Value.ToString()}','{recordset.Fields.Item("CANCELED").Value}','{recordset.Fields.Item("DocStatus").Value}','{recordset.Fields.Item("InvntSttus").Value}','{recordset.Fields.Item("Transfered").Value}','{recordset.Fields.Item("ObjType").Value}','{time2.ToString("yyyy/MM/dd")}','{recordset.Fields.Item("DocDueDate").Value.ToString("yyyy/MM/dd")}','{recordset.Fields.Item("CardCode").Value}','{recordset.Fields.Item("CardName").Value.Replace("'", "''")}','{recordset.Fields.Item("U_Type1").Value.Replace("'", "''")}','{HelperDAL.UniqueCode + "-" + recordset.Fields.Item(0).Value}','{false}','{false}','{false}')";
                        command2.ExecuteNonQuery();
                        LogConsumerDAL.Instance.Write($"Loaded Sale Qoutations :{recordset.Fields.Item("DocNum").Value} for Sales Center :{centers.Name} from SAP to ISC");
                        lastSQCreateDateFSAP = recordset.Fields.Item("DocDate").Value;
                        str2 = recordset.Fields.Item("DocEntry").Value.ToString();
                        recordset.MoveNext();
                    }
                }
            }

            return true;
        }

        public bool TransferSQFromISCToIMOS()
        {
            List<SaleQuotation> saleQuotationToIMOS = this.GetSaleQuotationToIMOS();
            SqlConnection iMOSConnection = new SqlConnection();
            SqlConnection ISCConnection = new SqlConnection();

            foreach (SalesCenters item in new SalesCenterDAL().GetSalesCenters())
            {
                List<SaleQuotation> list3 = (from sq in saleQuotationToIMOS
                                             where sq.DocNum.StartsWith(item.FirstOrder.Substring(0, 4))
                                             select sq).ToList<SaleQuotation>();

                iMOSConnection.ConnectionString = new SalesCenterDAL().GetConnectionString(item);

                try
                {
                    iMOSConnection.Open();
                }
                catch (Exception exception)
                {
                    LogConsumerDAL.Instance.Write($"Connection Failed for Sales Center :{item.Name}, SQL Server Eror : {exception.Message}");
                    continue;
                }

                LogConsumerDAL.Instance.Write("Initiating Getting Sales Qoutation Data from ISC to IMOS");

                var appSettings = AppConfiguration.Configuration.GetSection("AppSettings").Get<AppSettings>();

                ISCConnection.ConnectionString = HelperDAL.ISCConnectionString;
                ISCConnection.Open();

                foreach (SaleQuotation quotation in list3)
                {
                    string[] objArray1 = new string[28];
                    SqlTransaction ISCTransaction = ISCConnection.BeginTransaction();
                    SqlTransaction iMOSTransaction = iMOSConnection.BeginTransaction();
                    SqlCommand iMOSCommand = new SqlCommand
                    {
                        Connection = iMOSConnection,
                        Transaction = iMOSTransaction
                    };

                    objArray1[0] = "INSERT INTO [dbo].[CMSPROADMINIMPORT] ([TYPE],[NAME],[COMM],[ARTICLENO],[EMPLOYEE],[DATECREATE],[LCHANGE],[CUSTOMER],[CLIENT],[PROGRAM],[CONTYPE],[DESIGN],[COLOUR1],[COLOUR2],[COLOUR3],[COLOUR4],[COLOUR5],[INFO1],[INFO2],[INFO3],[INFO4],[INFO5],[INFO6],[INFO7],[INFO8],[INFO9],[INFO10],[EDITOR],[DESCRIPTION],[DELIVERY_DATE],[PICTURE_1],[TEXT_SHORT],[TEXT_LONG],[STATUS],[PRODUCTIONID],[STARTDATE],[ENDDATE],[EXPENSE],[RESPONSE],[REFSTAT],[SOURCE],[DOCUMENT],[DOCUMENT_INDEX],[IS_MAINDOC],[BOM_TYP],[DOKV_ID],[IMPORT_CAD],[IMPORT_ERP],[SHIPPING_DATE],[AUF_ID],[STK_ART],[EXPORTED])  VALUES (173, '";
                    objArray1[1] = quotation.DocNum;
                    objArray1[2] = "', 'ERP', '', '";
                    objArray1[3] = appSettings.UserName;
                    objArray1[4] = "', '";
                    objArray1[5] = quotation.DocDate.ToString("yyyy/MM/dd");
                    objArray1[6] = "', '";
                    objArray1[7] = quotation.DocDate.ToString("yyyy/MM/dd");
                    objArray1[8] = "', '";
                    objArray1[9] = quotation.CardCode;
                    objArray1[10] = "', '";
                    objArray1[11] = quotation.CardName;
                    objArray1[12] = "', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '";
                    objArray1[13] = quotation.DocDate.ToString("yyyy/MM/dd");
                    objArray1[14] = "', '','";
                    objArray1[15] = quotation.U_Type1;
                    objArray1[16] = "',";
                    objArray1[17] = "'', 1, 0, '', '', '', '', '0', '";
                    objArray1[18] = appSettings.UserName;
                    objArray1[19] = "', '";
                    objArray1[20] = quotation.IMOS_PO_ID;
                    objArray1[21] = "', 0, 1, 'A', 0, '";
                    objArray1[22] = quotation.DocDate.ToString("yyyy/MM/dd");
                    objArray1[23] = "', '";
                    objArray1[24] = quotation.DocDate.ToString("yyyy/MM/dd");
                    objArray1[25] = "', '";
                    objArray1[26] = quotation.DocDate.ToString("yyyy/MM/dd");
                    objArray1[27] = "', 0, '', 0)";

                    string str = string.Concat(objArray1);

                    iMOSCommand.CommandText = str;
                    iMOSCommand.ExecuteNonQuery();

                    new SqlCommand
                    {
                        Connection = ISCConnection,
                        Transaction = ISCTransaction,
                        CommandText = "Update OQUT set POSTED_IMOS='true' where DocEntry=" + quotation.DocEntry
                    }.ExecuteNonQuery();

                    LogConsumerDAL.Instance.Write($"Loaded Sale Qoutations :{quotation.DocNum} for Sales Center :{item.Name} from ISC to IMOS");

                    iMOSTransaction.Commit();
                    ISCTransaction.Commit();
                }

                iMOSConnection.Close();
                ISCConnection.Close();
            }

            return true;
        }

        public bool TransferSQFromISCToSAP()
        {
            List<SaleQuotation> saleQuotationToSAP = this.GetSaleQuotationTOSAP();

            foreach (SaleQuotation quotation in saleQuotationToSAP)
            {
                List<SaleQuotationLineItem> list = new List<SaleQuotationLineItem>();
                DataTable l_Data = new DataTable();
                DataTable l_ISCData = new DataTable();

                try
                {
                    if (this.GetSalesQuotationItemList(quotation.DocNum, ref l_Data))
                    {
                        this.ClearIMOSMappedTablesforSQ(quotation.DocNum);

                        if (this.AddTablesDatafromIMOStoISC("SalesQuotationItemsList", l_Data))
                        {
                            if (this.GetSalesQuotationforSAPItems(quotation.DocNum, ref l_ISCData))
                            {
                                foreach (DataRow l_Row in l_ISCData.Rows)
                                {
                                    try
                                    {
                                        SaleQuotationLineItem item = new SaleQuotationLineItem
                                        {
                                            LineNum = l_Row["Line_No"].ToString(),
                                            DocEntry = quotation.DocEntry,
                                            ItemCode = l_Row["ItemCode"].ToString(),
                                            WhsCode = l_Row["DfltWH"].ToString(),
                                            TaxCode = l_Row["VatGourpSa"].ToString(),
                                            Price = PublicFunctions.ConvertNullAsDouble(l_Row["Price"].ToString(), 0.0),
                                            Dscription = l_Row["ItemName"].ToString(),
                                            UOM = l_Row["SalUnitMsr"].ToString(),
                                            Quantity = PublicFunctions.ConvertNullAsDouble(l_Row["Qty"].ToString(), 0.0),
                                            UgpEntry = Convert.ToInt32(l_Row["UgpEntry"].ToString())
										};

                                        list.Add(item);
                                    }
                                    catch (Exception ex)
                                    {
                                        LogConsumerDAL.Instance.Write($"Adding List Line {l_Row["Line_No"].ToString()} for SQ {quotation.DocEntry} caused exception {ex.Message}");
                                    }
                                }

                                LogConsumerDAL.Instance.Write($"Saving in SAP the SQ {quotation.DocEntry} with {list.Count} lines.");
                                this.SaveSaleQuotationtoSAP(quotation.DocEntry, list);
                                LogConsumerDAL.Instance.Write($"Saved in SAP the SQ {quotation.DocEntry} with {list.Count} lines.");
                            }
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
                    l_ISCData.Dispose();

                    list.Clear();
                }
            }

            return true;
        }

        public bool TransferSQFromIMOSToISC()
        {
            LogConsumerDAL.Instance.Write("Initiating Getting Sales Qoutation Data from IMOS to ISC");

            List<SaleQuotation> saleQuotationToIMOS = this.GetSaleQuotationFromIMOS();
            string iscConnectionString = HelperDAL.ISCConnectionString;
            Common l_IMOSCommon = new Common();
            Common l_ISCCommon = new Common();
            string l_Param = string.Empty;
            string l_Query = string.Empty;

            l_ISCCommon.UseConnection(iscConnectionString);
            foreach (SalesCenters item in new SalesCenterDAL().GetSalesCenters())
            {
                List<SaleQuotation> list3 = (from sq in saleQuotationToIMOS
                                             where sq.DocNum.StartsWith(item.FirstOrder.Substring(0, 4))
                                             select sq).ToList<SaleQuotation>();
                string imosConnectionString = new SalesCenterDAL().GetConnectionString(item);
                l_IMOSCommon.UseConnection(imosConnectionString);

                foreach (SaleQuotation quotation in list3)
                {
                    DataTable dataTable = new DataTable();

                    Declarations.g_ConnectionString = imosConnectionString;
                    if (l_IMOSCommon.GetList($"SELECT OrderID FROM IDBORDEREXT WHERE OrderID = '{quotation.DocNum}'", ref dataTable))
                    {
                        l_Query = "UPDATE OQUT SET LogDate = ";

                        PublicFunctions.FieldToParam(quotation.DocDueDate, ref l_Param, Declarations.FieldTypes.Date);
                        l_Query += l_Param + ", Completed_IMOS=1, Posted_SAP=0 WHERE DocNum = ";

                        PublicFunctions.FieldToParam(quotation.DocNum, ref l_Param, Declarations.FieldTypes.String);
                        l_Query += l_Param;

                        l_ISCCommon.Execut(l_Query);

                        LogConsumerDAL.Instance.Write($"Loaded Sale Qoutations :{quotation.DocNum} for Sales Center :{item.Name} from IMOS to ISC");
                    }

                    dataTable.Dispose();
                }
            }

            return true;
        }

        public bool CreateSQFromOP()
        {
            DataTable l_Data = new DataTable();
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            int l_OOPRID = 0;

            try
            {
                m_Connection = new DBConnector(HelperDAL.ISCConnectionString);

                this.oCompany = new SAPDAL().ConnectSAP();
                Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);

                l_Query = "SELECT MAX(OpprId) OpprId FROM OOPR WITH (NOLOCK)";

                if (!this.m_Connection.GetData(l_Query, ref l_Data))
                {
                    return false;
                }

                l_OOPRID = PublicFunctions.ConvertNullAsInteger(l_Data.Rows[0]["OPPRID"], 0);

                recordset.DoQuery($"SELECT \"OpprId\",\"CardCode\",\"Source\" FROM OOPR WHERE \"OpprId\" > {l_OOPRID}");

                while (!recordset.EoF)
                {
                    l_Query = $"INSERT INTO OOPR(OpprId, CardCode, CardName, UPSAP, Source) VALUES ('{recordset.Fields.Item("OpprId").Value}', '{recordset.Fields.Item("CardCode").Value}', '{recordset.Fields.Item("CardCode").Value}', '{false}', '{recordset.Fields.Item("Source").Value}')";

                    this.m_Connection.Execute(l_Query);

                    recordset.MoveNext();
                }

                this.CreateSQInSAP();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                l_Data.Dispose();
            }

            return true;
        }

        private bool CreateSQInSAP()
        {
            DataTable l_Data = new DataTable();
            DateTime l_DocDate = DateTime.Now;
            SAPbobsCOM.Documents oSalesQuotation;

            string l_Query = string.Empty;
            string l_Param = string.Empty;
            string l_CardCode = string.Empty;
            string l_ItemID = "FG050000340";
            int l_OOPRID = 0;
			int l_Source = 0;
			int lRetCode;

            try
            {
                m_Connection = new DBConnector(HelperDAL.ISCConnectionString);
                this.oCompany = new SAPDAL().ConnectSAP();

                l_Query = "SELECT CardCode,OPPRID,Source FROM OOPR WITH (NOLOCK) WHERE ISNULL(UPSAP,0) = 0";

                if (this.m_Connection.GetData(l_Query, ref l_Data))
                {
                    l_CardCode = PublicFunctions.ConvertNullAsString(l_Data.Rows[0]["CardCode"], string.Empty);
                    l_OOPRID = PublicFunctions.ConvertNullAsInteger(l_Data.Rows[0]["OPPRID"], 0);
					l_Source = PublicFunctions.ConvertNullAsInteger(l_Data.Rows[0]["Source"], 0);

					foreach (DataRow l_Row in l_Data.Rows)
                    {
                        oSalesQuotation = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                        oSalesQuotation.CardCode = l_CardCode;
                        oSalesQuotation.DocDate = DateTime.Now.Date;
                        oSalesQuotation.TaxDate = DateTime.Now.Date;
                        oSalesQuotation.DocDueDate = DateTime.Now.Date;
                        oSalesQuotation.Series = l_Source;

						oSalesQuotation.Lines.Add();

						oSalesQuotation.Lines.ItemCode = l_ItemID;
                        oSalesQuotation.Lines.Quantity = 1;
                        oSalesQuotation.Lines.Price = 0;

						lRetCode = oSalesQuotation.Add();

						if (lRetCode != 0)
                        {
                            string sErrDesc = this.oCompany.GetLastErrorDescription();

                            LogConsumerDAL.Instance.Write($"Unabel to create SQ for Opportunity {l_OOPRID}" + sErrDesc);
                        }
                        else
                        {
                            PublicFunctions.FieldToParam(l_OOPRID, ref l_Param, Declarations.FieldTypes.Number);
                            l_Query = "UPDATE OOPR SET UPSAP = 1 WHERE OPPRID = " + l_Param;

                            this.m_Connection.Execute(l_Query);
                        }
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

            return true;
        }
    }
}

