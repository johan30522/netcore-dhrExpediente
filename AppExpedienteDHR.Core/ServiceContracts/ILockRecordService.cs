

namespace AppExpedienteDHR.Core.ServiceContracts
{
    public interface ILockRecordService
    {
        Task<int> LockRecord(int IdLocked, string EntityType, string LockedByUserId);
        Task UnlockRecord(int IdLocked, string EntityType, string LockedByUserId);
        Task UnlockById(int Id);
        Task<bool> KeepAliveLock(int Id);
        Task<(bool IsLocked, string LockedByUserName)> IsRecordLocked(int IdLocked, string EntityType);
    }
}
