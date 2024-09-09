using AppExpedienteDHR.Core.Domain.Entities.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.Dhr
{
    [Table("Denunciante", Schema = "dhr")]
    public class Denunciante
    {
        [Key]
        public int Id { get; set; }
        public int TipoIdentificacionId { get; set; }
        [StringLength(50)]
        public string NumeroIdentificacion { get; set; }
        [MaxLength(100)]
        public string Nombre { get; set; }
        [MaxLength(100)]
        public string PrimerApellido { get; set; }
        [MaxLength(100)]
        public string? SegundoApellido { get; set; }
        public int SexoId { get; set; }
        public int EstadoCivilId { get; set; }
        public string PaisOrigenCodigo { get; set; }
        public int EscolaridadId { get; set; }
        [MaxLength(20)]
        public string TelefonoCelular { get; set; }
        [MaxLength(50)]
        public string CorreoElectronico { get; set; }
        public bool EsMenorEdad { get; set; }
        public bool TieneRequerimientoEspecial { get; set; }
        public int ProvinciaCodigo { get; set; }
        public int CantonCodigo { get; set; }
        public int DistritoCodigo { get; set; }
        public string? DireccionExacta { get; set; }

        // Navegaciones
        [ForeignKey("TipoIdentificacionId")]
        public TipoIdentificacion TipoIdentificacion { get; set; }
        [ForeignKey("SexoId")]
        public Sexo Sexo { get; set; }
        [ForeignKey("EstadoCivilId")]
        public EstadoCivil EstadoCivil { get; set; }
        [ForeignKey("PaisOrigenCodigo")]
        public Pais PaisOrigen { get; set; }
        [ForeignKey("EscolaridadId")]
        public Escolaridad Escolaridad { get; set; }
        [ForeignKey("ProvinciaCodigo")]
        public Provincia Provincia { get; set; }
        [ForeignKey("CantonCodigo")]
        public Canton Canton { get; set; }
        [ForeignKey("DistritoCodigo")]
        public Distrito Distrito { get; set; }

        // Relación con Denuncia
        public ICollection<Denuncia> Denuncias { get; set; }
    }
}
