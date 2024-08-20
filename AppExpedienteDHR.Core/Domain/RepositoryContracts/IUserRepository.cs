

using AppExpedienteDHR.Core.Domain.IdentityEntities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts
{
    public interface IUserRepository: IRepository<ApplicationUser>
    {
        Task LockUser(string userId);
        Task UnLockUser(string userId);
    }
}
