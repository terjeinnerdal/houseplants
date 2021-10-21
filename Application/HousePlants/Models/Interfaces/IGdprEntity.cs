#nullable enable
namespace HousePlants.Models.Interfaces
{
    public interface IGdprEntity
    {
        bool Anonymize();
        bool Delete();
        string GetRegisteredGdprDataAsJson();
    }
}