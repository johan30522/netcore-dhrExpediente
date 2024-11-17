using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr
{
    public interface IExpedienteAdjuntoRepository : IRepository<ExpedienteAdjunto>
    {
        Task<IEnumerable<ExpedienteAdjunto>> ObtenerPorExpedienteId(int expedienteId);
    }
}
