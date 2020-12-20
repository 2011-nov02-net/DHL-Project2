using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading;

namespace Project2.DataModel
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : EntityBase
    {
        private readonly DHLProject2SchoolContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IQueryable<TEntity> _included;
        public Repository(DHLProject2SchoolContext context, 
            Func<DHLProject2SchoolContext, DbSet<TEntity>> dbSetFactory,
            Func<DbSet<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            _context = context;
            _dbSet = dbSetFactory(_context);
            if (_context.Model.FindEntityType(_dbSet.EntityType.ClrType) is null)
                throw new ArgumentException("DbSet does not belong to dbContext");
            _included = includes(_dbSet).AsQueryable();
        }
        public Type ElementType => _included.ElementType;
        public Expression Expression => _included.Expression;
        public IQueryProvider Provider => _included.Provider;
        public int Count => _dbSet.Count();
        public bool IsReadOnly => false;
        public void Clear() => throw new NotImplementedException();
        public bool Contains(TEntity item) => _dbSet.Contains(item);
        public void CopyTo(TEntity[] array, int arrayIndex) => 
            _dbSet.ToArray().CopyTo(array, arrayIndex);
        public IEnumerator<TEntity> GetEnumerator() => 
            _included.AsEnumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public void Add(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public bool Remove(TEntity item)
        { 
            _dbSet.Remove(item);
            _context.SaveChanges();
            return true;
        }
        public void Update(TEntity item)
        {
            _dbSet.Update(item);
            _context.SaveChanges();
        }
        public async ValueTask<TEntity> FindAsync(object keyValue)
        {
            return await _dbSet.FindAsync(keyValue);
        }
        public async ValueTask<bool> AddAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async ValueTask<bool> RemoveAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<object> UpdateAsync(TEntity item)
        {
            _dbSet.Update(item);
            return await _context.SaveChangesAsync();
        }
        IAsyncEnumerator<TEntity> IAsyncEnumerable<TEntity>.GetAsyncEnumerator(CancellationToken cancellationToken) => 
            _dbSet.AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);
    }
}