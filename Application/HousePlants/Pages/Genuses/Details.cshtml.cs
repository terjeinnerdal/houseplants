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
    public class DetailsModel : PageModel
    {
        private readonly HousePlantsDbContext _dbContext;

        public DetailsModel(HousePlantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
    }
}
