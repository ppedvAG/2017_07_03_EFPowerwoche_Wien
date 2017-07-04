using System;
using System.Linq;

namespace HalloEntityDataModel
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var model = new NORTHWNDEntities())
            {
                var employees = model.Employees.ToList();

                foreach (var e in employees)
                    Console.WriteLine(e.FirstName);
            }

            Console.ReadKey();
        }
    }
}
