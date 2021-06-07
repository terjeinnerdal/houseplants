using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HousePlants.Data;
using HousePlants.Models;
using HousePlants.Models.Requirements;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace HousePlants.Pages.Plants
{
    public sealed class CreatePlantVM
    {
        [Required, StringLength(128)] 
        public string CommonName { get; set; }
        [DataType(DataType.MultilineText), StringLength(20000)]
        public string Description { get; set; }
        public LightRequirement LightRequirement { get; set; }
        public WateringTechnique WateringTechnique { get; set; }
    }

    //public class CreatePlantVMValidator : AbstractValidator<CreatePlant>
    //{
    //    public CreatePlantValidator()
    //    {
    //        RuleFor(p => p.CommonName).NotEmpty().MaximumLength(128);
    //        RuleFor(p => p.Description).MaximumLength(20000);
    //    }
    //}

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePlantVM, Plant>();
        }
    }



    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private readonly HousePlantsContext _context;
        private readonly IMapper _mapper;

        public CreateModel(ILogger<CreateModel> logger, HousePlantsContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [BindProperty]
        public CreatePlantVM CreatePlantVmModel { get; set; }

        public IActionResult OnGet()
        {
            CreatePlantVmModel ??= new CreatePlantVM();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var plant = _mapper.Map<Plant>(CreatePlantVmModel);
            await _context.Plants.AddAsync(plant);
            await _context.SaveChangesAsync();

            var createdEvent = new
            {
                User = "tnnerdal", Event = "Created", Plant = CreatePlantVmModel.CommonName
            };

            _logger.LogInformation("Created new plant {@Plant}", createdEvent);

            return RedirectToPage("./Index");
        }
    }
}
