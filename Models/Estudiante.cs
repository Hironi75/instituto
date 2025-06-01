using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace pf_final_ds2.Models
{
    [Table("estudiante")]
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string CI { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Correo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Carrera { get; set; } = string.Empty;
    }
}
