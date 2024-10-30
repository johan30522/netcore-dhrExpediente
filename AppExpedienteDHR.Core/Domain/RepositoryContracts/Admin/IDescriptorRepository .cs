using AppExpedienteDHR.Core.Domain.Entities.Admin;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Admin
{
    public interface IDescriptorRepository: IRepository<Descriptor>
    {
        Task Update(Descriptor descriptor);
    }
}
