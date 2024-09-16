using AppExpedienteDHR.Core.Domain.Entities.Dhr;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr
{
    public interface IDenunciaAdjuntoRepository : IRepository<DenunciaAdjunto>
    {
        Task<IEnumerable<DenunciaAdjunto>> ObtenerPorDenunciaId(int denunciaId);
    }
}
