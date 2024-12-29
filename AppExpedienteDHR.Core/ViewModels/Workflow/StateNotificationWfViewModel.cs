

using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.ViewModels.Admin;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppExpedienteDHR.Core.ViewModels.Workflow
{
    public class StateNotificationWfViewModel
    {

        public int? StateId { get; set; }
        public int EmailTemplateId { get; set; }
        public int? GroupId { get; set; }
        public string? To { get; set; } // Lista separada por comas
        public string? Cc { get; set; } // Lista separada por comas
        public string? Bcc { get; set; } // Lista separada por comas

        // Lista de IDs seleccionados (entrada del usuario)
        public List<int>? SelectedGroupIds { get; set; } = new List<int>();


        [ForeignKey("StateId")]
        public StateWfViewModel? State { get; set; }
        [ForeignKey("EmailTemplateId")]
        public EmailTemplateViewModel? EmailTemplate { get; set; }
        [ForeignKey("GroupId")]
        public IEnumerable<GroupWfViewModel>? Groups { get; set; }



    }
}
