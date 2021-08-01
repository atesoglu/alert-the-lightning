using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Application.Flows.LightningStrikes.Commands;
using Application.Models;
using Application.Persistence;
using Application.Request;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HostedService
{
    public class Worker : BackgroundService
    {
        private readonly Queue<ProcessLightningStrikeCommand> _queue = new Queue<ProcessLightningStrikeCommand>();

        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceScopeFactory scopeFactory, ILogger<Worker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;

            using var scope = _scopeFactory.CreateScope();
            scope.ServiceProvider.GetRequiredService<IDataContext>().SeedData();
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_queue.Count == 0)
                {
                    var lines = await File.ReadAllLinesAsync("lightnings.json", cancellationToken);
                    //var lines = await File.ReadAllLinesAsync("lightnings-slim.json", cancellationToken);
                    lines.ToList().ForEach(line => _queue.Enqueue(JsonSerializer.Deserialize<ProcessLightningStrikeCommand>(line, new JsonSerializerOptions {PropertyNameCaseInsensitive = true})));
                }

                var command = _queue.Dequeue();

                using (var scope = _scopeFactory.CreateScope())
                {
                    var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<ProcessLightningStrikeCommand, LightningStrikeObjectModel>>();
                    await handler.HandleAsync(command, cancellationToken);
                }

                try
                {
                    await Task.Delay(300, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}