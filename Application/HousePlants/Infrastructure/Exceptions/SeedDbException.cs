using System;
using System.Runtime.Serialization;

namespace HousePlants.Infrastructure.Exceptions
{
    [Serializable]
    public class SeedDbException : Exception
    {
        public SeedDbException()
        {
        }

        public SeedDbException(string message) : base(message)
        {
        }

        public SeedDbException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SeedDbException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}