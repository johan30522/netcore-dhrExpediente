

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts
{
    public interface IContainerWork : IDisposable
    {
        // Add repositories here
        ICategoryRepository Category { get; }


        Task Save();

    }
}
