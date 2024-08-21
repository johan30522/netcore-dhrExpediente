
using System.ComponentModel.DataAnnotations;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class GroupWf
    {
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int? Order { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        public ICollection<FlowGroupWf> FlowGroups { get; set; } = new List<FlowGroupWf>();
        public ICollection<GroupUserWf> GroupUsers { get; set; } = new List<GroupUserWf>();
        public ICollection<ActionGroupWf> ActionGroups { get; set; } = new List<ActionGroupWf>();
    }
}
