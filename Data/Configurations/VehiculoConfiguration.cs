using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scaffolding_inverso.Models;

namespace Scaffolding_inverso.Data.Configurations;

public class VehiculoConfiguration : IEntityTypeConfiguration<Vehiculo>
{
    public void Configure(EntityTypeBuilder<Vehiculo> builder)
    {
        builder.ToTable("Vehiculos");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Placa)
            .IsRequired()
            .HasMaxLength(10)
            .HasColumnName("Nro_Placa");

        builder.HasIndex(v => v.Placa)
            .IsUnique()
            .HasDatabaseName("UQ_Vehiculos_Placa");

        builder.Property(v => v.Modelo)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(v => v.CostoMantenimiento)
            .HasPrecision(12, 2);

        builder.Property(v => v.FechaAlta)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(v => v.Cuartel)
            .WithMany(c => c.Vehiculos)
            .HasForeignKey(v => v.CuartelId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Vehiculos_Cuarteles");
    }
}
