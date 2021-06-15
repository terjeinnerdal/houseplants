using System;

namespace HousePlants.Exceptions
{
    public class ForbiddenFileExtensionException : Exception
    {
        public ForbiddenFileExtensionException(string extension) : base($"The file extension {extension} is not valid")
        {
        }
    }
}