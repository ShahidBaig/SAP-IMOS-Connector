using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMW.Common
{
	public class AppSettings
	{
		public string CompanyDB { get; set; }

		public string Server { get; set; }

		public string LicenseServer { get; set; }

		public string SLDServer { get; set; }

		public string DbUserName { get; set; }

		public string DbPassword { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string DbServerType { get; set; }

		public string UseTrusted { get; set; }

		public string TSI { get; set; }

        public string Sync { get; set; }
    }
}
