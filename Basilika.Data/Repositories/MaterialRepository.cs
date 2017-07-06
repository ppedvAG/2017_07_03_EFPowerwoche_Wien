using Basilika.Core.Models;
using Basilika.Core.Repositories;

namespace Basilika.Data.Repositories
{
    public class MaterialRepository : Repository<BlutContext, Material>, IMaterialRepository
    {
        public MaterialRepository(BlutContext context) 
            : base(context)
        { }
    }
}
