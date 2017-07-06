using Basilika.Core;
using Basilika.Core.Repositories;
using Basilika.Data.Repositories;
using System.Threading.Tasks;

namespace Basilika.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlutContext _context;

        public IBlutprobeRepository Blutproben { get; }
        public IMaterialRepository Materialien { get; }
        public IUntersuchungRepository Untersuchungen { get; }

        public UnitOfWork(BlutContext context)
        {
            _context = context;
            Blutproben = new BlutprobeRepository(context);
            Materialien = new MaterialRepository(context);
            Untersuchungen = new UntersuchungsRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
        public Task CompleteAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
