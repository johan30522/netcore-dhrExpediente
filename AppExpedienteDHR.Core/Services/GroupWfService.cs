using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ViewModels.Workflow;
using AutoMapper;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using Serilog;

namespace AppExpedienteDHR.Core.Services
{
    public class GroupWfService : IGroupWfService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GroupWfService(IContainerWork containerWork, IMapper mapper, ILogger logger)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task CreateGroup(GroupWfViewModel groupViewModel, int flowId)
        {
            try
            {
                FlowWf flow = await _containerWork.FlowWf.Get(flowId);
                if (flow == null)
                {
                    throw new Exception("Flow not found");
                }
                GroupWf group = _mapper.Map<GroupWf>(groupViewModel);
                group.FlowId = flowId;
                group.Flow = flow;
                await _containerWork.GroupWf.Add(group);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error creating group");
                throw;
            }
        }

        public async Task DeleteGroup(int id)
        {
            try
            {
                GroupWf group = await _containerWork.GroupWf.Get(id);
                if (group == null)
                {
                    throw new Exception("Group not found");
                }
                await _containerWork.GroupWf.Remove(group);
                await _containerWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error deleting group");
                throw;
            }
        }

        public async Task<GroupWfViewModel> GetGroup(int id)
        {
            try
            {


                GroupWf group = await _containerWork.GroupWf.Get(id);
                GroupWfViewModel groupViewModel = _mapper.Map<GroupWfViewModel>(group);
                return groupViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting group");
                throw;
            }
        }

        public async Task<IEnumerable<GroupWfViewModel>> GetGroups(int flowId)
        {
            try
            {
                IEnumerable<GroupWf> groups = await _containerWork.GroupWf.GetAll(g => g.FlowId == flowId);
                IEnumerable<GroupWfViewModel> groupViewModels = _mapper.Map<IEnumerable<GroupWfViewModel>>(groups);
                return groupViewModels;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting groups");
                throw;
            }
        }

        public async Task UpdateGroup(GroupWfViewModel groupViewModel)
        {
            try
            {
                GroupWf group = _mapper.Map<GroupWf>(groupViewModel);
                await _containerWork.GroupWf.Update(group);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating group");
                throw;
            }
        }
    }

}
