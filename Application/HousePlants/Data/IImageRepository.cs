using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HousePlants.Data
{
    public interface IImageRepository
    {
        Task<Guid> SaveImageAsync(IFormFile formFile);
    }
}