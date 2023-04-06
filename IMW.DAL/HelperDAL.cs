using IMW.Common;
using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace IMW.DAL
{

    public class HelperDAL
    {
        public static string ISCConnectionString {get; set;}

        public static string IMOSConnectionString { get; set; }

		public static string UniqueCode { get; set; } 

        static HelperDAL()
        {
			var appSettings = AppConfiguration.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

			ISCConnectionString = appSettings.iscConn;
			IMOSConnectionString = appSettings.imosConn;
			UniqueCode = DateTime.Now.Date.ToString("yyyy/MM/dd").Replace("/", "") + DateTime.Now.TimeOfDay.Hours.ToString("00") + DateTime.Now.TimeOfDay.Minutes.ToString("00") + DateTime.Now.TimeOfDay.Seconds.ToString("00");
		}
	}
}

