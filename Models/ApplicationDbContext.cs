using Microsoft.EntityFrameworkCore;

namespace pf_final_ds2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define your DbSet properties here. For example:
        // public DbSet<YourEntity> YourEntities { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Semestre> Semestres { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asistencia>()
                .HasOne(a => a.Inscripcion)
                .WithMany(i => i.Asistencias)
                .HasForeignKey(a => a.InscripcionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
