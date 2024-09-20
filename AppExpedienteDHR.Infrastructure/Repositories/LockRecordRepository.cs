using AppExpedienteDHR.Core.Domain.Entities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AppExpedienteDHR.Infrastructure.Repositories
{
    public class LockRecordRepository : Repository<LockRecord>, ILockRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public LockRecordRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        ///  permite bloquear un registro para evitar que otros usuarios lo modifiquen
        /// </summary>
        /// <param name="IdLocked"></param>
        /// <param name="EntityType"></param>
        /// <param name="LockedByUserId"></param>
        /// <returns> el id del registro de bloqueo</returns>

        public async Task<int> LockRecord(int IdLocked, string EntityType, string LockedByUserId)
        {
            // Verificar si ya existe un registro de bloqueo para el registro solicitado
            LockRecord existingLockRecord = await _context.LockRecords.FirstOrDefaultAsync(l => l.IdLocked == IdLocked && l.EntityType == EntityType && l.LockedByUserId == LockedByUserId);
            if (existingLockRecord == null) {
                // Si no existe, crear un nuevo registro de bloqueo
                LockRecord lockRecord = new LockRecord
                {
                    IdLocked = IdLocked,
                    IsLocked = true,
                    EntityType = EntityType,
                    LockedByUserId = LockedByUserId,
                    LockedAt = DateTime.Now
                };
                _context.LockRecords.Add(lockRecord);
                await _context.SaveChangesAsync();
                return lockRecord.Id;
            } else {
                // Si ya existe un registro de bloqueo con el mismo ID, tipo de entidad y usuario, actualizar la fecha de bloqueo
                existingLockRecord.LockedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                return existingLockRecord.Id;
            }
           
        }

        public async Task UnlockRecord(int IdLocked, string EntityType, string LockedByUserId)
        {
            LockRecord lockRecord = await _context.LockRecords.FirstOrDefaultAsync(l => l.IdLocked == IdLocked && l.EntityType == EntityType && l.LockedByUserId == LockedByUserId);
            if (lockRecord != null)
            {
                _context.LockRecords.Remove(lockRecord);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> KeepAliveLock(int Id)
        {
            LockRecord lockRecord = await _context.LockRecords.FirstOrDefaultAsync(l => l.Id == Id);
            if (lockRecord != null)
            {
                lockRecord.LockedAt = DateTime.Now;
                int rowsAffected = await _context.SaveChangesAsync();
                return rowsAffected > 0;
               
            }
            return false;
        }



    }
}
