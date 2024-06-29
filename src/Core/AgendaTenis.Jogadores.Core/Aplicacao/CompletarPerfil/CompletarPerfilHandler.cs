using AgendaTenis.Jogadores.Core.AcessoDados;
using AgendaTenis.Jogadores.Core.Dominio;
using AgendaTenis.Jogadores.Notificacoes;
using AgendaTenis.Jogadores.Notificacoes.Enums;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.Core.Aplicacao.CompletarPerfil;

public class CompletarPerfilHandler
{
    private readonly JogadoresDbContext _jogadoresDbContext;

    public CompletarPerfilHandler(JogadoresDbContext jogadoresDbContext)
    {
        _jogadoresDbContext = jogadoresDbContext;
    }

    public async Task<CompletarPerfilResponse> Handle(CompletarPerfilCommand request, CancellationToken cancellationToken)
    {
        var validacoes = await ValidarDados(request);
        if (validacoes.Any(c => c.Tipo is TipoNotificacaoEnum.Erro or TipoNotificacaoEnum.Aviso))
        {
            return new CompletarPerfilResponse()
            {
                Sucesso = false,
                Notificacoes = validacoes
            };
        }

        try
        {
            var jogador = MapearRequestParaEntity(request);
            var response = await _jogadoresDbContext.Jogador.AddAsync(jogador);
            await _jogadoresDbContext.SaveChangesAsync();
            return new CompletarPerfilResponse()
            {
                Sucesso = true
            };
        }
        catch (Exception)
        {
            return new CompletarPerfilResponse()
            {
                Sucesso = false,
                Notificacoes = new List<Notificacao>()
                {
                    new AgendaTenis.Jogadores.Notificacoes.Notificacao()
                    {
                        Mensagem = "Ocorreu um erro ao completar perfil do jogador",
                        Tipo = TipoNotificacaoEnum.Erro
                    }
                }
            };
        }
    }

    private async Task<List<Notificacao>> ValidarDados(CompletarPerfilCommand request)
    {
        // Seria itneressante criar outras validações de dados aqui, mas não deu tempo de planejar e implementar...

        var validacoes = new List<Notificacao>();

        var usuarioComPerfilCompleto = await _jogadoresDbContext.Jogador.AsNoTracking().AnyAsync(c => c.UsuarioId == request.UsuarioId);
        if (usuarioComPerfilCompleto)
        {
            validacoes.Add(new AgendaTenis.Jogadores.Notificacoes.Notificacao()
            {
                Mensagem = "Usuário já está com perfil completo",
                Tipo = TipoNotificacaoEnum.Aviso
            });
        }

        return validacoes;
    }

    private JogadorEntity MapearRequestParaEntity(CompletarPerfilCommand request)
    {
        return new JogadorEntity(
            request.UsuarioId,
            request.Nome,
            request.Sobrenome,
            request.DataNascimento,
            request.Telefone,
            request.Pais,
            request.Estado,
            request.Cidade,
            request.MaoDominante,
            request.Backhand,
            request.EstiloDeJogo);
    }
}
