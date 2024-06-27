using AgendaTenis.Jogadores.Notificacoes;

namespace AgendaTenis.Jogadores.Core.Aplicacao.CompletarPerfil;

public class CompletarPerfilResponse
{
    public bool Sucesso { get; set; }
    public List<Notificacao> Notificacoes { get; set; }
}
