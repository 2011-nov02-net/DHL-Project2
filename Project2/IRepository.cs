using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project2
{
    public interface IRepository<T> : IQueryable<T>, ICollection<T> 
    { 
        public void Update(T item);
    }
    public interface IRepositoryAsync<T> : IRepository<T>
    {
        public ValueTask<T> FindAsync(object keyValue);
        public ValueTask<bool> AddAsync(T item);
        public ValueTask<bool> RemoveAsync(T item);
        public Task<object> UpdateAsync(T item);
    }
}
