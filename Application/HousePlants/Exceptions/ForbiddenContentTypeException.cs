using System;

namespace HousePlants.Exceptions
{
    public class ForbiddenContentTypeException : Exception
    {
        public ForbiddenContentTypeException(string contentType)
            : base($"The content type {contentType} is not valid")
        {
        }
    }
}