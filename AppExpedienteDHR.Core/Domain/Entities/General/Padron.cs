
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace AppExpedienteDHR.Core.Domain.Entities.General
{

    [Table("Padron", Schema = "dbo")]  // Esquema por defecto "dbo"
    public class Padron
    {
        [Key]
        [MaxLength(20)]
        public string Cedula { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido1 { get; set; }

        [Required]
        [MaxLength(50)]
        public string Apellido2 { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [MaxLength(100)]
        public string ConocidoComo { get; set; }

        [Required]
        [MaxLength(1)]
        public string Sexo { get; set; }

        public DateTime? FechaNac { get; set; }

        [MaxLength(20)]
        public string EstadoCivil { get; set; }

        public DateTime? Updated { get; set; }
    }
}

