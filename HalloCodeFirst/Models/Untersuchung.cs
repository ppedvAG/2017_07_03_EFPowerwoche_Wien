using System;

namespace HalloCodeFirst.Models
{
    public class Untersuchung : Entity
    {
        public string Keim { get; set; }
        public DateTime Datum { get; set; }
        public string Beschreibung { get; set; }

        public int ProbeId { get; set; }
        public Blutprobe Probe { get; set; }
    }
}
