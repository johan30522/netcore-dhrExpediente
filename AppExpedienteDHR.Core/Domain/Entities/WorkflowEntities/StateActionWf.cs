
using System.ComponentModel.DataAnnotations.Schema;


namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class StateActionWf
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public int ActionId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("ActionId")]
        public StateWf? State { get; set; }
        [ForeignKey("StateId")]
        public ActionWf? Action { get; set; }
    }
}
