using Microsoft.EntityFrameworkCore;
using GYM.Core.Models;
using Models;

namespace GYM.Infraestructura.Data
{
    public class GYM_DBContext : DbContext
    {
        public GYM_DBContext(DbContextOptions<GYM_DBContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; } = default!;
        public DbSet<PersonaTelefono> PersonaTelefonos { get; set; } = default!;
        public DbSet<Telefono> Telefonos { get; set; } = default!;
        public DbSet<PersonaCorreo> PersonaCorreos { get; set; } = default!;
        public DbSet<Correo> Correos { get; set; } = default!;
        public DbSet<Cliente> Clientes { get; set; } = default!;
        public DbSet<Membresia> Membresias { get; set; } = default!;
        public DbSet<Asistencia> Asistencias { get; set; } = default!;
        public DbSet<Suscripcion> Suscripciones { get; set; } = default!;
        public DbSet<PersonaAsistencia> PersonaAsistencias { get; set; } = default!;
        public DbSet<Producto> Productos { get; set; } = default!;
        public DbSet<Inventario> Inventarios { get; set; } = default!;
        public DbSet<Categoria> Categorias { get; set; } = default!;
        public DbSet<Empleado> Empleados { get; set; } = default!;
        public DbSet<Turno> Turnos { get; set; } = default!;
        public DbSet<EmpleadoTurno> EmpleadoTurnos { get; set; } = default!;
        public DbSet<Salario> Salario { get; set; } = default!;
        public DbSet<Cargo> Cargo { get; set; } = default!;
        public DbSet<CargoSalario> CargoSalario { get; set; } = default!;
        public DbSet<EmpleadoCargo> EmpleadoCargo { get; set; } = default!;
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Auditoria> Auditorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Recorre todas las entidades y propiedades DateTime
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    // Si la propiedad es DateTime o DateTime?
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetColumnType("date"); // Se guarda como "date" en PostgreSQL
                    }
                }
            }

            modelBuilder.Entity<PersonaTelefono>().HasKey(pt => new { pt.id_persona, pt.id_telefono, pt.fecha_inicio });
            modelBuilder.Entity<PersonaCorreo>().HasKey(pe => new { pe.id_persona, pe.id_correo, pe.fecha_inicio });
            modelBuilder.Entity<PersonaAsistencia>().HasKey(pe => new { pe.id_persona, pe.id_asistencia });
            modelBuilder.Entity<Suscripcion>().HasKey(pe => new { pe.id_cliente, pe.id_membresia, pe.fecha_inicio });
            modelBuilder.Entity<Persona>().HasIndex(p => p.hashhuella).IsUnique();
            modelBuilder.Entity<DetalleVenta>().HasKey(d => new { d.id_venta, d.id_producto });
            modelBuilder.Entity<Stock>().ToTable("Stocks"); 
            modelBuilder.Entity<Stock>().HasKey(s => new { s.id_inventario, s.id_producto });
            modelBuilder.Entity<Stock>().Property(s => s.cantidad).HasColumnName("cantidad");
            modelBuilder.Entity<Stock>().HasOne(s => s.inventario).WithMany(i => i.Stocks).HasForeignKey(s => s.id_inventario);
            modelBuilder.Entity<Stock>().HasOne(s => s.producto).WithMany(p => p.Stocks).HasForeignKey(s => s.id_producto);
        }
    }
}