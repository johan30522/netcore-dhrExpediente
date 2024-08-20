
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

        Task<T> GetFirstOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = ""
        );

        Task Add(T entity);

        Task Remove(int id);
        Task Remove(T entity);
       

    }
}
