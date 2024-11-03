using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.ViewModels.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;

using Serilog;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;


namespace AppExpedienteDHR.Core.Services.Admin
{
    public class EventoTipologiaService : IEventoTipologiaService
    {

        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public EventoTipologiaService(
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

        public async Task<bool> DeleteEvento(int id)
        {
            try
                {
                // busca por el id 
                var evento = await _containerWork.Evento.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
                if (evento != null) {
                    evento.IsDeleted = true;
                    evento.DeletedAt = DateTime.Now;
                    await _containerWork.Save();
                    _cache.Remove("DerechosCache");
                    return true;
                } else { return false; }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar el evento");
                return false;
            }
        }

        public async Task<EventoViewModel> GetEventoById(int id)
        {
            try
            {
                var evento = await _containerWork.Evento.GetFirstOrDefault(x => x.Id == id && (x.IsDeleted == false || x.IsDeleted == null));
                return _mapper.Map<EventoViewModel>(evento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el evento");
                return null;
            }
        }

        public async Task<EventoViewModel> GetEventoByCode(string code)
        {
            try
            {
                var evento = await _containerWork.Evento.GetFirstOrDefault(x => x.Codigo == code && (x.IsDeleted == false || x.IsDeleted == null));
                return _mapper.Map<EventoViewModel>(evento);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el evento");
                return null;
            }
        }

        public async Task<IEnumerable<EventoViewModel>> GetEventos(int derechoId)
        {
            try
            {
                var eventos = await _containerWork.Evento.GetAll(x => (x.IsDeleted == false || x.IsDeleted == null) && x.DerechoId == derechoId);
                return _mapper.Map<IEnumerable<EventoViewModel>>(eventos);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener los eventos");
                return null;
            }
        }

        public async Task<EventoViewModel> InsertEvento(EventoViewModel evento)
        {
            try
            {
                var eventoEntity = _mapper.Map<Evento>(evento);
                eventoEntity.IsDeleted = false;

                await _containerWork.Evento.Add(eventoEntity);
                await _containerWork.Save();
                _cache.Remove("DerechosCache");
                return _mapper.Map<EventoViewModel>(eventoEntity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al insertar el evento");
                return null;
            }
        }

        public async Task<EventoViewModel> UpdateEvento(EventoViewModel evento)
        {
            try
            {
                var eventoEntity = await _containerWork.Evento.GetFirstOrDefault(x => x.Id == evento.Id && (x.IsDeleted == false || x.IsDeleted == null));
                if (eventoEntity != null)
                {
                    Evento eventoToUpdate = _mapper.Map<Evento>(evento);

                    await _containerWork.Evento.Update(eventoToUpdate);
                    await _containerWork.Save();
                    _cache.Remove("DerechosCache");

                    return _mapper.Map<EventoViewModel>(eventoEntity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el evento");
                return null;
            }
        }
    }
}
