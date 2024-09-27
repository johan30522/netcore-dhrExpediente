

using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.ServiceContracts.Workflow
{
    public interface IWorkflowService
    {
        Task<List<ActionWf>> GetAvailableActions(int flowId, int currentStateId);
        Task<List<ActionWf>> GetAvailableActions(int flowId, int currentStateId, string userId);
        Task<FlowRequestHeaderWf> ProcessAction<TRequest>(int flowId, int requestId, int actionId, string userId, string comments) where TRequest : class;
        Task<FlowRequestHeaderWf> ProcessAction<TRequest>(int requestId, int actionId, string comments) where TRequest : class;
        Task<FlowRequestHeaderWf> CreateFlowRequestHeader<TRequest>(int requestId, string requestType, int flowId) where TRequest : class;
        Task<FlowRequestHeaderWf> CreateFlowRequestHeader<TRequest>(int requestId, string requestType, int flowId, string userId) where TRequest : class;
        Task<bool> HasActions(int flowId, int currentStateId);
        Task<FlowRequestHeaderWf> GetFlowRequestHeader(int requestId, string requestType);


    }
}
