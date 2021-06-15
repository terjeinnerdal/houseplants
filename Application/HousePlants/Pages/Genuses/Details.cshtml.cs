﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly HousePlantsContext _context;

        public DetailsModel(HousePlantsContext context)
        {
            _context = context;
        }

        public Family Family { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Family = await _context.Families.FirstOrDefaultAsync(m => m.Id == id);

            if (Family == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}