using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;
using Microsoft.Extensions.Configuration;


namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IContainerWork _containerWork;
        private readonly IWorkflowService _workflowService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public ExpedienteService(IContainerWork containerWork, IMapper mapper, IWorkflowService workflowService, IConfiguration configuration)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _workflowService = workflowService;
            _configuration = configuration;
        }


        public async Task<ExpedienteViewModel> CreateExpediente(ExpedienteViewModel viewModel)
        {
            var expediente = _mapper.Map<Expediente>(viewModel);
            await _containerWork.Expediente.Add(expediente);
            await _containerWork.Save();


            //Obtiene el Flujo de trabajo de Expediente
            //TODO: El id del flujo de trabajo debe ser parametrizado
            var flow = await _containerWork.FlowWf.Get(_configuration.GetValue<int>("FlowAppCodes:Expediente"));
            if (flow == null)
            {
                throw new Exception("No se encontró el flujo de trabajo de Expediente");
            }
            //Crea el encabezado del flujo de trabajo
            var flowRequestHeader = await _workflowService.CreateFlowRequestHeader<Expediente>(expediente.Id, "Expediente", flow.Id);


            return _mapper.Map<ExpedienteViewModel>(expediente);
        }

        public async Task UpdateExpediente(ExpedienteViewModel viewModel)
        {
            var expediente = _mapper.Map<Expediente>(viewModel);
            await _containerWork.Expediente.Update(expediente);
            await _containerWork.Save();
        }

        public async Task<ExpedienteViewModel> GetExpediente(int id)
        {
            var expediente = await _containerWork.Expediente.Get(id);
            return _mapper.Map<ExpedienteViewModel>(expediente);
        }
    }
}
