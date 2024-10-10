using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Serilog;


namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IContainerWork _containerWork;
        private readonly IWorkflowService _workflowService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IDenuncianteService _denuncianteService;
        private readonly IPersonaAfectadaService _personaAfectadaService;


        public ExpedienteService(
            IContainerWork containerWork,
            IMapper mapper, 
            IWorkflowService workflowService,
            IConfiguration configuration, 
            ILogger logger,
            IDenuncianteService denuncianteService,
            IPersonaAfectadaService personaAfectadaService
            )
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _workflowService = workflowService;
            _configuration = configuration;
            _logger = logger;
            _denuncianteService = denuncianteService;
            _personaAfectadaService = personaAfectadaService;

        }


        public async Task<ExpedienteViewModel> CreateExpediente(ExpedienteViewModel viewModel)
        {
            try
            {
                Expediente expediente = _mapper.Map<Expediente>(viewModel);
                Denunciante denunciante;
                PersonaAfectada personaAfectada;


                if(viewModel.Denunciante != null)
                {
                   denunciante = await _denuncianteService.CreateOrUpdate(viewModel.Denunciante);
                expediente.Denunciante = null;
                expediente.DenuncianteId = denunciante.Id;
                }
                if (expediente.IncluyePersonaAfectada == true)
                {
                    if (viewModel.PersonaAfectada != null)
                    {
                        personaAfectada = await _personaAfectadaService.CreateOrUpdate(viewModel.PersonaAfectada);
                        expediente.PersonaAfectada = null;
                        expediente.PersonaAfectadaId = personaAfectada.Id;
                    }
                }
                else
                {
                    expediente.PersonaAfectadaId = null;
                    expediente.PersonaAfectada = null;
                }
                await _containerWork.Expediente.Add(expediente);
                await _containerWork.Save();
                //Obtiene el Flujo de trabajo de Expediente
                var flow = await _containerWork.FlowWf.Get(_configuration.GetValue<int>("FlowAppCodes:Expediente"));
                if (flow == null)
                {
                    throw new Exception("No se encontró el flujo de trabajo de Expediente");
                }
                //Crea el encabezado del flujo de trabajo
                var flowRequestHeader = await _workflowService.CreateFlowRequestHeader<Expediente>(expediente.Id, "Expediente", flow.Id);

                return _mapper.Map<ExpedienteViewModel>(expediente);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating Expediente");
                throw;
            }
        }

        public async Task UpdateExpediente(ExpedienteViewModel viewModel)
        {
            try
            { 
            Expediente expediente = _mapper.Map<Expediente>(viewModel);
            Denunciante denunciante;
            PersonaAfectada personaAfectada;

           // Crea o actualiza el denunciante
           if (viewModel.Denunciante != null)
            {
                denunciante = await _denuncianteService.CreateOrUpdate(viewModel.Denunciante);
                expediente.Denunciante = null;
                expediente.DenuncianteId = denunciante.Id;
            }

           // Si el expediente incluye persona afectada, crea o actualiza la persona afectada
           if (expediente.IncluyePersonaAfectada == true)
            {
                if (viewModel.PersonaAfectada != null)
                {
                    personaAfectada = await _personaAfectadaService.CreateOrUpdate(viewModel.PersonaAfectada);
                    expediente.PersonaAfectada = null;
                    expediente.PersonaAfectadaId = personaAfectada.Id;
                }
            } else
                    {
                expediente.PersonaAfectadaId = null;
                expediente.PersonaAfectada = null;
            }
            await _containerWork.Expediente.Update(expediente);
            await _containerWork.Save();
                }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating Expediente");
                throw;
            }
        }

        public async Task<ExpedienteViewModel> GetExpediente(int id)
        {
            var expediente = await _containerWork.Expediente.GetFirstOrDefault(e => e.Id == id, includeProperties: "Denunciante,PersonaAfectada");
            return _mapper.Map<ExpedienteViewModel>(expediente);
        }

        public async Task<(List<ExpedienteItemListViewModel> items, int totalItems)> GetExpedientesPaginadas(
            int pageIndex, int pageSize, string searchValue, string sortColumn, string sortDirection)
        {
            try
            {
                (IEnumerable<ExpedienteItemListViewModel> expedientes, int total) = await _containerWork.Expediente.GetExpedientesWithFlowPaginated(
                    pageIndex: pageIndex,
                    pageSize: pageSize,
                    searchValue: searchValue,
                    sortColumn: sortColumn,
                    sortDirection: sortDirection);

                //var expedientesListado = _mapper.Map<List<ExpedienteItemListViewModel>>(expedientes);

                return (expedientes.ToList(), total);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting GetDenunciasPaginadas");
                throw;
            }


        }




    }
}
