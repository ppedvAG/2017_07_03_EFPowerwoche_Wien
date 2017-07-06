using Basilika.Core.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Basilika.Data
{
    public class BlutContext : DbContext
    {
        public BlutContext() : base("name=BlutConnectionString")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Blutprobe> Blutproben { get; set; }
        public DbSet<Material> Materialien { get; set; }
        public DbSet<Untersuchung> Untersuchungen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Configurations.BlutprobeConfiguration());
            modelBuilder.Configurations.Add(new Configurations.MaterialConfiguration());
            modelBuilder.Configurations.Add(new Configurations.UntersuchungConfiguration());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Properties<string>()
                .Configure(c => c.IsRequired().HasMaxLength(50));
            modelBuilder.Properties<string>()
                .Where(p => p.Name.Equals("beschreibung", StringComparison.InvariantCultureIgnoreCase))
                .Configure(c => c.IsOptional().IsMaxLength());
        }
    }
}
