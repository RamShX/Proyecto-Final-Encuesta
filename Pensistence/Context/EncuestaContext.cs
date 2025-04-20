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
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<OpcionRespuesta> OpcionRespuestas { get; set; }
        public DbSet<RespuestaEncuesta> RespuestasEncuestas { get; set; }
        public DbSet<RespuestaPregunta> RespuestasPreguntas { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EncuestaContext).Assembly);

            //Configurar la relación entre Usuario y Rol
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Rol)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);

            //Configurar la relación entre Encuesta y Usuario
            modelBuilder.Entity<Encuesta>()
                .HasOne(e => e.Creador)
                .WithMany(u => u.Encuestas)
                .HasForeignKey(e => e.UsuarioId);

            //Relacion de Pregunta
            modelBuilder.Entity<Pregunta>()
                .HasOne(p => p.Encuesta)
                .WithMany(e => e.Preguntas)
                .HasForeignKey(p => p.EncuestaId);

            //Relacion de OpcionRespuesta
            modelBuilder.Entity<OpcionRespuesta>()
                .HasOne(o => o.Pregunta)
                .WithMany(p => p.OpcionesRespuesta)
                .HasForeignKey(o => o.PreguntaId);

            //relacion de RespuestaEncuesta
            modelBuilder.Entity<RespuestaEncuesta>()
                .HasOne(r => r.encuesta)
                .WithMany()
                .HasForeignKey(r => r.EncuestaId);

            modelBuilder.Entity<RespuestaEncuesta>()
                .HasOne(r => r.usuario)
                .WithMany()
                .HasForeignKey(r => r.usuarioId);

            //Relacion de RespuestaPregunta
            modelBuilder.Entity<RespuestaPregunta>()
                .HasOne(r => r.RespuestaEncuesta)
                .WithMany()
                .HasForeignKey(r => r.RespuestaEncuestaId);

            modelBuilder.Entity<RespuestaPregunta>()
                .HasOne(r => r.Pregunta)
                .WithMany()
                .HasForeignKey(r => r.PreguntaId);

            modelBuilder.Entity<RespuestaPregunta>()
                .HasOne(r => r.Opcion)
                .WithMany()
                .HasForeignKey(r => r.OpcionId);

            //Relacion de Notificacion
            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Usuario)
                .WithMany()
                .HasForeignKey(n => n.usuarioId);

            modelBuilder.Entity<Notificacion>()
                .HasOne(n => n.Encuesta)
                .WithMany()
                .HasForeignKey(n => n.EncuestaId);


        }
    }
    
}

