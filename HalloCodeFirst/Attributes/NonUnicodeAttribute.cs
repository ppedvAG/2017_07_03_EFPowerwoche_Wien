using System;

namespace HalloCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NonUnicodeAttribute : Attribute
    { }
}
