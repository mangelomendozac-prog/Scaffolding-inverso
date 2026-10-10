using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scaffolding_inverso.Models;

namespace Scaffolding_inverso.Data.Configurations;

public class CuartelConfiguration : IEntityTypeConfiguration<Cuartel>
{
    public void Configure(EntityTypeBuilder<Cuartel> builder)
    {
        builder.ToTable("Cuarteles", table =>
            table.HasCheckConstraint("CK_Cuarteles_Capacidad", "Capacidad > 0"));

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(80);

        builder.Property(c => c.Direccion)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(c => c.Nombre)
            .IsUnique()
            .HasDatabaseName("UQ_Cuarteles_Nombre");
    }
}
