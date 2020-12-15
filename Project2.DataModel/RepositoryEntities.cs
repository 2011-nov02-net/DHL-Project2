using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Project2.DataModel
{
    // https://www.c-sharpcorner.com/article/generic-repository-pattern-in-asp-net-core/
    public class Class1
    {
    }
    public class Entity<PkT> {
        PkT Id {get; set;}
    }
    public abstract class Repository<T> : IQueryable<T>
    {
        public Type ElementType {get; protected set;}
        public Expression Expression {get; protected set;}
        public IQueryProvider Provider {get; protected set;}
        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public abstract void Add(T x);
        public abstract void Remove(T x);
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
    partial class Class : Entity<int> { }
    partial class Person : Entity<int> { }
    partial class Building : Entity<int> { }
    partial class Department : Entity<int> { }
}
