using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;
using Serilog;


namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class DenuncianteService : IDenuncianteService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DenuncianteService(IContainerWork containerWork, IMapper mapper, ILogger logger)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DenuncianteViewModel> CreateDenunciante(DenuncianteViewModel viewModel)
        {
            var denunciante = _mapper.Map<Denunciante>(viewModel);
            await _containerWork.Denunciante.Add(denunciante);
            await _containerWork.Save();
            return _mapper.Map<DenuncianteViewModel>(denunciante);
        }

        public async Task<Denunciante> CreateOrUpdate(DenuncianteViewModel viewModel)
        {
            try
            {
                var denunciante = _mapper.Map<Denunciante>(viewModel);
                var denuncianteExistente = await _containerWork.Denunciante.GetFirstOrDefault(d => d.NumeroIdentificacion == denunciante.NumeroIdentificacion);
                if (denuncianteExistente != null)
                {
                    denunciante.Id = denuncianteExistente.Id;
                    denunciante = await _containerWork.Denunciante.Update(denunciante);
                }
                else
                {
                    await _containerWork.Denunciante.Add(denunciante);
                    await _containerWork.Save();
                }
                return denunciante;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear o actualizar denunciante");
                throw;
            }
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
