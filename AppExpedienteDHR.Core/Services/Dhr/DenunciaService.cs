using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IWorkflowService _workflowService;
        private readonly IConfiguration _configuration;
        private readonly IDenuncianteService _denuncianteService;
        private readonly IPersonaAfectadaService _personaAfectadaService;

        public DenunciaService(
            IContainerWork containerWork,
            IMapper mapper,
            ILogger logger,
            IAdjuntoService adjuntoService,
            IWorkflowService workflowService,
            IConfiguration configuration,
            IDenuncianteService denuncianteService,
            IPersonaAfectadaService personaAfectadaService
            )
        {
            _containerWork = containerWork;
            _adjuntoService = adjuntoService;
            _workflowService = workflowService;
            _configuration = configuration;
            _denuncianteService = denuncianteService;
            _personaAfectadaService = personaAfectadaService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DenunciaViewModel> CreateDenuncia(DenunciaViewModel viewModel)
        {
            try
            {
                Denuncia denuncia = _mapper.Map<Denuncia>(viewModel);
                Denunciante denunciante;
                PersonaAfectada personaAfectada;


                //Crear o actualizar denunciante
                if (viewModel.Denunciante != null)
                {
                    denunciante = await _denuncianteService.CreateOrUpdate(viewModel.Denunciante);
                    denuncia.Denunciante = null;
                    denuncia.DenuncianteId = denunciante.Id;
                }

                // Si la denuncia incluye persona afectada, se crea o actualiza
                if (denuncia.IncluyePersonaAfectada && viewModel.PersonaAfectada != null)
                {
                    personaAfectada = await _personaAfectadaService.CreateOrUpdate(viewModel.PersonaAfectada);
                    denuncia.PersonaAfectada = null;
                    denuncia.PersonaAfectadaId = personaAfectada.Id;
                }else 
                {
                    denuncia.IncluyePersonaAfectada = false;
                    denuncia.PersonaAfectadaId = null;
                    denuncia.PersonaAfectada = null;
                }
     
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
            Denunciante denunciante;
            PersonaAfectada personaAfectada;
            
            //Crea o actualiza el denunciante
            if (viewModel.Denunciante != null)
            {
                denunciante = await _denuncianteService.CreateOrUpdate(viewModel.Denunciante);
                denuncia.Denunciante = null;
                denuncia.DenuncianteId = denunciante.Id;
            }
            //Si la denuncia incluye persona afectada, se crea o actualiza
            if (denuncia.IncluyePersonaAfectada && viewModel.PersonaAfectada != null)
            {
                personaAfectada = await _personaAfectadaService.CreateOrUpdate(viewModel.PersonaAfectada);
                denuncia.PersonaAfectada = null;
                denuncia.PersonaAfectadaId = personaAfectada.Id;
            } else 
            {
                denuncia.IncluyePersonaAfectada = false;
                denuncia.PersonaAfectadaId = null;
                denuncia.PersonaAfectada = null;
            }

            if (denunciaExistente.IncluyePersonaAfectada && !viewModel.IncluyePersonaAfectada)
            // si ya tenia persona afectada y ya no se incluye persona afectada
            {
                if (denunciaExistente.PersonaAfectadaId != null)
                {
                    denuncia.IncluyePersonaAfectada = false;
                    denuncia.PersonaAfectadaId = null;
                    denuncia.PersonaAfectada = null;
                }
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
            var denunciaViewModel = _mapper.Map<DenunciaViewModel>(denuncia);
            // verifica si existe un expediente asociado a la denuncia
            var expediente = await _containerWork.Expediente.GetFirstOrDefault(x => x.DenunciaId == id);
           
            if (expediente != null)
            {
                denunciaViewModel.ExpedienteId = expediente.Id;
            }
            return denunciaViewModel;
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

        public async Task<bool> CreateExpediente(int idDenuncia)
        {
            try
            {
                var denuncia = await _containerWork.Denuncia.GetFirstOrDefault(x => x.Id == idDenuncia, includeProperties: "Denunciante,PersonaAfectada,DenunciaAdjuntos.Adjunto");
                if (denuncia == null)
                {
                    throw new Exception("La denuncia no existe");
                    return false;
                }

                // Crear el expediente
                var expediente = new Expediente
                {
                    DenunciaId = idDenuncia,
                    DenuncianteId = denuncia.DenuncianteId,
                    PersonaAfectadaId = denuncia.PersonaAfectadaId,
                    Detalle = denuncia.DetalleDenuncia,
                    Petitoria = denuncia.Petitoria
                };
                await _containerWork.Expediente.Add(expediente);
                await _containerWork.Save();

                // Obtener el flujo de trabajo de Expediente
                var flow = await _containerWork.FlowWf.Get(_configuration.GetValue<int>("FlowAppCodes:Expediente"));
                if (flow == null)
                {
                    throw new Exception("No se encontró el flujo de trabajo de Expediente");
                    return false;
                }

                //Crear el flujo de trabajo
                var flowRequestHeader = await _workflowService.CreateFlowRequestHeader<Expediente>(expediente.Id, "Expediente", flow.Id);
                return true;
 
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting flow");
                return false;
                throw;
            }
        }

        public async Task<(List<DenunciaItemListViewModel> items, int totalItems)> GetDenunciasPaginadas(
            int pageIndex, int pageSize, string searchValue, string sortColumn, string sortDirection)
        {
            try
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

                var denunciasListado = _mapper.Map<List<DenunciaItemListViewModel>>(denuncias);

                return (denunciasListado, total);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting flow");
                throw;
            }


        }
    }
}
