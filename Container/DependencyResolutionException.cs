using System;
using System.Runtime.Serialization;

namespace DeveloperSample.Container
{
    [Serializable]
    internal class DependencyResolutionException : Exception
    {
        public DependencyResolutionException()
        {
        }

        public DependencyResolutionException(string message) : base(message)
        {
        }

        public DependencyResolutionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DependencyResolutionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}