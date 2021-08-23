using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Models.Plant.Taxonomy;

namespace HousePlants.Pages.Genuses
{
    public class IndexModel : PageModel
    {
        private readonly HousePlantsDbContext _dbContext;

        public IndexModel(HousePlantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Family> Family { get;set; }

        public async Task OnGetAsync()
        {
            Family = await _dbContext.Families.ToListAsync();
        }
    }
}
