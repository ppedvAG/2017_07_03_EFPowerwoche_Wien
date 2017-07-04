using HalloCodeFirst.Attributes;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace HalloCodeFirst.Conventions
{
    public class IsUnicodeAttributeConvention : Convention
    {
        public IsUnicodeAttributeConvention()
        {
            Properties<string>()
                .Having(p => p.GetCustomAttributes().OfType<IsUnicodeAttribute>().SingleOrDefault())
                .Configure((c, a) => c.IsUnicode(a.IsUnicode));
        }
    }
}
