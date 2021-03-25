using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HousePlants.Data;
using HousePlants.Domain.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HousePlants.Pages.Plants
{
    public class CreatePlant
    {
        [StringLength(128)] public string CommonName { get; set; } = default!;
        [StringLength(128)] public string? LatinName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Date)]
        [Display(Prompt = "When did you aquire the plant")]
        public DateTime? AquiredDate { get; set; }
        public LightRequirement LightRequirement { get; set; }
        public WaterRequirement WaterRequirement { get; set; }
    }

    public class CreateModel : PageModel
    {
        private readonly HousePlants.Data.HousePlantsContext _context;
        private readonly IMapper _mapper;

        public CreateModel(HousePlants.Data.HousePlantsContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            if(CreatePlantModel == null)
                CreatePlantModel = new CreatePlant();

            return Page();
        }

        [BindProperty]
        public CreatePlant CreatePlantModel { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var plant = _mapper.Map<Plant>(CreatePlantModel);
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
