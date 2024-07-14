using AgendaTenis.Jogadores.Core.AcessoDados;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.Core.Aplicacao.VerificarPerfilCompleto;

public class VerificarPerfilCompletoHandler
{
    private readonly JogadoresDbContext _jogadoresDbContext;

    public VerificarPerfilCompletoHandler(JogadoresDbContext jogadoresDbContext)
    {
        _jogadoresDbContext = jogadoresDbContext;
    }

    public async Task<bool> Handle(VerificarPerfilCompletoCommand request, CancellationToken cancellationToken)
    {
        return await _jogadoresDbContext.Jogador.AsNoTracking().AnyAsync(c => c.UsuarioId == request.UsuarioId);
    }
}
