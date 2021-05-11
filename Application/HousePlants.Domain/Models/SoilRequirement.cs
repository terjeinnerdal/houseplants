namespace HousePlants.Domain.Models
{
    public enum SoilRequirement : short
    {
        None = 0,
        Fine = 1,
        Medium = 2,
        Coarse = 4,
        StandardMix = 8,
        PeatBasedMIx = 16,
        Sandy = 32
    }
}