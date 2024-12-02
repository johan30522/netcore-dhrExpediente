using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin;
using AppExpedienteDHR.Infrastructure.Data;
using AppExpedienteDHR.Infrastructure.Repositories;
using AppExpedienteDHR.Infrastructure.Repositories.Workflow;
using AppExpedienteDHR.Infrastructure.Repositories.General;
using AppExpedienteDHR.Infrastructure.Repositories.Admin;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Infrastructure.Repositories.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;



namespace AppExpedienteDHR.Infrastructure.Repositories
{
    public class ContainerWork : IContainerWork
    {
        private readonly ApplicationDbContext _context;
        private readonly CatalogDbContext _catalogContext;
        private readonly ILogger _logger;



        public ContainerWork(ApplicationDbContext context, CatalogDbContext catalogContext, ILogger logger)
        {
            _context = context;
            _catalogContext = catalogContext;
            _logger = logger;

            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);

            // Workflow repositories
            ActionWf = new ActionWfRepository(_context);
            StateWf = new StateWfRepository(_context);
            ActionGroupWf = new ActionGroupWfRepository(_context);
            GroupWf = new GroupWfRepository(_context);
            ActionRuleWf = new ActionRuleWfRepository(_context);
            FlowHistoryWf = new FlowHistoryWfRepository(_context);
            FlowWf = new FlowWfRepository(_context);
            GroupUserWf = new GroupUserWfRepository(_context);
            RequestFlowHeaderWf = new FlowRequestHeaderWfRepository(_context);

            // General repositories
            Distrito = new DistritoRepository(_context);
            Canton = new CantonRepository(_context);
            Provincia = new ProvinciaRepository(_context);
            EstadoCivil = new EstadoCivilRepository(_context);
            Pais = new PaisRepository(_context);
            TipoIdentificacion = new TipoIdentificacionRepository(_context);
            Escolaridad = new EscolaridadRepository(_context);
            Sexo= new SexoRepository(_context);

            // Catalog repositories
            Padron = new PadronRepository(_catalogContext);

            // Dhr repositories
            Denuncia = new DenunciaRepository(_context);
            Denunciante = new DenuncianteRepository(_context);
            DenunciaAdjunto = new DenunciaAdjuntoRepository(_context);
            ExpedienteAdjunto = new ExpedienteAdjuntoRepository(_context);
            Expediente = new ExpedienteRepository(_context);
            PersonaAfectada = new PersonaAfectadaRepository(_context);
            Adjunto = new AdjuntoRepository(_context);

            // Admin repositories
            Derecho = new DerechoRepository(_context);
            Especificidad = new EspecificidadRepository(_context);
            Descriptor = new DescriptorRepository(_context);
            Evento = new EventoRepository(_context);
            EmailTemplate = new EmailTemplateRepository(_context);



            LockRecord = new LockRecordRepository(_context, _logger);





        }
        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }


        #region Workflow repositories
        public IActionWfRepository ActionWf { get; private set; }
        public IStateWfRepository StateWf { get; private set; }
        public IActionGroupWfRepository ActionGroupWf { get; private set; }
        public IGroupWfRepository GroupWf { get; private set; }
        public IActionRuleWfRespository ActionRuleWf { get; private set; }
        public IFlowHistoryWfRepository FlowHistoryWf { get; private set; }
        public IFlowWfRepository FlowWf { get; private set; }
        public IGroupUserWfRepository GroupUserWf { get; private set; }
        public IFlowRequestHeaderWfRepository RequestFlowHeaderWf { get; private set; }

        #endregion

        #region General repositories

        public IDistritoRepository Distrito { get; private set; }
        public IEstadoCivilRepository EstadoCivil { get; private set; }
        public IPaisRepository Pais { get; private set; }
        public IProvinciaRepository Provincia { get; private set; }
        public ICantonRepository Canton { get; private set; }
        public ITipoIdentificacionRepository TipoIdentificacion { get; private set; }
        public IEscolaridadRepository Escolaridad { get; private set; }
        public ISexoRepository Sexo { get; private set; }
        public IPadronRepository Padron { get; private set; }
        #endregion

        #region Dhr repositories
        public IDenunciaRepository Denuncia { get; private set; }
        public IDenuncianteRepository Denunciante { get; private set; }
        public IDenunciaAdjuntoRepository DenunciaAdjunto { get; private set; }
        public IExpedienteAdjuntoRepository ExpedienteAdjunto { get; private set; }
        public IExpedienteRepository Expediente { get; private set; }
        public IPersonaAfectadaRepository PersonaAfectada { get; private set; }
        public IAdjuntoRepository Adjunto { get; private set; }

        #endregion

        #region Admin repositories
        public IDerechoRepository Derecho { get; private set; }
        public IEspecificidadRepository Especificidad { get; private set; }
        public IDescriptorRepository Descriptor { get; private set; }
        public IEventoRepository Evento { get; private set; }
        public IEmailTemplateRepository EmailTemplate { get; private set; }
        #endregion

        public ILockRecordRepository LockRecord { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_context); // Devuelve un repositorio genérico
        }

    }
}
