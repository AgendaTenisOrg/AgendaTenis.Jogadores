using AgendaTenis.Jogadores.Core.AcessoDados.Mapeamentos;
using AgendaTenis.Jogadores.Core.Dominio;
using Microsoft.EntityFrameworkCore;

namespace AgendaTenis.Jogadores.Core.AcessoDados;

public class JogadoresDbContext : DbContext
{
    public JogadoresDbContext(DbContextOptions<JogadoresDbContext> options) : base(options)
    {
    }

    public DbSet<JogadorEntity> Jogador { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new JogadorMapping());

        base.OnModelCreating(modelBuilder);
    }
}
