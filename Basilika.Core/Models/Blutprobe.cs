using System;
using System.Collections.Generic;

namespace Basilika.Core.Models
{
    public class Blutprobe : Entity
    {
        public string Wurscht { get; private set; }

        public string LFBIS { get; set; }
        public DateTime Datum { get; set; }

        public ICollection<Material> Materialien { get; } = new HashSet<Material>();
        public ICollection<Untersuchung> Untersuchungen { get; } = new HashSet<Untersuchung>();
    }
}
