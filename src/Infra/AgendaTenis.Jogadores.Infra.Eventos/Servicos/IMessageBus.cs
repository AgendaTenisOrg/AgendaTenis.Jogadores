using RabbitMQ.Client;

namespace AgendaTenis.Jogadores.Infra.Eventos.Servicos;

public interface IMessageBus
{
    public IConnection GetConnection();
    IModel GetChannel(IConnection connection);
}
