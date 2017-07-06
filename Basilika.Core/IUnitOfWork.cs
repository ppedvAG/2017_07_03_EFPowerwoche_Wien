using Basilika.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Basilika.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBlutprobeRepository Blutproben { get; }
        IMaterialRepository Materialien { get; }
        IUntersuchungRepository Untersuchungen { get; }

        void Complete();
        Task CompleteAsync();
    }
}
