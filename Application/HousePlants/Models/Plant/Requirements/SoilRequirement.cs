namespace HousePlants.Models.Plant.Requirements
{
    public enum SoilRequirement : short
    {
        NotSet = 0,
        Fine = 1,
        Medium = 2,
        Coarse = 4,
        StandardMix = 8,
        PeatBasedMIx = 16,
        Sandy = 32
    }
}