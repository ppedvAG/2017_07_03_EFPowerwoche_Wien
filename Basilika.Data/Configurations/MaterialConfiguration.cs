using Basilika.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Basilika.Data.Configurations
{
    internal class MaterialConfiguration : EntityTypeConfiguration<Material>
    {
        public MaterialConfiguration()
        {
            ToTable("Materialien");

            Property(m => m.Id)
                .HasColumnName("MaterialId");

            Property(m => m.Name)
                .IsUnicode(false);
        }
    }
}
