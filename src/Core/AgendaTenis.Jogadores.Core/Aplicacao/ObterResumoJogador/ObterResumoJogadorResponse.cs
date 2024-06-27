using AgendaTenis.Jogadores.Core.Enums;

namespace AgendaTenis.Jogadores.Core.Aplicacao.ObterResumoJogador;

public class ObterResumoJogadorResponse
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public string NomeCompleto { get; set; }
    public int Idade { get; set; }
    public double Pontuacao { get; set; }
    public CategoriaEnum Categoria { get; set; }
}
