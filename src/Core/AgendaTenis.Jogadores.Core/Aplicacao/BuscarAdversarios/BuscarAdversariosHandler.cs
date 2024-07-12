using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Regras;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosHandler
{
    private readonly JogadoresDbContext _jogadoresDbContext;

    public BuscarAdversariosHandler(JogadoresDbContext jogadoresDbContext)
    {
        _jogadoresDbContext = jogadoresDbContext;
    }

    public async Task<BuscarAdversariosResponse> Handle(BuscarAdversariosCommand request, CancellationToken cancellationToken)
    {
        var pontuacaoMinima = request.Categoria.GetValueOrDefault().ObterPontuacaoMinima();
        var pontuacaoMaxima = request.Categoria.GetValueOrDefault().ObterPontuacaoMaxima();

        var initialQuery = _jogadoresDbContext.Jogador
            .AsNoTracking()
            .Where(c => c.UsuarioId != request.UsuarioId
                        && (request.IdCidade == null || c.IdCidade == request.IdCidade)
                        && (request.Categoria == null || c.PontuacaoAtual >= pontuacaoMinima && c.PontuacaoAtual <= pontuacaoMaxima));

        var adversarios = await initialQuery
                                .Skip((request.pagina - 1) * request.itensPorPagina)
                                .Take(request.itensPorPagina)
                                .Select(p => new AdversarioQueryModel()
                                {
                                    Id = p.Id,
                                    UsuarioId = p.UsuarioId,
                                    NomeCompleto = $"{p.Nome} {p.Sobrenome}",
                                    Pontuacao = p.PontuacaoAtual
                                })
                                .ToListAsync();

        var total = await initialQuery.CountAsync();

        var response = new BuscarAdversariosResponse()
        {
            TotalDeItens = total,
            Adversarios = adversarios.Select(p => new BuscarAdversariosResponse.Adversario()
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                NomeCompleto = p.NomeCompleto,
                Pontuacao = p.Pontuacao,
                Categoria = new Enums.CategoriaEnumModel(p.Pontuacao.ObterCategoria())
            }).ToList()
        };

        return response;
    }

    // Criei esta classe apenas para auxiliar a fazer a query com o Entity Framework
    public class AdversarioQueryModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public double Pontuacao { get; set; }
    }
}
