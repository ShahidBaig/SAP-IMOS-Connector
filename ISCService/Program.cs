using ISCService;
using Microsoft.Extensions.Logging.EventLog;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting.WindowsServices;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(options =>
    {
        if(OperatingSystem.IsWindows())
        {
            options.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information);
        }
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<JobService>();
        services.AddHostedService<Worker>();

        //if (OperatingSystem.IsWindows())
        //{
        //    services.Configure<EventLogSettings>(config =>
        //    {
        //        config.LogName = "ISC Service";
        //        config.SourceName = "ISC Service Source";
        //    });
        //}
    })
    .UseWindowsService(options =>
    {
        options.ServiceName = "ISC Service";
    })
    .Build();

await host.RunAsync();
