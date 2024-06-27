using MediatR;

namespace AgendaTenis.Jogadores.Core.Aplicacao.ObterResumoJogador;

public class ObterResumoJogadorCommand : IRequest<ObterResumoJogadorResponse>
{
    public int UsuarioId { get; set; }
}
