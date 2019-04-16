using System;
using System.Runtime.Serialization;

namespace IoC.Exceptions
{
    public class DependencyException : Exception
    {
        public DependencyException()
        {
        }

        public DependencyException(string message) 
            : base(message)
        {
        }

        public DependencyException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        protected DependencyException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
