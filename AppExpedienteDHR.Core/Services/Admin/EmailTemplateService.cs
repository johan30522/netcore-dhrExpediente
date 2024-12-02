using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using AppExpedienteDHR.Core.ViewModels.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.ServiceContracts.Admin;


namespace AppExpedienteDHR.Core.Services.Admin
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;


        public EmailTemplateService(
            IContainerWork containerWork,
            IMapper mapper,
            ILogger logger,
            IMemoryCache cache
        )
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) // Duración de caché de 60 minutos
            };
        }

        public async Task<bool> DeleteEmailTemplate(int id)
        {
            try
            {
                // busca por el id 
                var emailTemplate = await _containerWork.EmailTemplate.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
                if (emailTemplate != null)
                {
                    emailTemplate.IsDeleted = true;
                    emailTemplate.DeletedAt = DateTime.Now;
                    await _containerWork.Save();
                    // Limpiar caché para actualizar con la nueva inserción
                    _cache.Remove("EmailTemplateCache");
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar el email template");
                return false;
            }
        }

        public async Task<EmailTemplateViewModel> GetEmailTemplateById(int id)
        {
            try
            {
                var emailTemplate = await _containerWork.EmailTemplate.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
                return _mapper.Map<EmailTemplateViewModel>(emailTemplate);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el email template");
                return null;
            }
        }

        public async Task<IEnumerable<EmailTemplateViewModel>> GetEmailTemplates()
        {
            try
            {
                var emailTemplates = await _containerWork.EmailTemplate.GetAll(x => x.IsDeleted == false || x.IsDeleted == null);
                return _mapper.Map<IEnumerable<EmailTemplateViewModel>>(emailTemplates);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los email templates");
                return null;
            }
        }

        public async Task<IEnumerable<EmailTemplateViewModel>> GetAllEmailTemplates()
        {
            try
            {
                // Si la caché ya contiene los datos, los retorna
                if (_cache.TryGetValue("EmailTemplateCache", out IEnumerable<EmailTemplateViewModel> emailTemplatesViewModel))
                    return emailTemplatesViewModel;
                // Si no están en caché, los carga de la base de datos
                var emailTemplates = await _containerWork.EmailTemplate.GetAll();
                emailTemplatesViewModel = _mapper.Map<IEnumerable<EmailTemplateViewModel>>(emailTemplates);
                // Guarda en caché
                _cache.Set("EmailTemplateCache", emailTemplatesViewModel, _cacheOptions);
                return emailTemplatesViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los email templates");
                return null;
            }
        }

        public async Task<bool> UpdateEmailTemplate(EmailTemplateViewModel emailTemplateViewModel)
        {
            try
            {
                var emailTemplate = await _containerWork.EmailTemplate.GetFirstOrDefault(x => x.Id == emailTemplateViewModel.Id && (x.IsDeleted == false || x.IsDeleted == null));
                if (emailTemplate != null)
                {
                    EmailTemplate emailTemplateUpdate = _mapper.Map<EmailTemplate>(emailTemplateViewModel);
                    await _containerWork.EmailTemplate.Update(emailTemplateUpdate);
                    await _containerWork.Save();
                    _cache.Remove("EmailTemplateCache");
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el email template");
                return false;
            }
        }

        public async Task<EmailTemplateViewModel> InsertEmailTemplate(EmailTemplateViewModel emailTemplateViewModel)
        {
            try
            {
                var emailTemplate = _mapper.Map<EmailTemplate>(emailTemplateViewModel);
                await _containerWork.EmailTemplate.Add(emailTemplate);
                await _containerWork.Save();
                _cache.Remove("EmailTemplateCache");
                return _mapper.Map(emailTemplate, emailTemplateViewModel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al insertar el email template");
                return null;
            }
        }


    }
}

