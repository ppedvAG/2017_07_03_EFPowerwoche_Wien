using System;
using System.Collections.Generic;

namespace HalloCodeFirst.Models
{
    public class Blutprobe : Entity
    {
        public string LFBIS { get; set; }
        public DateTime Datum { get; set; }

        public ICollection<Material> Materialien { get; } = new HashSet<Material>();
        public ICollection<Untersuchung> Untersuchungen { get; } = new HashSet<Untersuchung>();
    }
}
