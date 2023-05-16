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

    public class BOMDAL
    {
        private Company oCompany = ((Company) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));

        public List<BOM> GetBillOfMaterial()
        {
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<BOM> list = new List<BOM>();
            SqlDataReader reader = new SqlCommand { 
                Connection = connection,
                CommandText = "select Father,ChildNum,VisOrder,Code,Quantity,Warehouse,Price,Currency,PriceList,OrigPrice,OrigCurr from ITT1"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                BOM item = new BOM {
                    Father = reader["ItemCode"].ToString(),
                    ChildNum = reader["ItemCode"].ToString(),
                    VisOrder = reader["ItemCode"].ToString(),
                    Code = reader["ItemCode"].ToString(),
                    Quantity = Convert.ToDouble(reader["ItemCode"].ToString()),
                    Warehouse = reader["ItemCode"].ToString(),
                    Price = Convert.ToDouble(reader["ItemCode"].ToString()),
                    Currency = reader["ItemCode"].ToString(),
                    PriceList = reader["ItemCode"].ToString(),
                    OrigPrice = Convert.ToDouble(reader["ItemCode"].ToString()),
                    OrigCurr = reader["ItemCode"].ToString()
                };
                list.Add(item);
            }
        }

        //public bool LoadBillOfMaterial()
        //{
        //    SqlConnection connection = new SqlConnection {
        //        ConnectionString = HelperDAL.ISCConnectionString
        //    };
        //    connection.Open();
        //    SqlTransaction transaction = connection.BeginTransaction();
        //    this.oCompany = new SAPDAL().ConnectSAP();
        //    <>o__1.<>p__0 ??= CallSite<Func<CallSite, object, Recordset>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Recordset), typeof(BOMDAL)));
        //    Recordset recordset = <>o__1.<>p__0.Target(<>o__1.<>p__0, this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset));
        //    recordset.DoQuery("SELECT Father,ChildNum,VisOrder,Code,Quantity,Warehouse,Price,Currency,PriceList,OrigPrice,OrigCurr FROM ITT1");
        //    while (true)
        //    {
        //        if (recordset.EoF)
        //        {
        //            <>o__1.<>p__3 ??= CallSite<Func<CallSite, object, Recordset>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Recordset), typeof(BOMDAL)));
        //            Recordset recordset2 = <>o__1.<>p__3.Target(<>o__1.<>p__3, this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset));
        //            recordset2.DoQuery(string.Format("SELECT * from OITT", new object[0]));
        //            while (true)
        //            {
        //                if (recordset2.EoF)
        //                {
        //                    transaction.Commit();
        //                    connection.Close();
        //                    return true;
        //                }
        //                SqlCommand command2 = new SqlCommand {
        //                    Connection = connection,
        //                    Transaction = transaction
        //                };
        //                if (<>o__1.<>p__5 == null)
        //                {
        //                    <>o__1.<>p__5 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(BOMDAL)));
        //                }
        //                if (<>o__1.<>p__4 == null)
        //                {
        //                    CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[0x1d];
        //                    argumentInfo[0] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null);
        //                    argumentInfo[1] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null);
        //                    argumentInfo[2] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[3] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[4] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[5] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[6] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[7] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[8] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[9] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[10] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[11] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[12] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[13] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[14] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[15] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x10] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x11] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x12] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x13] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[20] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x15] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x16] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x17] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x18] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x19] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x1a] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x1b] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    argumentInfo[0x1c] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //                    <>o__1.<>p__4 = CallSite<<>F<CallSite, Type, string, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(BOMDAL), argumentInfo));
        //                }
        //                command2.CommandText = <>o__1.<>p__5.Target(<>o__1.<>p__5, <>o__1.<>p__4.Target(<>o__1.<>p__4, typeof(string), "INSERT INTO OITT (Code, TreeType, PriceList, Qauntity, CreateDate, UpdateDate, Transfered, DataSource, UserSign, SCNCounter, DispCurr, ToWH, Object, LogInstac, UserSign2, OcrCode, HideComp, OcrCode2, OcrCode3, OcrCode4, OcrCode5, UpdateTime, Project, PlAvgSize, Name, CreateTS, UpdateTS) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}')", recordset2.Fields.Item(0).Value, recordset2.Fields.Item(1).Value, recordset2.Fields.Item(2).Value, recordset2.Fields.Item(3).Value, recordset2.Fields.Item(4).Value, recordset2.Fields.Item(5).Value, recordset2.Fields.Item(6).Value, recordset2.Fields.Item(7).Value, recordset2.Fields.Item(8).Value, recordset2.Fields.Item((int) 9).Value, recordset2.Fields.Item((int) 10).Value, recordset2.Fields.Item((int) 11).Value, recordset2.Fields.Item((int) 12).Value, recordset2.Fields.Item((int) 13).Value, recordset2.Fields.Item((int) 14).Value, recordset2.Fields.Item((int) 15).Value, recordset2.Fields.Item((int) 0x10).Value, recordset2.Fields.Item((int) 0x11).Value, recordset2.Fields.Item((int) 0x12).Value, recordset2.Fields.Item((int) 0x13).Value, recordset2.Fields.Item((int) 20).Value, recordset2.Fields.Item((int) 0x15).Value, recordset2.Fields.Item((int) 0x16).Value, recordset2.Fields.Item((int) 0x17).Value, recordset2.Fields.Item((int) 0x18).Value, recordset2.Fields.Item((int) 0x19).Value, recordset2.Fields.Item((int) 0x1a).Value));
        //                command2.ExecuteNonQuery();
        //                recordset2.MoveNext();
        //            }
        //        }
        //        SqlCommand command = new SqlCommand {
        //            Connection = connection,
        //            Transaction = transaction
        //        };
        //        if (<>o__1.<>p__2 == null)
        //        {
        //            <>o__1.<>p__2 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(BOMDAL)));
        //        }
        //        if (<>o__1.<>p__1 == null)
        //        {
        //            CSharpArgumentInfo[] argumentInfo = new CSharpArgumentInfo[13];
        //            argumentInfo[0] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.IsStaticType | CSharpArgumentInfoFlags.UseCompileTimeType, null);
        //            argumentInfo[1] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant | CSharpArgumentInfoFlags.UseCompileTimeType, null);
        //            argumentInfo[2] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[3] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[4] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[5] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[6] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[7] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[8] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[9] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[10] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[11] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            argumentInfo[12] = CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null);
        //            <>o__1.<>p__1 = CallSite<Func<CallSite, Type, string, object, object, object, object, object, object, object, object, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Format", null, typeof(BOMDAL), argumentInfo));
        //        }
        //        command.CommandText = <>o__1.<>p__2.Target(<>o__1.<>p__2, <>o__1.<>p__1.Target(<>o__1.<>p__1, typeof(string), "Insert into ITT1(Father,ChildNum,VisOrder,Code,Quantity,Warehouse,Price,Currency,PriceList,OrigPrice,OrigCurr) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", recordset.Fields.Item(0).Value, recordset.Fields.Item(1).Value, recordset.Fields.Item(2).Value, recordset.Fields.Item(3).Value, recordset.Fields.Item(4).Value, recordset.Fields.Item(5).Value, recordset.Fields.Item(6).Value, recordset.Fields.Item(7).Value, recordset.Fields.Item(8).Value, recordset.Fields.Item((int) 9).Value, recordset.Fields.Item((int) 10).Value));
        //        command.ExecuteNonQuery();
        //        recordset.MoveNext();
        //    }
        //}
    }
}

