#nullable enable
namespace HousePlants.Models.Interfaces
{
    public interface IUserOwnedEntity<T>
    {
        T Owner { get; set; }
    }
}