using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HalloCodeFirst.Conventions
{
    public class DateConvention : Convention
    {
        public DateConvention()
        {
            Properties<DateTime>()
                .Configure(c => c.HasColumnType("date"));
        }
    }
}
