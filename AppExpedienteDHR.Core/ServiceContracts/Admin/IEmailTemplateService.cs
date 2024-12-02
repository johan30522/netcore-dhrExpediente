using AppExpedienteDHR.Core.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts.Admin
{
    public interface IEmailTemplateService
    {
        Task<bool> DeleteEmailTemplate(int id);
        Task<EmailTemplateViewModel> GetEmailTemplateById(int id);
        Task<IEnumerable<EmailTemplateViewModel>> GetEmailTemplates();
        Task<IEnumerable<EmailTemplateViewModel>> GetAllEmailTemplates();
        Task<bool> UpdateEmailTemplate(EmailTemplateViewModel emailTemplateViewModel);
        Task<EmailTemplateViewModel> InsertEmailTemplate(EmailTemplateViewModel emailTemplateViewModel);

    }
}
