using IMW.Common;
using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using Newtonsoft.Json;
using IMW.DB;
using System.Data;

namespace IMW.DAL
{

    public class HelperDAL
    {
        public static string ISCConnectionString {get; set;}

        public static string IMOSConnectionString { get; set; }

		public static string UniqueCode { get; set; }

        static HelperDAL()
        {
            UniqueCode = DateTime.Now.Date.ToString("yyyy/MM/dd").Replace("/", "") + DateTime.Now.TimeOfDay.Hours.ToString("00") + DateTime.Now.TimeOfDay.Minutes.ToString("00") + DateTime.Now.TimeOfDay.Seconds.ToString("00");
		}

        public static void SetupConnectionStrings()
        {
            try
            {
                var appSettings = AppConfiguration.Configuration?.GetSection("ConnectionStrings")?.Get<ConnectionStrings>();

                ISCConnectionString = appSettings?.iscConn;
                IMOSConnectionString = appSettings?.imosConn;
            }
            catch (Exception)
            {
            }
        }

        public static void SetupConnectionStrings(string p_ISCConn, string p_IMOSConn)
        {
            try
            {
                ISCConnectionString = p_ISCConn;
                IMOSConnectionString = p_IMOSConn;
            }
            catch (Exception)
            {
            }
        }

        public static AppSettings GetSettings()
        {
            DBConnector l_Conn = new DBConnector(HelperDAL.ISCConnectionString);
            DataTable l_Data = new DataTable();
            AppSettings appSettings = new AppSettings();

            try
            {
                l_Conn.GetData("SELECT * FROM AppSettings WITH (NOLOCK)", ref l_Data);

                foreach (DataRow row in l_Data.Rows)
                {
                    if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "CompanyDB")
                    {
                        appSettings.CompanyDB = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "Server")
                    {
                        appSettings.Server = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "SLDServer")
                    {
                        appSettings.SLDServer = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "LicenseServer")
                    {
                        appSettings.LicenseServer = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "DbUserName")
                    {
                        appSettings.DbUserName = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "DbPassword")
                    {
                        appSettings.DbPassword = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "UserName")
                    {
                        appSettings.UserName = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "Password")
                    {
                        appSettings.Password = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "DbServerType")
                    {
                        appSettings.DbServerType = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "UseTrusted")
                    {
                        appSettings.UseTrusted = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "TSI")
                    {
                        appSettings.TSI = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "Sync")
                    {
                        appSettings.Sync = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                    else if (PublicFunctions.ConvertNullAsString(row["Tag"], string.Empty) == "BoMLastAutoID")
                    {
                        appSettings.BoMLastAutoID = PublicFunctions.ConvertNullAsString(row["TagValue"], string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                l_Data.Dispose();
            }

            return appSettings;
        }

        public static bool SaveTagValue(string p_Tag, string p_TagValue)
        {
            try
            {
                DBConnector l_Conn = new DBConnector(HelperDAL.ISCConnectionString);

                return l_Conn.Execute($"UPDATE AppSettings SET TagValue = '{p_TagValue}' WHERE Tag = '{p_Tag}'");
            }
            catch(Exception ex)
            {

            }

            return true;
        }
    }
}

