using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AutoMapper;
using Serilog;

namespace AppExpedienteDHR.Core.Services.WorkFlow
{
    public class FlowHandlerService : IFlowHandlerService
    {
        private readonly IContainerWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly RuleEvaluator _ruleEvaluator;



        public FlowHandlerService(IContainerWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _ruleEvaluator = new RuleEvaluator();
        }

        /// <summary>
        /// Se obtienen las acciones disponibles para un usuario en un estado de flujo
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="currentStateId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ActionWf>> GetAvailableActions(int flowId, int currentStateId, string userId)
        {
            try
            {
                IEnumerable<GroupWf> groups = await _unitOfWork.GroupWf.GetGroupsByUserId(userId);
                if (groups.Any()) {
                    _logger.Information("El usuario {userId} no tiene grupos asignados", userId);
                    return new List<ActionWf>();
                }
                var GroupIds = groups.Select(g => g.Id).ToList();
                var actions = await _unitOfWork.ActionWf.GetActionForStateAnGroups(currentStateId, GroupIds);
                if (!actions.Any())
                {
                    _logger.Information("No se encontraron acciones disponibles para el usuario {userId} en el estado {currentStateId} del flujo {flowId}", userId, currentStateId, flowId);
                    return new List<ActionWf>();
                }

                return actions.ToList();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las acciones disponibles para el usuario {userId} en el estado {currentStateId} del flujo {flowId}", userId, currentStateId, flowId);
                throw;
            }
        }

        /// <summary>
        /// Método genérico que trabaja con cualquier entidad de solicitud
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="flowId"></param>
        /// <param name="requestId"></param>
        /// <param name="actionId"></param>
        /// <param name="userId"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<StateWf> ProcessAction<TRequest>(int flowId, int requestId, int actionId, string userId, string comments) where TRequest : class
        {
            var action = await _unitOfWork.ActionWf.GetFirstOrDefault(a => a.Id == actionId);
            if (action == null)
            {
                throw new Exception($"No se encontró la acción con ID {actionId}");
            }

            // Obtener la solicitud genérica
            var requestRepository = _unitOfWork.GetRepository<TRequest>(); // Obtener el repositorio genérico
            var request = await requestRepository.Get(requestId); // Obtener la solicitud usando el repositorio genérico
            if(request == null) {
                throw new Exception("Solicitud no encontrada");
            }
            //Obtiene el Flujo de la solicitud
            var flow = await _unitOfWork.FlowWf.Get(flowId);
            if (flow == null)
            {
                throw new Exception($"No se encontró el flujo con ID {flowId}");
            }

            //Obtiene el encabezado del flujo de la solicitud
            var flowRequestHeader = await _unitOfWork.RequestFlowHeaderWf.GetFirstOrDefault(r => r.RequestId == requestId && r.RequestType == flow.Name);
            if (flowRequestHeader == null)
            {
                throw new Exception($"No se encontró el encabezado del flujo para la solicitud de tipo {flow.Name} con ID {requestId}");
            }


            // Verificar si la acción tiene reglas
            StateWf nextState;
            if (action.EvaluationType == "Rule")
            {
                nextState = await EvaluateRules(action, request); // Evaluamos reglas
            }
            else
            {
                nextState = action.NextState; // Estado estático
            }



            // Actualizar el estado del flujo de la solicitud
            flowRequestHeader.CurrentStateId = nextState.Id;
            if(nextState.IsFinalState)
            {
                flowRequestHeader.IsCompleted = true;
                flowRequestHeader.CompletedDate = DateTime.UtcNow;
            }
            await _unitOfWork.Save();



            // Guardar el historial del flujo
            await SaveFlowHistory(flowId, requestId, action, nextState, userId, comments);

            return nextState;
        }

        private async Task<StateWf> EvaluateRules<TRequest>(ActionWf action, TRequest request)
        {
            foreach (var rule in action.ActionRules.OrderBy(r => r.Order))
            {
                var isValid = _ruleEvaluator.Evaluate(request, rule.RuleJson);
                if (isValid)
                {
                    return rule.ResultState;
                }
            }
            throw new Exception("No se pudo evaluar ninguna regla válida.");
        }
        private async Task SaveFlowHistory(int requestFlowId, int requestId, ActionWf action, StateWf nextState, string userId, string comments)
        {
            var history = new FlowHistoryWf
            {
                RequestFlowHeaderId = requestFlowId,
                PreviousStateId = action.StateId,
                NewStateId = nextState.Id,
                ActionPerformedId = action.Id,
                PerformedByUserId = userId,
                Comments = comments
            };

            await _unitOfWork.FlowHistoryWf.Add(history);
            await _unitOfWork.Save();

        }
        /// <summary>
        /// Permite crear el encabezado de un flujo de solicitud
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="requestId"></param>
        /// <param name="requestType"></param>
        /// <param name="flowId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        public async Task<FlowRequestHeaderWf> CreateFlowRequestHeader<TRequest>(int requestId, string requestType, int flowId, string userId) where TRequest : class
        {
            var initialFlowState = await _unitOfWork.StateWf.GetFirstOrDefault(s => s.FlowId == flowId && s.IsInitialState);

            var flowRequestHeader = new FlowRequestHeaderWf
            {
                RequestId = requestId, // ID de la solicitud (ej: Denuncia, Expediente, etc.)
                RequestType = requestType, // Tipo de solicitud (ej: "Denuncia", "Expediente")
                FlowId = flowId, // Flujo asociado
                CurrentStateId = initialFlowState.Id, // Estado inicial del flujo
                CreatedByUserId = userId, // Usuario que inicia el flujo
                CreatedDate = DateTime.UtcNow // Fecha de creación
            };

            await _unitOfWork.RequestFlowHeaderWf.Add(flowRequestHeader);
            await _unitOfWork.Save();
            return flowRequestHeader;
        }
    }
}
