using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class IngredienteConfiguration : IEntityTypeConfiguration<Ingrediente>
{
    public void Configure(EntityTypeBuilder<Ingrediente> builder)
    {
        builder.ToTable("Ingredientes");

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.Descripcion)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(p => p.Precio)
        .IsRequired()
        .HasColumnType("decimal");

        builder.Property(p => p.Stock)
        .IsRequired()
        .HasColumnType("int");

        builder.HasIndex(p => p.Nombre)
        .IsUnique();

    }
}
