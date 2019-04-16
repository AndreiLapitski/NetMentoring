using System;

namespace IoC.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExportAttribute : Attribute
    {
        public ExportAttribute(){}

        public ExportAttribute(Type contract)
        {
            Contract = contract;
        }

        public  Type Contract { get; }
    }
}
