using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;


namespace AppExpedienteDHR.Infrastructure.Repositories.Dhr
{
    public class ExpedienteRepository : Repository<Expediente>, IExpedienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpedienteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task Update(Expediente expediente)
        {
            Expediente expedienteToUpdate = await _context.Expedientes.FirstOrDefaultAsync(e => e.Id == expediente.Id);
            if (expedienteToUpdate != null)
            {
                expedienteToUpdate.Detalle = expediente.Detalle;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(List<ExpedienteItemListViewModel> items, int totalItems)> GetExpedientesWithFlowPaginated(
            int pageIndex = 0,
            int pageSize = 10,
            string searchValue = "",
            string sortColumn = "FechaCreacion",
            string sortDirection = "asc"
)
        {
            try
            {
                // Consulta base para Expedientes, asegurando que no estén eliminados
                var expedientesQuery = _context.Expedientes.Where(e => e.IsDeleted == false || e.IsDeleted == null);

                // Consulta base para FlowRequestHeaderWf, asegurando que no estén eliminados y que el tipo sea 'Expediente'
                var flowHeaderQuery = _context.FlowRequestHeaderWfs
                    .Where(fh => fh.RequestType == "Expediente" && (fh.IsDeleted == false || fh.IsDeleted == null));

                // Realizar el join entre Expediente y FlowRequestHeaderWf basado en el RequestId
                var query = from expediente in expedientesQuery
                            join flowHeader in flowHeaderQuery on expediente.Id equals flowHeader.RequestId
                            select new ExpedienteItemListViewModel
                            {
                                ExpedienteId = expediente.Id,
                                ExpedienteDetalle = expediente.Detalle,
                                ExpedienteFechaCreacion = expediente.FechaCreacion,
                                EstadoActual = flowHeader.CurrentState.Name,
                                TipoSolicitud = flowHeader.RequestType,
                                FechaCreacionFlujo = flowHeader.CreatedDate,
                                FlujoCompletado = flowHeader.IsCompleted
                            };

                // Aplicar búsqueda si es necesario
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var lowerSearchValue = $"%{searchValue.ToLower()}%";

                    query = query.Where(q =>
                        EF.Functions.Contains(q.ExpedienteDetalle, searchValue) || // Búsqueda Full-Text en Detalle
                        EF.Functions.Like(q.EstadoActual.ToLower(), $"%{searchValue.ToLower()}%") || // Insensible a mayúsculas con LIKE
                        EF.Functions.Like(q.TipoSolicitud.ToLower(), $"%{searchValue.ToLower()}%"));  // Insensible a mayúsculas con LIKE
                }

                // Contar el total de elementos antes de aplicar la paginación
                int totalItems = await query.CountAsync();

                // Aplicar ordenación si es necesario
                if (!string.IsNullOrEmpty(sortColumn))
                {
                    query = query.OrderBy($"{sortColumn} {sortDirection}");
                }

                // Aplicar paginación
                var items = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

                return (items, totalItems);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
