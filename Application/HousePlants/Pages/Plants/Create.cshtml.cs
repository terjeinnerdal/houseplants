using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using HousePlants.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HousePlants.Data;
using HousePlants.Models;
using HousePlants.Models.Plant;
using HousePlants.Models.Plant.Requirements;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace HousePlants.Pages.Plants
{
    public sealed class CreatePlantVM
    {
        public string CommonName { get; set; }
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
        private readonly HousePlantsDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateModel(ILogger<CreateModel> logger, HousePlantsDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
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
            await _dbContext.Plants.AddAsync(plant);
            await _dbContext.SaveChangesAsync();

            var createdEvent = new
            {
                User = "tnnerdal", Event = "Created", Plant = CreatePlantVmModel.CommonName
            };

            _logger.LogInformation("Created new plant {@Plant}", createdEvent);

            return RedirectToPage("./Index");
        }
    }
}
