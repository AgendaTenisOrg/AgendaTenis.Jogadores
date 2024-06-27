using AgendaTenis.Jogadores.Core.Enums;
using MediatR;

namespace AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;

public class BuscarAdversariosCommand : IRequest<BuscarAdversariosResponse>
{
    public int? UsuarioId { get; set; }
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public CategoriaEnum? Categoria { get; set; }
}
