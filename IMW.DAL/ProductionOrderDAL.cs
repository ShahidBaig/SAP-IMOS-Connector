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

    public class ProductionOrderDAL
    {
        private Company oCompany = new SAPbobsCOM.Company();

        public int GetLastProductionOrderFSAP()
        {
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<SaleQuotation> list = new List<SaleQuotation>();
            SqlCommand command = new SqlCommand {
                Connection = connection,
                CommandText = "Select isnull(max(DocEntry),0) Last from OWOR"
            };
            int num = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            command.Dispose();
            return num;
        }

        public List<ProductionOrder> GetProductionOrder()
        {
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<ProductionOrder> list = new List<ProductionOrder>();
            SqlDataReader reader = new SqlCommand { 
                Connection = connection,
                CommandText = "SELECT DocEntry, DocNum, Series, ItemCode, Status, Type, PlannedQty, CmpltQty, RjctQty, PostDate, DueDate, OriginAbs, OriginNum, OriginType, UserSign, Comments, CloseDate, RlsDate, CardCode, Warehouse, Uom, LineDirty, JrnlMemo, TransId, CreateDate, Printed, OcrCode, PIndicator, OcrCode2, OcrCode3, OcrCode4, OcrCode5, SeqCode, Serial, SeriesStr, SubStr, LogInstanc, UserSign2, UpdateDate, Project, SupplCode, UomEntry, PickRmrk, SysCloseDt, SysCloseTm, CloseVerNm, StartDate, ObjType, ProdName, Priority, RouDatCalc, UpdAlloc, CreateTS, UpdateTS, VersionNum, DataSource, SAPPassprt FROM OWOR"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                ProductionOrder item = new ProductionOrder {
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
                    CardName = reader["CardName"].ToString()
                };
                list.Add(item);
            }
        }

        public ProductionOrder GetProductionOrderLines(ProductionOrder po)
        {
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlDataReader reader = new SqlCommand { 
                Connection = connection,
                CommandText = $"SELECT DocEntry, LineNum, ItemCode, BaseQty, PlannedQty, IssuedQty, IssueType, wareHouse, VisOrder, WipActCode, CompTotal, OcrCode, OcrCode2, OcrCode3, OcrCode4, OcrCode5, LocCode, LogInstanc, Project, UomEntry, UomCode, ItemType, AdditQty, LineText, PickStatus, PickQty, PickIdNo, ReleaseQty, ResAlloc, StartDate, EndDate, StageId, BaseQtyNum, BaseQtyDen, ReqDays, RtCalcProp, Status FROM WOR1 where DocEntry='{po.DocEntry}'"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return po;
                }
                ProductionOrderLineItem item = new ProductionOrderLineItem {
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
                po.Lines.Add(item);
            }
        }

        public bool LoadProductionOrder()
        {
            int lastProductionOrderFSAP = this.GetLastProductionOrderFSAP();
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            //connection.Open();
            //SqlTransaction transaction = connection.BeginTransaction();
            //this.oCompany = new SAPDAL().ConnectSAP();
            //<>o__1.<>p__0 ??= CallSite<Func<CallSite, object, Recordset>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Recordset), typeof(ProductionOrderDAL)));
            //Recordset recordset = <>o__1.<>p__0.Target(<>o__1.<>p__0, this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset));
            //recordset.DoQuery($"SELECT * FROM OWOR Where DocEntry > {lastProductionOrderFSAP}");
            //while (true)
            //{
            //    if (recordset.EoF)
            //    {
            //        <>o__1.<>p__5 ??= CallSite<Func<CallSite, object, Recordset>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Recordset), typeof(ProductionOrderDAL)));
            //        Recordset recordset2 = <>o__1.<>p__5.Target(<>o__1.<>p__5, this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset));
            //        recordset2.DoQuery($"SELECT * from WOR1 Where DocEntry > {lastProductionOrderFSAP}");
            //        while (true)
            //        {
            //            if (recordset2.EoF)
            //            {
            //                transaction.Commit();
            //                connection.Close();
            //                return true;
            //            }
            //            SqlCommand command2 = new SqlCommand {
            //                Connection = connection,
            //                Transaction = transaction
            //            };
            //            if (<>o__1.<>p__7 == null)
            //            {
            //                <>o__1.<>p__7 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(ProductionOrderDAL)));
            //            }
            //            if (<>o__1.<>p__6 == null)
            //            {
            //                CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[0x27];
            //                argumentInfo[0] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null);
            //                argumentInfo[1] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null);
            //                argumentInfo[2] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[3] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[4] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[5] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[6] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[7] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[8] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[9] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[10] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[11] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[12] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[13] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[14] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[15] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x10] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x11] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x12] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x13] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[20] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x15] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x16] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x17] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x18] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x19] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x1a] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x1b] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x1c] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x1d] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[30] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x1f] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x20] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x21] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x22] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x23] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x24] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x25] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                argumentInfo[0x26] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //                <>o__1.<>p__6 = CallSite<<>F<CallSite, Type, string, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(ProductionOrderDAL), argumentInfo));
            //            }
            //            command2.CommandText = <>o__1.<>p__7.Target(<>o__1.<>p__7, <>o__1.<>p__6.Target(<>o__1.<>p__6, typeof(string), "Insert into WOR1(DocEntry, LineNum, ItemCode, BaseQty, PlannedQty, IssuedQty, IssueType, wareHouse, VisOrder, WipActCode, CompTotal, OcrCode, OcrCode2, OcrCode3, OcrCode4, OcrCode5, LocCode, LogInstanc, Project, UomEntry, UomCode, ItemType, AdditQty, LineText, PickStatus, PickQty, PickIdNo, ReleaseQty, ResAlloc, StartDate, EndDate, StageId, BaseQtyNum, BaseQtyDen, ReqDays, RtCalcProp, Status) values ({0},{1},'{2}',{3},{4},{5},'{6}','{7}',{8},'{9}',{10},'{11}','{12}','{13}','{14}','{15}',{16},{17},'{18}',{19},'{20}',{21},'{22}','{23}','{24}',{25},{26},{27},'{28}','{29}','{30}',{31},{32},{33},{34},{35},'{36}')", recordset2.Fields.Item(0).Value, recordset2.Fields.Item(1).Value, recordset2.Fields.Item(2).Value, recordset2.Fields.Item(3).Value, recordset2.Fields.Item(4).Value, recordset2.Fields.Item(5).Value, recordset2.Fields.Item(6).Value, recordset2.Fields.Item(7).Value, recordset2.Fields.Item(8).Value, recordset2.Fields.Item((int) 9).Value, recordset2.Fields.Item((int) 10).Value, recordset2.Fields.Item((int) 11).Value, recordset2.Fields.Item((int) 12).Value, recordset2.Fields.Item((int) 13).Value, recordset2.Fields.Item((int) 14).Value, recordset2.Fields.Item((int) 15).Value, recordset2.Fields.Item((int) 0x10).Value, recordset2.Fields.Item((int) 0x11).Value, recordset2.Fields.Item((int) 0x12).Value, recordset2.Fields.Item((int) 0x13).Value, recordset2.Fields.Item((int) 20).Value, recordset2.Fields.Item((int) 0x15).Value, recordset2.Fields.Item((int) 0x16).Value, recordset2.Fields.Item((int) 0x17).Value, recordset2.Fields.Item((int) 0x18).Value, recordset2.Fields.Item((int) 0x19).Value, recordset2.Fields.Item((int) 0x1a).Value, recordset2.Fields.Item((int) 0x1b).Value, recordset2.Fields.Item((int) 0x1c).Value, recordset2.Fields.Item((int) 0x1d).Value, recordset2.Fields.Item((int) 30).Value, recordset2.Fields.Item((int) 0x1f).Value, recordset2.Fields.Item((int) 0x20).Value, recordset2.Fields.Item((int) 0x21).Value, recordset2.Fields.Item((int) 0x22).Value, recordset2.Fields.Item((int) 0x23).Value, recordset2.Fields.Item((int) 0x24).Value));
            //            command2.ExecuteNonQuery();
            //            recordset2.MoveNext();
            //        }
            //    }
            //    SqlCommand command = new SqlCommand {
            //        Connection = connection,
            //        Transaction = transaction
            //    };
            //    if (<>o__1.<>p__4 == null)
            //    {
            //        <>o__1.<>p__4 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(ProductionOrderDAL)));
            //    }
            //    if (<>o__1.<>p__3 == null)
            //    {
            //        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[0x3b];
            //        argumentInfo[0] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null);
            //        argumentInfo[1] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null);
            //        argumentInfo[2] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[3] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[4] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[5] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[6] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[7] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[8] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[9] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[10] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[11] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[12] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[13] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[14] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[15] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x10] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x11] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x12] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x13] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[20] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x15] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x16] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x17] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x18] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x19] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x1a] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x1b] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x1c] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x1d] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[30] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x1f] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x20] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x21] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x22] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x23] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x24] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x25] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x26] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x27] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[40] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x29] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x2a] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x2b] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x2c] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x2d] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x2e] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x2f] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x30] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x31] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[50] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x33] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x34] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x35] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x36] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x37] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x38] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x39] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        argumentInfo[0x3a] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
            //        <>o__1.<>p__3 = CallSite<<>F<CallSite, Type, string, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(ProductionOrderDAL), argumentInfo));
            //    }
            //    if (<>o__1.<>p__2 == null)
            //    {
            //        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null), CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null) };
            //        <>o__1.<>p__2 = CallSite<Func<CallSite, object, string, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Replace", null, typeof(ProductionOrderDAL), argumentInfo));
            //    }
            //    if (<>o__1.<>p__1 == null)
            //    {
            //        CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
            //        <>o__1.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "ToString", null, typeof(ProductionOrderDAL), argumentInfo));
            //    }
            //    command.CommandText = <>o__1.<>p__4.Target(<>o__1.<>p__4, <>o__1.<>p__3.Target(<>o__1.<>p__3, typeof(string), "Insert into OWOR(DocEntry, DocNum, Series, ItemCode, Status, Type, PlannedQty, CmpltQty, RjctQty, PostDate, DueDate, OriginAbs, OriginNum, OriginType, UserSign, Comments, CloseDate, RlsDate, CardCode, Warehouse, Uom, LineDirty, JrnlMemo, TransId, CreateDate, Printed, OcrCode, PIndicator, OcrCode2, OcrCode3, OcrCode4, OcrCode5, SeqCode, Serial, SeriesStr, SubStr, LogInstanc, UserSign2, UpdateDate, Project, SupplCode, UomEntry, PickRmrk, SysCloseDt, SysCloseTm, CloseVerNm, StartDate, ObjType, ProdName, Priority, RouDatCalc, UpdAlloc, CreateTS, UpdateTS, VersionNum, DataSource, SAPPassprt) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}')", recordset.Fields.Item(0).Value, recordset.Fields.Item(1).Value, recordset.Fields.Item(2).Value, recordset.Fields.Item(3).Value, recordset.Fields.Item(4).Value, recordset.Fields.Item(5).Value, recordset.Fields.Item(6).Value, recordset.Fields.Item(7).Value, recordset.Fields.Item(8).Value, recordset.Fields.Item((int) 9).Value, recordset.Fields.Item((int) 10).Value, recordset.Fields.Item((int) 11).Value, recordset.Fields.Item((int) 12).Value, recordset.Fields.Item((int) 13).Value, recordset.Fields.Item((int) 14).Value, recordset.Fields.Item((int) 15).Value, recordset.Fields.Item((int) 0x10).Value, recordset.Fields.Item((int) 0x11).Value, recordset.Fields.Item((int) 0x12).Value, recordset.Fields.Item((int) 0x13).Value, recordset.Fields.Item((int) 20).Value, recordset.Fields.Item((int) 0x15).Value, recordset.Fields.Item((int) 0x16).Value, recordset.Fields.Item((int) 0x17).Value, recordset.Fields.Item((int) 0x18).Value, recordset.Fields.Item((int) 0x19).Value, recordset.Fields.Item((int) 0x1a).Value, recordset.Fields.Item((int) 0x1b).Value, recordset.Fields.Item((int) 0x1c).Value, recordset.Fields.Item((int) 0x1d).Value, recordset.Fields.Item((int) 30).Value, recordset.Fields.Item((int) 0x1f).Value, recordset.Fields.Item((int) 0x20).Value, recordset.Fields.Item((int) 0x21).Value, recordset.Fields.Item((int) 0x22).Value, recordset.Fields.Item((int) 0x23).Value, recordset.Fields.Item((int) 0x24).Value, recordset.Fields.Item((int) 0x25).Value, recordset.Fields.Item((int) 0x26).Value, recordset.Fields.Item((int) 0x27).Value, recordset.Fields.Item((int) 40).Value, recordset.Fields.Item((int) 0x29).Value, recordset.Fields.Item((int) 0x2a).Value, recordset.Fields.Item((int) 0x2b).Value, recordset.Fields.Item((int) 0x2c).Value, recordset.Fields.Item((int) 0x2d).Value, recordset.Fields.Item((int) 0x2e).Value, recordset.Fields.Item((int) 0x2f).Value, <>o__1.<>p__2.Target(<>o__1.<>p__2, <>o__1.<>p__1.Target(<>o__1.<>p__1, recordset.Fields.Item((int) 0x30).Value), "'", "''"), recordset.Fields.Item((int) 0x31).Value, recordset.Fields.Item((int) 50).Value, recordset.Fields.Item((int) 0x33).Value, recordset.Fields.Item((int) 0x34).Value, recordset.Fields.Item((int) 0x35).Value, recordset.Fields.Item((int) 0x36).Value, recordset.Fields.Item((int) 0x37).Value, recordset.Fields.Item((int) 0x38).Value));
            //    command.ExecuteNonQuery();
            //    recordset.MoveNext();
            //}
            return false;
        }
    }
}

