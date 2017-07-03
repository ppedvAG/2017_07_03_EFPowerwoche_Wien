using System.Linq;

namespace HalloLinqToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthwindClassesDataContext())
            {
                var employees = context.Employees.First();
                employees.FirstName = "Stanislaus";
                context.SubmitChanges();

                //var sepp = new Employee { FirstName = "Sepp", LastName = "Huber" };
                //context.Employees.InsertOnSubmit(sepp);
                //context.SubmitChanges();


                //var sepp = context.Employees.FirstOrDefault(e => e.FirstName == "Sepp" && e.LastName == "Huber");
                //context.Employees.DeleteOnSubmit(sepp);
                //context.SubmitChanges();
            }
        }
    }
}
