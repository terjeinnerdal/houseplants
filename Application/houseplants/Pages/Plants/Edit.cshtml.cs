using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HousePlants.Data;
using HousePlants.Domain;
using HousePlants.Domain.Models;
using HousePlants.Domain.Models.Requirements;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace HousePlants.Pages.Plants
{
    public class EditModel : PageModel
    {
        private readonly HousePlantsContext _context;

        public EditModel(HousePlantsContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Plant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(Plant.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToPage("./Index");
        }

        private bool PlantExists(Guid id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }

        public LightRequirement[] GetSelectedLightRequirements(LightRequirement lightRequirement)
        {
            var result = new List<LightRequirement>();
            foreach(LightRequirement r in Enum.GetValues(typeof(LightRequirement)))
            {
                if ((lightRequirement & r) != 0)
                {
                    result.Add(r);
                }
            }

            return result.ToArray();
        }
    }
}
