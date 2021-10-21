using Microsoft.AspNetCore.Identity;

namespace HousePlants
{
    public interface IUserOwnedEntity 
    {
        public string OwnerId { get; set; } 
        public IdentityUser Owner { get; set; }
    }
}