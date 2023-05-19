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

    public class CustomerDAL
    {
        private Company oCompany = ((Company)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090"))));

        public Customer GetCustomer(Customer c)
        {
            this.oCompany = new SAPDAL().ConnectSAP();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            recordset.DoQuery("SELECT CntctCode, Name, Tel1 FROM OCPR WHERE CardCode = '" + c.CustomerCode + "'");
            while (!recordset.EoF)
            {
                c.ContactCode = recordset.Fields.Item(0).Value.ToString();
                c.ContactPerson = recordset.Fields.Item(1).Value.ToString();
                c.Phone = recordset.Fields.Item(2).Value.ToString();

                recordset.MoveNext();
            }
            return c;
        }

        public List<Customer> GetCustomers()
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            List<Customer> list = new List<Customer>();
            SqlDataReader reader = new SqlCommand
            {
                Connection = connection,
                CommandText = "select CardCode,CardName,DocEntry from OCRD"
            }.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    connection.Close();
                    return list;
                }
                Customer item = new Customer
                {
                    CustomerCode = reader["CardCode"].ToString(),
                    CustomerName = reader["CardName"].ToString(),
                    DocEntry = Convert.ToInt32(reader["DocEntry"].ToString())
                };
                list.Add(item);
            }
        }

        public int GetLastCustomerFSAP()
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
                CommandText = "Select isnull(max(DocEntry),0) Last from OCRD"
            };
            int num = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            command.Dispose();
            return num;
        }

        public bool LoadCustomers()
        {
            int lastCustomerFSAP = this.GetLastCustomerFSAP();
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            this.oCompany = new SAPDAL().ConnectSAP();
            List<Customer> list = new List<Customer>();
            Recordset recordset = this.oCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
            recordset.DoQuery($"SELECT * FROM OCRD where \"DocEntry\" > {lastCustomerFSAP.ToString()}");
            while (true)
            {
                if (recordset.EoF)
                {
                    transaction.Commit();
                    connection.Close();
                    return true;
                }
                Customer customer = new Customer();
                customer.CustomerCode = recordset.Fields.Item("CARDCODE").Value;
                customer.CustomerName = recordset.Fields.Item("CARDNAME").Value;
                customer.DocEntry = recordset.Fields.Item("DOCENTRY").Value;
                new SqlCommand
                {
                    Connection = connection,
                    Transaction = transaction,
                    CommandText = $"Insert into OCRD(CardCode,CardName,DocEntry) values ('{customer.CustomerCode}','{customer.CustomerName}','{customer.DocEntry}')"
                }.ExecuteNonQuery();
                recordset.MoveNext();
            }
            return false;
        }
    }
}

