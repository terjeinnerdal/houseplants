using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HousePlants.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Models.Plant.Taxonomy;

namespace HousePlants.Pages.Genuses
{
    public class DeleteModel : PageModel
    {
        private readonly HousePlantsDbContext _dbContext;

        public DeleteModel(HousePlantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Family Family { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _dbContext.Families.FirstOrDefaultAsync(m => m.Id == id);

            if (Family == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _dbContext.Families.FindAsync(id);

            if (Family != null)
            {
                _dbContext.Families.Remove(Family);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
