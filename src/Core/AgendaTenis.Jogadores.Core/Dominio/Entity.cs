namespace AgendaTenis.Jogadores.Core.Dominio;

public class Entity
{
    public Entity()
    {
        DataCriacao = DateTime.UtcNow;
    }

    public int Id { get; private set; }
    public DateTime DataCriacao { get; private set; }
}
