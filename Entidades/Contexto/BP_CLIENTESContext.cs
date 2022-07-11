using Microsoft.EntityFrameworkCore;
using Modelo.Entidades;

namespace Modelo.Contexto
{
    public class BP_CLIENTESContext : DbContext
    {
        public BP_CLIENTESContext(DbContextOptions<BP_CLIENTESContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Cuenta> Cuenta { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<TipoCuentum> TipoCuenta { get; set; }
        public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CLIENTE_PERSONA");
            });

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasKey(e => e.CuentaId)
                    .HasName("PK_BP.CUENTA");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUENTA_CLIENTE");

                entity.HasOne(d => d.TipoCuenta)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.TipoCuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CUENTA_TIPO_CUENTA");
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasOne(d => d.Cuenta)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.CuentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIENTO_CUENTA");

                entity.HasOne(d => d.TipoMovimiento)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.TipoMovimientoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MOVIMIENTO_TIPO_MOVIMIENTO");
            });
        }
    }
}
