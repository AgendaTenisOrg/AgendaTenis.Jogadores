using AgendaTenis.Jogadores.Core.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgendaTenis.Jogadores.Core.AcessoDados.Mapeamentos;

public class JogadorMapping : IEntityTypeConfiguration<JogadorEntity>
{
    public void Configure(EntityTypeBuilder<JogadorEntity> builder)
    {
        builder.ToTable("Jogador", "jogadores");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
             .IsRequired()
             .HasColumnName("Nome")
             .HasColumnType("varchar(100)");

        builder.Property(c => c.Sobrenome)
             .IsRequired()
             .HasColumnName("Sobrenome")
             .HasColumnType("varchar(100)");
    }
}
