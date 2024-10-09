using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AutoMapper;
using Serilog;


namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class LoadFormPropsService: ILoadFormPropsService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        //Services for catalogs
        private readonly IPadronService _padronService;
        private readonly IProvinciaService _provinciaService;
        private readonly ICantonService _cantonService;
        private readonly IDistritoService _distritoService;
        private readonly ITipoIdentificacionService _tipoIdentificacionService;
        private readonly ISexoService _sexoService;
        private readonly IEstadoCivilService _estadoCivilService;
        private readonly IPaisService _paisService;
        private readonly IEscolaridadService _escolaridadService;

        public LoadFormPropsService(
            IContainerWork containerWork, 
            IMapper mapper, IPadronService padronService, 
            IProvinciaService provinciaService, 
            ICantonService cantonService, 
            IDistritoService distritoService, 
            ITipoIdentificacionService tipoIdentificacionService, 
            ISexoService sexoService, 
            IEstadoCivilService estadoCivilService, 
            IPaisService paisService, 
            IEscolaridadService escolaridadService,
            ILogger logger
            )
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _padronService = padronService;
            _provinciaService = provinciaService;
            _cantonService = cantonService;
            _distritoService = distritoService;
            _tipoIdentificacionService = tipoIdentificacionService;
            _sexoService = sexoService;
            _estadoCivilService = estadoCivilService;
            _paisService = paisService;
            _escolaridadService = escolaridadService;
            _logger = logger;
        }

        public async Task<DenunciaViewModel> LoadCatalogsForDenuncia(DenunciaViewModel model)
        {
            var provincias = await _provinciaService.GetAllProvincias();
            var sexos = await _sexoService.GetAllSexos();
            var estadosCiviles = await _estadoCivilService.GetAllEstadoCivil();
            var tiposIdentificacion = await _tipoIdentificacionService.GetAllTipoIdentificaciones();
            var paises = await _paisService.GetAllPaises();
            var escolaridades = await _escolaridadService.GetAllEscolaridades();

            model.ListTiposIdentificacion = tiposIdentificacion;
            model.ListSexos = sexos;
            model.ListEstadosCiviles = estadosCiviles;
            model.ListPaises = paises;
            model.ListEscolaridades = escolaridades;
            model.ListProvincias = provincias;

            return model;
        }



        public async Task<ExpedienteViewModel> LoadCatalogsForExpediente(ExpedienteViewModel model)
        {
            var provincias = await _provinciaService.GetAllProvincias();
            var sexos = await _sexoService.GetAllSexos();
            var estadosCiviles = await _estadoCivilService.GetAllEstadoCivil();
            var tiposIdentificacion = await _tipoIdentificacionService.GetAllTipoIdentificaciones();
            var paises = await _paisService.GetAllPaises();
            var escolaridades = await _escolaridadService.GetAllEscolaridades();

            model.ListTiposIdentificacion = tiposIdentificacion;
            model.ListSexos = sexos;
            model.ListEstadosCiviles = estadosCiviles;
            model.ListPaises = paises;
            model.ListEscolaridades = escolaridades;
            model.ListProvincias = provincias;

            return model;
        }


    }
}
