using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.Entities.General;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr
{
    public interface IAdjuntoRepository : IRepository<Adjunto>
    {
        Task<Adjunto> ObtenerPorRuta(string ruta);

    }
}
