using AppExpedienteDHR.Core.Domain.Entities;
using AppExpedienteDHR.Core.Domain.Entities.Dhr;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts
{
    public interface ILockRecordRepository : IRepository<LockRecord>
    {
        Task<int> LockRecord(int IdLocked, string EntityType, string LockedByUserId);
        Task UnlockRecord(int IdLocked, string EntityType, string LockedByUserId);
        Task<bool> KeepAliveLock(int Id);

    }
}
