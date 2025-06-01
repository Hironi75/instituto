using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pf_final_ds2.Models
{
    [Table("asignacion")]
    public class Asignacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DocenteId { get; set; }

        [Required]
        public int MateriaId { get; set; }

        [ForeignKey("DocenteId")]
        public Usuario? Docente { get; set; }

        [ForeignKey("MateriaId")]
        public Materia? Materia { get; set; }
    }
}
