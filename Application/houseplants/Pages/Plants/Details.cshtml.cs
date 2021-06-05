using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Domain;
using HousePlants.Domain.Models;

namespace HousePlants.Pages.Plants
{
    public class DetailsModel : PageModel
    {
        private readonly HousePlantsContext _context;

        public DetailsModel(HousePlantsContext context)
        {
            _context = context;
        }

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
    }
}
