using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Domain.Models;

namespace HousePlants.Pages.Plants
{
    public class IndexModel : PageModel
    {
        private readonly HousePlantsContext _context;

        public IndexModel(HousePlantsContext context)
        {
            _context = context;
        }

        public IList<Plant> Plant { get;set; }

        public async Task OnGetAsync()
        {
            Plant = await _context.Plants.ToListAsync();
        }
    }
}
