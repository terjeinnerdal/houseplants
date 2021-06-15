using System;
using System.Linq;
using HousePlants.Exceptions;

namespace HousePlants.Pages.Plants
{
    public static class ImageValidator
    {
        public static string[] AllowedExtensions => new[] {"jpg", "jpeg", "png"};

        public static void ValidateContentType(string contentType)
        {
            if (contentType != "image/png" || contentType != "image/jpg")
            {
                throw new ForbiddenContentTypeException(contentType);
            }
        }


        public static void ValidateExtensionAsJpegOrPng(string fileName)
        {
            var extension = GetFileExtension(fileName);

            if (string.IsNullOrEmpty(extension))
            {
                throw new NoFileExtensionException(fileName);
            }

            if (!AllowedExtensions.Contains(extension))
            {
                throw new ForbiddenFileExtensionException(fileName);
            }
        }
        
        public static string GetFileExtension(string fileName)
        {
            if (fileName == null || string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"Specify {nameof(fileName)}", nameof(fileName));
            }

            string extension = string.Empty;
            if (fileName.Contains('.'))
            {
                extension = fileName.Substring(fileName.LastIndexOf('.'));
            }

            return extension;
        }
    }
}