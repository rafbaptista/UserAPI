using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WevoTest.Domain.Enums;
using WevoTest.Domain.Extensions;
using WevoTest.Domain.Interfaces;
using WevoTest.Domain.Interfaces.Repositories;
using WevoTest.Infra.Data.Context;

namespace WevoTest.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntity
    {
        protected WevoTestContext _context;

        public RepositoryBase(WevoTestContext context)
        {
            _context = context;
        }             

        public void Add(TEntity obj)
        {
            _context.Set<TEntity>().Add(obj);            
        }

        public void Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);            
        }

        public IEnumerable<TEntity> GetAll()
        {            
            return _context.Set<TEntity>().AsNoTracking().ToList();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return QueryBy(predicate, navigationProperties).ToList();            
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, EOrderBy orderBy, string orderByPropertyName, IComparer<object> comparer, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return QueryBy(predicate, orderBy, orderByPropertyName, comparer,navigationProperties).ToList();
        }

        public IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity,object>>[] navigationProperties)
        {            
            IQueryable<TEntity> query = IncludeProperties(navigationProperties).AsNoTracking().Where(predicate);
            return query;
        }

        public IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, EOrderBy orderBy, string orderByPropertyName, IComparer<object> comparer, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> query = IncludeProperties(navigationProperties).AsNoTracking().Where(predicate);

            if (orderBy == EOrderBy.Asc)
                query = query.OrderBy(orderByPropertyName, comparer);
            else
                query = query.OrderByDescending(orderByPropertyName, comparer);
                                        
            return query;
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return QueryBy(predicate).Count();
        }

        public TEntity GetById(int id)
        {                        
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity Find(int id)
        {
            return _context.Set<TEntity>().AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Update(TEntity obj)
        {                        
            _context.Entry(obj).State = EntityState.Modified;            
        }              

        private IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var property in navigationProperties)
            {
                query = query.Include(property);
            }
            return query;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //private IQueryable<TEntity> OrderBy(IQueryable<TEntity> query, EOrderBy orderBy, string propertyName, IComparer<object> comparer)
        //{
        //    if (orderBy == EOrderBy.Asc)
        //        return query.OrderBy(propertyName, comparer);
        //    else
        //        return query.OrderByDescending(propertyName, comparer);
        //}

    }
}
