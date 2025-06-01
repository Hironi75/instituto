using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pf_final_ds2.Models
{
    [Table("materia")]
    public class Materia
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string Codigo { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public int Nivel { get; set; }

        [Required]
        public int CargaHoraria { get; set; }
    }
}
