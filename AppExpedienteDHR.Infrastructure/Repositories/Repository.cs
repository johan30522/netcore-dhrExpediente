using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;


namespace AppExpedienteDHR.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        //private readonly ApplicationDbContext _context;
        private readonly DbContext _context;
        internal DbSet<T> dbSet;

        //public Repository(ApplicationDbContext context)
        public Repository(DbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }



        public async Task<T> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task<T> Get(string id)
        {
            return await dbSet.FindAsync(id);
        }



        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            try
            {
                // se crea un query
                IQueryable<T> query = dbSet;

                // se aplica el filtro si existe
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // se incluyen las propiedades si existen
                if (includeProperties != null)
                {
                    // se divide las propiedades por coma y se itera sobre ellas
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                // se aplica el ordenamiento si existe
                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                // si no existe ordenamiento se retorna la lista
                return await query.ToListAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// Obtiene una lista paginada de elementos de tipo <typeparamref name="T"/> aplicando filtros, búsqueda, ordenación y paginación.
        /// </summary>
        /// <typeparam name="T">El tipo de entidad que se está consultando.</typeparam>
        /// <param name="filter">Expresión opcional para filtrar los resultados antes de la búsqueda y paginación.</param>
        /// <param name="orderBy">Función opcional para ordenar los resultados de manera personalizada.</param>
        /// <param name="includeProperties">Lista de propiedades relacionadas a incluir, separadas por comas (ej. "Propiedad1,Propiedad2").</param>
        /// <param name="pageIndex">Índice de la página actual para la paginación (0 para la primera página).</param>
        /// <param name="pageSize">Cantidad de elementos por página.</param>
        /// <param name="searchValue">Valor de búsqueda que se aplicará a las columnas especificadas.</param>
        /// <param name="searchColumns">Columnas a las que se aplicará la búsqueda, separadas por comas (ej. "Nombre,Apellido").</param>
        /// <param name="sortColumn">Columna por la cual ordenar los resultados (ej. "Nombre").</param>
        /// <param name="sortDirection">Dirección de la ordenación: "asc" para ascendente y "desc" para descendente.</param>
        /// <returns>Una tupla que contiene una lista paginada de elementos y el total de elementos sin paginar.</returns>
        /// <example>
        /// Ejemplo de uso con búsqueda, paginación y ordenación:
        /// var (items, totalItems) = await _repository.GetAllPaginated(
        ///     searchValue: "John Doe",
        ///     searchColumns: "Nombre,Apellido,Direccion",
        ///     sortColumn: "Nombre",
        ///     sortDirection: "asc",
        ///     pageIndex: 0,
        ///     pageSize: 10);
        /// </example>
        public async Task<(List<T> items, int totalItems)> GetAllPaginated(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "",
            int pageIndex = 0, // Índice de página para la paginación
            int pageSize = 10, // Tamaño de página para la paginación
            string searchValue = "", // Valor de búsqueda
            string searchColumns = "", // Columnas para búsqueda, separadas por comas
            string sortColumn = "", // Columna para ordenar
            string sortDirection = "asc" // Dirección de la ordenación (asc o desc)
        )
        {
            try
            {
                // Se crea la consulta base
                IQueryable<T> query = dbSet;

                // Se aplica el filtro si existe
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Se incluyen las propiedades si existen
                if (!string.IsNullOrEmpty(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProperty);
                    }
                }

                // Aplicar búsqueda si se proporciona un valor de búsqueda
                if (!string.IsNullOrEmpty(searchValue) && !string.IsNullOrEmpty(searchColumns))
                {
                    // Aquí usamos Dynamic LINQ para crear un filtro dinámico
                    var searchTerms = searchValue.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var searchFilters = searchColumns.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    // Crear una condición de búsqueda dinámica combinando las columnas
                    string dynamicFilter = string.Join(" OR ", searchFilters.Select(col => string.Join(" AND ", searchTerms.Select(term => $"{col}.Contains(@0)"))));

                    // Aplicar el filtro dinámico
                    query = query.Where(dynamicFilter, searchTerms);
                }

                // Contar el total de registros antes de la paginación
                int totalItems = await query.CountAsync();

                // Aplicar ordenamiento dinámico usando Dynamic LINQ
                if (!string.IsNullOrEmpty(sortColumn))
                {
                    query = query.OrderBy($"{sortColumn} {sortDirection}");
                }
                else if (orderBy != null)
                {
                    query = orderBy(query);
                }

                // Aplicar paginación
                var items = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

                // Retornar la lista paginada y el total de elementos
                return (items, totalItems);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            // se crea un query
            IQueryable<T> query = dbSet;

            // se aplica el filtro si existe
            if (filter != null)
            {
                query = query.Where(filter);
            }

            // se incluyen las propiedades si existen
            if (includeProperties != null)
            {
                // se divide las propiedades por coma y se itera sobre ellas
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            // se retorna el primer elemento que cumpla con el filtro
            return await query.FirstOrDefaultAsync();

        }

        public async Task Remove(int id)
        {
            T entity = await dbSet.FindAsync(id);
            Remove(entity);
        }


        public async Task Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
