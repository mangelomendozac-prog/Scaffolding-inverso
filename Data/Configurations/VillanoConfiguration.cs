using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scaffolding_inverso.Models;

namespace Scaffolding_inverso.Data.Configurations;

public class VillanoConfiguration : IEntityTypeConfiguration<Villano>
{
    public void Configure(EntityTypeBuilder<Villano> builder)
    {
        builder.ToTable("Villanos", table =>
            table.HasCheckConstraint("CK_Villanos_NivelAmenaza", "Nivel_Amenaza BETWEEN 1 AND 5"));

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Alias)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.NivelAmenaza)
            .HasColumnName("Nivel_Amenaza");

        builder.Property(v => v.Recompensa)
            .HasPrecision(10, 2);

        builder.Property(v => v.FechaRegistro)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(v => v.Ciudad)
            .WithMany(c => c.Villanos)
            .HasForeignKey(v => v.CiudadId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Villanos_Ciudades");
    }
}
