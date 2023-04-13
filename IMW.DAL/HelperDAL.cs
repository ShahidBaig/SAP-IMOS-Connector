using IMW.Common;
using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace IMW.DAL
{

    public class HelperDAL
    {
        public static string ISCConnectionString {get; set;}

        public static string IMOSConnectionString { get; set; }

		public static string UniqueCode { get; set; }

        private static string settingsPath = string.Empty;
        public static string SettingsPath
        {
            get { return settingsPath; }   // get method
            set 
            { 
                if(settingsPath != value)
                {
                    var configJson1 = File.ReadAllText(Path.Combine(value, "appsettings.json"));
                    var jsonNodeOptions1 = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
                    var node1 = JsonNode.Parse(configJson1, jsonNodeOptions1);

                    ISCConnectionString = node1["ConnectionStrings"]["iscConn"].ToString();
                    IMOSConnectionString = node1["ConnectionStrings"]["imosConn"].ToString();
                }

                settingsPath = value; 
            }  // set method
        }

        static HelperDAL()
        {
            if (string.IsNullOrEmpty(SettingsPath))
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
            else
            {
                var configJson1 = File.ReadAllText(Path.Combine(SettingsPath, "appsettings.json"));
                var jsonNodeOptions1 = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
                var node1 = JsonNode.Parse(configJson1, jsonNodeOptions1);

                ISCConnectionString = node1["ConnectionStrings"]["iscConn"].ToString();
                IMOSConnectionString = node1["ConnectionStrings"]["imosConn"].ToString();
            }

            UniqueCode = DateTime.Now.Date.ToString("yyyy/MM/dd").Replace("/", "") + DateTime.Now.TimeOfDay.Hours.ToString("00") + DateTime.Now.TimeOfDay.Minutes.ToString("00") + DateTime.Now.TimeOfDay.Seconds.ToString("00");
		}

        public static AppSettings GetSettings()
        {
            if (string.IsNullOrEmpty(SettingsPath))
            {
                AppSettings appSettings = AppConfiguration.Configuration.GetSection("AppSettings").Get<AppSettings>();

                return appSettings;
            }
            else
            {
                var configJson1 = File.ReadAllText(Path.Combine(SettingsPath, "appsettings.json"));
                var jsonNodeOptions1 = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
                var node = JsonNode.Parse(configJson1, jsonNodeOptions1);
                AppSettings appSetings = new AppSettings();

                appSetings.CompanyDB = node["AppSettings"]["CompanyDB"].ToString();
                appSetings.Server = node["AppSettings"]["Server"].ToString();
                appSetings.SLDServer = node["AppSettings"]["SLDServer"].ToString();
                appSetings.LicenseServer = node["AppSettings"]["LicenseServer"].ToString();
                appSetings.DbUserName = node["AppSettings"]["DbUserName"].ToString();
                appSetings.DbPassword = node["AppSettings"]["DbPassword"].ToString();
                appSetings.UserName = node["AppSettings"]["UserName"].ToString();
                appSetings.Password = node["AppSettings"]["Password"].ToString();
                appSetings.DbServerType = node["AppSettings"]["DbServerType"].ToString();
                appSetings.UseTrusted = node["AppSettings"]["UseTrusted"].ToString();
                appSetings.TSI = node["AppSettings"]["TSI"].ToString();
                appSetings.Sync = node["AppSettings"]["Sync"].ToString();

                return appSetings;
            }
        }
    }
}

