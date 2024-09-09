using AppExpedienteDHR.Core.Domain.RepositoryContracts;
using AppExpedienteDHR.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


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



        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
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
