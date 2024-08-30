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


        public async Task<GroupWfViewModel> CreateGroup(GroupWfViewModel groupViewModel, int flowId, List<string>? UserIds)
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

                if (UserIds != null)
                {
                    foreach (var userId in UserIds)
                    {
                        GroupUserWf userGroup = new GroupUserWf
                        {
                            UserId = userId,
                            GroupId = group.Id
                        };
                        await _containerWork.GroupUserWf.Add(userGroup);
                    }
                    await _containerWork.Save();
                }

                // Cargar usuarios asociados
                var groupWithUsers = await _containerWork.GroupWf.GetFirstOrDefault(
                    g => g.Id == group.Id,
                    includeProperties: "GroupUsers.User");

                GroupWfViewModel groupViewModelRes = _mapper.Map<GroupWfViewModel>(groupWithUsers);

                return groupViewModelRes;

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

                GroupWf group = await _containerWork.GroupWf.GetFirstOrDefault(
                   g => g.Id == id,
                   includeProperties: "GroupUsers.User");

                GroupWfViewModel groupViewModel = _mapper.Map<GroupWfViewModel>(group);

                // si hay usuarios asociados, carga en una lista los ids asociados
                if (group.GroupUsers != null)
                {
                    groupViewModel.SelectedUserIds = group.GroupUsers.Select(gu => gu.UserId).ToList();
                }

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

        public async Task<GroupWfViewModel> UpdateGroup(GroupWfViewModel groupViewModel, List<string>? UserIds)
        {
            try
            {
                GroupWf group = _mapper.Map<GroupWf>(groupViewModel);
                await _containerWork.GroupWf.Update(group);
                await _containerWork.Save();

                //Todo: Revisar si esta logica se puede pasar a un Store Procedure

                // verifica si los usuarios asociados han cambiado
                if (UserIds != null)
                {
                    // obtiene los usuarios asociados al grupo
                    IEnumerable<GroupUserWf> groupUsers = await _containerWork.GroupUserWf.GetAll(gu => gu.GroupId == group.Id);

                    // obtiene los ids de los usuarios asociados
                    List<string> groupUserIds = groupUsers.Select(gu => gu.UserId).ToList();

                    // obtiene los ids de los usuarios que se quieren asociar
                    List<string> newUserIds = UserIds.Except(groupUserIds).ToList();

                    // obtiene los ids de los usuarios que se quieren desasociar
                    List<string> removeUserIds = groupUserIds.Except(UserIds).ToList();

                    // se asocian los nuevos usuarios
                    foreach (var userId in newUserIds)
                    {
                        GroupUserWf userGroup = new GroupUserWf
                        {
                            UserId = userId,
                            GroupId = group.Id
                        };
                        await _containerWork.GroupUserWf.Add(userGroup);
                    }

                    // se desasocian los usuarios
                    foreach (var userId in removeUserIds)
                    {
                        GroupUserWf userGroup = groupUsers.FirstOrDefault(gu => gu.UserId == userId);
                        await _containerWork.GroupUserWf.Remove(userGroup);
                    }

                    await _containerWork.Save();
                }


                return groupViewModel;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating group");
                throw;
            }
        }
    }

}
