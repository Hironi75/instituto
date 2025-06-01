using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pf_final_ds2.Models
{
    [Table("certificado")]
    public class Certificado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EstudianteId { get; set; }

        [Required]
        public int SemestreId { get; set; }

        [Required]
        public DateTime FechaGeneracion { get; set; }

        [Required]
        public string Documento { get; set; } = string.Empty;

        [ForeignKey("EstudianteId")]
        public Estudiante? Estudiante { get; set; }

        [ForeignKey("SemestreId")]
        public Semestre? Semestre { get; set; }
    }
}
