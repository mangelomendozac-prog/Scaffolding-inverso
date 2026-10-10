using Microsoft.EntityFrameworkCore;
using Scaffolding_inverso.Models;
using Scaffolding_inverso.Data.Configurations;

namespace Scaffolding_inverso.Data;

public partial class HeroesContext : DbContext
{
    public HeroesContext(DbContextOptions<HeroesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Heroes> Heroes { get; set; }

    public virtual DbSet<SuperPoderes> SuperPoderes { get; set; }

    public DbSet<Ciudad> Ciudades => Set<Ciudad>();
    public DbSet<Villano> Villanos => Set<Villano>();
    public DbSet<Cuartel> Cuarteles => Set<Cuartel>();
    public DbSet<Vehiculo> Vehiculos => Set<Vehiculo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CiudadConfiguration());
        modelBuilder.ApplyConfiguration(new VillanoConfiguration());
        modelBuilder.ApplyConfiguration(new CuartelConfiguration());
        modelBuilder.ApplyConfiguration(new VehiculoConfiguration());
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
