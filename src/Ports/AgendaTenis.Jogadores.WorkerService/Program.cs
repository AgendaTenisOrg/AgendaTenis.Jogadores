using AgendaTenis.Eventos.Configuracao;
using AgendaTenis.Eventos.Servicos;
using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Aplicacao.AtualizarPontuacao;
using AgendaTenis.Jogadores.Core.Eventos.Consumidores;
using AgendaTenis.Jogadores.WorkerService.Workers;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName;
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    Console.WriteLine("Logging" + context.Configuration["Logging:LogLevel:Default"]);
                    Console.WriteLine("Connection string" + context.Configuration.GetConnectionString("Jogadores"));

                    services.AddHostedService<PlacarConfirmadoWorker>();

                    services.AddScoped<AtualizarPontuacaoHandler>();
                    services.AddScoped<PlacarConfirmadoConsumidor>();

                    services.AddDbContext<JogadoresDbContext>(options =>
                        options.UseNpgsql(context.Configuration.GetConnectionString("Jogadores")));

                    services.Configure<RabbitMQConfiguracao>(context.Configuration.GetSection("RabbitMQ"));
                    services.AddSingleton<IMessageBus, RabbitMessageBus>();
                });

            var host = builder.Build();
            host.Run();
        }
    }
}
