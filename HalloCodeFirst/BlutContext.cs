﻿using HalloCodeFirst.Models;
using System.Data.Entity;

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
        }
    }
}