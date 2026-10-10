using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scaffolding_inverso.Models;

namespace Scaffolding_inverso.Data.Configurations;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("Ciudades");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Pais)
            .IsRequired()
            .HasMaxLength(60);

        builder.HasIndex(c => c.Nombre)
            .IsUnique()
            .HasDatabaseName("UQ_Ciudades_Nombre");
    }
}
