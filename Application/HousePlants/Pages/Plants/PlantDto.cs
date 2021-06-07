using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HousePlants.Pages.Plants
{
    public class PlantDto
    {
        [HiddenInput]
        public Guid Id { get; private set; }
        public string Title { get; set; }
        [StringLength(128)] public string LatinName { get; set; }
        [StringLength(128)] public string CommonName { get; set; }
        [DataType(DataType.Date)] public DateTime AquiredDate { get; set; }
        
        //List<string> Tags { get; set; }
        // public string TagsString
        // {
        //     get => string.Join(',', Tags);
        //     set
        //     {
        //         Tags = value != null
        //             ? TagsString.Split(',').Select(tag => tag.Trim()).ToList()
        //             : throw new ArgumentNullException(nameof(value));
        //     }
        // }
    }
}