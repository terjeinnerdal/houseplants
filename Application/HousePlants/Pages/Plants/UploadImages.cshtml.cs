using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HousePlants.Models;
using HousePlants.Models.FileUpload;
using HousePlants.Models.Plant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HousePlants.Pages.Plants
{
    public class UploadImagesModel : PageModel
    {
        private readonly ILogger<Models.FileUpload.IndexModel> _logger;
        private readonly string _fullPath = System.AppDomain.CurrentDomain.BaseDirectory + "uploads";

        public UploadImagesModel(ILogger<Models.FileUpload.IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty] public FileUpload FileUpload { get; set; }

        public void OnGet()
        {
            ViewData["SuccessMessage"] = "";
        }

        public IActionResult OnPostUpload(FileUpload fileUpload)
        {
            if (!Directory.Exists(_fullPath))
            {
                Directory.CreateDirectory(_fullPath);
            }

            foreach (var aformFile in fileUpload.FormFiles)
            {
                ImageValidator.ValidateContentType(aformFile.ContentType);
                ImageValidator.ValidateExtensionAsJpegOrPng(aformFile.FileName);

                var formFile = aformFile;
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(_fullPath, formFile.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        formFile.CopyToAsync(stream);
                    }

                    // File upload to database
                    //Getting FileName
                    var fileName = Path.GetFileName(formFile.FileName);
                    //Getting file Extension
                    var fileExtension = Path.GetExtension(fileName);
                    // concatenating  FileName + FileExtension
                    var newFileName = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);

                    var documentViewmodel = new DocumentViewmodel()
                    {
                        Id = 0,
                        FileName = newFileName,
                        FileType = fileExtension,
                        Created = DateTime.Now,
                        Modified = DateTime.Now
                    };

                    using (var target = new MemoryStream())
                    {
                        formFile.CopyTo(target);
                        documentViewmodel.FileData = target.ToArray();
                    }

                    // use this documentViewmodel to save record in database 
                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.
            ViewData["SuccessMessage"] = $"{fileUpload.FormFiles.Count} files uploaded!";
            return Page();
        }


//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;

//namespace HousePlants.Pages.Plants
//{
//    public class UploadImagesModel : PageModel
//    {
//        private readonly IWebHostEnvironment _webHostEnvironment;
//        private readonly IImageRepository _imageRepository;

//        public UploadImagesModel(IWebHostEnvironment webHostEnvironment, IImageRepository imageRepository)
//        {
//            _webHostEnvironment = webHostEnvironment;
//            _imageRepository = imageRepository;
//        }

//        [BindProperty]
//        public IFormFile UploadedFile { get; set; }

//        public async Task OnPostAsync()
//        {
//            // 1. Always validate the file types 
//            ImageValidator.ValidateContentType(UploadedFile.ContentType);
//            //  - and extension
//            ImageValidator.ValidateExtensionAsJpegOrPng(UploadedFile.FileName);

//            // 2. Rename the file and use safe name. Save image will create a Guid as Id
//            await _imageRepository.SaveImageAsync(UploadedFile);

//            // 3. Do not upload into app tree folder. Saved to c:\temp\uploads now, but this will not work under Linux.

//            // 4. Validate files in both client and server side. It is easy to break client validation.
//            // 5. Limit the file size while uploading. Always check the file size before upload
//            // 6. Only allow the required file extension (exclude like exe) - Fixed in ImageValidator.ValidateExtensionsAsJpegOrPng
//            // 7. Do not replace or overwrite the existing file with new one - Either new Id here or handle it in IImageRepository
//            // 8. Use a virus/Malware scanner to the file before upload - How?
//            // 9. Validate user before uploading files - Only logged in users can upload files
//            // 10. Always grant read and write permission to the location but never execute permission

//        }
//    }
//}
    }
}