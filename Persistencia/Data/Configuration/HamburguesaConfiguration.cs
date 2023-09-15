using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class HamburguesaConfiguration : IEntityTypeConfiguration<Hamburguesa>
{
    public void Configure(EntityTypeBuilder<Hamburguesa> builder)
    {
        builder.ToTable("Haburguesas");

        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.Precio)
        .IsRequired()
        .HasColumnType("decimal");

        //definimos la ralacion de las llaves foraneas
        builder.HasOne(p => p.Categoria)
        .WithMany(p => p.Hamburguesas)
        .HasForeignKey(p => p.Categoria_id)
        .IsRequired();

        builder.HasOne(p => p.Chef)
        .WithMany(p => p.Hamburguesas)
        .HasForeignKey(p => p.Chef_id)
        .IsRequired();

        //definimos la relacion a la tabla d¿intermedia de muchos a muchos 
        builder
            .HasMany(p => p.Ingredientes)
            .WithMany(p => p.Hamburguesas)
            .UsingEntity<Hamburguesa_ingredientes>(

                j => j
                    .HasOne(p => p.Ingrediente)
                    .WithMany(p => p.Hamburguesa_Ingredientes)
                    .HasForeignKey(p => p.Ingrediente_id),

                j => j
                    .HasOne(p => p.Hamburguesa)
                    .WithMany(p => p.Hamburguesa_Ingredientes)
                    .HasForeignKey(p => p.Hamburguesa_id),

                j => 
                    {
                        j.HasKey(p => new { p.Ingrediente_id, p.Hamburguesa_id});
                    }
            );
    }
}
