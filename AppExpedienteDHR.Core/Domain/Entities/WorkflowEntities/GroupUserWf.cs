using AppExpedienteDHR.Core.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class GroupUserWf:CommonProps
    {
        [Required]
        public string UserId { get; set; } // Assuming Identity uses string for UserId
        [Required]
        public int GroupId { get; set; }
 

        // Navigation properties
        [ForeignKey("GroupId")]
        public GroupWf? Group { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; } // Reference to Identity User
    }
}
