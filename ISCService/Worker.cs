using System;

namespace ISCService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly JobService _jobService;

        public Worker(JobService jobService, ILogger<Worker> logger)
        {
            _logger = logger;
            _jobService = jobService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                int index = 0;
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

                while (!stoppingToken.IsCancellationRequested)
                {
                    _jobService.SetupSyncSettings();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    index = 0;

                    while (index < jobs.Length)
                    {
                        try
                        {
                            _logger.LogInformation("Worker executing job " + jobs[index] + " at: {time}", DateTimeOffset.Now);
                            _jobService.ExecuteJob(jobs[index]);
                            _logger.LogInformation("Worker completed job " + jobs[index] + " at: {time}", DateTimeOffset.Now);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogInformation("Worker job " + jobs[index] + " exception at: {time}", DateTimeOffset.Now);
                            _logger.LogInformation(ex.Message);
                        }

                        index++;
                    }

                    _logger.LogInformation("Worker completed at: {time}", DateTimeOffset.Now);

                    await Task.Delay(10000, stoppingToken);
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogInformation("Worker cancelled exception at: {time}", DateTimeOffset.Now);
                _logger.LogInformation(ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogInformation("Worker exception at: {time}", DateTimeOffset.Now);
                _logger.LogInformation(ex.Message);
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ISC Service");

            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping ISC Service");

            return base.StopAsync(cancellationToken);
        }
    }
}