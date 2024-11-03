using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.ViewModels.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;

using Serilog;
using AutoMapper;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;


namespace AppExpedienteDHR.Core.Services.Admin
{

    public class DerechoTipologiaService : IDerechoTipologiaService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;


        public DerechoTipologiaService(
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

        public async Task<bool> DeleteDerechoTipologia(int id)
        {
            try
            {
                // busca por el id 
                var derechoTipologia = await _containerWork.Derecho.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
                if (derechoTipologia != null)
                {
                    derechoTipologia.IsDeleted = true;
                    derechoTipologia.DeletedAt = DateTime.Now;
                    await _containerWork.Save();
                    // Limpiar caché para actualizar con la nueva inserción
                    _cache.Remove("DerechosCache");
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
                
                // Si la caché ya contiene los datos, los retorna
                if (_cache.TryGetValue("DerechosCache", out IEnumerable<DerechoViewModel> derechosViewModel))
                    return derechosViewModel;

                // Si no están en caché, los carga de la base de datos
                var derechos = await _containerWork.Derecho.GetAll(
                    includeProperties: "Eventos.Especificidades");

                derechosViewModel = _mapper.Map<IEnumerable<DerechoViewModel>>(derechos);

                // Guarda en caché
                _cache.Set("DerechosCache", derechosViewModel, _cacheOptions);
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
                var derechoTipologia = await _containerWork.Derecho.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
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
                var derechoTipologia = await _containerWork.Derecho.GetFirstOrDefault(x => x.Codigo == id && (x.IsDeleted == false || x.IsDeleted == null));
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
                await _containerWork.Derecho.Add(derecho);
                await _containerWork.Save();
                // Limpiar caché para actualizar con la nueva inserción
                _cache.Remove("DerechosCache");

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
                // Limpiar caché para actualizar con la nueva inserción
                _cache.Remove("DerechosCache");
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
