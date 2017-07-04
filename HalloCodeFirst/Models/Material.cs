using HalloCodeFirst.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HalloCodeFirst.Models
{
    [Table("Materialien")]
    public class Material : Entity
    {
        //[Key]
        //[Column("MaterialId")]
        //// [NotMapped] -> Property wird von EF ignoriert
        //public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        //[Column(TypeName = "varchar")]
        //[NonUnicode]
        [IsUnicode(false)]
        public string Name { get; set; }

        public string Beschreibung { get; set; }

        public ICollection<Blutprobe> Blutproben { get; } = new HashSet<Blutprobe>();
    }
}
