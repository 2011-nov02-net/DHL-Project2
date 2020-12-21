using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project2
{
    public interface IRepository<T> : IQueryable<T>, ICollection<T> 
    { 
        public void Update(T item);
    }
    /// <summery>
    /// This is a wrapper for the dbSet and dbContext that allows them to generically treated as an 
    /// mutable Asyncronous collection.
    /// It support by virtue of implementing IQueriable and IAsyncEnumerable the async extension methods on dbSet 
    /// like FirstOrDefaultAsync and other Linq operations and their async versions even Include 
    /// as these are all extension methods on IQueryable.
    /// </summery>
    public interface IRepositoryAsync<T> : IRepository<T>, IAsyncEnumerable<T>
    {
        public ValueTask<T> FindAsync(object keyValue);
        public ValueTask<bool> AddAsync(T item);
        public ValueTask<bool> RemoveAsync(T item);
        public Task<object> UpdateAsync(T item);
    }
}
