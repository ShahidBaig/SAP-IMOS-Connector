using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMW.DAL;
using System.Text.Json.Nodes;
using System.Text.Json;
using IMW.Common;

namespace ISCService
{
    public sealed class JobService
    {
        bool syncItems = false;
        bool syncCustomers = false;
        bool createSQFromSAPOPToSAPSQ = false;
        bool transferSQFromSAPToISC = false;
        bool transferSQFromISCToIMOS = false;
        bool transferSQFromIMOSToISC = false;
        bool transferSQFromISCToSAP = false;

        public JobService() 
        {
            SetupSyncSettings();
        }

        private string GetSettings(string name)
        {
            try
            {
                var configJson1 = File.ReadAllText("appsettings.json");
                var jsonNodeOptions1 = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
                var node1 = JsonNode.Parse(configJson1, jsonNodeOptions1);

                var configJson = File.ReadAllText(Path.Combine(node1["AppSettings"]["ISCAppPath"].ToString(), "appsettings.json"));

                var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
                var node = JsonNode.Parse(configJson, jsonNodeOptions);

                return node["AppSettings"][name].ToString();
            }
            catch (Exception)
            {
            }

            return "0|0|1|1|1|1|1";
        }

        private void SetupSyncSettings()
        {
            string[] splits = GetSettings("Sync").Split('|');
            int index = 0;

            while (index < splits.Length)
            {
                if (index == 0)
                {
                    syncItems = splits[index] == "1" ? true : false;
                }
                else if (index == 1)
                {
                    syncCustomers = splits[index] == "1" ? true : false;
                }
                else if (index == 2)
                {
                    createSQFromSAPOPToSAPSQ = splits[index] == "1" ? true : false;
                }
                else if (index == 3)
                {
                    transferSQFromSAPToISC = splits[index] == "1" ? true : false;
                }
                else if (index == 4)
                {
                    transferSQFromISCToIMOS = splits[index] == "1" ? true : false;
                }
                else if (index == 5)
                {
                    transferSQFromIMOSToISC = splits[index] == "1" ? true : false;
                }
                else if (index == 6)
                {
                    transferSQFromISCToSAP = splits[index] == "1" ? true : false;
                }

                index++;
            }
        }

        public void ExecuteJob(string jobName)
        {
            if (jobName == "LoadItems" && syncItems)
            {
                new ItemsDAL().LoadItem();
            }
            else if (jobName == "LoadCustomers" && syncCustomers)
            {
                new CustomerDAL().LoadCustomers();
            }
            else if (jobName == "CreateSQFromSAPOPToSAPSQ" && createSQFromSAPOPToSAPSQ)
            {
                new SaleQuotationDAL().CreateSQFromOP();
            }
            else if (jobName == "TransferSQfromSAPtoISC" && transferSQFromSAPToISC)
            {
                new SaleQuotationDAL().TransferSQFromoSAPToISC();
            }
            else if (jobName == "TransferSQfromISCtoIMOS" && transferSQFromISCToIMOS)
            {
                new SaleQuotationDAL().TransferSQFromISCToIMOS();
            }
            else if (jobName == "TransferSQfromIMOStoISC" && transferSQFromIMOSToISC)
            {
                new SaleQuotationDAL().TransferSQFromIMOSToISC();
            }
            else if (jobName == "TransferSQfromISCtoSAP" && transferSQFromISCToSAP)
            {
                new SaleQuotationDAL().TransferSQFromISCToSAP();
            }
        }
    }
}
