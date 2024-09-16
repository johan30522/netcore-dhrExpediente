using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;


namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;

        public ExpedienteService(IContainerWork containerWork, IMapper mapper)
        {
            _containerWork = containerWork;
            _mapper = mapper;
        }

        public async Task<ExpedienteViewModel> CreateExpediente(ExpedienteViewModel viewModel)
        {
            var expediente = _mapper.Map<Expediente>(viewModel);
            await _containerWork.Expediente.Add(expediente);
            await _containerWork.Save();
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
