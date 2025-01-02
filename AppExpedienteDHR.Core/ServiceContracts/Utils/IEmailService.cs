using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.ServiceContracts.Utils
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = false, string cc = null, string bcc = null);
        Task SendEmailWithAttachmentAsync(string to, string subject, string body, byte[] attachment, string attachmentName, bool isHtml = false, string cc = null, string bcc = null);
        Task SendEmailFromTemplateAsync(string to, string subject, string htmlContent, string cc = null, string bcc = null);
        void ValidateEmailSettings();
    }
}
