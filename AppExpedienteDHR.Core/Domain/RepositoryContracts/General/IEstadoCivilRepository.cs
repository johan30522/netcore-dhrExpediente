

using AppExpedienteDHR.Core.Domain.Entities.General;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.General
{
    public interface IEstadoCivilRepository: IRepository<Entities.General.EstadoCivil>
    {
        Task Update(EstadoCivil estadoCivil);
    }
}
