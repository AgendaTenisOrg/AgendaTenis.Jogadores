using AgendaTenis.Jogadores.Core.Eventos.Consumidores;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaTenis.Jogadores.WorkerService.Workers;

public class PlacarConfirmadoWorker : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PlacarConfirmadoWorker(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            PlacarConfirmadoConsumidor consumidor = scope.ServiceProvider.GetRequiredService<PlacarConfirmadoConsumidor>();

            await consumidor.Consume(stoppingToken);
        }
    }
}
