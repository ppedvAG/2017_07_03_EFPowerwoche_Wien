using Inheritance.Models;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            TPT();
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
