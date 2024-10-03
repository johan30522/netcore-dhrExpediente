
using System.ComponentModel.DataAnnotations;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class FlowWf:CommonProps
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Order { get; set; }

        // Relación uno a muchos con StateWf
        public ICollection<StateWf> States { get; set; } = new List<StateWf>();

        // Relación uno a muchos con GroupWf
        public ICollection<GroupWf> Groups { get; set; } = new List<GroupWf>();

        public ICollection<FlowRequestHeaderWf> RequestFlowHeaders { get; set; } = new List<FlowRequestHeaderWf>();

    }
}
