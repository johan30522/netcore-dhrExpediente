using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories.Admin
{
    public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public EmailTemplateRepository(DbContext context) : base(context)
        {
            _context = context as ApplicationDbContext;
        }
        public async Task Update(EmailTemplate emailTemplate)
        {
            var emailTemplateToUpdate = await _context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == emailTemplate.Id);
            if (emailTemplateToUpdate != null)
            {
                emailTemplateToUpdate.BodyTemplate = emailTemplate.BodyTemplate;
                emailTemplateToUpdate.SubjectTemplate = emailTemplate.SubjectTemplate;
                await _context.SaveChangesAsync();
            }
        }

    }
}
