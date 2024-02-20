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
    using IMW.DB;
    using System.Data;
    using System.Text.Json.Nodes;
    using System.Text.Json;
    using System.Xml.Linq;
    using static System.Runtime.CompilerServices.RuntimeHelpers;
    using System.Text;
    using System.Security.Policy;
    using SAPB1;

    public class BOMDAL
    {
        public static AppSettings appSettings = HelperDAL.GetSettings();
        //private Company oCompany = ((Company)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));
        SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
        private static Dictionary<string, string> BoMLinks = new Dictionary<string, string>();

        public List<KeyValuePair<string, int>> GetNewOrders(Int64 p_BoMLastAutoID)
        {
            Common l_Common = new Common();
            DataTable l_Data = new DataTable();
            List<KeyValuePair<string, int>> l_Orders = new List<KeyValuePair<string, int>>();

            try
            {
                p_BoMLastAutoID = 109035815;

                l_Common.UseConnection(HelperDAL.IMOSConnectionString);
                l_Common.GetList($"SELECT OrderID, MAX(AutoID) OrderAutoID FROM IDBEXT WITH (NOLOCK) WHERE OrderID = '{p_BoMLastAutoID}' GROUP BY OrderID", ref l_Data);
                //l_Common.GetList($"SELECT OrderID, MAX(AutoID) OrderAutoID FROM IDBEXT WITH (NOLOCK) WHERE AutoID > {p_BoMLastAutoID} GROUP BY OrderID", ref l_Data);

                foreach (DataRow l_Row in l_Data.Rows)
                {
                    l_Orders.Add(new KeyValuePair<string, int>(PublicFunctions.ConvertNullAsString(l_Row["OrderID"], string.Empty), PublicFunctions.ConvertNullAsInteger(l_Row["OrderAutoID"], 0)));
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

            return l_Orders;
        }

        private bool GetBoMItemList(string p_OrderID, ref DataTable p_Data)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            DBConnector l_Connection = new DBConnector();

            l_Connection.ConnectionString = HelperDAL.IMOSConnectionString;

            l_Query = "EXEC sp_GetSAPBoMItems ";

            PublicFunctions.FieldToParam(p_OrderID, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;

            return l_Connection.GetData(l_Query, ref p_Data);
        }

        public bool ClearIMOSMappedTablesforBoM(string p_OrderID)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            DBConnector l_Connection = new DBConnector();

            l_Connection.ConnectionString = HelperDAL.ISCConnectionString;

            l_Query = "EXEC sp_ClearIMOSMappedTablesforBoM ";

            PublicFunctions.FieldToParam(p_OrderID, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;

            return l_Connection.Execute(l_Query);
        }

        public bool AddTablesDatafromIMOStoISC(string p_TableName, DataTable p_Data)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            DBConnector l_Connection = new DBConnector();

            l_Connection.ConnectionString = HelperDAL.ISCConnectionString;

            return l_Connection.BulkInsert(p_TableName, p_Data);
        }

        public bool GetBoMforSAPItems(string p_OrderID, ref DataTable p_Data)
        {
            string l_Query = string.Empty;
            string l_Param = string.Empty;
            DBConnector l_Connection = new DBConnector();

            l_Connection.ConnectionString = HelperDAL.ISCConnectionString;

            l_Query = "EXEC sp_GetSAPBoMItemsISC ";

            PublicFunctions.FieldToParam(p_OrderID, ref l_Param, Declarations.FieldTypes.String);
            l_Query += l_Param;

            return l_Connection.GetData(l_Query, ref p_Data);
        }

        public bool SyncBoM()
        {
            Int64 bomLastAutoID = 0;
            List<KeyValuePair<string, int>> l_Orders;

            Int64.TryParse(appSettings.BoMLastAutoID, out bomLastAutoID);

            this.oCompany = new SAPDAL().ConnectSAP();

            try
            {
                BoMLinks.Clear();
                l_Orders = GetNewOrders(bomLastAutoID);
                //SAPbobsCOM.Recordset sapConn = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

                LogConsumerDAL.Instance.Write($"Found {l_Orders.Count} orders to sync BoM.");

                foreach (KeyValuePair<string, int> l_Order in l_Orders)
                {
                    DataTable l_BoMItems = new DataTable();
                    DataTable l_BoMItemsTypeOne = new DataTable();
                    DataTable l_BoMData = new DataTable();

                    LogConsumerDAL.Instance.Write($"Started processing {l_Order.Key} order for BoM sync.");

                    try
                    {
                        if (GetBoMItemList(l_Order.Key, ref l_BoMItems))
                        {
                            this.ClearIMOSMappedTablesforBoM(l_Order.Key);

                            if (this.AddTablesDatafromIMOStoISC("BoMItemsList", l_BoMItems))
                            {
                                if (this.GetBoMforSAPItems(l_Order.Key, ref l_BoMData))
                                {
                                    if (l_BoMData.Select("ItemCode IS NULL OR ItemCode = ''").Length > 0)
                                    {
                                        foreach (DataRow l_Row in l_BoMData.Select("ItemCode IS NULL OR ItemCode = ''"))
                                        {
                                            LogConsumerDAL.Instance.Write($"Errored processing {l_Order.Key} order, the Article ID [{PublicFunctions.ConvertNullAsString(l_Row["ID"], string.Empty)}] is not mapped.");
                                        }
                                    }
                                    else if (l_BoMData.Select("Type <> 1 AND (ParentID IS NULL OR ParentID = '' OR ParentID = '0')").Length > 0)
                                    {
                                        foreach (DataRow l_Row in l_BoMData.Select("Type <> 1 AND (ParentID IS NULL OR ParentID = '' OR ParentID = '0')"))
                                        {
                                            LogConsumerDAL.Instance.Write($"Errored processing {l_Order.Key} order, the Article ID [{PublicFunctions.ConvertNullAsString(l_Row["ID"], string.Empty)}] has no parent.");
                                        }
                                    }

                                    l_BoMData.DefaultView.RowFilter = "Type = 1 AND ItemCode IS NOT NULL AND ItemCode <> ''";
                                    //l_BoMData.DefaultView.RowFilter = "Type = 1 AND ItemCode in ('FG040001290','FG040000867')";
                                    l_BoMItemsTypeOne = l_BoMData.DefaultView.ToTable();
                                    List<BoM> l_BoMs = new List<BoM>();

                                    foreach (DataRow l_RowOne in l_BoMItemsTypeOne.Rows)
                                    {
                                        BoM l_BoM = new BoM();

                                        l_BoM.TreeCode = PublicFunctions.ConvertNullAsString(l_RowOne["ItemCode"], string.Empty);
                                        l_BoM.TreeType = SAPB1.BoItemTreeTypes.IProductionTree;
                                        l_BoM.Quantity = PublicFunctions.ConvertNullAsDouble(l_RowOne["Qty"], 0);
                                        l_BoM.Warehouse = PublicFunctions.ConvertNullAsString(l_RowOne["DfltWH"], string.Empty);

                                        this.AddBoMChilds(l_BoMs, l_BoM, l_BoMData, l_RowOne, PublicFunctions.ConvertNullAsString(l_RowOne["ItemCode"], string.Empty), PublicFunctions.ConvertNullAsString(l_RowOne["ItemCode"], string.Empty), PublicFunctions.ConvertNullAsInteger(l_RowOne["Type"], 0));

                                        l_BoMs.Add(l_BoM);
                                    }

                                    AppSettings appSettings = HelperDAL.GetSettings();
                                    string serviceRoot = "https://sapapp10.iwm.com.pk:50000/b1s/v2/";
                                    ServiceLayer context = new ServiceLayer(new Uri(serviceRoot));
                                    List<string> l_BoMlist = new List<string>();

                                    B1Session session = context.Login(appSettings.CompanyDB, appSettings.UserName, appSettings.Password, "3").GetValue();
                                    Authentication.Login(context, appSettings.CompanyDB, appSettings.UserName, appSettings.Password, "3");

                                    foreach (BoM l_iBoM in l_BoMs)
                                    {
                                        if (l_BoMlist.Contains(l_iBoM.TreeCode))
                                        {
                                            continue;
                                        }
                                        if (l_iBoM.Items.Count > 0)
                                        {
                                            bool createBoM = true;
                                            SAPbobsCOM.ProductTrees oBoM = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oProductTrees) as SAPbobsCOM.ProductTrees;

                                            if (oBoM.GetByKey(l_iBoM.TreeCode))
                                            {
                                                oBoM.Remove();

                                                if (this.oCompany.GetLastErrorCode() != 0)
                                                {
                                                    createBoM = false;
                                                    LogConsumerDAL.Instance.Write($"Errored processing {l_Order.Key} order: {this.oCompany.GetLastErrorDescription()}");
                                                    break;
                                                }
                                            }

                                            if (createBoM)
                                            {
                                                SAPB1.ProductTree l_BoM = SAPB1.ProductTree.CreateProductTree(l_iBoM.TreeCode);

                                                l_BoM.TreeType = SAPB1.BoItemTreeTypes.IProductionTree;
                                                l_BoM.Quantity = l_iBoM.Quantity;
                                                l_BoM.Warehouse = l_iBoM.Warehouse;
                                                l_BoM.PlanAvgProdSize = 1;


                                                foreach (BoMItem l_Item in l_iBoM.Items)
                                                {
                                                    SAPB1.ProductTreeLine l_Line = new SAPB1.ProductTreeLine();

                                                    l_Line.ItemType = l_Item.ItemType;
                                                    l_Line.ItemCode = l_Item.ItemCode;
                                                    l_Line.ParentItem = l_iBoM.TreeCode;
                                                    l_Line.Warehouse = l_Item.Warehouse;
                                                    l_Line.Quantity = l_Item.Quantity;
                                                    l_Line.IssueMethod = SAPB1.BoIssueMethod.Im_Manual;
                                                    l_Line.U_Department = "BM";

                                                    l_BoM.ProductTreeLines.Add(l_Line);
                                                }
                                                context.AddToProductTrees(l_BoM);
                                                l_BoMlist.Add(l_iBoM.TreeCode);                                                


                                            }
                                        }
                                    }

                                    context.SaveChanges();
                                    Authentication.Logout(context);
                                }
                            }
                        }

                        LogConsumerDAL.Instance.Write($"Completed processing {l_Order.Key} order for BoM sync.");
                    }
                    catch (Exception ex)
                    {
                        LogConsumerDAL.Instance.Write($"Exception processing {l_Order.Key} order: {ex.Message}");
                    }
                    finally
                    {
                        HelperDAL.SaveTagValue("BoMLastAutoID", l_Order.Value.ToString());

                        l_BoMItems.Dispose();
                        l_BoMItemsTypeOne.Dispose();
                        l_BoMData.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write($"Exception processing BoM Sync: {ex.Message}");

                throw;
            }

            return true;
        }

        private void AddBoMChilds(List<BoM> p_BoMs, BoM p_BoMObj, DataTable p_BoMData, DataRow p_ParentRow, string p_RootItem, string p_ParentItem, Int32 p_type)
        {
            DataTable l_Data = null;
            int iRow = 1;

            try
            {
                string l_ParentWarehouse = PublicFunctions.ConvertNullAsString(p_ParentRow["DfltWH"], string.Empty);

                p_BoMData.DefaultView.RowFilter = $"ItemCode IS NOT NULL AND ItemCode <> '' AND ParentID = '{PublicFunctions.ConvertNullAsString(p_ParentRow["ID"], string.Empty)}' AND Type {(p_type == 3 ? ">=" : "=")} {p_type + 1}";
                l_Data = p_BoMData.DefaultView.ToTable();

                foreach (DataRow l_ChildRow in l_Data.Rows)
                {
                    BoMItem l_BoMItem = new BoMItem();
                    string l_ChildWarehouse = PublicFunctions.ConvertNullAsString(l_ChildRow["DfltWH"], string.Empty);
                    string l_Warehouse = string.IsNullOrEmpty(l_ChildWarehouse) ? l_ParentWarehouse : l_ChildWarehouse;

                    if (p_ParentItem != PublicFunctions.ConvertNullAsString(l_ChildRow["ItemCode"], string.Empty))
                    {
                        if (p_type == 1 || p_type == 2)
                        {
                            l_BoMItem = VerifyItemInSAP(p_RootItem, PublicFunctions.ConvertNullAsString(l_ChildRow["ItemCode"], string.Empty),
                                l_Warehouse,
                                PublicFunctions.ConvertNullAsInteger(l_ChildRow["Type"], 0), p_ParentItem);

                            if (l_BoMItem == null)
                            {
                                return;
                            }
                        }

                        l_BoMItem.ParentItem = p_ParentItem;
                        l_BoMItem.ItemCode = string.IsNullOrEmpty(l_BoMItem.ItemCode) ? PublicFunctions.ConvertNullAsString(l_ChildRow["ItemCode"], string.Empty) : l_BoMItem.ItemCode;
                        l_BoMItem.ItemType = PublicFunctions.ConvertNullAsInteger(l_ChildRow["Type"], 0) == 10 ? SAPB1.ProductionItemType.Pit_Resource : SAPB1.ProductionItemType.Pit_Item;
                        l_BoMItem.Warehouse = l_Warehouse;
                        l_BoMItem.Quantity = PublicFunctions.ConvertNullAsDouble(l_ChildRow["Qty"], 0);
                        l_BoMItem.PriceList = PublicFunctions.ConvertNullAsInteger(l_ChildRow["PriceList"], 0);
                        l_BoMItem.LineNo = iRow;

                        p_BoMObj.Items.Add(l_BoMItem);

                        iRow++;
                    }

                    if (p_type < 3)
                    {
                        BoM l_BoM = new BoM();

                        if (string.IsNullOrEmpty(l_BoMItem.ItemCode))
                        {
                            l_BoMItem = VerifyItemInSAP(p_RootItem, PublicFunctions.ConvertNullAsString(l_ChildRow["ItemCode"], string.Empty),
                                l_Warehouse,
                                PublicFunctions.ConvertNullAsInteger(l_ChildRow["Type"], 0), p_ParentItem);

                            if (l_BoMItem == null)
                            {
                                return;
                            }
                        }

                        l_BoM.TreeCode = l_BoMItem.ItemCode;
                        l_BoM.TreeType = SAPB1.BoItemTreeTypes.IProductionTree;
                        l_BoM.Quantity = PublicFunctions.ConvertNullAsDouble(l_ChildRow["Qty"], 0);
                        l_BoM.Warehouse = l_Warehouse;

                        this.AddBoMChilds(p_BoMs, l_BoM, p_BoMData, l_ChildRow, p_RootItem, PublicFunctions.ConvertNullAsString(l_ChildRow["ItemCode"], string.Empty), p_type + 1);

                        p_BoMs.Add(l_BoM);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (l_Data != null)
                {
                    l_Data.Dispose();
                }
            }
        }

        private BoMItem VerifyItemInSAP(string p_RootItem, string p_ItemCode, string p_Warehouse, int p_Type, string p_ParentItem)
        {
            SAPbobsCOM.Items vItem = null;
            ItemsDAL l_Items = new ItemsDAL();
            BoMItem l_BoMItem = new BoMItem();
            int result = 0;
            string l_ItemCode = string.Empty;
            DataTable l_ItemDt = new DataTable();
            Recordset recordset = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            Recordset recordset2 = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            Recordset recordset3 = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            recordset.DoQuery("SELECT \"ItemCode\",\"ItemName\",\"FrgnName\",\"BLength1\",\"BWidth1\",\"ExitPrice\",\"AvgPrice\",\"PriceUnit\",\"CreateDate\"," +
                  "\"BuyUnitMsr\",\"SalUnitMsr\",\"UgpEntry\",\"DfltWH\",\"VatGourpSa\",\"BHeight1\",\"U_Grp1Name\",\"U_Grp2Name\",\"U_Grp3Name\",\"U_Grp4Name\" " +
                  "FROM OITM WHERE \"ItemName\" = '" + p_ItemCode + "'");
            bool foundItem = false;
            while (!recordset.EoF)
            {
                try
                {
                    l_ItemCode = recordset.Fields.Item("ItemCode").Value;

                    if (BoMLinks.ContainsKey(l_ItemCode) && BoMLinks[l_ItemCode] == p_RootItem)
                    {
                        foundItem = true;
                        recordset.MoveLast();
                    }
                    else
                    {
                        recordset2.DoQuery("SELECT \"Father\",\"ChildNum\",\"VisOrder\",\"Code\",\"Quantity\",\"Warehouse\",\"Price\",\"Currency\",\"PriceList\"," +
                                "\"OrigPrice\",\"OrigCurr\",\"IssueMthd\",\"Uom\",\"Comment\",\"LogInstanc\",\"Object\",\"PrncpInput\",\"Project\",\"Type\",\"WipActCode\"," +
                                "\"AddQuantit\",\"LineText\",\"StageId\" " +
                                "FROM ITT1 WHERE \"Code\" = '" + l_ItemCode + "' AND \"Father\" = '" + p_RootItem + "'");

                        if (recordset2.RecordCount > 0)
                        {
                            foundItem = true;
                            recordset.MoveLast();
                        }
                        else if(!BoMLinks.ContainsKey(l_ItemCode))
                        {
                            recordset3.DoQuery("SELECT \"Father\",\"ChildNum\",\"VisOrder\",\"Code\",\"Quantity\",\"Warehouse\",\"Price\",\"Currency\",\"PriceList\"," +
                            "\"OrigPrice\",\"OrigCurr\",\"IssueMthd\",\"Uom\",\"Comment\",\"LogInstanc\",\"Object\",\"PrncpInput\",\"Project\",\"Type\",\"WipActCode\"," +
                            "\"AddQuantit\",\"LineText\",\"StageId\" " +
                            "FROM ITT1 WHERE \"Code\" = '" + l_ItemCode + "'");

                            if (recordset3.RecordCount == 0)
                            {
                                foundItem = true;
                                recordset.MoveLast();
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    LogConsumerDAL.Instance.Write($"Exception: {recordset.Fields.Item("ItemCode").Value} - {ex.Message}");
                }
                recordset.MoveNext();
            }

            if (!foundItem)
                l_ItemCode = string.Empty;

            vItem = this.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oItems);
            //l_ItemCode = l_Items.GetItemBy(this.oCompany, "ItemName", p_ItemCode);

            if (string.IsNullOrEmpty(l_ItemCode))
            {
                if (p_Type == 2)
                {
                    vItem.Series = p_RootItem.StartsWith("FG01") ? 1882 /*KT*/ : p_RootItem.StartsWith("FG04") ? 1884 /*WR*/ : p_RootItem.StartsWith("FG03") ? 1883 : 0;
                }
                else if (p_Type == 3)
                {
                    vItem.Series = p_RootItem.StartsWith("FG01") ? 1885 /*KT*/ : p_RootItem.StartsWith("FG04") ? 1887 /*WR*/ : p_RootItem.StartsWith("FG03") ? 1886 : 0;
                }

                vItem.ItemName = p_ItemCode;
                vItem.ForeignName = p_ItemCode;
                vItem.WhsInfo.WarehouseCode = p_Warehouse;
                vItem.ItemType = SAPbobsCOM.ItemTypeEnum.itItems;
                vItem.PlanningSystem = SAPbobsCOM.BoPlanningSystem.bop_MRP;
                vItem.ProcurementMethod = SAPbobsCOM.BoProcurementMethod.bom_Make;
                vItem.CostAccountingMethod = SAPbobsCOM.BoInventorySystem.bis_Standard;

                result = vItem.Add();
                if (result != 0)
                {
                    return null;
                }

                l_ItemCode = l_Items.GetItemBy(this.oCompany, "ItemName", p_ItemCode);
            }

            l_BoMItem.ItemCode = l_ItemCode;
            l_BoMItem.Warehouse = p_Warehouse;

            if (!BoMLinks.ContainsKey(l_ItemCode))
                BoMLinks[l_ItemCode] = p_RootItem;

            return l_BoMItem;
        }

        //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(Object obj, 
        //        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
        //        System.Security.Cryptography.X509Certificates.X509Chain chain,
        //        System.Net.Security.SslPolicyErrors sslPolicyError) 
        //    {
        //        return true;
        //    };
    }

    public class BoM
    {
        public string TreeCode { get; set; }
        public SAPB1.BoItemTreeTypes TreeType { get; set; }
        public string Warehouse { get; set; }
        public double Quantity { get; set; }

        public List<BoMItem> Items { get; set; } = new List<BoMItem>();
    }

    public class BoMItem
    {
        public int LineNo { get; set; }
        public string ParentItem { get; set; }
        public string ItemCode { get; set; }
        public SAPB1.ProductionItemType ItemType { get; set; }
        public string Warehouse { get; set; }
        public double Quantity { get; set; }
        public int PriceList { get; set; }
    }
}

