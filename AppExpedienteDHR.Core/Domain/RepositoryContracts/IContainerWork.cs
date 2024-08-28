
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Workflow;
namespace AppExpedienteDHR.Core.Domain.RepositoryContracts
{
    public interface IContainerWork : IDisposable
    {
        // Add repositories here
        ICategoryRepository Category { get; }
        IUserRepository User { get; }

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
