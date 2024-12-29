

using System.ComponentModel.DataAnnotations.Schema;
using AppExpedienteDHR.Core.Domain.Entities.Admin;

namespace AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities
{
    public class StateNotificationWf : CommonProps
    {
        public int StateId { get; set; }
        public int EmailTemplateId { get; set; }
        public string? To { get; set; } // Lista separada por comas
        public string? Cc { get; set; } // Lista separada por comas
        public string? Bcc { get; set; } // Lista separada por comas


        [ForeignKey("StateId")]
        public StateWf? State { get; set; }
        [ForeignKey("EmailTemplateId")]
        public EmailTemplate? EmailTemplate { get; set; }

       public ICollection<NotificationGroupWf> NotificationGroups { get; set; }



    }
}
