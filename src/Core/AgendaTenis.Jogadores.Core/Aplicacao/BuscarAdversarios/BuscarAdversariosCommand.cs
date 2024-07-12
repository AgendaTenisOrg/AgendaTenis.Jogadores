using AgendaTenis.Jogadores.Core.Enums;

namespace AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosCommand 
{
    public int? UsuarioId { get; set; }
    public int? IdCidade { get; set; }
    public CategoriaEnum? Categoria { get; set; }
    public int pagina { get; set; }
    public int itensPorPagina { get; set; }
}
