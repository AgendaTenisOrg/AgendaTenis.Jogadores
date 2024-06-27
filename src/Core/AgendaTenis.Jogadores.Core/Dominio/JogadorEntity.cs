using AgendaTenis.Jogadores.Core.Exceptions;

namespace AgendaTenis.Jogadores.Core.Dominio;

public class JogadorEntity : Entity
{
    public JogadorEntity(
        int usuarioId,
        string nome,
        string sobrenome,
        DateTime dataNascimento,
        string telefone,
        string pais,
        string estado,
        string cidade,
        string maoDominante,
        string backhand,
        string estiloDeJogo)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Sobrenome = sobrenome;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Pais = pais;
        Estado = estado;
        Cidade = cidade;
        MaoDominante = maoDominante;
        Backhand = backhand;
        EstiloDeJogo = estiloDeJogo;
    }

    private JogadorEntity() { }

    public int UsuarioId { get; private set; }
    public string Nome { get; private set; }
    public string Sobrenome { get; private set; }
    public DateTime DataNascimento { get; private set; }
    public string Telefone { get; private set; }
    public string Pais { get; private set; }
    public string Estado { get; private set; }
    public string Cidade { get; private set; }
    public string MaoDominante { get; private set; }
    public string Backhand { get; private set; }
    public string EstiloDeJogo { get; private set; }
    public double PontuacaoAtual { get; private set; }

    public void Vencer()
    {
        PontuacaoAtual += 10;

        if (PontuacaoAtual < 0)
            PontuacaoAtual = 0;
    }

    public void Perder()
    {
        PontuacaoAtual -= 10;

        if (PontuacaoAtual < 0)
            PontuacaoAtual = 0;
    }
}
