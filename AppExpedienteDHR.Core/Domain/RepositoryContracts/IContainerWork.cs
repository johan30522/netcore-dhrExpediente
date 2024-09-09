
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.General;
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





        // worflow repositories
        IActionWfRepository ActionWf { get; }
        IStateWfRepository StateWf { get; }
        IActionGroupWfRepository ActionGroupWf { get; }
        IGroupWfRepository GroupWf { get; }
        IActionRuleWfRespository ActionRuleWf { get; }
        IFlowHistoryWfRepository FlowHistoryWf { get; }
        IFlowWfRepository FlowWf { get; }
        IGroupUserWfRepository GroupUserWf { get; }
        IRequestFlowHistoryWfRepository RequestFlowHistoryWf { get; }

         Task Save();

    }
}
