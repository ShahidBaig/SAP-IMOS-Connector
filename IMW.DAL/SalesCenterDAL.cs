namespace IMW.DAL
{
    using IMW.Common;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class SalesCenterDAL
    {
        public bool ConnectSalesCenters(SalesCenters sc)
        {
            bool flag;
            try
            {
                SqlConnection connection = new SqlConnection {
                    ConnectionString = this.GetConnectionString(sc)
                };
                connection.Open();
                flag = true;
                connection.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public string GetConnectionString(SalesCenters sc) => 
            $"Server={sc.SQLServer};Database={sc.DatabaseName};User id={sc.DbUserName};password={sc.DBPassword}";

        internal SalesCenters GetSalesCenter(string orderId)
        {
            SalesCenters centers = new SalesCenters();
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand {
                CommandText = $"select * from IMOSCenters where FirstOrder like '{orderId.Substring(0, 4)}%'",
                Connection = connection
            };
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    return centers;
                }
                centers.Name = reader["Name"].ToString();
                centers.FirstOrder = reader["FirstOrder"].ToString();
                centers.MachineAddress = reader["MachineAddress"].ToString();
                centers.SQLServer = reader["SQLServer"].ToString();
                centers.DatabaseName = reader["DatabaseName"].ToString();
                centers.DbUserName = reader["DbUserName"].ToString();
                centers.DBPassword = reader["DbPassword"].ToString();
            }
        }

        public List<SalesCenters> GetSalesCenters()
        {
            List<SalesCenters> list = new List<SalesCenters>();
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand {
                CommandText = "select * from IMOSCenters", // WHERE NAME = 'M-FC'",
                Connection = connection
            };
            SqlDataReader reader = command.ExecuteReader();
            while (true)
            {
                if (!reader.Read())
                {
                    reader.Close();
                    command.Dispose();
                    connection.Close();
                    return list;
                }
                SalesCenters item = new SalesCenters {
                    Name = reader["Name"].ToString(),
                    FirstOrder = reader["FirstOrder"].ToString(),
                    MachineAddress = reader["MachineAddress"].ToString(),
                    SQLServer = reader["SQLServer"].ToString(),
                    DatabaseName = reader["DatabaseName"].ToString(),
                    DbUserName = reader["DbUserName"].ToString(),
                    DBPassword = reader["DbPassword"].ToString()
                };
                list.Add(item);
            }
        }

        public bool SaveSalesCenter(SalesCenters sc, bool isUpdate)
        {
            SqlConnection connection = new SqlConnection {
                ConnectionString = HelperDAL.ISCConnectionString
            };
            connection.Open();
            SqlCommand command = new SqlCommand {
                Connection = connection
            };
            if (isUpdate)
            {
                command.CommandText = string.Format("Update [dbo].[IMOSCenters] set [FirstOrder]='{1}',[MachineAddress]='{2}',[SQLServer]='{3}',[DatabaseName]='{4}',[DBUserName]='{5}',[DBPassword]='{6}' where [Name]='{0}'", new object[] { sc.Name, sc.FirstOrder, sc.MachineAddress, sc.SQLServer, sc.DatabaseName, sc.DbUserName, sc.DBPassword });
            }
            else
            {
                command.CommandText = $"INSERT INTO[dbo].[IMOSCenters] ([Name],[FirstOrder],[MachineAddress],[SQLServer],[DatabaseName],[DBUserName],[DBPassword]) VALUES ('{sc.Name}','{sc.FirstOrder}','{sc.MachineAddress}','{sc.SQLServer}','{sc.DatabaseName}','{sc.DbUserName}','{sc.DBPassword}')";
            }
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }
    }
}

