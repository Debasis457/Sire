using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Sire.Domain.Context;
using Sire.Helper;
using Microsoft.EntityFrameworkCore;

namespace Sire.Common.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : SireContext
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public UnitOfWork(TContext context, IJwtTokenAccesser jwtTokenAccesser)
        {
            Context = context;
            _jwtTokenAccesser = jwtTokenAccesser;
        }

        public int Save()
        {
           return Context.SaveChanges(_jwtTokenAccesser);
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync(_jwtTokenAccesser);
        }

        public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
        {
            return Context.Set<TEntity>().FromSql(sql, parameters);
        }

        public TContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Begin()
        {
            Context.Begin();
        }
        public void Commit()
        {
            Context.Commit();
        }

        public void Rollback()
        {
            Context.Rollback();
        }
    }
}