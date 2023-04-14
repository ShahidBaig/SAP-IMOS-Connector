using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMW.DAL;
using System.Text.Json.Nodes;
using System.Text.Json;
using IMW.Common;
using System.ServiceProcess;

namespace ISCService
{
    public sealed class JobService
    {
        public bool SyncItems { get; set; }  = false;
        public bool SyncCustomers { get; set; } = false;
        public bool CreateSQFromSAPOPToSAPSQ { get; set; } = false;
        public bool TransferSQFromSAPToISC { get; set; } = false;
        public bool TransferSQFromISCToIMOS { get; set; } = false;
        public bool TransferSQFromIMOSToISC { get; set; } = false;
        public bool TransferSQFromISCToSAP { get; set; } = false;

        public JobService() 
        {
            SetupSyncSettings();
        }

        private string GetSettings(string name)
        {
            var configJson1 = File.ReadAllText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"));
            var jsonNodeOptions1 = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
            var node1 = JsonNode.Parse(configJson1, jsonNodeOptions1);
            var configJson = File.ReadAllText(Path.Combine(node1["AppSettings"]["ISCAppPath"].ToString(), "appsettings.json"));
            var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
            var node = JsonNode.Parse(configJson, jsonNodeOptions);

            HelperDAL.SettingsPath = node1["AppSettings"]["ISCAppPath"].ToString();

            return node["AppSettings"][name].ToString();
        }

        private bool[] GetJobSyncFlags()
        {
            bool[] flags = { SyncItems, SyncCustomers, CreateSQFromSAPOPToSAPSQ, TransferSQFromSAPToISC, TransferSQFromISCToIMOS, TransferSQFromIMOSToISC, TransferSQFromISCToSAP };

            return flags;
        }

        public string[] GetJobs()
        {
            string[] jobs =
            {
                    "LoadItems",
                    "LoadCustomers",
                    "CreateSQFromSAPOPToSAPSQ",
                    "TransferSQfromSAPtoISC",
                    "TransferSQfromISCtoIMOS",
                    "TransferSQfromIMOStoISC",
                    "TransferSQfromISCtoSAP"
                };

            return jobs;
        }

        public bool[] SetupSyncSettings()
        {
            string[] splits = GetSettings("Sync").Split('|');
            int index = 0;

            while (index < splits.Length)
            {
                if (index == 0)
                {
                    SyncItems = splits[index] == "1" ? true : false;
                }
                else if (index == 1)
                {
                    SyncCustomers = splits[index] == "1" ? true : false;
                }
                else if (index == 2)
                {
                    CreateSQFromSAPOPToSAPSQ = splits[index] == "1" ? true : false;
                }
                else if (index == 3)
                {
                    TransferSQFromSAPToISC = splits[index] == "1" ? true : false;
                }
                else if (index == 4)
                {
                    TransferSQFromISCToIMOS = splits[index] == "1" ? true : false;
                }
                else if (index == 5)
                {
                    TransferSQFromIMOSToISC = splits[index] == "1" ? true : false;
                }
                else if (index == 6)
                {
                    TransferSQFromISCToSAP = splits[index] == "1" ? true : false;
                }

                index++;
            }

            return GetJobSyncFlags();
        }

        public void ExecuteJob(string jobName)
        {
            if (jobName == "LoadItems" && SyncItems)
            {
                new ItemsDAL().LoadItem();
            }
            else if (jobName == "LoadCustomers" && SyncCustomers)
            {
                new CustomerDAL().LoadCustomers();
            }
            else if (jobName == "CreateSQFromSAPOPToSAPSQ" && CreateSQFromSAPOPToSAPSQ)
            {
                new SaleQuotationDAL().CreateSQFromOP();
            }
            else if (jobName == "TransferSQfromSAPtoISC" && TransferSQFromSAPToISC)
            {
                new SaleQuotationDAL().TransferSQFromoSAPToISC();
            }
            else if (jobName == "TransferSQfromISCtoIMOS" && TransferSQFromISCToIMOS)
            {
                new SaleQuotationDAL().TransferSQFromISCToIMOS();
            }
            else if (jobName == "TransferSQfromIMOStoISC" && TransferSQFromIMOSToISC)
            {
                new SaleQuotationDAL().TransferSQFromIMOSToISC();
            }
            else if (jobName == "TransferSQfromISCtoSAP" && TransferSQFromISCToSAP)
            {
                new SaleQuotationDAL().TransferSQFromISCToSAP();
            }
        }
    }
}
