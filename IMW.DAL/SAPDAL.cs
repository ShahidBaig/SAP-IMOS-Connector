namespace IMW.DAL
{
    using IMW.Common;
    using SAPbobsCOM;
    using System;
    using System.Configuration;
    using System.Runtime.InteropServices;
	using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class SAPDAL
    {
        public Company ConnectSAP()
        {
            Company company2;
            //Company company = (Company) Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("632F4591-AA62-4219-8FB6-22BCF5F60090")));
            SAPbobsCOM.Company company = new SAPbobsCOM.Company();

            AppSettings appSettings = HelperDAL.GetSettings();

			try
            {
                company.CompanyDB = appSettings.CompanyDB;
				company.Server = appSettings.Server;
				company.LicenseServer = appSettings.LicenseServer;
                company.SLDServer = appSettings.SLDServer;
				company.DbUserName = appSettings.DbUserName;
				company.DbPassword = appSettings.DbPassword;
				company.UserName = appSettings.UserName;
				company.Password = appSettings.Password;

                if (appSettings.DbServerType == "dst_MSSQL2014")
                {
                    company.DbServerType = BoDataServerTypes.dst_MSSQL2014;
                }
                else if (appSettings.DbServerType == "dst_HANADB")
                {
                    company.DbServerType = BoDataServerTypes.dst_HANADB;
                }
                if (appSettings.UseTrusted == "True")
                {
                    company.UseTrusted = true;
                }
                else if (appSettings.UseTrusted == "False")
                {
                    company.UseTrusted = false;
                }

                int num = company.Connect();
                
                string lastErrorDescription = company.GetLastErrorDescription();
                company2 = (company.GetLastErrorCode() == 0) ? company : null;

                if (company2 is null)
                {
                    LogConsumerDAL.Instance.Write($"SAP Connection: {lastErrorDescription}");
                }
            }
            catch (Exception ex)
            {
                LogConsumerDAL.Instance.Write($"SAP Connection Exception: {ex.Message}");

                throw ex;
            }

            return company2;
        }
    }
}

