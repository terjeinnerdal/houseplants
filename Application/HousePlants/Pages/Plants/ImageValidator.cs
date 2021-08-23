using System;
using System.IO;
using System.Linq;
using HousePlants.Infrastructure.Exceptions;

namespace HousePlants.Pages.Plants
{
    public static class ImageValidator
    {
        public static string[] AllowedExtensions => new[] {"jpg", "jpeg", "png"};

        public static void ValidateContentType(string contentType)
        {
            if (contentType != "image/png" && contentType != "image/jpg" && contentType != "image/jpeg")
            {
                throw new ForbiddenContentTypeException(contentType);
            }
        }


        public static void ValidateExtensionAsJpegOrPng(string fileName)
        {
            var extension = Path.GetExtension(fileName).Trim('.');

            if (string.IsNullOrEmpty(extension))
            {
                throw new NoFileExtensionException(fileName);
            }

            if (!AllowedExtensions.Contains(extension))
            {
                throw new ForbiddenFileExtensionException(fileName);
            }
        }
    }
}