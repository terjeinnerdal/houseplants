using System;
using System.Threading.Tasks;
using HousePlants.Areas.Identity.Data;
using HousePlants.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Models;
using HousePlants.Models.Plant;

namespace HousePlants.Pages.Plants
{
    public class DeleteModel : PageModel
    {
        private readonly HousePlantsDbContext _dbContext;

        public DeleteModel(HousePlantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Plant = await _dbContext.Plants.FindAsync(id);

            if (Plant != null)
            {
                _dbContext.Plants.Remove(Plant);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
