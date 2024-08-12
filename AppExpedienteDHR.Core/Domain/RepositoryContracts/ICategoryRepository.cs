using AppExpedienteDHR.Core.Domain.Entities;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task Update(Category category);
    }
}
