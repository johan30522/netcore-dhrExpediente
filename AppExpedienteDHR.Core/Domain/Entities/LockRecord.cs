using AppExpedienteDHR.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities
{
    [Table("LockRecords")]
    public class LockRecord
    {
        [Key]
        public int Id { get; set; }
        public int IdLocked { get; set; }  // Referencia al ID de la solicitud bloqueada
        [StringLength(100)]
        public string EntityType { get; set; }  // Tipo de entidad (por ejemplo, "SolicitudAprobacion", "SolicitudPrestamo")
        public bool IsLocked { get; set; }
        [StringLength(450)]
        public string LockedByUserId { get; set; }
        public DateTime LockedAt { get; set; }
        public TimeSpan LockDuration { get; set; }

        [ForeignKey("LockedByUserId")]
        public ApplicationUser LockedByUser { get; set; }

        // Agregar la propiedad RowVersion para concurrencia optimista
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public LockRecord()
        {
            LockedAt = DateTime.Now;
            LockDuration = TimeSpan.FromMinutes(5);
        }
    }
}
