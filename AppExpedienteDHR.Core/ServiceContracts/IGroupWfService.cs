using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IGroupWfService
    {
        Task<IEnumerable<GroupWfViewModel>> GetGroups(int flowId);
        Task<GroupWfViewModel> GetGroup(int id);
        Task CreateGroup(GroupWfViewModel groupViewModel, int flowId);
        Task UpdateGroup(GroupWfViewModel groupViewModel);
        Task DeleteGroup(int id);
    }
}
