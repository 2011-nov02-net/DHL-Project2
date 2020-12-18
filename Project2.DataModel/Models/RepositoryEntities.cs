<<<<<<< HEAD:Project2.DataModel/RepositoryEntities.cs
﻿using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Project2.DataModel
{
    public interface IRepository<T> : IQueryable<T>, ICollection<T> { 
        public void Update(T item);
        public ValueTask<T> FindAsync(object keyValue);
        public ValueTask<bool> AddAsync(T item);
        public ValueTask<bool> RemoveAsync(T item);
        public Task<object> UpdateAsync(T item);
    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<int>
    {
        private readonly DHLProject2SchoolContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IQueryable<TEntity> _included;
        public Repository(DHLProject2SchoolContext context, DbSet<TEntity> dbSet, 
            Func<DbSet<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {
            if (context.Model.FindEntityType(dbSet.EntityType.ClrType) is null)
                throw new ArgumentException("DbSet does not belong to dbContext");
            _context = context;
            _dbSet = dbSet;
            _included = includes(dbSet).AsQueryable();
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
    }
    // https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/
    public class Entity<PkT> {
        public PkT Id {get; set;}
    }
    partial class Class : Entity<int> { }
    partial class Person : Entity<int> { }
    partial class Building : Entity<int> { }
    partial class Department : Entity<int> { }
    /*
    public abstract class Repository<T> : IQueryable<T>, ICollection<T>
    {
        public Type ElementType {get; protected set;}
        public Expression Expression {get; protected set;}
        public IQueryProvider Provider {get; protected set;}
        public abstract int Count { get; }
        public abstract bool IsReadOnly { get; }

        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract void Add(T x);
        public abstract void Remove(T x);
        public abstract void Clear();
        public abstract bool Contains(T item);
        public abstract void CopyTo(T[] array, int arrayIndex);

        bool ICollection<T>.Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
    public class RepositoryEF<TEntity> : Repository<TEntity> where TEntity : Entity<int>
    {
        private readonly DHLProject2SchoolContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public RepositoryEF(DHLProject2SchoolContext context, DbSet<TEntity> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
            ElementType = _dbSet.AsQueryable().ElementType;
            Expression = _dbSet.AsQueryable().Expression;
            Provider = _dbSet.AsQueryable().Provider;
        }
        public override IEnumerator<TEntity> GetEnumerator() => _dbSet.AsQueryable().GetEnumerator();
        public override void Add(TEntity x)
        {
            _dbSet.Add(x);
            _context.SaveChanges();
        }
        public async void AddAsync(TEntity x)
        {
            await _dbSet.AddAsync(x);
            await _context.SaveChangesAsync();
        }
        public override void Remove(TEntity x)
        {
            _dbSet.Remove(x);
            _context.SaveChanges();
        }
    }
    */
}
=======
>>>>>>> master:Project2.DataModel/Models/RepositoryEntities.cs
