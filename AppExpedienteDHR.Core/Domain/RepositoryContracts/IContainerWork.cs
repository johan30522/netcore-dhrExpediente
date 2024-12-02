
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts
{
    public interface IContainerWork : IDisposable
    {
        // Add repositories here
        ICategoryRepository Category { get; }
        IUserRepository User { get; }

        // general repositories
        IDistritoRepository Distrito { get; }
        IEstadoCivilRepository EstadoCivil { get; }
        IPaisRepository Pais { get; }
        IProvinciaRepository Provincia { get; }
        ICantonRepository Canton { get; }
        ITipoIdentificacionRepository TipoIdentificacion { get; }
        IEscolaridadRepository Escolaridad { get; }
        ISexoRepository Sexo { get; }
        IPadronRepository Padron { get; }



       




        // worflow repositories
        IActionWfRepository ActionWf { get; }
        IStateWfRepository StateWf { get; }
        IActionGroupWfRepository ActionGroupWf { get; }
        IGroupWfRepository GroupWf { get; }
        IActionRuleWfRespository ActionRuleWf { get; }
        IFlowHistoryWfRepository FlowHistoryWf { get; }
        IFlowWfRepository FlowWf { get; }
        IGroupUserWfRepository GroupUserWf { get; }
        IFlowRequestHeaderWfRepository RequestFlowHeaderWf { get; }


        //DHr
        IDenuncianteRepository Denunciante { get; }
        IExpedienteRepository Expediente { get; }
        IDenunciaRepository Denuncia { get; }
        IDenunciaAdjuntoRepository DenunciaAdjunto { get; }
        IExpedienteAdjuntoRepository ExpedienteAdjunto { get; }
        IPersonaAfectadaRepository PersonaAfectada { get; }
        IAdjuntoRepository Adjunto { get; }
        ILockRecordRepository LockRecord { get; }

        //Admin
        IDerechoRepository Derecho { get; }
        IEspecificidadRepository Especificidad { get; }
        IDescriptorRepository Descriptor { get; }
        IEventoRepository Evento { get; }
        IEmailTemplateRepository EmailTemplate { get; }




        Task Save();
        IRepository<T> GetRepository<T>() where T : class;

    }
}
