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
        public static AppSettings appSettings = HelperDAL.GetSettings();
        private Company oCompany = new SAPbobsCOM.Company();
        private DBConnector m_Connection = new DBConnector();

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
                CommandText = $"SELECT DocEntry FROM OQUT WITH (NOLOCK) where DocNum='{orderId}'"
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
                CommandText = $"select isnull(max(docdate),0) LastDate from oqut WITH (NOLOCK) where DocNum like '{sc.FirstOrder.Substring(0, 4)}%'"
            };
            DateTime time = Convert.ToDateTime(command.ExecuteScalar());
            connection.Close();
            command.Dispose();
            DateTime Jan1st1900 = new DateTime(1900, 1, 1);
            if (time <= Jan1st1900)
            {
                time = DateTime.Now.AddDays(-30);
            }
            return time;
        }

        private bool IsSQExists(string docNum)
        {
            DBConnector l_Conn = new DBConnector();
            DataTable l_Data = new DataTable();

            l_Conn.ConnectionString = HelperDAL.ISCConnectionString;

            try
            {
                return l_Conn.GetData($"SELECT DocNum FROM OQUT WITH (NOLOCK) WHERE DocNum = '{docNum}'", ref l_Data);
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

                    l_Query = "SELECT MAX(LogDate) LogDate From OrderLogging WITH (NOLOCK) WHERE FUNCTION_KEY = 'Process Data Transfer' AND ORDERID = ";

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

        public List<SaleQuotation> GetSaleQuotationToIMOS()
        {
            DBConnector l_Conn = new DBConnector();
            DataTable l_Data = new DataTable();
            List<SaleQuotation> list = new List<SaleQuotation>();

            l_Conn.ConnectionString = HelperDAL.ISCConnectionString;
            l_Conn.GetData("SELECT DocEntry, DocNum, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1, IMOS_PO_ID FROM OQUT WITH (NOLOCK) where Posted_IMOS=0", ref l_Data);

            foreach (DataRow l_Row in l_Data.Rows)
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
                    DocDueDate = Convert.ToDateTime(l_Row["DocDueDate"].ToString()),
                    CardCode = l_Row["CardCode"].ToString(),
                    CardName = l_Row["CardName"].ToString(),
                    IMOS_PO_ID = l_Row["IMOS_PO_ID"].ToString(),
                    U_Type1 = l_Row["U_Type1"].ToString(),
                };

                list.Add(item);
            }

            l_Data.Dispose();

            return list;
        }

        public List<SaleQuotation> GetSaleQuotationTOSAP()
        {
            DBConnector l_Conn = new DBConnector();
            DataTable l_Data = new DataTable();
            List<SaleQuotation> list = new List<SaleQuotation>();


            l_Conn.ConnectionString = HelperDAL.ISCConnectionString;
            l_Conn.GetData("SELECT DocEntry, DocNum, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1,IMOS_PO_ID FROM OQUT where Posted_IMOS=1 and Completed_IMOS=1 and Posted_SAP = 0", ref l_Data);

            foreach (DataRow l_Row in l_Data.Rows)
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
                    DocDueDate = Convert.ToDateTime(l_Row["DocDueDate"].ToString()),
                    CardCode = l_Row["CardCode"].ToString(),
                    CardName = l_Row["CardName"].ToString(),
                    IMOS_PO_ID = l_Row["IMOS_PO_ID"].ToString(),
                    U_Type1 = l_Row["U_Type1"].ToString(),
                };

                list.Add(item);
            }

            l_Data.Dispose();

            return list;
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
            DBConnector l_Conn = new DBConnector();
            DataTable l_Data = new DataTable();
            List<SaleQuotation> list = new List<SaleQuotation>();

            l_Conn.ConnectionString = HelperDAL.ISCConnectionString;
            l_Conn.GetData("SELECT DocEntry, DocNum, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1,IMOS_PO_ID FROM OQUT WITH (NOLOCK) where Posted_IMOS=1 and Completed_IMOS=1 and Posted_SAP = 0", ref l_Data);

            foreach (DataRow l_Row in l_Data.Rows)
            {
                SaleQuotation item = new SaleQuotation
                {
                    DocEntry = PublicFunctions.ConvertNullAsString(l_Row["DocEntry"], string.Empty),
                    DocNum = PublicFunctions.ConvertNullAsString(l_Row["DocNum"], string.Empty),
                    DocType = PublicFunctions.ConvertNullAsString(l_Row["DocType"], string.Empty),
                    Canceled = PublicFunctions.ConvertNullAsString(l_Row["CANCELED"], string.Empty),
                    DocStatus = PublicFunctions.ConvertNullAsString(l_Row["DocStatus"], string.Empty),
                    InvntSttus = PublicFunctions.ConvertNullAsString(l_Row["InvntSttus"], string.Empty),
                    Transfered = PublicFunctions.ConvertNullAsString(l_Row["Transfered"], string.Empty),
                    ObjType = PublicFunctions.ConvertNullAsString(l_Row["ObjType"], string.Empty),
                    DocDate = Convert.ToDateTime(l_Row["DocDate"].ToString()),
                    DocDueDate = Convert.ToDateTime(l_Row["DocDueDate"].ToString()),
                    CardCode = PublicFunctions.ConvertNullAsString(l_Row["CardCode"], string.Empty),
                    CardName = PublicFunctions.ConvertNullAsString(l_Row["CardName"], string.Empty),
                    IMOS_PO_ID = PublicFunctions.ConvertNullAsString(l_Row["IMOS_PO_ID"], string.Empty),
                    U_Type1 = PublicFunctions.ConvertNullAsString(l_Row["U_Type1"], string.Empty)
                };

                list.Add(item);
            }

            l_Data.Dispose();

            return list;
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

        public bool TransferSQFromoSAPToISC()
        {
            try
            {
                LogConsumerDAL.Instance.Write($"Starting SAP to ISC");

                List<SalesCenters> salesCenters = new SalesCenterDAL().GetSalesCenters();
                this.oCompany = new SAPDAL().ConnectSAP();
                Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                DBConnector l_Conn = new DBConnector();
                string lastSQNo = string.Empty;

                l_Conn.ConnectionString = HelperDAL.ISCConnectionString;

                foreach (SalesCenters centers in salesCenters)
                {
                    try
                    {
                        DateTime lastSQCreateDateFSAP = this.GetLastSQCreateDateFSAP(centers);

                        lastSQNo = string.Empty;

                        recordset.DoQuery($"SELECT * FROM OQUT where \"DocDate\" >= '{$"{lastSQCreateDateFSAP.Year}/{lastSQCreateDateFSAP.Month}/{lastSQCreateDateFSAP.Day}"}' and \"DocNum\" like '{centers.FirstOrder.Substring(0, 4)}%'");

                        while (true)
                        {
                            if (recordset.EoF)
                            {
                                if (!string.IsNullOrEmpty(lastSQNo))
                                {
                                    l_Conn.Execute($"EXEC SP_LastSalesQuotation '{lastSQNo}', '{centers.Name}', '{lastSQCreateDateFSAP.ToShortDateString()}'");
                                }

                                break;
                            }

                            string docNum = recordset.Fields.Item("DocNum").Value.ToString();
                            DateTime time2 = recordset.Fields.Item("DocDate").Value;

                            if (IsSQExists(docNum))
                            {
                                recordset.MoveNext();
                            }
                            else
                            {
                                LogConsumerDAL.Instance.Write($"Loading Sale Qoutations :{recordset.Fields.Item("DocNum").Value} for Sales Center :{centers.Name} from SAP to ISC");
                                l_Conn.Execute($"Insert into OQUT(DocEntry, DocNum, Series, DocType, CANCELED, DocStatus, InvntSttus, Transfered, ObjType, DocDate, DocDueDate, CardCode, CardName, U_Type1, IMOS_PO_ID,Posted_IMOS,Posted_SAP,Completed_IMOS) values ('{recordset.Fields.Item("DocEntry").Value.ToString()}','{recordset.Fields.Item("DocNum").Value}','{recordset.Fields.Item("Series").Value}','{recordset.Fields.Item("DocType").Value.ToString()}','{recordset.Fields.Item("CANCELED").Value}','{recordset.Fields.Item("DocStatus").Value}','{recordset.Fields.Item("InvntSttus").Value}','{recordset.Fields.Item("Transfered").Value}','{recordset.Fields.Item("ObjType").Value}','{time2.ToString("yyyy/MM/dd")}','{recordset.Fields.Item("DocDueDate").Value.ToString("yyyy/MM/dd")}','{recordset.Fields.Item("CardCode").Value}','{recordset.Fields.Item("CardName").Value.Replace("'", "''")}','{recordset.Fields.Item("U_Type1").Value.Replace("'", "''")}','{HelperDAL.UniqueCode + "-" + recordset.Fields.Item(0).Value}','{false}','{false}','{false}')");

                                lastSQCreateDateFSAP = recordset.Fields.Item("DocDate").Value;
                                lastSQNo = recordset.Fields.Item("DocEntry").Value.ToString();

                                recordset.MoveNext();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogConsumerDAL.Instance.Write($"Exception SAP to ISC for Sales Center: {centers.Name} : {ex.Message}");
                    }
                }

                LogConsumerDAL.Instance.Write($"Completed SAP to ISC");
            }
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write($"Exception SAP to ISC: {ex.Message}");
            }

            return true;
        }

        public bool TransferSQFromISCToIMOS()
        {
            LogConsumerDAL.Instance.Write("Starting ISC to IMOS");

            try
            {
                List<SaleQuotation> saleQuotationToIMOS = this.GetSaleQuotationToIMOS();
                DBConnector iMOSConnection = new DBConnector();
                DBConnector ISCConnection = new DBConnector();

                ISCConnection.ConnectionString = HelperDAL.ISCConnectionString;
                iMOSConnection.ConnectionString = HelperDAL.IMOSConnectionString;

                foreach (SalesCenters item in new SalesCenterDAL().GetSalesCenters())
                {
                    List<SaleQuotation> list3 = (from sq in saleQuotationToIMOS
                                                 where sq.DocNum.StartsWith(item.FirstOrder.Substring(0, 4))
                                                 select sq).ToList<SaleQuotation>();

                    foreach (SaleQuotation quotation in list3)
                    {
                        string[] objArray1 = new string[28];
                        string l_param = string.Empty;

                        try
                        {
                            objArray1[0] = "INSERT INTO [dbo].[CMSPROADMINIMPORT] ([TYPE],[NAME],[COMM],[ARTICLENO],[EMPLOYEE],[DATECREATE],[LCHANGE],[CUSTOMER],[CLIENT],[PROGRAM],[CONTYPE],[DESIGN],[COLOUR1],[COLOUR2],[COLOUR3],[COLOUR4],[COLOUR5],[INFO1],[INFO2],[INFO3],[INFO4],[INFO5],[INFO6],[INFO7],[INFO8],[INFO9],[INFO10],[EDITOR],[DESCRIPTION],[DELIVERY_DATE],[PICTURE_1],[TEXT_SHORT],[TEXT_LONG],[STATUS],[PRODUCTIONID],[STARTDATE],[ENDDATE],[EXPENSE],[RESPONSE],[REFSTAT],[SOURCE],[DOCUMENT],[DOCUMENT_INDEX],[IS_MAINDOC],[BOM_TYP],[DOKV_ID],[IMPORT_CAD],[IMPORT_ERP],[SHIPPING_DATE],[AUF_ID],[STK_ART],[EXPORTED])  VALUES (173, '";
                            objArray1[1] = quotation.DocNum;
                            objArray1[2] = "', 'ERP', '', ";
                            PublicFunctions.FieldToParam(appSettings.UserName, ref l_param, Declarations.FieldTypes.String);
                            objArray1[3] = l_param;
                            objArray1[4] = ", '";
                            objArray1[5] = quotation.DocDate.ToString("yyyy/MM/dd");
                            objArray1[6] = "', '";
                            objArray1[7] = quotation.DocDate.ToString("yyyy/MM/dd");
                            objArray1[8] = "', '";
                            objArray1[9] = quotation.CardCode;
                            objArray1[10] = "', ";
                            PublicFunctions.FieldToParam(quotation.CardName, ref l_param, Declarations.FieldTypes.String);
                            objArray1[11] = l_param;
                            objArray1[12] = ", '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '";
                            objArray1[13] = quotation.DocDate.ToString("yyyy/MM/dd");
                            objArray1[14] = "', '','";
                            objArray1[15] = quotation.U_Type1;
                            objArray1[16] = "',";
                            objArray1[17] = "'', 1, 0, '', '', '', '', '0', ";
                            PublicFunctions.FieldToParam(appSettings.UserName, ref l_param, Declarations.FieldTypes.String);
                            objArray1[18] = l_param;
                            objArray1[19] = ", '";
                            objArray1[20] = quotation.IMOS_PO_ID;
                            objArray1[21] = "', 0, 1, 'A', 0, '";
                            objArray1[22] = quotation.DocDate.ToString("yyyy/MM/dd");
                            objArray1[23] = "', '";
                            objArray1[24] = quotation.DocDate.ToString("yyyy/MM/dd");
                            objArray1[25] = "', '";
                            objArray1[26] = quotation.DocDate.ToString("yyyy/MM/dd");
                            objArray1[27] = "', 0, '', 0)";

                            string str = string.Concat(objArray1);

                            iMOSConnection.Execute(str);

                            ISCConnection.Execute("Update OQUT set POSTED_IMOS=1 where DocEntry=" + quotation.DocEntry);

                            LogConsumerDAL.Instance.Write($"Loaded Sale Qoutations :{quotation.DocNum} for Sales Center :{item.Name} from ISC to IMOS");
                        }
                        catch (Exception ex)
                        {
                            LogConsumerDAL.Instance.Write($"Exception Sale Qoutations :{quotation.DocNum}: {ex.Message}");
                        }
                    }
                }

                LogConsumerDAL.Instance.Write("Completed ISC to IMOS");
            }
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write($"Exception ISC to IMOS: {ex.Message}");
            }

            return true;
        }

        public bool TransferSQFromIMOSToISC()
        {
            LogConsumerDAL.Instance.Write("Starting IMOS to ISC");

            try
            {
                List<SaleQuotation> saleQuotationToIMOS = this.GetSaleQuotationFromIMOS();
                string iscConnectionString = HelperDAL.ISCConnectionString;
                Common l_IMOSCommon = new Common();
                Common l_ISCCommon = new Common();
                string l_Param = string.Empty;
                string l_Query = string.Empty;

                l_ISCCommon.UseConnection(iscConnectionString);
                foreach (SalesCenters item in new SalesCenterDAL().GetSalesCenters())
                {
                    try
                    {
                        List<SaleQuotation> list3 = (from sq in saleQuotationToIMOS
                                                     where sq.DocNum.StartsWith(item.FirstOrder.Substring(0, 4))
                                                     select sq).ToList<SaleQuotation>();
                        l_IMOSCommon.UseConnection(HelperDAL.IMOSConnectionString);

                        foreach (SaleQuotation quotation in list3)
                        {
                            DataTable dataTable = new DataTable();

                            try
                            {
                                l_IMOSCommon.UseConnection(HelperDAL.IMOSConnectionString);
                                if (l_IMOSCommon.GetList($"SELECT OrderID FROM IDBORDEREXT WITH (NOLOCK) WHERE OrderID = '{quotation.DocNum}'", ref dataTable))
                                {
                                    l_Query = "UPDATE OQUT SET LogDate = ";

                                    PublicFunctions.FieldToParam(quotation.DocDueDate, ref l_Param, Declarations.FieldTypes.Date);
                                    l_Query += l_Param + ", Completed_IMOS=1, Posted_SAP=0 WHERE DocNum = ";

                                    PublicFunctions.FieldToParam(quotation.DocNum, ref l_Param, Declarations.FieldTypes.String);
                                    l_Query += l_Param;

                                    l_ISCCommon.Execut(l_Query);

                                    LogConsumerDAL.Instance.Write($"Loaded Sale Qoutations :{quotation.DocNum} for Sales Center :{item.Name} from IMOS to ISC");
                                }
                            }
                            catch (Exception ex)
                            {
                                LogConsumerDAL.Instance.Write($"Exception Sale Qoutations :{quotation.DocNum} : {ex.Message}");
                            }
                            finally
                            {
                                dataTable.Dispose();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogConsumerDAL.Instance.Write($"Exception for Sales Center :{item.Name} : {ex.Message}");
                    }
                }

                LogConsumerDAL.Instance.Write("Completed IMOS to ISC");
            }
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write($"Exception IMOS to ISC: {ex.Message}");
            }

            return true;
        }

        public bool TransferSQFromISCToSAP()
        {
            LogConsumerDAL.Instance.Write($"Starting ISC To SAP");

            List<SaleQuotation> saleQuotationToSAP = this.GetSaleQuotationTOSAP();

            try
            {
                LogConsumerDAL.Instance.Write($"Found [{saleQuotationToSAP.Count}] SQs to Sync.");

                foreach (SaleQuotation quotation in saleQuotationToSAP)
                {
                    List<SaleQuotationLineItem> list = new List<SaleQuotationLineItem>();
                    DataTable l_Data = new DataTable();
                    DataTable l_ISCData = new DataTable();

                    try
                    {
                        LogConsumerDAL.Instance.Write($"Getting SQ {quotation.DocNum} from IMOS.");
                        if (this.GetSalesQuotationItemList(quotation.DocNum, ref l_Data))
                        {
                            this.ClearIMOSMappedTablesforSQ(quotation.DocNum);

                            LogConsumerDAL.Instance.Write($"Saving SQ {quotation.DocNum} in ISC.");
                            if (this.AddTablesDatafromIMOStoISC("SalesQuotationItemsList", l_Data))
                            {
                                LogConsumerDAL.Instance.Write($"Preparing SQ {quotation.DocNum} in ISC.");
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
                                                UgpEntry = Convert.ToInt32(l_Row["UgpEntry"].ToString()),
                                                SequenceNo = PublicFunctions.ConvertNullAsDouble(l_Row["SequenceNo"].ToString(), 0)
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
                        else
                        {
                            LogConsumerDAL.Instance.Write($"No data found for SQ {quotation.DocNum}.");
                        }
                    }
                    catch (Exception ex)
                    {
                        LogConsumerDAL.Instance.Write($"Exception while saving SQs in SAP: {ex.Message}");

                        throw;
                    }
                    finally
                    {
                        l_Data.Dispose();
                        l_ISCData.Dispose();

                        list.Clear();
                    }
                }

                LogConsumerDAL.Instance.Write($"Completed ISC To SAP");
            }
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write($"Exception ISC To SAP: {ex.Message}");
            }

            return true;
        }

        public bool SaveSaleQuotationtoSAP(string DocEntry, List<SaleQuotationLineItem> LineItems)
        {
            this.oCompany = new SAPDAL().ConnectSAP();

            Common l_ISCCommon = new Common();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            Dictionary<string, double> taxRates = new Dictionary<string, double>();
            Dictionary<int, string> uomCodes = new Dictionary<int, string>();
            Dictionary<string, Dictionary<string, Item>> itemCodes = new Dictionary<string, Dictionary<string, Item>>();
            Dictionary<string, string> whsCodes = new Dictionary<string, string>();
            List<string> taxList = new List<string>();
            List<string> whsList = new List<string>();
            List<int> uomList = new List<int>();
            string l_Param = string.Empty;
            string l_Query = string.Empty;
            string iscConnectionString = HelperDAL.ISCConnectionString;
            string cardCode = string.Empty;
            bool l_Update = false;
            bool l_ObjectUpdate = false;
            int num = 1;
            int priceList = -1;
            int index = 0;
            double grossTotal = 0;

            try
            {
                SAPbobsCOM.Documents oSQ = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations) as SAPbobsCOM.Documents;
                oSQ.GetByKey(Convert.ToInt32(DocEntry));

                cardCode = oSQ.CardCode;

                Recordset recordset2 = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                recordset2.DoQuery($"SELECT \"ListNum\", \"Discount\" FROM OCRD WHERE \"CardCode\" = '{cardCode}'");

                while (!recordset2.EoF)
                {
                    priceList = recordset2.Fields.Item("ListNum").Value;

                    recordset2.MoveNext();
                }

                l_Query = $"SELECT I.\"ItemCode\", I.\"ItemName\", I.\"validFor\", I.\"frozenFor\", I.\"SalUnitMsr\", I.\"U_InCharges\", I.\"FrgnName\", P.\"Price\", P.\"Currency\", C.\"AvgPrice\", C.\"WhsCode\" FROM OITM I LEFT OUTER JOIN ITM1 P ON I.\"ItemCode\" = P.\"ItemCode\"  LEFT OUTER JOIN OITW C ON I.\"ItemCode\" = C.\"ItemCode\" WHERE P.\"PriceList\" = {priceList} AND I.\"ItemCode\" IN (";

                foreach (SaleQuotationLineItem item in LineItems)
                {
                    l_Query += $"'{item.ItemCode}'";

                    if (index < LineItems.Count - 1)
                    {
                        l_Query += ",";
                    }
                    else
                    {
                        l_Query += ")";
                    }

                    if (!whsList.Contains(item.WhsCode))
                    {
                        whsList.Add(item.WhsCode);
                    }

                    index++;
                }

                recordset2.DoQuery(l_Query);

                while (!recordset2.EoF)
                {
                    Dictionary<string, Item> itemWhs;
                    Item lineItem = new Item();

                    if (itemCodes.ContainsKey(recordset2.Fields.Item("ItemCode").Value))
                    {
                        itemWhs = itemCodes[recordset2.Fields.Item("ItemCode").Value];
                    }
                    else
                    {
                        itemWhs = new Dictionary<string, Item>();
                    }

                    lineItem.ItemCode = recordset2.Fields.Item("ItemCode").Value;
                    lineItem.ItemName = recordset2.Fields.Item("ItemName").Value;
                    lineItem.SalUnitMsr = recordset2.Fields.Item("SalUnitMsr").Value;
                    lineItem.U_InCharges = recordset2.Fields.Item("U_InCharges").Value;
                    lineItem.FrgnName = recordset2.Fields.Item("FrgnName").Value;
                    lineItem.Price = recordset2.Fields.Item("Price").Value;
                    lineItem.Currency = recordset2.Fields.Item("Currency").Value;
                    lineItem.AvgCost = recordset2.Fields.Item("AvgPrice").Value;
                    lineItem.DfltWH = recordset2.Fields.Item("WhsCode").Value;
                    lineItem.Active = recordset2.Fields.Item("validFor").Value;
                    lineItem.Inactive = recordset2.Fields.Item("frozenFor").Value;

                    lineItem.Active = lineItem.Active.ToUpper();
                    lineItem.Inactive = lineItem.Inactive.ToUpper();

                    itemWhs.Add(lineItem.DfltWH, lineItem);

                    if (!itemCodes.ContainsKey(lineItem.ItemCode))
                    {
                        itemCodes.Add(lineItem.ItemCode, itemWhs);
                    }

                    recordset2.MoveNext();
                }

                l_Query = $"SELECT \"WhsCode\", \"Street\" FROM OWHS WHERE \"WhsCode\" IN (";

                index = 0;
                foreach (string whsCode in whsList)
                {
                    l_Query += $"'{whsCode}'";

                    if (index < whsList.Count - 1)
                    {
                        l_Query += ",";
                    }
                    else
                    {
                        l_Query += ")";
                    }

                    index++;
                }

                recordset2.DoQuery(l_Query);

                while (!recordset2.EoF)
                {
                    if (!whsCodes.ContainsKey(recordset2.Fields.Item("WhsCode").Value))
                    {
                        whsCodes.Add(recordset2.Fields.Item("WhsCode").Value, recordset2.Fields.Item("Street").Value);
                    }

                    recordset2.MoveNext();
                }

                foreach (SaleQuotationLineItem item in LineItems)
                {
                    Item lineItem = null;

                    item.Updated = 0;

                    try
                    {
                        if (itemCodes.ContainsKey(item.ItemCode))
                        {
                            lineItem = itemCodes[item.ItemCode][item.WhsCode];
                        }

                        if (lineItem?.Active == "Y" && lineItem?.Inactive != "Y")
                        {
                            if (!string.IsNullOrEmpty(item.WhsCode))
                            {
                                if (whsCodes.ContainsKey(item.WhsCode))
                                {
                                    item.WhsAddress = whsCodes[item.WhsCode];
                                }
                            }

                            for (int i = 0; i < oSQ.Lines.Count; i++)
                            {
                                oSQ.Lines.SetCurrentLine(i);

                                if (item.ItemCode == oSQ.Lines.ItemCode)
                                {
                                    oSQ.Lines.Quantity = item.Quantity;

                                    item.Updated = 1;

                                    i = oSQ.Lines.Count;
                                }
                            }

                            if (item.Updated == 0)
                            {
                                if (oSQ.Lines.Count > 0)
                                {
                                    oSQ.Lines.SetCurrentLine(oSQ.Lines.Count - 1);
                                }

                                oSQ.Lines.ItemCode = item.ItemCode;
                                oSQ.Lines.ItemDescription = item.Dscription;
                                oSQ.Lines.SerialNum = lineItem.FrgnName.Substring(0, (lineItem.FrgnName.Length >= 17 ? 17 : lineItem.FrgnName.Length));
                                oSQ.Lines.WarehouseCode = item.WhsCode;
                                oSQ.Lines.Price = item.Price;
                                oSQ.Lines.Quantity = item.Quantity;
                                oSQ.Lines.Currency = lineItem.Currency;
                                oSQ.Lines.Address = item.WhsAddress;

                                oSQ.Lines.UserFields.Fields.Item("U_InsChargVal").Value = lineItem.U_InCharges.ToString();

                                oSQ.Lines.GrossBuyPrice = lineItem.AvgCost;

                                oSQ.Lines.Add();
                            }
                        }
                        else
                        {
                            item.Updated = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogConsumerDAL.Instance.Write($"Exception for [{item.ItemCode}] - " + ex.Message);
                    }
                }

                for (int i = 0; i < oSQ.Lines.Count; i++)
                {
                    oSQ.Lines.SetCurrentLine(i);

                    grossTotal += oSQ.Lines.GrossTotal;
                }

                oSQ.DocTotal = grossTotal;

                int result = oSQ.Update();

                if (result != 0)
                {
                    string lastErrorDescription = this.oCompany.GetLastErrorDescription();

                    LogConsumerDAL.Instance.Write($"Error Saving SQ [{DocEntry}]: {lastErrorDescription}");
                }
                else
                {
                    grossTotal = 0;
                    oSQ.GetByKey(Convert.ToInt32(DocEntry));

                    for (int i = 0; i < oSQ.Lines.Count; i++)
                    {
                        oSQ.Lines.SetCurrentLine(i);

                        grossTotal += oSQ.Lines.GrossTotal;
                    }

                    oSQ.DocTotal = grossTotal;

                    result = oSQ.Update();

                    if (result != 0)
                    {
                        string lastErrorDescription = this.oCompany.GetLastErrorDescription();

                        LogConsumerDAL.Instance.Write($"Error Saving SQ [{DocEntry}]: {lastErrorDescription}");
                    }

                    foreach (SaleQuotationLineItem item in LineItems)
                    {
                        if (item.Updated == 0)
                        {
                            try
                            {
                                Recordset recordset1 = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                                Item lineItem = itemCodes[item.ItemCode][item.WhsCode];

                                recordset1.DoQuery($"UPDATE QUT1 SET \"StockPrice\"={lineItem.AvgCost}, \"StockValue\"=\"Quantity\" * {lineItem.AvgCost} WHERE \"DocEntry\"={DocEntry} AND \"ItemCode\"='{item.ItemCode}'");
                            }
                            catch (Exception) { }
                        }
                    }

                    l_Query = $"UPDATE OQUT SET Posted_SAP=1 WHERE DocEntry = ";

                    PublicFunctions.FieldToParam(DocEntry, ref l_Param, Declarations.FieldTypes.String);
                    l_Query += l_Param;

                    l_ISCCommon.UseConnection(iscConnectionString);
                    l_ISCCommon.Execut(l_Query);
                }
            }
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write(ex.Message + ex.InnerException?.ToString());
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
            DataTable l_SourceData = new DataTable();
            DateTime l_DocDate = DateTime.Now;
            SAPbobsCOM.Documents oSalesQuotation;
            SAPbobsCOM.Document_Lines oSalesQuotationLine;

            string l_Query = string.Empty;
            string l_SourceDesc = string.Empty;
            string l_SAPSource = string.Empty;
            string l_Param = string.Empty;
            string l_CardCode = string.Empty;
            string l_ItemID = "FG010000020";
            int l_OOPRID = 0;
            int l_Source = 0;
            int l_SAPSeries = 0;
            int lRetCode;

            try
            {
                m_Connection = new DBConnector(HelperDAL.ISCConnectionString);
                this.oCompany = new SAPDAL().ConnectSAP();

                l_Query = "SELECT CardCode,OPPRID,Source FROM OOPR WITH (NOLOCK) WHERE ISNULL(UPSAP,0) = 0";

                if (this.m_Connection.GetData(l_Query, ref l_Data))
                {
                    foreach (DataRow l_Row in l_Data.Rows)
                    {
                        l_CardCode = PublicFunctions.ConvertNullAsString(l_Row["CardCode"], string.Empty);
                        l_OOPRID = PublicFunctions.ConvertNullAsInteger(l_Row["OPPRID"], 0);
                        l_Source = PublicFunctions.ConvertNullAsInteger(l_Row["Source"], 0);

                        Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                        recordset.DoQuery($"SELECT \"Descript\" FROM OOSR WHERE \"Num\" = '{l_Source}'");

                        while (!recordset.EoF)
                        {
                            l_SourceDesc = recordset.Fields.Item("Descript").Value;

                            l_Query = "SELECT SAPSource FROM OpportunitySource WITH (NOLOCK) WHERE OppoSource = ";

                            PublicFunctions.FieldToParam(l_SourceDesc, ref l_Param, Declarations.FieldTypes.String);
                            l_Query += l_Param;

                            if (this.m_Connection.GetData(l_Query, ref l_SourceData))
                            {
                                l_SAPSource = PublicFunctions.ConvertNullAsString(l_SourceData.Rows[0]["SAPSource"].ToString(), string.Empty);

                                Recordset recordset2 = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                                recordset2.DoQuery($"SELECT \"Series\" FROM NNM1 WHERE \"SeriesName\" = '{l_SAPSource}' AND \"ObjectCode\" = '23'");

                                while (!recordset2.EoF)
                                {
                                    l_SAPSeries = recordset2.Fields.Item("Series").Value;

                                    recordset2.MoveNext();
                                }

                                if (l_SAPSeries == 0)
                                {
                                    l_SAPSeries = 14;
                                }
                            }
                            else
                            {
                                l_SAPSeries = 14;
                            }

                            recordset.MoveNext();
                        }

                        oSalesQuotation = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oQuotations);

                        oSalesQuotation.DocType = SAPbobsCOM.BoDocumentTypes.dDocument_Items;
                        oSalesQuotation.CardCode = l_CardCode;
                        oSalesQuotation.DocDate = DateTime.Now.Date;
                        oSalesQuotation.TaxDate = DateTime.Now.Date;
                        oSalesQuotation.DocDueDate = DateTime.Now.Date;
                        oSalesQuotation.Series = l_SAPSeries;
                        oSalesQuotation.BPL_IDAssignedToInvoice = 1;

                        oSalesQuotationLine = oSalesQuotation.Lines;

                        oSalesQuotationLine.ItemCode = l_ItemID;
                        oSalesQuotationLine.Quantity = 1;
                        oSalesQuotationLine.VatGroup = "STX01";
                        oSalesQuotationLine.WarehouseCode = "M-F-UNT3";
                        oSalesQuotationLine.UoMEntry = -1;

                        lRetCode = oSalesQuotation.Add();

                        if (lRetCode != 0)
                        {
                            string sErrDesc = this.oCompany.GetLastErrorDescription();

                            LogConsumerDAL.Instance.Write($"Unable to create SQ for Opportunity {l_OOPRID}" + sErrDesc);
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
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write(ex.Message);

                throw ex;
            }
            finally
            {
                l_Data.Dispose();
            }

            return true;
        }
    }
}

