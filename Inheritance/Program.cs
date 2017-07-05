using Inheritance.Models;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            TPH();
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
