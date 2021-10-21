using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousePlants.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HousePlants.Data;
using HousePlants.Models.Plant.Taxonomy;

namespace HousePlants.Pages.Genuses
{
    public class CreateModel : PageModel
    {
        private readonly HousePlantsDbContext _dbContext;

        public CreateModel(HousePlantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Family Family { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Families.Add(Family);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
