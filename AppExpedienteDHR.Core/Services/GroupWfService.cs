using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;

namespace AppExpedienteDHR.Core.Services
{
    public class GroupWfService : IGroupWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;

        public GroupWfService(IContainerWork containerWork, IMapper mapper)
        {
            _containerWork = containerWork;
            _mapper = mapper;
        }

        public async Task CreateGroup(GroupWfViewModel groupViewModel)
        {
            GroupWf group = _mapper.Map<GroupWf>(groupViewModel);
            await _containerWork.GroupWf.Add(group);
            await _containerWork.Save();
        }

        public async Task DeleteGroup(int id)
        {
            GroupWf group = await _containerWork.GroupWf.Get(id);
            if (group == null)
            {
                throw new Exception("Group not found");
            }
            await _containerWork.GroupWf.Remove(group);
            await _containerWork.Save();
        }

        public async Task<GroupWfViewModel> GetGroup(int id)
        {
            GroupWf group = await _containerWork.GroupWf.Get(id);
            GroupWfViewModel groupViewModel = _mapper.Map<GroupWfViewModel>(group);
            return groupViewModel;
        }

        public async Task<IEnumerable<GroupWfViewModel>> GetGroups()
        {
            IEnumerable<GroupWf> groups = await _containerWork.GroupWf.GetAll();
            IEnumerable<GroupWfViewModel> groupViewModels = _mapper.Map<IEnumerable<GroupWfViewModel>>(groups);
            return groupViewModels;
        }

        public async Task UpdateGroup(GroupWfViewModel groupViewModel)
        {
            GroupWf group = _mapper.Map<GroupWf>(groupViewModel);
            await _containerWork.GroupWf.Update(group);
        }
    }

}
