using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StreamProcessor.BL.Consumers;
using StreamProcessor.BL.Data;
using StreamProcessor.BL.Models;
using StreamProcessor.BL.Producers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace StreamProcessor.API.Services
{
    public class StreamProcessor : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Process _generator;

        public StreamProcessor(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _generator = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine("Generators", GetGeneratorFileName()),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            _generator.Start();

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var scope = _scopeFactory.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IStatisticsRepository>();

            var producer = new MessagesProducer(_generator.StandardOutput, stoppingToken);

            var consumers = new List<IConsumer<EventMessage>>
            {
                new EventTypesCountConsumer(repo),
                new WordAppearancesConsumer(repo)
                // Can add here more consumers, for logging and other purposes
            };

            foreach (var consumer in consumers)
                producer.Subscribe(consumer);

            producer.Start();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            // Need to kill the generator process here

            return base.StopAsync(cancellationToken);
        }

        private string GetGeneratorFileName()
        {
            if (OperatingSystem.IsWindows())
                return "generator-windows.exe";
            else if (OperatingSystem.IsMacOS())
                return "generator-macosx";
            else if (OperatingSystem.IsLinux())
                return "generator-linux";

            throw new NotSupportedException("Not supported operating system");
        }
    }
}
