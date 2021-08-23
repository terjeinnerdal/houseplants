using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Models;
using HousePlants.Models.Plant;
using HousePlants.Models.Plant.Requirements;

namespace HousePlants.Pages.Plants
{
    public class EditModel : PageModel
    {
        private readonly HousePlantsDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(HousePlantsDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        [BindProperty]
        public Plant Plant { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Plant = await _dbContext.Plants.FirstOrDefaultAsync(m => m.Id == id);
            
            if (Plant == null)
            {
                return NotFound();
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Attach(Plant).State = EntityState.Modified;
            var file = Path.Combine(_webHostEnvironment.ContentRootPath, "uploads", UploadedFile.FileName);
            await using var fileStream = new FileStream(file, FileMode.Create);
            await UploadedFile.CopyToAsync(fileStream);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(Plant.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool PlantExists(Guid id)
        {
            return _dbContext.Plants.Any(e => e.Id == id);
        }

        public LightRequirement[] GetSelectedLightRequirements(LightRequirement lightRequirement)
        {
            var result = new List<LightRequirement>();
            foreach(LightRequirement r in Enum.GetValues(typeof(LightRequirement)))
            {
                if ((lightRequirement & r) != 0)
                {
                    result.Add(r);
                }
            }

            return result.ToArray();
        }
    }
}
