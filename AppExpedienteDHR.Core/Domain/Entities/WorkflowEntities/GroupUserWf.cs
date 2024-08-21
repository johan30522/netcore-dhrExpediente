using AppExpedienteDHR.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class GroupUserWf
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Assuming Identity uses string for UserId
        public int GroupId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("GroupId")]
        public GroupWf? Group { get; set; }
        public ApplicationUser? User { get; set; } // Reference to Identity User
    }
}
