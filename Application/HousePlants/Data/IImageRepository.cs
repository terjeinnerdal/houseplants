using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HousePlants.Pages.Plants
{
    public interface IImageRepository
    {
        Task<Guid> SaveImageAsync(IFormFile formFile);
    }
}