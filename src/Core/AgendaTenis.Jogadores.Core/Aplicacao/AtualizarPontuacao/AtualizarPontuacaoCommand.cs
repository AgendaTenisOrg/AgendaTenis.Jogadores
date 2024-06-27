using MediatR;

namespace AgendaTenis.Jogadores.Core.Aplicacao.AtualizarPontuacao;

public class AtualizarPontuacaoCommand : IRequest
{
    public int VencedorId { get; set; }
    public int PerdedorId { get; set; }
}
