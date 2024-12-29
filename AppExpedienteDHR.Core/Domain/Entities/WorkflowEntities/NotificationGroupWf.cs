

using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class NotificationGroupWf
    {
        public int GroupId { get; set; }
        public int NotificationId { get; set; }

        // Navigation properties
        [ForeignKey("GroupId")]
        public GroupWf Group { get; set; }
        [ForeignKey("NotificationId")]
        public StateNotificationWf StateNotification { get; set; }
    }
}
