using AgendaTenis.Jogadores.Core.Enums;

namespace AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosCommand 
{
    public int? UsuarioId { get; set; }
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public CategoriaEnum? Categoria { get; set; }
}
