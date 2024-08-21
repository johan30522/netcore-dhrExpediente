
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class ActionGroupWf
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int ActionId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Navigation properties
        [ForeignKey("GroupId")]
        public GroupWf Group { get; set; }
        [ForeignKey("ActionId")]
        public ActionWf? Action { get; set; }
    }
}
