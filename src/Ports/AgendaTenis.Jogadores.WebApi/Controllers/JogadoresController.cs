using AgendaTenis.Jogadores.Core.Aplicacao.BuscarAdversarios;
using AgendaTenis.Jogadores.Core.Aplicacao.CompletarPerfil;
using AgendaTenis.Jogadores.Core.Aplicacao.ObterResumoJogador;
using AgendaTenis.Jogadores.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AgendaTenis.Jogadores.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JogadoresController : ControllerBase
{
    [HttpPost("Perfil/Completar")]
    [Authorize]
    public async Task<IActionResult> CompletarPerfil([FromServices] CompletarPerfilHandler handler, [FromBody] CompletarPerfilCommand request)
    {
        request.UsuarioId = int.Parse(User.Identity.Name);
        var response = await handler.Handle(request, new CancellationToken());
        return Ok(response);
    }

    [HttpGet("Adversarios/Buscar")]
    [Authorize]
    public async Task<IActionResult> BuscarAdversarios([FromServices] BuscarAdversariosHandler handler, int? idCidade, CategoriaEnum? categoria, int pagina = 1, int itensPorPagina = 10)
    {
        var request = new BuscarAdversariosCommand()
        {
            UsuarioId = int.Parse(User.Identity.Name),
            IdCidade = idCidade,
            Categoria = categoria,
            pagina = pagina,
            itensPorPagina = itensPorPagina
        };

        var response = await handler.Handle(request, new CancellationToken());
        return Ok(response);
    }

    [HttpGet("Resumo")]
    [Authorize]
    public async Task<IActionResult> ObterResumoJogador([FromServices] ObterResumoJogadorHandler handler)
    {
        var request = new ObterResumoJogadorCommand() { UsuarioId = int.Parse(User.Identity.Name) };
        var response = await handler.Handle(request, new CancellationToken());
        return Ok(response);
    }
}
