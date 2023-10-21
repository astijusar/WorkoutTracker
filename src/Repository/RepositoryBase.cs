using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using Repository.Models;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationContext RepositoryContext;

        protected RepositoryBase(ApplicationContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public virtual void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            RepositoryContext.Set<T>().Update(entity);
        }

        public virtual void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public virtual IQueryable<T> FindAll(bool trackChanges)
        {
            return !trackChanges ?
                RepositoryContext.Set<T>()
                    .AsNoTracking() :
                RepositoryContext.Set<T>();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
                RepositoryContext.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                RepositoryContext.Set<T>()
                    .Where(expression);
        }
    }
}
