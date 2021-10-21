#nullable enable
namespace HousePlants.Models.Interfaces
{
    public interface ISecretEntity
    {
        void Encrypt();
        void Decrypt();
    }
}