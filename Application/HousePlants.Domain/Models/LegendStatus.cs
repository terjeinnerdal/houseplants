using System;

namespace HousePlants.Domain.Models
{
    [Flags]
    public enum LegendStatus
    {
        None,
        Survivor = 1,
        OldBoy = 2,
        Neglected = 4,
        LowMaint = 8,
        HighMaint = 16,
        FamilyMember = 32
    }
}