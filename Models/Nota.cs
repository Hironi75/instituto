using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pf_final_ds2.Models
{
    [Table("nota")]
    public class Nota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InscripcionId { get; set; }

        [Required]
        public decimal Parcial1 { get; set; }

        [Required]
        public decimal Parcial2 { get; set; }

        [Required]
        public decimal ExamenFinal { get; set; }

        [Required]
        public decimal NotaFinal { get; set; }

        [ForeignKey("InscripcionId")]
        public Inscripcion? Inscripcion { get; set; }
    }
}
