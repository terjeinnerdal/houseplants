using System;
using System.Threading.Tasks;
using HousePlants.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Models;

namespace HousePlants.Pages.Plants
{
    public class DeleteModel : PageModel
    {
        private readonly HousePlantsContext _context;

        public DeleteModel(HousePlantsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Plant Plant { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Plant = await _context.Plants.FirstOrDefaultAsync(m => m.Id == id);

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

            Plant = await _context.Plants.FindAsync(id);

            if (Plant != null)
            {
                _context.Plants.Remove(Plant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
