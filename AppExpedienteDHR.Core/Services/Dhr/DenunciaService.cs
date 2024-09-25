using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Collections.Generic;

namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class DenunciaService : IDenunciaService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly IAdjuntoService _adjuntoService;
        private readonly ILogger _logger;

        public DenunciaService(IContainerWork containerWork, IMapper mapper, ILogger logger, IAdjuntoService adjuntoService)
        {
            _containerWork = containerWork;
            _adjuntoService = adjuntoService;

            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DenunciaViewModel> CreateDenuncia(DenunciaViewModel viewModel)
        {
            try
            {
                Denuncia denuncia = _mapper.Map<Denuncia>(viewModel);
                Denunciante denunciante = _mapper.Map<Denunciante>(viewModel.Denunciante);
                var personaAfectada = _mapper.Map<PersonaAfectada>(viewModel.PersonaAfectada);


                // verifica si el denunciante ya existe por la cedula
                var denuncianteExistente = await _containerWork.Denunciante.GetFirstOrDefault(x => x.NumeroIdentificacion == denunciante.NumeroIdentificacion);
                // si existe, se actualiza, sino se crea
                if (denuncianteExistente != null)
                {
                    denunciante.Id = denuncianteExistente.Id;
                    denunciante = await _containerWork.Denunciante.Update(denunciante);
                }
                else
                {
                    await _containerWork.Denunciante.Add(denunciante);
                    await _containerWork.Save();
                    //denunciante = await _containerWork.Denunciante.GetFirstOrDefault(x => x.NumeroIdentificacion == denunciante.NumeroIdentificacion);
                }
                //Quita el denuncante de la denuncia
                denuncia.Denunciante = null;
                denuncia.PersonaAfectada = null;

                // TODO: Add PersonaAfectada

                denuncia.DenuncianteId = denunciante.Id;
                await _containerWork.Denuncia.Add(denuncia);
                await _containerWork.Save();

                // Si la denuncia tiene archivos adjuntos, se guardan
                if (viewModel.Files != null && viewModel.Files.Length > 0)
                {
                    var idsAdjuntos = new List<int>();
                    foreach (var file in viewModel.Files)
                    {
                        var idAdjunto = await _adjuntoService.CrearAnexoAsync(file);
                        idsAdjuntos.Add(idAdjunto);
                    }
                    // Se asocian los archivos adjuntos a la denuncia
                    if (idsAdjuntos.Count > 0)
                    {
                        foreach (var idAdjunto in idsAdjuntos)
                        {
                            var denunciaAdjunto = new DenunciaAdjunto
                            {
                                DenunciaId = denuncia.Id,
                                AdjuntoId = idAdjunto
                            };
                            await _containerWork.DenunciaAdjunto.Add(denunciaAdjunto);
                        }
                        await _containerWork.Save();
                    }
                }
                return _mapper.Map<DenunciaViewModel>(denuncia);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting flow");
                throw;
            }
        }

        public async Task UpdateDenuncia(DenunciaViewModel viewModel)
        {
            //obtiene la denuncia para verificar que exista
            var denunciaExistente = await _containerWork.Denuncia.GetFirstOrDefault(x => x.Id == viewModel.Id);
            if (denunciaExistente == null)
            {
                throw new Exception("La denuncia no existe");
            }
            Denuncia denuncia = _mapper.Map<Denuncia>(viewModel);
            Denunciante denunciante = _mapper.Map<Denunciante>(viewModel.Denunciante);
            var personaAfectada = _mapper.Map<PersonaAfectada>(viewModel.PersonaAfectada);


            // verifica si el denunciante ya existe por la cedula
            var denuncianteExistente = await _containerWork.Denunciante.GetFirstOrDefault(x => x.NumeroIdentificacion == denunciante.NumeroIdentificacion);
            // si existe, se actualiza, sino se crea
            if (denuncianteExistente != null)
            {
                denunciante.Id = denuncianteExistente.Id;
                denunciante = await _containerWork.Denunciante.Update(denunciante);
            }
            else
            {
                await _containerWork.Denunciante.Add(denunciante);
                denunciante = await _containerWork.Denunciante.GetFirstOrDefault(x => x.NumeroIdentificacion == denunciante.NumeroIdentificacion);
            }

            //Actualiza Persona Afectada
            if (denunciaExistente.IncluyePersonaAfectada && viewModel.IncluyePersonaAfectada)
            // si ya tenia persona afectada y se incluye persona afectada
            {
                if (denunciaExistente.PersonaAfectadaId != viewModel.PersonaAfectadaId)
                {
                    if (denunciaExistente.PersonaAfectadaId != null)
                    {
                        personaAfectada.Id = denunciaExistente.PersonaAfectadaId.Value;
                        await _containerWork.PersonaAfectada.Update(personaAfectada);
                    }
                    else
                    {
                        personaAfectada = await _containerWork.PersonaAfectada.Add(personaAfectada);
                        await _containerWork.Save();
                    }
                }
            }
            else if (denunciaExistente.IncluyePersonaAfectada && !viewModel.IncluyePersonaAfectada)
            // si ya tenia persona afectada y ya no se incluye persona afectada
            {
                if (denunciaExistente.PersonaAfectadaId != null)
                {
                    denuncia.IncluyePersonaAfectada = false;
                    denuncia.PersonaAfectadaId = null;
                }
            }
            else if (!denunciaExistente.IncluyePersonaAfectada && viewModel.IncluyePersonaAfectada)
            // si no tenia persona afectada y ahora si se incluye persona afectada
            {
                // busca si la persona afectada si existe por la cedula
                var personaAfectadaExistente = await _containerWork.PersonaAfectada.GetFirstOrDefault(x => x.NumeroIdentificacion == personaAfectada.NumeroIdentificacion);
                // si existe, se actualiza, sino se crea
                if (personaAfectadaExistente != null)
                {
                    personaAfectada.Id = personaAfectadaExistente.Id;
                    await _containerWork.PersonaAfectada.Update(personaAfectada);
                }
                else
                {
                    personaAfectada = await _containerWork.PersonaAfectada.Add(personaAfectada);
                    await _containerWork.Save();
                }
            }


            denuncia.DenuncianteId = denunciante.Id;
            if (denuncia.IncluyePersonaAfectada && personaAfectada != null)
            {
                denuncia.PersonaAfectadaId = personaAfectada?.Id;
            } else
            {
                denuncia.PersonaAfectadaId = null;
            }
            await _containerWork.Denuncia.Update(denuncia);
            await _containerWork.Save();
        }

        public async Task DeleteDenuncia(int id)
        {
            var denuncia = await _containerWork.Denuncia.Get(id);
            if (denuncia != null)
            {
                await _containerWork.Denuncia.Remove(denuncia);
                await _containerWork.Save();
            }
        }

        public async Task<DenunciaViewModel> GetDenuncia(int id)
        {
            var denuncia = await _containerWork.Denuncia.GetFirstOrDefault(x => x.Id == id, includeProperties: "Denunciante,PersonaAfectada,DenunciaAdjuntos.Adjunto");
            return _mapper.Map<DenunciaViewModel>(denuncia);
        }

        public async Task AgregarAnexoDenuncia(int id, IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    // Crear el anexo usando el servicio
                    var idAnexo = await _adjuntoService.CrearAnexoAsync(file);

                    // Relacionar el anexo con la denuncia
                    var denunciaAdjunto = new DenunciaAdjunto
                    {
                        DenunciaId = id,
                        AdjuntoId = idAnexo
                    };
                    await _containerWork.DenunciaAdjunto.Add(denunciaAdjunto);
                    await _containerWork.Save();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error agregando anexo a la denuncia");
                throw;
            }
        }
        public async Task<(List<DenunciaListadoViewModel> items, int totalItems)> GetDenunciasPaginadas(
            int pageIndex, int pageSize, string searchValue, string sortColumn, string sortDirection)
        {

            (IEnumerable<Denuncia> denuncias, int total) = await _containerWork.Denuncia.GetAllPaginated(
                filter: null, // No se necesita un filtro inicial en este caso
                includeProperties: "Denunciante", // Incluir la propiedad relacionada "Denunciante"
                pageIndex: pageIndex,
                pageSize: pageSize,
                searchValue: searchValue,
                searchColumns: "Denunciante.Nombre,DetalleDenuncia,Petitoria", // Columnas donde se aplicará la búsqueda
                sortColumn: sortColumn,
                sortDirection: sortDirection);

            var denunciasListado = _mapper.Map<List<DenunciaListadoViewModel>>(denuncias);

            return (denunciasListado, total);


        }
    }
}
