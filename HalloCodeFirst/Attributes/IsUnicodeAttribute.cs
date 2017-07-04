using System;

namespace HalloCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IsUnicodeAttribute : Attribute
    {
        public IsUnicodeAttribute(bool isUnicode) => IsUnicode = isUnicode;

        public bool IsUnicode { get; }
    }
}
