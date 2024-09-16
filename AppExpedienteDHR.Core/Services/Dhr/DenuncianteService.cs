using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;


namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class DenuncianteService : IDenuncianteService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;

        public DenuncianteService(IContainerWork containerWork, IMapper mapper)
        {
            _containerWork = containerWork;
            _mapper = mapper;
        }

        public async Task<DenuncianteViewModel> CreateDenunciante(DenuncianteViewModel viewModel)
        {
            var denunciante = _mapper.Map<Denunciante>(viewModel);
            await _containerWork.Denunciante.Add(denunciante);
            await _containerWork.Save();
            return _mapper.Map<DenuncianteViewModel>(denunciante);
        }

        public async Task UpdateDenunciante(DenuncianteViewModel viewModel)
        {
            var denunciante = _mapper.Map<Denunciante>(viewModel);
            await _containerWork.Denunciante.Update(denunciante);
            await _containerWork.Save();
        }

        public async Task<DenuncianteViewModel> GetDenunciante(int id)
        {
            var denunciante = await _containerWork.Denunciante.Get(id);
            return _mapper.Map<DenuncianteViewModel>(denunciante);
        }
    }
}
