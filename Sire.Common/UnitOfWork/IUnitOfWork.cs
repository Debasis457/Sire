using System;
using System.Linq;
using System.Threading.Tasks;
using Sire.Domain.Context;

namespace Sire.Common.UnitOfWork
{
    public interface IUnitOfWork<TContext>:IDisposable
        where TContext : SireContext
    {
        TContext Context { get; }
        int Save();
        Task<int> SaveAsync();
        void Begin();
        void Commit();
        void Rollback();
        IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
    }
}