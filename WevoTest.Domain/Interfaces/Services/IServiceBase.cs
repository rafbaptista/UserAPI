using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WevoTest.Domain.Enums;

namespace WevoTest.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        void Delete(TEntity obj);
        void Update(TEntity obj);
        IEnumerable<TEntity> GetAll();        
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, EOrderBy orderBy, string orderByPropertyName, IComparer<object> comparer, params Expression<Func<TEntity, object>>[] navigationProperties);
        IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties);
        IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, EOrderBy orderBy, string orderByPropertyName, IComparer<object> comparer, params Expression<Func<TEntity, object>>[] navigationProperties);
        int Count(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        TEntity Find(int id);
    }
}
