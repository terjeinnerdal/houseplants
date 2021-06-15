using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace HousePlants.Pages.Plants
{
    public class DiskImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public DiskImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<Guid> SaveImageAsync(IFormFile formFile)
        {
            var id = Guid.NewGuid();
            string extension = ImageValidator.GetFileExtension(formFile.FileName);
            string fileAsBase64String = ConvertIFormFileToBase64String(formFile);
            string fileName = Path.Combine(_webHostEnvironment.ContentRootPath, $"{id}.{extension}");
            await File.WriteAllTextAsync(fileName, fileAsBase64String);
            return id;
        }

        public string ConvertIFormFileToBase64String(IFormFile file)
        {
            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return Convert.ToBase64String(fileBytes);
            }

            throw new EmptyFileException();
        }
    }
}