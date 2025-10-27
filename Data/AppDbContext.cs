using ApiInmoZarate.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInmoZarate.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Propietario> Propietarios => Set<Propietario>();
    public DbSet<Inmueble> Inmuebles => Set<Inmueble>();
    public DbSet<Inquilino> Inquilinos => Set<Inquilino>();
    public DbSet<Contrato> Contratos => Set<Contrato>();
    public DbSet<Pago> Pagos => Set<Pago>();

    protected override void OnModelCreating(ModelBuilder m)
    {
        // tablas
        m.Entity<Propietario>().ToTable("propietario");
        m.Entity<Inmueble>().ToTable("inmueble");
        m.Entity<Inquilino>().ToTable("inquilino");
        m.Entity<Contrato>().ToTable("contrato");
        m.Entity<Pago>().ToTable("pago");

        // Propietario
        m.Entity<Propietario>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Dni).HasMaxLength(20);
            e.HasIndex(x => x.Dni).IsUnique();
            e.Property(x => x.Apellido).HasMaxLength(100);
            e.Property(x => x.Nombre).HasMaxLength(100);
            e.Property(x => x.Telefono).HasMaxLength(50);
            e.Property(x => x.Email).HasMaxLength(120);
            e.Property(x => x.DireccionContacto).HasMaxLength(200);
        });

        // Inmueble
        m.Entity<Inmueble>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Direccion).HasMaxLength(200).IsRequired();
            e.Property(x => x.Uso).HasMaxLength(50);
            e.Property(x => x.Tipo).HasMaxLength(50);
            e.Property(x => x.Estado).HasMaxLength(30);
            e.Property(x => x.Precio).HasPrecision(12, 2);

            e.HasIndex(x => x.PropietarioId).HasDatabaseName("idx_inmueble_propietario");

            e.HasOne(x => x.Propietario)
             .WithMany(p => p.Inmuebles)
             .HasForeignKey(x => x.PropietarioId)
             .OnDelete(DeleteBehavior.Restrict); // RESTRICT
        });

        // Inquilino
        m.Entity<Inquilino>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Dni).HasMaxLength(20);
            e.HasIndex(x => x.Dni).IsUnique();
            e.Property(x => x.NombreCompleto).HasMaxLength(150);
            e.Property(x => x.LugarTrabajo).HasMaxLength(150);
            e.Property(x => x.GaranteNombre).HasMaxLength(150);
            e.Property(x => x.GaranteDni).HasMaxLength(20);
            e.Property(x => x.Telefono).HasMaxLength(50);
            e.Property(x => x.Email).HasMaxLength(120);
        });

        // Contrato
        m.Entity<Contrato>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.MontoAlquiler).HasPrecision(12, 2);

            // índices
            e.HasIndex(x => x.InmuebleId).HasDatabaseName("idx_contrato_inmueble");
            e.HasIndex(x => x.InquilinoId).HasDatabaseName("idx_contrato_inquilino");
            e.HasIndex(x => new { x.FechaInicio, x.FechaFin }).HasDatabaseName("idx_contrato_fechas");

            // FKs
            e.HasOne(x => x.Inmueble)
             .WithMany(i => i.Contratos)
             .HasForeignKey(x => x.InmuebleId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Inquilino)
             .WithMany(i => i.Contratos)
             .HasForeignKey(x => x.InquilinoId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // Pago
        m.Entity<Pago>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).ValueGeneratedOnAdd();
            e.Property(x => x.Importe).HasPrecision(12, 2);

            e.HasIndex(x => x.ContratoId).HasDatabaseName("idx_pago_contrato");
            e.HasIndex(x => new { x.ContratoId, x.NumeroPago })
             .IsUnique()
             .HasDatabaseName("uk_pago_numero_por_contrato");

            e.HasOne(x => x.Contrato)
             .WithMany(c => c.Pagos)
             .HasForeignKey(x => x.ContratoId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
