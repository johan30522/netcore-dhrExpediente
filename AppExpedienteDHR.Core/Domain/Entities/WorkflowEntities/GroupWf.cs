
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class GroupWf
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public int? Order { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public int FlowId { get; set; }

        [ForeignKey("FlowId")]
        public FlowWf Flow { get; set; }

        // Relación uno a muchos con ActionGroupWf
        public ICollection<ActionGroupWf> ActionGroups { get; set; } = new List<ActionGroupWf>();

        // Relación uno a muchos con GroupRuleWf
        public ICollection<GroupUserWf> GroupUsers { get; set; } = new List<GroupUserWf>();

    }
}
