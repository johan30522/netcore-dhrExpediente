using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;
using Serilog;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;

namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class PersonaAfectadaService: IPersonaAfectadaService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public PersonaAfectadaService(IContainerWork containerWork, IMapper mapper, ILogger logger)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PersonaAfectada> CreateOrUpdate(PersonaAfectadaViewModel viewModel)
        {
            var personaAfectada = _mapper.Map<PersonaAfectada>(viewModel);
            var personaAfectadaExistente = await _containerWork.PersonaAfectada.GetFirstOrDefault(d => d.NumeroIdentificacion == personaAfectada.NumeroIdentificacion);
            if (personaAfectadaExistente != null)
            {
                personaAfectada.Id = personaAfectadaExistente.Id;
                await _containerWork.PersonaAfectada.Update(personaAfectada);
            }
            else
            {
                await _containerWork.PersonaAfectada.Add(personaAfectada);
                await _containerWork.Save();
            }
            return personaAfectada;
            
        }

    }
}
