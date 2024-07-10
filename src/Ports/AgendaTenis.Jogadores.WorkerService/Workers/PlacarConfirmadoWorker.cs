using AgendaTenis.Jogadores.Core.Eventos.Consumidores;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AgendaTenis.Jogadores.WorkerService.Workers
{
    public class PlacarConfirmadoWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PlacarConfirmadoWorker> _logger;

        public PlacarConfirmadoWorker(IServiceScopeFactory serviceScopeFactory, ILogger<PlacarConfirmadoWorker> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
               
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumidor = scope.ServiceProvider.GetRequiredService<PlacarConfirmadoConsumidor>();
                        await consumidor.Consume(stoppingToken);

                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Ocorreu um erro no consumo de mensagens.");
                    }

                    await Task.Delay(1000, stoppingToken);
                }

            }
        }
    }
}
