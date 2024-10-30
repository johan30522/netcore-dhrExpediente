using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.ViewModels.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;

using Serilog;
using AutoMapper;
using System.Collections.Generic;


namespace AppExpedienteDHR.Core.Services.Admin
{

    public class DerechoTipologiaService : IDerechoTipologiaService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DerechoTipologiaService(
            IContainerWork containerWork,
            IMapper mapper,
            ILogger logger
        )
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> DeleteDerechoTipologia(int id)
        {
            try
            {
                // busca por el id 
                var derechoTipologia = await _containerWork.Derecho.GetFirstOrDefault(x => x.Id == id && x.IsDeleted == false || x.IsDeleted == null);
                if (derechoTipologia != null)
                {
                    derechoTipologia.IsDeleted = true;
                    derechoTipologia.DeletedAt = DateTime.Now;
                    await _containerWork.Save();
                    return true;
                }
                else { return false; }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar el derecho");
                return false;
            }
        }

        public async Task<IEnumerable<DerechoViewModel>> GetDerechoTipologia()
        {
            try
            {
                var derechoTipologia = await _containerWork.Derecho.GetAll(
                    x => x.IsDeleted == false || x.IsDeleted == null,
                    includeProperties: "Eventos.Especificidades"
                    );
                IEnumerable<DerechoViewModel> derechosViewModel = _mapper.Map<IEnumerable<DerechoViewModel>>(derechoTipologia);
                return derechosViewModel;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los derechos");
                return null;
            }
        }

        public async Task<DerechoViewModel> GetDerechoTipologiaById(int id)
        {
            try
            {
                var derechoTipologia = await _containerWork.Derecho.GetFirstOrDefault(x => x.Id == id && x.IsDeleted == false || x.IsDeleted == null);
                return _mapper.Map<DerechoViewModel>(derechoTipologia);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el derecho");
                return null;
            }
        }

        public async Task<DerechoViewModel> GetDerechoTipologiaByCode(string id)
        {
            try
            {
                var derechoTipologia = await _containerWork.Derecho.GetFirstOrDefault(x => x.Codigo == id && x.IsDeleted == false || x.IsDeleted == null);
                return _mapper.Map<DerechoViewModel>(derechoTipologia);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el derecho");
                return null;
            }
        }


        public async Task<DerechoViewModel> InsertDerechoTipologia(DerechoViewModel derechoTipologia)
        {
            try
            {
                var derecho = _mapper.Map<Derecho>(derechoTipologia);
                await _containerWork.Save();
                return _mapper.Map<DerechoViewModel>(derecho);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al insertar el derecho");
                return null;
            }
        }

        public async Task<DerechoViewModel> UpdateDerechoTipologia(DerechoViewModel derechoTipologia)
        {
            try
            {
                var derecho = _mapper.Map<Derecho>(derechoTipologia);
                await _containerWork.Derecho.Update(derecho);
                await _containerWork.Save();
                return _mapper.Map<DerechoViewModel>(derecho);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el derecho");
                return null;
            }
        }
    }
}
