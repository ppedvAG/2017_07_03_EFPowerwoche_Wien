using System.Collections.Generic;

namespace Basilika.Core.Models
{
    public class Material : Entity
    {
        public string Name { get; set; }
        public string Beschreibung { get; set; }

        public ICollection<Blutprobe> Blutproben { get; } = new HashSet<Blutprobe>();
    }
}
