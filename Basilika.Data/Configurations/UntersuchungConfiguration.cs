using Basilika.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Basilika.Data.Configurations
{
    internal class UntersuchungConfiguration : EntityTypeConfiguration<Untersuchung>
    {
        public UntersuchungConfiguration()
        {
            ToTable("Untersuchungen");

            Property(u => u.Id)
                .HasColumnName("UntersuchungsId");

            Property(u => u.Keim)
                .IsRequired()
                .HasMaxLength(50);

            Property(u => u.Datum)
                .HasColumnType("date");
        }
    }
}
