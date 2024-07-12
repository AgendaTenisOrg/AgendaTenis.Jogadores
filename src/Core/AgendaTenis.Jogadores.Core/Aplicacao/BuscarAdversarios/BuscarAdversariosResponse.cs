using AgendaTenis.Jogadores.Core.Enums;

namespace AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosResponse
{
    public List<Adversario> Adversarios { get; set; }
    public int TotalDeItens { get; set; }

    public class Adversario
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public double Pontuacao { get; set; }
        public CategoriaEnumModel Categoria { get; set; }
    }
}
