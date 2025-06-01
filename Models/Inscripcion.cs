using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pf_final_ds2.Models
{
    [Table("inscripcion")]
    public class Inscripcion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [Required]
        public int SemestreId { get; set; }

        [Required]
        public DateTime FechaInscripcion { get; set; }

        [ForeignKey("EstudianteId")]
        public Estudiante? Estudiante { get; set; }

        [ForeignKey("MateriaId")]
        public Materia? Materia { get; set; }

        [ForeignKey("SemestreId")]
        public Semestre? Semestre { get; set; }

        public ICollection<Asistencia>? Asistencias { get; set; }
    }
}
