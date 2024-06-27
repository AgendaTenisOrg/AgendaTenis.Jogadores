namespace AgendaTenis.Jogadores.Infra.Eventos.Base;

public interface IEventConsumer
{
    Task Consume(CancellationToken stoppingToken);
}
