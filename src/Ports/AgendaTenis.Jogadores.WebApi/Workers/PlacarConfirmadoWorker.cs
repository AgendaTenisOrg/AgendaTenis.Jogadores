using AgendaTenis.Jogadores.Core.Eventos.Consumidores;

namespace AgendaTenis.Jogadores.WebApi.Workers;

public class PlacarConfirmadoWorker : BackgroundService
{
    private readonly PlacarConfirmadoConsumidor _consumidor;

    public PlacarConfirmadoWorker(PlacarConfirmadoConsumidor consumidor)
    {
        _consumidor = consumidor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _consumidor.Consume(stoppingToken);
    }
}
