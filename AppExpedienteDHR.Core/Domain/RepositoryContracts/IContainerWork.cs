
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
        IFlowGroupWfRepository FlowGroupWf { get; }
        IFlowHistoryWfRepository FlowHistoryWf { get; }
        IFlowStateWfRepository FlowStateWf { get; }
        IFlowWfRepository FlowWf { get; }
        IGroupUserWfRepository GroupUserWf { get; }
        IRequestFlowHistoryWfRepository RequestFlowHistoryWf { get; }
        IStateActionWfRepository StateActionWf { get; }

         Task Save();

    }
}
