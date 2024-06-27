using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Regras;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosHandler : IRequestHandler<BuscarAdversariosCommand, BuscarAdversariosResponse>
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

        var adversarios = await _jogadoresDbContext.Jogador
            .AsNoTracking()
            //.Where(c => c.UsuarioId != request.UsuarioId
            //            && c.Pais == request.Pais
            //            && c.Estado == request.Estado
            //            && c.Cidade == request.Cidade
            //            && (request.Categoria == null || c.PontuacaoAtual >= pontuacaoMinima && c.PontuacaoAtual <= pontuacaoMaxima))
            .Select(p => new AdversarioQueryModel()
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                NomeCompleto = $"{p.Nome} {p.Sobrenome}",
                Pontuacao = p.PontuacaoAtual
            }).ToListAsync();

        var response = new BuscarAdversariosResponse()
        {
            Adversarios = adversarios.Select(p => new BuscarAdversariosResponse.Adversario()
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                NomeCompleto = p.NomeCompleto,
                Pontuacao = p.Pontuacao,
                Categoria = p.Pontuacao.ObterCategoria()
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
