using HalloCodeFirst.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HalloCodeFirst.Configurations
{
    internal class BlutprobeConfiguration : EntityTypeConfiguration<Blutprobe>
    {
        public BlutprobeConfiguration()
        {
            ToTable("BlutProben");

            //HasKey(b => new { b.Id, b.LFBIS });
            HasKey(b => b.Id);

            Property(b => b.Wurscht)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed)
                .IsMaxLength();

            Property(b => b.Id)
                .HasColumnName("ProbenId");

            Property(b => b.LFBIS)
                .IsRequired()
                .HasMaxLength(7);

            Property(b => b.Datum)
                .IsRequired()
                .HasColumnType("date");

            HasMany(b => b.Untersuchungen)
                .WithRequired(u => u.Probe)
                .HasForeignKey(u => u.ProbeId)
                .WillCascadeOnDelete(true);     // Default true

            HasMany(b => b.Materialien)
                .WithMany(m => m.Blutproben)
                .Map(map =>
                {
                    map.ToTable("BlutprobenMaterialien");
                    map.MapLeftKey("ProbenId");
                    map.MapRightKey("MaterialId");
                });
        }
    }
}
