using HalloCodeFirst.Conventions;
using HalloCodeFirst.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HalloCodeFirst
{
    public class BlutContext : DbContext
    {
        public BlutContext() : base("name=BlutConnectionString")
        { }

        public DbSet<Blutprobe> Blutproben { get; set; }
        public DbSet<Material> Materialien { get; set; }
        public DbSet<Untersuchung> Untersuchungen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Configurations.BlutprobeConfiguration());
            modelBuilder.Configurations.Add(new Configurations.UntersuchungConfiguration());
            modelBuilder.Entity<Material>().Property(m => m.Id).HasColumnName("MaterialId");

            modelBuilder.Conventions.Add<DateConvention>();
            modelBuilder.Conventions.Add<IsUnicodeAttributeConvention>();
            modelBuilder.Conventions.Add<NonUnicodeAttributeConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<string>()
                .Configure(c => c.IsRequired().HasMaxLength(50));
            modelBuilder.Properties<string>()
                .Where(p => p.Name.Equals("beschreibung", StringComparison.InvariantCultureIgnoreCase))
                .Configure(c => c.IsOptional().IsMaxLength());
        }
    }
}
