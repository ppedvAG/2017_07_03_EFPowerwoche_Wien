using Basilika.Core.Models;
using Basilika.Core.Repositories;

namespace Basilika.Data.Repositories
{
    public class UntersuchungsRepository : Repository<BlutContext, Untersuchung>, IUntersuchungRepository
    {
        public UntersuchungsRepository(BlutContext context) 
            : base(context)
        { }
    }
}
