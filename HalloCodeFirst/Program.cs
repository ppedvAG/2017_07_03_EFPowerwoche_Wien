using HalloCodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tynamix.ObjectFiller;

namespace HalloCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            PreLoading();

            Console.WriteLine("Console Fertig.");
            Console.ReadKey();
        }

        public static void PreLoading()
        {
            using (var context = new BlutContext())
            {
                var proben = context.Blutproben.Take(20).ToList();

                context.Untersuchungen.Where(u => u.ProbeId <= 20).ToList();

                foreach (var probe in proben)
                {
                    Console.WriteLine($"{probe.Datum} - {probe.LFBIS}");
                    foreach (var u in probe.Untersuchungen)
                        Console.WriteLine($"\t{u.Keim,-8} - {u.Datum}");
                }
            }
        }
        public static void EagerLoading()
        {
            using (var context = new BlutContext())
            {
                var proben = context.Blutproben.Include(p => p.Untersuchungen).Take(20).ToList();

                foreach (var probe in proben)
                {
                    Console.WriteLine($"{probe.Datum} - {probe.LFBIS}");
                    foreach (var u in probe.Untersuchungen)
                        Console.WriteLine($"\t{u.Keim,-8} - {u.Datum}");
                }
            }
        }
        public static void LazyLoading()
        {
            using (var context = new BlutContext())
            {
                context.Configuration.LazyLoadingEnabled = true;        // Default

                var proben = context.Blutproben.Take(20).ToList();

                foreach (var probe in proben)
                {
                    Console.WriteLine($"{probe.Datum} - {probe.LFBIS}");
                    foreach (var u in probe.Untersuchungen)
                        Console.WriteLine($"\t{u.Keim,-8} - {u.Datum}");
                }
            }
        }

        private static void IQueryableExample()
        {
            using (var context = new BlutContext())
            {
                var proben = context.Blutproben.Where(b => b.Datum.Year <= 2010);
                proben = proben.OrderBy(p => p.Datum.Year);
                proben = proben.Where(p => p.Datum.Year > 2005);

                foreach (var p in proben.ToList())
                    Console.WriteLine($"{p.LFBIS} | {p.Datum.ToString("dd.MM.yyyy")}");
            }
        }

        private static void LetztePositiveUntersuchung(int probenId)
        {
            using (var context = new BlutContext())
            {
                var letztePositiveUntersuchung = context.Untersuchungen
                    .Where(u => u.ProbeId == probenId)
                    .Where(u => u.Keim == "Positiv")
                    .OrderByDescending(u => u.Datum)
                    .FirstOrDefault();

                Console.WriteLine($"{letztePositiveUntersuchung.Datum} - {letztePositiveUntersuchung.Beschreibung}");
            }
        }

        private static async void CreateSampleData()
        {
            var probenFiller = new Filler<Blutprobe>();
            probenFiller.Setup()
                .OnProperty(p => p.Id).IgnoreIt()
                .OnProperty(p => p.Untersuchungen).IgnoreIt()
                .OnProperty(p => p.Materialien).IgnoreIt()
                .OnProperty(p => p.Datum).Use(new DateTimeRange(earliestDate: DateTime.Now.AddYears(-20)))
                .OnProperty(p => p.LFBIS).Use(new PatternGenerator("{C:1000000}"));

            var untersuchungenFiller = new Filler<Untersuchung>();
            untersuchungenFiller.Setup()
                .OnProperty(u => u.Id).IgnoreIt()
                .OnProperty(u => u.Probe).IgnoreIt()
                .OnProperty(u => u.ProbeId).IgnoreIt()
                .OnProperty(u => u.Datum).Use(new DateTimeRange(earliestDate: DateTime.Now.AddYears(-20)))
                .OnProperty(u => u.Keim).Use(new[] { "Positiv", "Negativ" })
                .OnProperty(u => u.Beschreibung).Use(new Lipsum(LipsumFlavor.InDerFremde));

            var proben = probenFiller.Create(500);

            using (var context = new BlutContext())
            {
                var materialien = context.Materialien.ToList();

                var random = new Random();
                foreach (var p in proben)
                {
                    for (int i = 0; i < random.Next(0, materialien.Count); i++)
                    {
                        var randomMaterial = materialien.GetRandom();
                        randomMaterial.Blutproben.Add(p);
                    }

                    var untersuchungen = untersuchungenFiller.Create(random.Next(1, 200));
                    foreach (var u in untersuchungen)
                        p.Untersuchungen.Add(u);
                }

                await context.SaveChangesAsync();
                Console.WriteLine("Speichern Fertig!");
            }
        }
    }

    public static class EnumerableExtentions
    {
        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            var random = new Random();
            var randomIndex = random.Next(0, source.Count() - 1);

            return source.ElementAt(randomIndex);
        }
    }
}
