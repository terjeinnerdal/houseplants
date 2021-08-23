﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HousePlants.Models.FileUpload
{
    public class FileUpload
    {
        [Required]
        [Display(Name = "File")]
        public List<IFormFile> FormFiles { get; set; }
        public string SuccessMessage { get; set; }
    }

     public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly string fullPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "UploadImages";
       
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public FileUpload FileUpload { get; set; }

        public void OnGet()
        {
            ViewData["SuccessMessage"] = "";
        }
        
        public IActionResult OnPostUpload(FileUpload fileUpload)
        {
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            foreach (var aformFile in fileUpload.FormFiles)
            {
                var formFile = aformFile;
                if (formFile.Length > 0)
                {
                    var filePath = Path.Combine(fullPath, formFile.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        formFile.CopyToAsync(stream);
                    }

                    // File upload to database
                    var fileName = Path.GetFileName(formFile.FileName);
                    var fileExtension = Path.GetExtension(fileName);
                    var fileNameWithExtension = string.Concat(Convert.ToString(Guid.NewGuid()), fileExtension);
                    var documentViewmodel = new DocumentViewmodel()
                    {
                        Id = 0,
                        FileName = fileNameWithExtension,
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
            ViewData["SuccessMessage"] = fileUpload.FormFiles.Count.ToString() + " files uploaded!!";
            return Page();
        }
        

        //public IActionResult OnPostUpload(FileUpload fileUpload)
        //{
        //    if (!Directory.Exists(fullPath))
        //    {
        //        Directory.CreateDirectory(fullPath);
        //    }
        //    foreach (var aformFile in fileUpload.FormFiles)
        //    {
        //        var formFile = aformFile;
        //        if (formFile.Length > 0)
        //        {
        //            var filePath = Path.Combine(fullPath, formFile.FileName);

        //            using (var stream = System.IO.File.Create(filePath))
        //            {
        //                formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }

        //    // Process uploaded files
        //    // Don't rely on or trust the FileName property without validation.
        //    ViewData["SuccessMessage"] = fileUpload.FormFiles.Count.ToString() + " files uploaded!!";
        //    return Page();
        //}
    }

     public class DocumentViewmodel
     {      
        public int Id { get; set; }
        [MaxLength(250)]
        public string FileName { get; set; }
        [MaxLength(100)]
        public string FileType { get; set; }
        [MaxLength]
        public byte[] FileData { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}