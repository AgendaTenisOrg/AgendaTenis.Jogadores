using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.Core.Aplicacao.AtualizarPontuacao;

public class AtualizarPontuacaoHandler
{
    private readonly JogadoresDbContext _jogadoresDbContext;

    public AtualizarPontuacaoHandler(JogadoresDbContext jogadoresDbContext)
    {
        _jogadoresDbContext = jogadoresDbContext;
    }

    public async Task Handle(AtualizarPontuacaoCommand request, CancellationToken cancellationToken)
    {
        using (var transaction = await _jogadoresDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                // Estamos indo duas vezes ao banco. Talvez fosse mais interessante realizar uma única ida ao banco e já obter o vencedor e perdedor
                var vencedor = await _jogadoresDbContext.Jogador.FirstOrDefaultAsync(c => c.UsuarioId == request.VencedorId);
                if (vencedor is null)
                    throw new JogadorNaoEncontradoException($"Vencedor com id {request.VencedorId} não foi encontrado");

                var perdedor = await _jogadoresDbContext.Jogador.FirstOrDefaultAsync(c => c.UsuarioId == request.PerdedorId);
                if (perdedor is null)
                    throw new JogadorNaoEncontradoException($"Perdedor com id {request.PerdedorId} não foi encontrado");

                vencedor.Vencer();
                perdedor.Perder();

                await _jogadoresDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (JogadorNaoEncontradoException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
