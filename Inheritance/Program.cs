using System;
using Inheritance.Models;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            TPC();
        }

        private static void TPC()
        {
            using (var context = new InheritanceContext())
            {
                // Nachteil: Muss Ids selber vergeben

                var t = new Tisch { Id = 1, Material = "Holz", AnzahlFuesse = 7 };
                var u = new Uhr { Id = 2, Material = "Plastik", Uhrzeit = DateTime.Now };

                context.Produkte.Add(t);
                context.Produkte.Add(u);

                context.SaveChanges();
            }
        }
        private static void TPT()
        {
            using (var context = new InheritanceContext())
            {
                var p = new PKW { Geschwindigkeit = 100, Sitzplaetzte = 5 };
                var l = new LKW { Geschwindigkeit = 80, MaxLadung = 10000 };

                context.Fahrzeuge.Add(p);
                context.Fahrzeuge.Add(l);

                context.SaveChanges();
            }
        }
        private static void TPH()
        {
            using (var context = new InheritanceContext())
            {
                var c = new Customer { Name = "Susi", Address = "Wien" };
                var e = new Employee { Name = "Hansi", Salary = 30000 };

                context.People.Add(c);
                context.People.Add(e);

                context.SaveChanges();
            }
        }
    }
}
