using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Regras;
using AgendaTenis.Jogadores.Infra.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace AgendaTenis.Jogadores.Core.Aplicacao.ObterResumoJogador;

public class ObterResumoJogadorHandler
{
    private readonly JogadoresDbContext _jogadoresDbContext;
    private readonly IDistributedCache _cache;

    public ObterResumoJogadorHandler(JogadoresDbContext jogadoresDbContext, IDistributedCache cache)
    {
        _jogadoresDbContext = jogadoresDbContext;
        _cache = cache;
    }

    public async Task<ObterResumoJogadorResponse> Handle(ObterResumoJogadorCommand request, CancellationToken cancellationToken)
    {
        string recordId = $"_jogadores_resumo_{request.UsuarioId}";
        var jogadorResumo = await _cache.GetRecordAsync<ObterResumoJogadorResponse>(recordId);

        if (jogadorResumo is null)
        {
            var jogador = await _jogadoresDbContext.Jogador
            .AsNoTracking()
            .Where(c => c.UsuarioId == request.UsuarioId)
            .Select(p => new ObterResumoQueryModel
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                NomeCompleto = $"{p.Nome} {p.Sobrenome}",
                DataNascimento = p.DataNascimento,
                Pontuacao = p.PontuacaoAtual
            }).FirstOrDefaultAsync();

            jogadorResumo = new ObterResumoJogadorResponse()
            {
                Id = jogador.Id,
                UsuarioId = jogador.UsuarioId,
                NomeCompleto = jogador.NomeCompleto,
                Idade = CalcularIdade(jogador.DataNascimento),
                Pontuacao = jogador.Pontuacao,
                Categoria = jogador.Pontuacao.ObterCategoria()
            };

            await _cache.SetRecordAsync(recordId, jogadorResumo, TimeSpan.FromMinutes(2));
        }


        return jogadorResumo;
    }

    private int CalcularIdade(DateTime dataNascimento)
    {
        int idade = DateTime.Now.Year - dataNascimento.Year;

        if (DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
        {
            idade--;
        }

        return idade;
    }

    // Criei esta classe apenas para auxiliar a fazer a query com o Entity Framework
    public class ObterResumoQueryModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public double Pontuacao { get; set; }
    }
}


