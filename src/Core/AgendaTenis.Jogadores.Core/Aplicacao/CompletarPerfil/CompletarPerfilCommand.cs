using System.Text.Json.Serialization;

namespace AgendaTenis.Jogadores.Core.Aplicacao.CompletarPerfil;

public class CompletarPerfilCommand
{
    [JsonIgnore]
    public int UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; }
    public string Pais { get; set; }
    public string Estado { get; set; }
    public string Cidade { get; set; }
    public string MaoDominante { get; set; }
    public string Backhand { get; set; }
    public string EstiloDeJogo { get; set; }
}
