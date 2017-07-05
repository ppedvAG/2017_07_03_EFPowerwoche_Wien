using System.Data.Entity;

namespace Inheritance
{
    public class InheritanceContext : DbContext
    {
        public InheritanceContext() 
            : base("Data Source=.;Initial Catalog=InheritanceDb;Integrated Security=True")
        { }
    }
}
