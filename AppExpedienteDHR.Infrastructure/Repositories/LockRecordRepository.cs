using AppExpedienteDHR.Core.Domain.Entities;
using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace AppExpedienteDHR.Infrastructure.Repositories
{
    public class LockRecordRepository : Repository<LockRecord>, ILockRecordRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public LockRecordRepository(ApplicationDbContext context, ILogger logger) : base(context)
        {
            _context = context;
            _logger = logger;
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
            int intentos = 0;
            const int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                try
                {
                    // Verificar si ya existe un registro de bloqueo para el registro solicitado
                    var existingLockRecord = await _context.LockRecords
                        .FirstOrDefaultAsync(l => l.IdLocked == IdLocked && l.EntityType == EntityType);

                    if (existingLockRecord == null)
                    {
                        // Si no existe, crear un nuevo registro de bloqueo
                        var lockRecord = new LockRecord
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
                    }
                    else
                    {
                        // Si ya existe un registro de bloqueo con el mismo ID y tipo de entidad, actualizar la fecha de bloqueo
                        existingLockRecord.LockedAt = DateTime.Now;

                        // Actualizar el registro, usando concurrencia optimista (RowVersion)
                        _context.Entry(existingLockRecord).OriginalValues["RowVersion"] = existingLockRecord.RowVersion;

                        await _context.SaveChangesAsync();
                        return existingLockRecord.Id;
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Loggear advertencia si hay un conflicto de concurrencia
                    _logger.Warning(ex, "Error al bloquear el registro {IdLocked} de tipo {EntityType} por el usuario {LockedByUserId}. Intento {intentos}", IdLocked, EntityType, LockedByUserId, intentos);
                    intentos++;

                    if (intentos >= maxIntentos)
                    {
                        _logger.Error(ex, "Se superaron los intentos de bloqueo para el registro {IdLocked} de tipo {EntityType} por el usuario {LockedByUserId}", IdLocked, EntityType, LockedByUserId);
                        throw; // Lanzar la excepción si se supera el número de intentos
                    }
                }
                catch (Exception ex)
                {
                    // Log any other exceptions
                    _logger.Error(ex, "Error inesperado al bloquear el registro {IdLocked} de tipo {EntityType} por el usuario {LockedByUserId}", IdLocked, EntityType, LockedByUserId);
                    throw;
                }
            }

            return 0;

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
            const int maxIntentos = 3;
            int intentos = 0;

            while (intentos < maxIntentos)
            {
                try
                {
                    LockRecord lockRecord = await _context.LockRecords.FirstOrDefaultAsync(l => l.Id == Id);
                    if (lockRecord != null)
                    {
                        // Guardar la versión original del RowVersion
                        _context.Entry(lockRecord).OriginalValues["RowVersion"] = lockRecord.RowVersion;

                        lockRecord.LockedAt = DateTime.Now;
                        int rowsAffected = await _context.SaveChangesAsync();

                        return rowsAffected > 0; // Si se guardaron los cambios, retornamos true
                    }
                    return false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Loggear advertencia si hay un conflicto de concurrencia
                    _logger.Warning(ex, "Conflicto de concurrencia al intentar actualizar el lock {Id}. Intento {intentos}", Id, intentos);
                    intentos++;

                    if (intentos >= maxIntentos)
                    {
                        _logger.Error(ex, "Se superaron los intentos para mantener vivo el lock {Id}", Id);
                        throw; // Lanzar la excepción si se supera el número de intentos
                    }
                }
                catch (Exception ex)
                {
                    // Loggear cualquier otra excepción
                    _logger.Error(ex, "Error inesperado al intentar mantener vivo el lock {Id}", Id);
                    throw;
                }
            }

            return false; // Retornar false si no se pudo actualizar el registro
        }



    }
}
