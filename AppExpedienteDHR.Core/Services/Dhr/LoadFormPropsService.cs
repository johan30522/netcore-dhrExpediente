using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.General;
using AppExpedienteDHR.Core.ServiceContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using Microsoft.Extensions.Caching.Memory;
using AutoMapper;
using Serilog;
using AppExpedienteDHR.Core.Domain.Entities.General;
using Microsoft.Extensions.Configuration;


namespace AppExpedienteDHR.Core.Services.Dhr
{
    public class LoadFormPropsService: ILoadFormPropsService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

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
            ILogger logger,
            IMemoryCache cache,
            IConfiguration configuration
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
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<DenunciaViewModel> LoadCatalogsForDenuncia(DenunciaViewModel model)
        {
            model.ListProvincias = (await GetOrSetCache("Provincias", async () => await _provinciaService.GetAllProvincias())).ToList();
            model.ListTiposIdentificacion = (await GetOrSetCache("TiposIdentificacion", async () => await _tipoIdentificacionService.GetAllTipoIdentificaciones())).ToList();
            model.ListSexos = (await GetOrSetCache("Sexos", async () => await _sexoService.GetAllSexos())).ToList();
            model.ListEstadosCiviles = (await GetOrSetCache("EstadosCiviles", async () => await _estadoCivilService.GetAllEstadoCivil())).ToList();
            model.ListPaises = (await GetOrSetCache("Paises", async () => await _paisService.GetAllPaises())).ToList();
            model.ListEscolaridades = (await GetOrSetCache("Escolaridades", async () => await _escolaridadService.GetAllEscolaridades())).ToList();

            return model;
        }



        public async Task<ExpedienteViewModel> LoadCatalogsForExpediente(ExpedienteViewModel model)
        {

            model.ListProvincias = (await GetOrSetCache("Provincias", async () => await _provinciaService.GetAllProvincias())).ToList();
            model.ListSexos = (await GetOrSetCache("Sexos", async () => await _sexoService.GetAllSexos())).ToList();
            model.ListEstadosCiviles = (await GetOrSetCache("EstadosCiviles", async () => await _estadoCivilService.GetAllEstadoCivil())).ToList();
            model.ListTiposIdentificacion = (await GetOrSetCache("TiposIdentificacion", async () => await _tipoIdentificacionService.GetAllTipoIdentificaciones())).ToList();
            model.ListPaises = (await GetOrSetCache("Paises", async () => await _paisService.GetAllPaises())).ToList();
            model.ListEscolaridades = (await GetOrSetCache("Escolaridades", async () => await _escolaridadService.GetAllEscolaridades())).ToList();

            return model;
        }

        // Método genérico para obtener los datos cacheados o establecerlos si no existen
        private async Task<IEnumerable<T>> GetOrSetCache<T>(string cacheKey, Func<Task<IEnumerable<T>>> getDataFunc)
        {
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<T> cacheEntry))
            {
                // Si no está en el caché, obtenemos los datos y los almacenamos
                cacheEntry = await getDataFunc();

                // Configuramos el caché para que dure 60 minutos
                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_configuration.GetValue<int>("MemoryCache:Expiration"))
                };

                _cache.Set(cacheKey, cacheEntry, cacheOptions);
            }

            return cacheEntry;
        }


    }
}
