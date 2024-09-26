
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("Denuncias", Schema = "dhr")]
    public class Denuncia:CommonProps
    {
        //[Key]
        //[Required]
        //public int Id { get; set; }
        [Required]
        public int DenuncianteId { get; set; }
        [StringLength(2000)]
        [Required]
        public string DetalleDenuncia { get; set; }
        [StringLength(1000)]
        [Required]
        public string Petitoria { get; set; }
        [Required]
        public DateTime FechaDenuncia { get; set; } = DateTime.Now;
        public int? PersonaAfectadaId { get; set; }
        [Required]
        public bool AceptaTerminos { get; set; }
        public bool IncluyePersonaAfectada { get; set; } = false;

        // Navegaciones
        [ForeignKey("DenuncianteId")]
        public Denunciante Denunciante { get; set; }
        [ForeignKey("PersonaAfectadaId")]
        public PersonaAfectada PersonaAfectada { get; set; }
        public ICollection<DenunciaAdjunto> DenunciaAdjuntos { get; set; }

        // Relación con Expediente
        public Expediente Expediente { get; set; }

    }
}
