using AgendaTenis.Jogadores.Infra.Eventos.Configuracao;
using AgendaTenis.Jogadores.Infra.Eventos.Servicos;

namespace AgendaTenis.Jogadores.WebApi.ConfiguracaoDeServicos;

public static class MessageBus
{
    public static void RegistrarMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQConfiguracao>(configuration.GetSection("RabbitMQ"));
        services.AddSingleton<IMessageBus, RabbitMessageBus>();
    }
}
