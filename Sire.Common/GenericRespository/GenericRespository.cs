using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sire.Common.UnitOfWork;
using Sire.Data.Entities.Common;
using Sire.Domain.Context;
using Sire.Helper;
using Microsoft.EntityFrameworkCore;

namespace Sire.Common.GenericRespository
{
    public class GenericRespository<TC, TContext> : IGenericRepository<TC>
        where TC : class
        where TContext : SireContext
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;
        protected readonly TContext Context;
        internal readonly DbSet<TC> DbSet;

        protected GenericRespository(IUnitOfWork<TContext> uow, IJwtTokenAccesser jwtTokenAccesser)
        {
            Context = uow.Context;
            DbSet = Context.Set<TC>();
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public IQueryable<TC> All => Context.Set<TC>();

        public void Add(TC entity)
        {
            Context.Add(entity);
        }

        public IQueryable<TC> AllIncluding(params Expression<Func<TC, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties);
        }

        public IEnumerable<TC> FindByInclude(Expression<Func<TC, bool>> predicate,
            params Expression<Func<TC, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<TC> results = query.Where(predicate).ToList();
            return results;
        }

        public IEnumerable<TC> FindBy(Expression<Func<TC, bool>> predicate)
        {
            var queryable = DbSet.AsNoTracking();
            IEnumerable<TC> results = queryable.Where(predicate).ToList();
            return results;
        }

        public async Task<IEnumerable<TC>> FindByAsync(Expression<Func<TC, bool>> predicate)
        {
            // IQueryable<TC> queryable = DbSet.AsNoTracking();
            IEnumerable<TC> results = await DbSet.AsNoTracking().Where(predicate).ToListAsync();
            return results;
        }

        public TC Find(int id)
        {
            return Context.Set<TC>().Find(id);
        }

        public async Task<TC> FindAsync(int id)
        {
            return await Context.Set<TC>().FindAsync(id);
        }

        public virtual void Update(TC entity)
        {
            Context.Update(entity);
        }

        public void InsertUpdateGraph(TC entity)
        {
            Context.Set<TC>().Add(entity);
            Context.ApplyStateChanges(_jwtTokenAccesser);
        }

        public virtual void Delete(int id)
        {
            var entity = Context.Set<TC>().Find(id);
            if (entity != null) Delete(entity);
        }

        public virtual void Delete(TC entityData)
        {
            var entity = entityData as BaseEntity;
            if (entity != null)
            {
                entity.DeletedBy = _jwtTokenAccesser.UserId;
                entity.DeletedDate = DateTime.Now.ToUniversalTime();
                Context.Update(entity);
            }
        }

        public virtual void Active(TC entityData)
        {
            var entity = entityData as BaseEntity;
            if (entity != null)
            {
                entity.DeletedBy = null;
                entity.DeletedDate = null;
                Context.Update(entity);
            }
        }

        private IQueryable<TC> GetAllIncluding(params Expression<Func<TC, object>>[] includeProperties)
        {
            var queryable = DbSet.AsNoTracking();

            return includeProperties.Aggregate
                (queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public virtual void Remove(TC entity)
        {
            Context.Remove(entity);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}