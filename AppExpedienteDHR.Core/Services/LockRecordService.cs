using AppExpedienteDHR.Core.Domain.Entities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Core.ServiceContracts;
using AutoMapper;
using Serilog;
using AppExpedienteDHR.Core.ViewModels.User;

namespace AppExpedienteDHR.Core.Services
{
    public class LockRecordService : ILockRecordService
    {
        private readonly IContainerWork _containerWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public LockRecordService(IContainerWork containerWork, IMapper mapper, ILogger logger, IUserService userService)
        {
            _containerWork = containerWork;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        public async Task<int> LockRecord(int IdLocked, string EntityType, string LockedByUserId)
        {
            try
            {
                int id = await _containerWork.LockRecord.LockRecord(IdLocked, EntityType, LockedByUserId);
                if (id == 0)
                {
                    _logger.Error("Error al bloquear registro");
                }
                return id;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al bloquear registro");
                return 0;
            }
        }
        public async Task UnlockRecord(int IdLocked, string EntityType, string LockedByUserId)
        {
            try
            {
                await _containerWork.LockRecord.UnlockRecord(IdLocked, EntityType, LockedByUserId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al desbloquear registro");
            }
        }
        public async Task UnlockById(int Id)
        {
            try
            {
                LockRecord lockRecord = await _containerWork.LockRecord.GetFirstOrDefault(l => l.Id == Id);
                if (lockRecord != null)
                {
                    await UnlockRecord(lockRecord.IdLocked, lockRecord.EntityType, lockRecord.LockedByUserId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al desbloquear registro por ID");
            }
        }
        public async Task<bool> KeepAliveLock(int Id)
        {
            try
            {
                return await _containerWork.LockRecord.KeepAliveLock(Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al mantener vivo el bloqueo");
                return false;
            }
        }

        public async Task<(bool IsLocked, string LockedByUserName)> IsRecordLocked(int IdLocked, string EntityType)
        {
            try
            {
                // verificar si el registro esta bloqueado por el usuario actual, si es asi devuelve false
                UserViewModel user = await _userService.GetCurrentUser();
                if (user != null)
                {
                    LockRecord lockRecordCurrentUser = await _containerWork.LockRecord.GetFirstOrDefault(l => l.IdLocked == IdLocked && l.EntityType == EntityType && l.LockedByUserId == user.Id);
                    if (lockRecordCurrentUser != null)
                    {
                        return (false,null);
                    }
                }
                // verificar si el registro esta bloqueado por otro usuario
                // include the user in the query
                LockRecord lockRecordAnotherUser = await _containerWork.LockRecord.GetFirstOrDefault(l => l.IdLocked == IdLocked && l.EntityType == EntityType, includeProperties: "LockedByUser");
                if (lockRecordAnotherUser == null)
                {
                    return (false, null);
                }
                else
                {
                    // si esta bloqueado, verificar si el tiempo de bloqueo ha expirado, si es asi desbloquea el registro, sino devuelve true
                    if (lockRecordAnotherUser.LockedAt.Add(lockRecordAnotherUser.LockDuration) < DateTime.Now)
                    {
                        await UnlockRecord(IdLocked, EntityType, lockRecordAnotherUser.LockedByUserId);
                        return (false, null);
                    }
                    return (true, lockRecordAnotherUser.LockedByUser.FullName);

                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al verificar si el registro está bloqueado");
                return (false, null);
            }
        }


    }
}
