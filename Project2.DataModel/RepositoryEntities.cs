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
    /*
    Dbset requires that any type paramiter be a reference type, so the line "TEntity : EntityBase"
    Satisfies that constraint. But then it requires that the ModelPartials file make the entities inherit from EntityBase
    most of the code in this class is implementing the IQueriable and Icollection interfaces using the method 
    that implement those interfaces for dbSet
    */
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : EntityBase
    {
        private readonly DHLProject2SchoolContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IQueryable<TEntity> _included;
        /// <summery>
        /// This constructor allows dependency injection and consistency with DbSet and dbContext
        /// it takes a context provided by dependency injection and function that gets the dbSet from the context
        /// then it checks that the dbSet belongs to the dbContext 
        /// then it provides some default includes with another function
        /// </summery>
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
            if(!_dbSet.Contains(item) ) return false;
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
            try 
            {
                await _dbSet.AddAsync(item);
                await _context.SaveChangesAsync();
                return true;
            } catch(Exception) { return false; }
        }
        public async ValueTask<bool> RemoveAsync(TEntity item)
        {
            try {
                if(!await _dbSet.ContainsAsync(item) ) return false;
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            } catch(Exception) { return false; }
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