using Sire.Common.GenericRespository;
using Sire.Common.UnitOfWork;
using Sire.Data.Entities.ShipManagement;
using Sire.Domain.Context;
using Sire.Helper;

namespace Sire.Respository.ShipManagement
{
    public class Piq_HvpqRepository : GenericRespository<Piq_Hvpq, SireContext>, IPiq_HvpqRepository
    {
        private readonly IJwtTokenAccesser _jwtTokenAccesser;

        public Piq_HvpqRepository(IUnitOfWork<SireContext> uow,
            IJwtTokenAccesser jwtTokenAccesser)
            : base(uow, jwtTokenAccesser)
        {
            _jwtTokenAccesser = jwtTokenAccesser;
        }

    }
}
