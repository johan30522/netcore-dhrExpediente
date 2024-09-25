
using System.Linq.Expressions;


namespace AppExpedienteDHR.Core.Domain.RepositoryContracts
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<T> Get(string id);

        Task<List<T>> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = ""
        );

        Task<(List<T> items, int totalItems)> GetAllPaginated(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int pageIndex = 0, // Índice de página para la paginación
            int pageSize = 10, // Tamaño de página para la paginación
            string searchValue = "", // Valor de búsqueda
            string searchColumns = "", // Columnas para búsqueda, separadas por comas
            string sortColumn = "", // Columna para ordenar
            string sortDirection = "asc" // Dirección de la ordenación (asc o desc)
        );

        Task<T> GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = ""
        );

        Task<T> Add(T entity);

        Task Remove(int id);
        Task Remove(T entity);
       

    }
}
