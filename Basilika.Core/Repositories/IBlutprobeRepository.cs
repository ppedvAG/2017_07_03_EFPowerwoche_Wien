using Basilika.Core.Models;

namespace Basilika.Core.Repositories
{
    public interface IBlutprobeRepository : IRepository<Blutprobe>
    {
        Blutprobe GetWithMaterialien(int id);
    }
}
