using AgendaTenis.Jogadores.Infra.Eventos.Base;

namespace AgendaTenis.Jogadores.Infra.Eventos.Mensagens;

public class PlacarConfirmadoMensagem : IEventMessage
{
    public string Id { get; set; }
    public string DesafianteId { get; set; }
    public string AdversarioId { get; set; }
    public DateTime DataDaPartida { get; set; }
    public int ModeloDaPartida { get; set; }
    public string? VencedorId { get; set; }
}
