using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pf_final_ds2.Models
{
    [Table("asistencia")]
    public class Asistencia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InscripcionId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public bool Asistio { get; set; }

        [ForeignKey("InscripcionId")]
        public Inscripcion? Inscripcion { get; set; }
    }
}
