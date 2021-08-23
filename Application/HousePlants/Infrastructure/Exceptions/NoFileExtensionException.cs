using System;

namespace HousePlants.Infrastructure.Exceptions
{
    public class NoFileExtensionException : Exception
    {
        public NoFileExtensionException(string fileName) 
            : base($"{fileName} does not have an extension")
        {
        }
    }
}