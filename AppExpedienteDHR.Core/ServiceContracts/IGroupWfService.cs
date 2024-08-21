using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface IGroupWfService
    {
        Task<IEnumerable<GroupWfViewModel>> GetGroups();
        Task<GroupWfViewModel> GetGroup(int id);
        Task CreateGroup(GroupWfViewModel groupViewModel);
        Task UpdateGroup(GroupWfViewModel groupViewModel);
        Task DeleteGroup(int id);
    }
}
