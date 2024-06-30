using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Aplicacao.AtualizarPontuacao;
using AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;
using AgendaTenis.Jogadores.Core.Aplicacao.CompletarPerfil;
using AgendaTenis.Jogadores.Core.Aplicacao.ObterResumoJogador;
using AgendaTenis.Jogadores.Core.Eventos.Consumidores;
using AgendaTenis.Jogadores.WebApi.ConfiguracaoDeServicos;
using AgendaTenis.Jogadores.WebApi.Workers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegistrarMessageBus(builder.Configuration);

builder.Services.AdicionarConfiguracaoSwagger();
builder.Services.AdicionarAutenticacaoJWT(builder.Configuration);

builder.Services.AddDbContext<JogadoresDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Jogadores"),
        b => b.MigrationsAssembly("AgendaTenis.Jogadores.WebApi")));

builder.Services.AddScoped<CompletarPerfilHandler>();
builder.Services.AddScoped<BuscarAdversariosHandler>();
builder.Services.AddScoped<ObterResumoJogadorHandler>();
builder.Services.AddScoped<AtualizarPontuacaoHandler>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "agendaTenis";
});

builder.Services.AddHostedService<PlacarConfirmadoWorker>();
builder.Services.AddScoped<PlacarConfirmadoConsumidor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();