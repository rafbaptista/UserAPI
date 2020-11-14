using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WevoTest.Domain.Enums;
using WevoTest.Domain.Interfaces.Repositories;
using WevoTest.Domain.Interfaces.Services;

namespace WevoTest.Domain.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Add(TEntity obj)
        {
            _repository.Add(obj);            
        }

        public virtual void Delete(TEntity obj)
        {
            _repository.Delete(obj);
        }

        public TEntity Find(int id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {            
            return _repository.GetAll();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return _repository.FindBy(predicate, navigationProperties);
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return _repository.QueryBy(predicate, navigationProperties);
        }

        public virtual void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Count(predicate);
        }

        public IQueryable<TEntity> QueryBy(Expression<Func<TEntity, bool>> predicate, EOrderBy orderBy, string orderByPropertyName, IComparer<object> comparer, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return _repository.QueryBy(predicate, orderBy, orderByPropertyName, comparer, navigationProperties);            
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, EOrderBy orderBy, string orderByPropertyName, IComparer<object> comparer, params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            return _repository.FindBy(predicate, orderBy, orderByPropertyName, comparer, navigationProperties);
        }
    }
}
