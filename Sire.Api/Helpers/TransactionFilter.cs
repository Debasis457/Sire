using System.Linq;
using Sire.Common.UnitOfWork;
using Sire.Domain.Context;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sire.Api.Helpers
{
    public class TransactionFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork<SireContext> _uow;

        public TransactionFilter(IUnitOfWork<SireContext>  uow)
        {
           _uow = uow;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
           
            if (context.Filters.Any(t => t.GetType() == typeof(TransactionRequiredAttribute)))
            {
                if (context.Exception == null && context.ModelState.IsValid)
                    _uow.Commit();
                else
                    _uow.Rollback();
            }

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.Filters.Any(t => t.GetType() == typeof(TransactionRequiredAttribute)))
                return;
            _uow.Begin();
        }

     
    }

    public class TransactionRequiredAttribute : ActionFilterAttribute
    {
    }
}