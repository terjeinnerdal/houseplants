using System;
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
    public class EditModel : PageModel
    {
        private readonly HousePlantsDbContext _dbContext;

        public EditModel(HousePlantsDbContext dbContext)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Attach(Family).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(Family.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FamilyExists(Guid id)
        {
            return _dbContext.Families.Any(e => e.Id == id);
        }
    }
}
