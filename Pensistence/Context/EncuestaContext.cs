using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Pensistence.Context
{
    public class EncuestaContext : DbContext
    {
        public EncuestaContext(DbContextOptions<EncuestaContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EncuestaContext).Assembly);

            //Configurar la relación entre Usuario y Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);
        }
    }
    
}

