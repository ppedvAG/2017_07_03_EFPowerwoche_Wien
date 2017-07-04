using HalloCodeFirst.Attributes;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;

namespace HalloCodeFirst.Conventions
{
    public class NonUnicodeAttributeConvention : Convention
    {
        public NonUnicodeAttributeConvention()
        {
            Properties<string>()
                .Where(p => p.GetCustomAttributes().OfType<NonUnicodeAttribute>().Any())
                .Configure(c => c.IsUnicode(false));
        }
    }
}
