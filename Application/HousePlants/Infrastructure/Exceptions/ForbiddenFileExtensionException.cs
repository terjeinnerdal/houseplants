using System;

namespace HousePlants.Infrastructure.Exceptions
{
    public class ForbiddenFileExtensionException : Exception
    {
        public ForbiddenFileExtensionException(string extension) : base($"The file extension {extension} is not valid")
        {
        }
    }
}