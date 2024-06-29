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
    //[Authorize]
    public async Task<IActionResult> BuscarAdversarios([FromServices] BuscarAdversariosHandler handler, string pais, string estado, string cidade, CategoriaEnum categoria)
    {
        var request = new BuscarAdversariosCommand()
        {
            UsuarioId = int.Parse(User.Identity.Name),
            Pais = pais,
            Estado = estado,
            Cidade = cidade,
            Categoria = categoria
        };

        var response = await handler.Handle(request, new CancellationToken());
        return Ok(response);
    }

    [HttpGet("Resumo")]
    //[Authorize]
    public async Task<IActionResult> ObterResumoJogador([FromServices] ObterResumoJogadorHandler handler)
    {
        var request = new ObterResumoJogadorCommand() { UsuarioId = int.Parse(User.Identity.Name) };
        var response = await handler.Handle(request, new CancellationToken());
        return Ok(response);
    }
}
