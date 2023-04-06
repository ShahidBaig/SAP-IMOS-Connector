namespace IMW.WinUI
{
    using IMW.Common;
    using Microsoft.Extensions.Configuration;
    using SAPbobsCOM;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Runtime.InteropServices;

    public class SAPSettings
    {
        public static SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
        public static DataTable TableLines;
        public static Documents oOrder;
        public static Documents oInvoice;
        public static SAPbobsCOM.Recordset oRecordSet;

        public static void ConnectSAP()
        {
            try
			{
				var appSettings = AppConfiguration.Configuration.GetSection("AppSettings").Get<AppSettings>();

				oCompany.CompanyDB = appSettings.CompanyDB;
                oCompany.Server = appSettings.Server;
                oCompany.LicenseServer = appSettings.LicenseServer;
                oCompany.SLDServer = appSettings.SLDServer;
                oCompany.DbUserName = appSettings.DbUserName;
                oCompany.DbPassword = appSettings.DbPassword;
                oCompany.UserName = appSettings.UserName;
                oCompany.Password = appSettings.Password;

                if (appSettings.DbServerType == "dst_MSSQL2014")
                {
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                }
                else if (appSettings.DbServerType == "dst_HANADB")
                {
                    oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_HANADB;
                }
                if (appSettings.UseTrusted == "True")
                {
                    oCompany.UseTrusted = true;
                }
                else if (appSettings.UseTrusted == "False")
                {
                    oCompany.UseTrusted = false;
                }
                int num = oCompany.Connect();
                string lastErrorDescription = oCompany.GetLastErrorDescription();
                if (oCompany.GetLastErrorCode() != 0)
                {
                }
            }
            catch (Exception)
            {
            }
        }
    }
}

