using AppExpedienteDHR.Core.Domain.Entities.Dhr;
using AppExpedienteDHR.Core.Domain.RepositoryContracts.Dhr;
using AppExpedienteDHR.Core.ViewModels.Dhr;
using AppExpedienteDHR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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
                expedienteToUpdate.Petitoria = expediente.Petitoria;
                expedienteToUpdate.IncluyePersonaAfectada = expediente.IncluyePersonaAfectada;
                expedienteToUpdate.DerechoId = expediente.DerechoId;
                expedienteToUpdate.EventoId = expediente.EventoId;
                expedienteToUpdate.EspecificidadId = expediente.EspecificidadId;
                expedienteToUpdate.DescriptorId = expediente.DescriptorId;

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
                var expedientesQuery = _context.Expedientes
                    .Include(e => e.Denunciante)
                    .Where(e => e.IsDeleted == false || e.IsDeleted == null);

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
                                FlujoCompletado = flowHeader.IsCompleted,
                                DenuncianteFullName = expediente.Denunciante != null
                                    ? $"{expediente.Denunciante.Nombre} {expediente.Denunciante.PrimerApellido} {expediente.Denunciante.SegundoApellido}".Trim()
                                    : string.Empty
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

                // Evaluar la consulta en el cliente(memoria) para poder aplicar la ordenación por DenuncianteFullName
                var orderedQuery = query.AsEnumerable(); // Forzar la evaluación en memoria aquí




                // Aplicar ordenación
                if (!string.IsNullOrEmpty(sortColumn))
                {
                    if (sortColumn.Equals("DenuncianteFullName", StringComparison.OrdinalIgnoreCase))
                    {
                        // Ordenamiento en memoria para DenuncianteFullName
                        orderedQuery = sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase)
                            ? query.AsEnumerable().OrderBy(q => q.DenuncianteFullName)
                            : query.AsEnumerable().OrderByDescending(q => q.DenuncianteFullName);
                    }
                    else
                    {
                        // Ordenamiento en la base de datos para otras columnas
                        query = query.OrderBy($"{sortColumn} {sortDirection}");
                    }
                }

                // Aplicar paginación
                var items = orderedQuery.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                return (items, totalItems);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
