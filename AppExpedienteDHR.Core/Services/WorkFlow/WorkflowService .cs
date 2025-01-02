using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.Entities.WorkflowEntities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AppExpedienteDHR.Core.ServiceContracts.Admin;
using AppExpedienteDHR.Core.ServiceContracts.Utils;
using AppExpedienteDHR.Core.ServiceContracts.Workflow;
using AutoMapper;
using Serilog;

namespace AppExpedienteDHR.Core.Services.WorkFlow
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IContainerWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly RuleEvaluator _ruleEvaluator;
        private readonly IUserService _userService;
        private readonly ITemplateRenderer _templateRenderer;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IRequestWfService _requestWfService;



        public WorkflowService(
            IContainerWork unitOfWork, 
            IMapper mapper, 
            ILogger logger, 
            IUserService userService, 
            ITemplateRenderer templateRenderer, 
            IEmailService emailService, 
            IEmailTemplateService emailTemplateService,
            IRequestWfService requestWfService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _ruleEvaluator = new RuleEvaluator();
            _userService = userService;
            _templateRenderer = templateRenderer;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
            _requestWfService = requestWfService;
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
                if (!groups.Any())
                {
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
        /// Se obtienen las acciones disponibles para el usuario actual en un estado de flujo
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="currentStateId"></param>
        /// <returns></returns>

        public async Task<List<ActionWf>> GetAvailableActions(int flowId, int currentStateId)
        {
            try
            {
                var currentUser = await _userService.GetCurrentUser();
                if (currentUser == null)
                {
                    _logger.Information("No se encontró el usuario actual");
                    return new List<ActionWf>();
                }
                List<ActionWf> actionWfs = await GetAvailableActions(flowId, currentStateId, currentUser.Id);
                return actionWfs;


            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las acciones disponibles en el estado {currentStateId} del flujo {flowId}", currentStateId, flowId);
                throw;
            }
        }
        /// <summary>
        /// Permite verificar si un usuario tiene acciones disponibles en un estado de flujo
        /// </summary>
        /// <param name="flowId"></param>
        /// <param name="currentStateId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> HasActions(int flowId, int currentStateId)
        {
            var actions = await GetAvailableActions(flowId, currentStateId);
            return actions.Any(); // si tiene acciones retorna true
        }

        /// <summary>
        /// Permite retornar el encabezado de un flujo de solicitud
        /// </summary>
        /// <param name="requestId"></param>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public async Task<FlowRequestHeaderWf> GetFlowRequestHeader(int requestId, string requestType)
        {
            // include the flow and the current state
            return await _unitOfWork.RequestFlowHeaderWf.GetFirstOrDefault(r => r.RequestId == requestId && r.RequestType == requestType, includeProperties: "Flow,CurrentState");
        }





        /// <summary>
        /// Permitir que un usuario realice una acción en una solicitud
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
        public async Task<FlowRequestHeaderWf> ProcessAction<TRequest>(int flowId, int requestId, int actionId, string userId, string comments) where TRequest : class
        {
            var flow = await GetFlow(flowId);
            var flowRequestHeader = await GetFlowRequestHeader(requestId);
            var action = await GetAction(actionId);

            var request = await GetRequest<TRequest>(flowRequestHeader.RequestId);
            var nextState = await DetermineNextState(action, request);

            await UpdateFlowState(flowRequestHeader, nextState);
            await SaveFlowHistory(requestId, flowId, action, nextState, userId, comments);
            await SendNotification(nextState, flowRequestHeader.RequestId, request.GetType().Name);

            return flowRequestHeader;
           
        }





        /// <summary>
        /// Permitir que un usuario actual realice una acción en una solicitud
        /// Método genérico que trabaja con cualquier entidad de solicitud
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="requestId"></param>
        /// <param name="actionId"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<FlowRequestHeaderWf> ProcessAction<TRequest>(int requestId, int actionId, string comments) where TRequest : class
        {
            var currentUser = await _userService.GetCurrentUser();
            if (currentUser == null)
            {
                throw new Exception("No se encontró el usuario actual");
            }
            var flowRequestHeader = await _unitOfWork.RequestFlowHeaderWf.GetFirstOrDefault(r => r.Id == requestId);
            if (flowRequestHeader == null)
            {
                throw new Exception($"No se encontró el encabezado del flujo para la solicitud con ID {requestId}");
            }
            return await ProcessAction<TRequest>(flowRequestHeader.FlowId, requestId, actionId, currentUser.Id, comments);
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
            if (initialFlowState == null)
            {
                throw new Exception("No se encontró el estado inicial del flujo");
            }

            var flowRequestHeader = new FlowRequestHeaderWf
            {
                RequestId = requestId, // ID de la solicitud (ej: Denuncia, Expediente, etc.)
                RequestType = requestType, // Tipo de solicitud (ej: "Denuncia", "Expediente")
                FlowId = flowId, // Flujo asociado
                CurrentStateId = initialFlowState.Id, // Estado inicial del flujo
                CreatedByUserId = userId, // Usuario que inicia el flujo
                CreatedDate = DateTime.Now // Fecha de creación
            };

            await _unitOfWork.RequestFlowHeaderWf.Add(flowRequestHeader);
            await _unitOfWork.Save();
            return flowRequestHeader;
        }
        /// <summary>
        /// Permitir que un usuario actual cree el encabezado de un flujo de solicitud
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="requestId"></param>
        /// <param name="requestType"></param>
        /// <param name="flowId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public async Task<FlowRequestHeaderWf> CreateFlowRequestHeader<TRequest>(int requestId, string requestType, int flowId) where TRequest : class
        {
            var currentUser = await _userService.GetCurrentUser();
            if (currentUser == null)
            {
                throw new Exception("No se encontró el usuario actual");
            }
            return await CreateFlowRequestHeader<TRequest>(requestId, requestType, flowId, currentUser.Id);
        }



        public async Task<List<FlowHistoryWf>> GetFlowHistoryByRequestHeaderId(int requestFlowHeaderId)
        {
            try
            {
                return await _unitOfWork.FlowHistoryWf.GetAll(
                    filter: h => h.RequestFlowHeaderId == requestFlowHeaderId,
                    orderBy: q => q.OrderByDescending(h => h.ActionDate),
                    includeProperties: "PreviousState,NewState,ActionPerformed,PerformedByUser"
                );
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el historial del flujo para el encabezado {RequestFlowHeaderId}", requestFlowHeaderId);
                throw;
            }
        }


        // Métodos auxiliares:
        /// <summary>
        /// Obtiene un flujo
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<FlowWf> GetFlow(int flowId) =>
            await _unitOfWork.FlowWf.Get(flowId) ?? throw new Exception($"No se encontró el flujo con ID {flowId}");
        /// <summary>
        /// Obtiene el encabezado de un flujo
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<FlowRequestHeaderWf> GetFlowRequestHeader(int requestId) =>
            await _unitOfWork.RequestFlowHeaderWf.GetFirstOrDefault(r => r.Id == requestId)
            ?? throw new Exception($"No se encontró el encabezado del flujo para la solicitud con ID {requestId}");
        /// <summary>
        /// Obtiene una acción
        /// </summary>
        /// <param name="actionId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<ActionWf> GetAction(int actionId) =>
            await _unitOfWork.ActionWf.GetFirstOrDefault(a => a.Id == actionId, includeProperties: "ActionRules,NextState")
            ?? throw new Exception($"No se encontró la acción con ID {actionId}");
        /// <summary>
        /// Obtiene una solicitud genérica
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="requestId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<TRequest> GetRequest<TRequest>(int requestId) where TRequest : class
        {
            var repository = _unitOfWork.GetRepository<TRequest>();
            return await repository.Get(requestId) ?? throw new Exception("Solicitud no encontrada");
        }
        /// <summary>
        /// Determina el siguiente estado de un flujo
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="action"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        private async Task<StateWf> DetermineNextState<TRequest>(ActionWf action, TRequest request)
        {
            return action.EvaluationType == "Rule"
                ? await EvaluateRules(action, request)
                : action.NextState ?? throw new Exception("No se definió el siguiente estado");
        }
        /// <summary>
        /// Actualiza el estado de un flujo
        /// </summary>
        /// <param name="header"></param>
        /// <param name="nextState"></param>
        /// <returns></returns>

        private async Task UpdateFlowState(FlowRequestHeaderWf header, StateWf nextState)
        {
            header.CurrentStateId = nextState.Id;
            if (nextState.IsFinalState)
            {
                header.IsCompleted = true;
                header.CompletedDate = DateTime.Now;
            }
            await _unitOfWork.Save();
        }
        /// <summary>
        /// Se encarga de enviar una notificaciónes del estado de un flujo
        /// </summary>
        /// <param name="stateWf"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task SendNotification(StateWf stateWf, int idSolicitud, string tipoSolicitud)
        {
            try
            {
                if (!stateWf.IsNotificationActive)
                {
                    _logger.Information("Notificación no activa para el estado {StateId}", stateWf.Id);
                    return; // Si las notificaciones no están activas, salir.
                }

                // Se obtiene la solicitud completa
                var completeRequest = await _requestWfService.GetCompleteRequest(idSolicitud, tipoSolicitud);
                if (completeRequest == null)
                {
                    _logger.Warning("No se pudo obtener la solicitud completa para la notificación del estado {StateId}", stateWf.Id);
                    return; // Si no se pudo obtener la solicitud, salir.
                }

                // Obtener el estado con la notificación y los grupos asociados
                var stateWithNotification = await _unitOfWork.StateWf.GetFirstOrDefault(
                    s => s.Id == stateWf.Id && s.IsDeleted == false,
                    includeProperties: "Actions,StateNotification.NotificationGroups.Group.GroupUsers.User");

                var notification = stateWithNotification?.StateNotification;
                if (notification == null)
                {
                    _logger.Warning("No se encontró configuración de notificación para el estado {StateId}", stateWf.Id);
                    return; // Si no hay notificación configurada, salir.
                }

                // Obtener la plantilla de correo
                var emailTemplate = await _emailTemplateService.GetEmailTemplateById(notification.EmailTemplateId);
                if (emailTemplate == null)
                {
                    _logger.Warning("No se encontró plantilla de correo para la notificación del estado {StateId}", stateWf.Id);
                    return; // Si no hay plantilla configurada, salir.
                }

                // Renderizar la plantilla de correo
                var subject = await _templateRenderer.RenderAsync(emailTemplate.SubjectTemplate, completeRequest);
                var emailBody = await _templateRenderer.RenderAsync(emailTemplate.BodyTemplate, completeRequest);
                if (string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(emailBody))
                {
                    _logger.Warning("No se pudo renderizar el correo para la notificación del estado {StateId}", stateWf.Id);
                    return; // Si no se pudo renderizar el correo, salir.
                }

                // Obtener los destinatarios
                var groups = notification.NotificationGroups.Select(g => g.Group);
                var groupEmails = groups.SelectMany(g => g.GroupUsers.Select(gu => gu.User.Email)).ToList();
                var emailsStringFromGroups = string.Join(";", groupEmails);
                var emailsString = $"{notification.To};{emailsStringFromGroups}".Trim(';');

                // Enviar el correo
                if (!string.IsNullOrEmpty(emailsString))
                {
                    await _emailService.SendEmailAsync(emailsString, subject, emailBody, true);
                    _logger.Information("Notificación enviada correctamente para el estado {StateId} y solicitud {RequestId}", stateWf.Id, idSolicitud);
                }
                else
                {
                    _logger.Warning("No se encontraron destinatarios para la notificación del estado {StateId}", stateWf.Id);
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier excepción y registrar el error
                _logger.Error(ex, "Error al enviar la notificación para el estado {StateId} y solicitud {RequestId}", stateWf.Id, idSolicitud);
            }
        }
        /// <summary>
        /// Evalua las reglas de una acción y retorna el siguiente estado
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="action"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

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

        /// <summary>
        /// Guarda el historial del flujo
        /// </summary>
        /// <param name="requestHeaderFlowId"></param>
        /// <param name="requestId"></param>
        /// <param name="action"></param>
        /// <param name="nextState"></param>
        /// <param name="userId"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        private async Task SaveFlowHistory(int requestHeaderFlowId, int requestId, ActionWf action, StateWf nextState, string userId, string comments)
        {
            var history = new FlowHistoryWf
            {
                RequestFlowHeaderId = requestHeaderFlowId,
                ActionDate = DateTime.Now,
                PreviousStateId = action.StateId,
                NewStateId = nextState.Id,
                ActionPerformedId = action.Id,
                PerformedByUserId = userId,
                Comments = comments
            };

            await _unitOfWork.FlowHistoryWf.Add(history);
            await _unitOfWork.Save();

        }



    }
}
