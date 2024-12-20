﻿using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.ViewModels.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;

using Serilog;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;


namespace AppExpedienteDHR.Core.Services.Admin
{
    public class EspecificidadTipologiaService : IEspecificidadTipologiaService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;


        public EspecificidadTipologiaService(
            IContainerWork containerWork,
            IMapper mapper,
            IMemoryCache cache,
            ILogger logger
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

        public async Task<bool> DeleteEspecificidad(int id)
        {
            try
                {
                // busca por el id 
                var especificidad = await _containerWork.Especificidad.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
                if (especificidad != null) {
                    especificidad.IsDeleted = true;
                    especificidad.DeletedAt = DateTime.Now;
                    await _containerWork.Save();
                    _cache.Remove("DerechosCache");
                    return true;
                } else { return false; }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar la especificidad");
                return false;
            }
        }

        public async Task<EspecificidadViewModel> GetEspecificidadById(int id)
        {
            try
            {
                var especificidad = await _containerWork.Especificidad.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
                return _mapper.Map<EspecificidadViewModel>(especificidad);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la especificidad");
                return null;
            }
        }

        public async Task<EspecificidadViewModel> GetEspecificidadByCode(string code)
        {
            try
            {
                var especificidad = await _containerWork.Especificidad.GetFirstOrDefault(x => x.Codigo == code && (x.IsDeleted == false || x.IsDeleted == null));
                return _mapper.Map<EspecificidadViewModel>(especificidad);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la especificidad");
                return null;
            }
        }

        public async Task<IEnumerable<EspecificidadViewModel>> GetEspecificidades(int eventoId)
        {
            try
            {
                var especificidad = await _containerWork.Especificidad.GetAll(x => (x.IsDeleted == false || x.IsDeleted == null) && x.EventoId == eventoId);
                return _mapper.Map<IEnumerable<EspecificidadViewModel>>(especificidad);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las especificidades");
                return null;
            }
        }

        public async Task<EspecificidadViewModel> InsertEspecificidad(EspecificidadViewModel especificidad)
        {
            try
            {
                var especificidadEntity = _mapper.Map<Especificidad>(especificidad);
                especificidadEntity.IsDeleted = false;
                await _containerWork.Especificidad.Add(especificidadEntity);
                await _containerWork.Save();
                _cache.Remove("DerechosCache");
                return _mapper.Map<EspecificidadViewModel>(especificidadEntity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al insertar la especificidad");
                return null;
            }
        }

        public async Task<EspecificidadViewModel> UpdateEspecificidad(EspecificidadViewModel especificidad)
        {
            try
            {
                var especificidadEntity = _mapper.Map<Especificidad>(especificidad);
                await _containerWork.Especificidad.Update(especificidadEntity);
                await _containerWork.Save();
                _cache.Remove("DerechosCache");
                return _mapper.Map<EspecificidadViewModel>(especificidadEntity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar la especificidad");
                return null;
            }
        }
    }
}
