using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HousePlants.Pages.Plants
{
    public class UploadImagesModel : PageModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageRepository _imageRepository;

        public UploadImagesModel(IWebHostEnvironment webHostEnvironment, IImageRepository imageRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _imageRepository = imageRepository;
        }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task OnPostAsync()
        {
            // 1. Always validate the file types 
            ImageValidator.ValidateContentType(UploadedFile.ContentType);
            //  - and extension
            ImageValidator.ValidateExtensionAsJpegOrPng(UploadedFile.FileName);

            // 2. Rename the file and use safe name. Save image will create a Guid as Id
            await _imageRepository.SaveImageAsync(UploadedFile);

            // 3. Do not upload into app tree folder. Saved to c:\temp\uploads now, but this will not work under Linux.

            // 4. Validate files in both client and server side. It is easy to break client validation.
            // 5. Limit the file size while uploading. Always check the file size before upload
            // 6. Only allow the required file extension (exclude like exe) - Fixed in ImageValidator.ValidateExtensionsAsJpegOrPng
            // 7. Do not replace or overwrite the existing file with new one - Either new Id here or handle it in IImageRepository
            // 8. Use a virus/Malware scanner to the file before upload - How?
            // 9. Validate user before uploading files - Only logged in users can upload files
            // 10. Always grant read and write permission to the location but never execute permission

        }
    }
}
