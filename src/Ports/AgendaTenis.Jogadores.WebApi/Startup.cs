using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Aplicacao.AtualizarPontuacao;
using AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;
using AgendaTenis.Jogadores.Core.Aplicacao.CompletarPerfil;
using AgendaTenis.Jogadores.Core.Aplicacao.ObterResumoJogador;
using AgendaTenis.Jogadores.WebApi.ConfiguracaoDeServicos;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.RegistrarMessageBus(Configuration);

        services.AdicionarConfiguracaoSwagger();
        services.AdicionarAutenticacaoJWT(Configuration);

        services.AddDbContext<JogadoresDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("Jogadores")));

        services.AddScoped<CompletarPerfilHandler>();
        services.AddScoped<BuscarAdversariosHandler>();
        services.AddScoped<ObterResumoJogadorHandler>();
        services.AddScoped<AtualizarPontuacaoHandler>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("Redis");
            options.InstanceName = "agendaTenis";
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, JogadoresDbContext jogadoresDbContext)
    {
        if (env.IsDevelopment() || env.EnvironmentName == "Container")
        {
            Console.WriteLine("Swagger habilitado");

            // Em ambiente de desenvolvimento a aplicação já aplica as migrations para deixar o banco de dados pronto
            jogadoresDbContext.Database.Migrate();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
