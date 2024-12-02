using AppExpedienteDHR.Core.Domain.Entities.Admin;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin
{
    public interface IEmailTemplateRepository : IRepository<EmailTemplate>
    {

            Task Update(EmailTemplate emailTemplate);

    }
}
