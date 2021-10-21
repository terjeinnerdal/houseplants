using NodaTime;
#nullable enable
namespace HousePlants.Models.Interfaces
{
    public interface INodaTimeEntity
    {
        Instant Created { get; set; }
        Instant Modified { get; set; }
    }
}