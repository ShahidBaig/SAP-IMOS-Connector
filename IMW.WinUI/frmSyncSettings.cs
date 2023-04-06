using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using IMW.Common;
using Microsoft.Extensions.Configuration;
using IMW.DAL;
using System.ServiceProcess;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace IMW.WinUI
{
    public partial class frmSyncSettings : Form
    {
        ServiceController sc = null;
        string serviceName = "ISC Service";

        public frmSyncSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var configJson = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"));

                var jsonNodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = true };
                var node = JsonNode.Parse(configJson, jsonNodeOptions);
                var options = new JsonSerializerOptions { WriteIndented = true };

                node["AppSettings"]["Sync"] = $"{(chkSyncItems.Checked ? 1 : 0)}|{(chkSyncCustomers.Checked ? 1 : 0)}|{(chkCreateSQInSAPForEachOpportunity.Checked ? 1 : 0)}|{(chkTransferSQFromSAPToISC.Checked ? 1 : 0)}|{(chkTransferSQFromISCToIMOS.Checked ? 1 : 0)}|{(chkTransferSQFromIMOSToISC.Checked ? 1 : 0)}|{(chkTransferSQFromISCToSAP.Checked ? 1 : 0)}";

                File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), node.ToJsonString(options));
                MessageBox.Show("Settings saved successfully!", "Sync Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void frmSyncSettings_Load(object sender, EventArgs e)
        {
            try
            {
                AppSettings appSettings = AppConfiguration.Configuration.GetSection("AppSettings").Get<AppSettings>();
                string settings = appSettings.Sync;
                string[] splits = settings.Split('|');
                int index = 0;

                while (index < splits.Length)
                {
                    if (index == 0)
                    {
                        chkSyncItems.Checked = splits[index] == "1" ? true : false;
                        btnLoadItems.Enabled = splits[index] == "1" ? false : true;
                    }
                    else if (index == 1)
                    {
                        chkSyncCustomers.Checked = splits[index] == "1" ? true : false;
                        btnLoadCustomers.Enabled = splits[index] == "1" ? false : true;
                    }
                    else if (index == 2)
                    {
                        chkCreateSQInSAPForEachOpportunity.Checked = splits[index] == "1" ? true : false;
                        btnCreateSQInSAPForEachOpportunity.Enabled = splits[index] == "1" ? false : true;
                    }
                    else if (index == 3)
                    {
                        chkTransferSQFromSAPToISC.Checked = splits[index] == "1" ? true : false;
                        btnTransferSQfromSAPtoISC.Enabled = splits[index] == "1" ? false : true;
                    }
                    else if (index == 4)
                    {
                        chkTransferSQFromISCToIMOS.Checked = splits[index] == "1" ? true : false;
                        btnTransferSQfromISCtoIMOS.Enabled = splits[index] == "1" ? false : true;
                    }
                    else if (index == 5)
                    {
                        chkTransferSQFromIMOSToISC.Checked = splits[index] == "1" ? true : false;
                        btnTransferSQFromIMOSToISC.Enabled = splits[index] == "1" ? false : true;
                    }
                    else if (index == 6)
                    {
                        chkTransferSQFromISCToSAP.Checked = splits[index] == "1" ? true : false;
                        btnTransferSQfromISCtoSAP.Enabled = splits[index] == "1" ? false : true;
                    }

                    index++;
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                this.sc = new ServiceController(serviceName);

                btnInstallSyncService.Enabled = false;
                btnUnInstallSyncService.Enabled = true;

                if (sc.Status.Equals(ServiceControllerStatus.Stopped) || sc.Status.Equals(ServiceControllerStatus.StopPending))
                {
                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                }
                else if (sc.Status.Equals(ServiceControllerStatus.Running) || sc.Status.Equals(ServiceControllerStatus.StartPending))
                {
                    btnStartService.Enabled = false;
                    btnStopService.Enabled = true;
                }
            }
            catch (Exception)
            {
                btnStartService.Enabled = false;
                btnStopService.Enabled = false;

                btnInstallSyncService.Enabled = true;
                btnUnInstallSyncService.Enabled = false;
            }
        }

        private void chkSyncItems_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnLoadItems.Enabled = !chkSyncItems.Checked;
            }
            catch (Exception) { }
        }

        private void chkSyncCustomers_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnLoadCustomers.Enabled = !chkSyncCustomers.Checked;
            }
            catch (Exception) { }
        }

        private void chkTransferSQFromSAPToISC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnTransferSQfromSAPtoISC.Enabled = !chkTransferSQFromSAPToISC.Checked;
            }
            catch (Exception) { }
        }

        private void chkTransferSQFromISCToIMOS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnTransferSQfromISCtoIMOS.Enabled = !chkTransferSQFromISCToIMOS.Checked;
            }
            catch (Exception) { }
        }

        private void chkTransferSQFromISCToSAP_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnTransferSQfromISCtoSAP.Enabled = !chkTransferSQFromISCToSAP.Checked;
            }
            catch (Exception) { }
        }

        private void chkTransferSQFromIMOSToISC_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnTransferSQFromIMOSToISC.Enabled = !chkTransferSQFromIMOSToISC.Checked;
            }
            catch (Exception) { }
        }

        private void btnLoadItems_Click(object sender, EventArgs e)
        {
            try
            {
                LogConsumerDAL.Instance.Write("Initiating Getting Item Master Data from SAP to ISC");
                new ItemsDAL().LoadItem();
                LogConsumerDAL.Instance.Write("Completed Iteration for Item Master Data from SAP to ISC");
            }
            catch (Exception) { }
        }

        private void btnLoadCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                LogConsumerDAL.Instance.Write("Initiating Getting Customers Data from SAP to ISC");
                new CustomerDAL().LoadCustomers();
                LogConsumerDAL.Instance.Write("Completed Iteration for Customers Data from SAP to ISC");
            }
            catch (Exception) { }
        }

        private void btnTransferSQfromSAPtoISC_Click(object sender, EventArgs e)
        {
            try
            {
                LogConsumerDAL.Instance.Write("Initiating Getting Sales Qoutation Data from SAP to ISC");
                new SaleQuotationDAL().TransferSQFromoSAPToISC();
                LogConsumerDAL.Instance.Write("Completed Iteration for Qoutation Data from SAP to ISC");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnTransferSQfromISCtoIMOS_Click(object sender, EventArgs e)
        {
            try
            {
                LogConsumerDAL.Instance.Write("Initiating Getting Sales Qoutation Data from ISC to IMOS");
                new SaleQuotationDAL().TransferSQFromISCToIMOS();
                LogConsumerDAL.Instance.Write("Completed Iteration for Qoutation Data from ISC to IMOS");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnTransferSQfromISCtoSAP_Click(object sender, EventArgs e)
        {
            try
            {
                LogConsumerDAL.Instance.Write("Initiating Getting Sales Qoutation Data from ISC to SAP");
                new SaleQuotationDAL().TransferSQFromISCToSAP();
                LogConsumerDAL.Instance.Write("Completed Iteration Sales Qoutation from ISC to SAP");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnTransferSQFromIMOSToISC_Click(object sender, EventArgs e)
        {
            try
            {
                LogConsumerDAL.Instance.Write("Initiating Getting Sales Qoutation Data from IMOS to ISC");
                new SaleQuotationDAL().TransferSQFromIMOSToISC();
                LogConsumerDAL.Instance.Write("Completed Iteration Sales Qoutation from IMOS to ISC");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnStartService_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = $"C:\\Windows\\System32\\sc.exe";
                p.StartInfo.Arguments = $"start \"{serviceName}\"";

                p.Start();
                p.WaitForExit();

                btnStartService.Enabled = false;
                btnStopService.Enabled = true;
            }
            catch (Exception)
            {

            }
        }

        private void btnStopService_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = $"C:\\Windows\\System32\\sc.exe";
                p.StartInfo.Arguments = $"stop \"{serviceName}\"";

                p.Start();
                p.WaitForExit();

                btnStartService.Enabled = true;
                btnStopService.Enabled = false;
            }
            catch (Exception)
            {

            }
        }

        private void btnInstallSyncService_Click(object sender, EventArgs e)
        {
            try
            {
                //string servicePath = "C:\\Personal\\Interwood\\IMW_Code\\ISCService\\bin\\Debug\\net6.0\\ISCService.exe";
                string servicePath = Path.Combine(Application.StartupPath.Replace("\\App", "\\Service"),  "ISCService.exe");
                Process p = new Process();

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = $"C:\\Windows\\System32\\sc.exe";
                p.StartInfo.Arguments = $"create \"{serviceName}\" binpath=\"{servicePath}\" start=auto";

                p.Start();
                
                string output = p.StandardOutput.ReadToEnd().ToLower();
                p.WaitForExit();

                if (output.Contains("success"))
                {
                    this.sc = new ServiceController(serviceName);

                    btnStartService.Enabled = true;
                    btnStopService.Enabled = false;
                    btnInstallSyncService.Enabled = false;
                    btnUnInstallSyncService.Enabled = true;
                    
                    MessageBox.Show("ISC Service installed.", "Install ISC Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Access is denied. Please run this program with Admin rights and try again.", "Install ISC Service", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnUnInstallSyncService_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.sc != null)
                {
                    if (sc.Status.Equals(ServiceControllerStatus.Running) || sc.Status.Equals(ServiceControllerStatus.StartPending))
                    {
                        this.sc.Stop();
                    }
                }

                Process p = new Process();

                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = $"C:\\Windows\\System32\\sc.exe";
                p.StartInfo.Arguments = $"delete \"{serviceName}\"";

                p.Start();
                p.WaitForExit();

                btnStartService.Enabled = false;
                btnStopService.Enabled = false;

                btnInstallSyncService.Enabled = true;
                btnUnInstallSyncService.Enabled = false;
            }
            catch (Exception)
            {
            }
        }

        private void btnCreateSQInSAPForEachOpportunity_Click(object sender, EventArgs e)
        {
            try
            {
                LogConsumerDAL.Instance.Write("Initiating Creating SQ from SAP Opportunity to SAP SQ");
                new SaleQuotationDAL().CreateSQFromOP();
                LogConsumerDAL.Instance.Write("Completed Creating SQ from SAP Opportunity to SAP SQ");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chkCreateSQInSAPForEachOpportunity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                btnCreateSQInSAPForEachOpportunity.Enabled = !chkCreateSQInSAPForEachOpportunity.Checked;
            }
            catch (Exception) { }
        }
    }
}
