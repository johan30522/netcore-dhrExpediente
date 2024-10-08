﻿using AppExpedienteDHR.Core.ViewModels.Workflow;

namespace AppExpedienteDHR.Core.ServiceContracts.Workflow
{
    public interface IGroupWfService
    {
        Task<IEnumerable<GroupWfViewModel>> GetGroups(int flowId);
        Task<GroupWfViewModel> GetGroup(int id);
        Task<IEnumerable<GroupWfViewModel>> SearchGroup(string search);
        Task<GroupWfViewModel> CreateGroup(GroupWfViewModel groupViewModel, int flowId, List<string>? UserIds);
        Task<GroupWfViewModel> UpdateGroup(GroupWfViewModel groupViewModel, List<string>? UserIds);
        Task DeleteGroup(int id);
    }
}
