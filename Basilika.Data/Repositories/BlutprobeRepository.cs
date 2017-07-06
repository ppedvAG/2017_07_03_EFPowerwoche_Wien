using Basilika.Core.Models;
using Basilika.Core.Repositories;
using System.Data.Entity;
using System.Linq;

namespace Basilika.Data.Repositories
{
    public class BlutprobeRepository : Repository<BlutContext, Blutprobe>, IBlutprobeRepository
    {
        public BlutprobeRepository(BlutContext context) 
            : base(context)
        { }

        public Blutprobe GetWithMaterialien(int id)
        {
            return Context.Blutproben
                .Include(b => b.Materialien)
                .SingleOrDefault(b => b.Id == id);
        }
    }
}
