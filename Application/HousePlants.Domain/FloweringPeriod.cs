using System;

namespace HousePlants.Domain
{
    [Keyless]
    public sealed class FloweringPeriod
    {
        public Month Start { get; private set; }
        public Month End { get; private set; }

        public FloweringPeriod(Month start, Month end)
        {
            if (start == Month.NotSet)
                throw new ArgumentException("FloweringPeriod must have a start date", nameof(start));

            if (end == Month.NotSet)
                throw new ArgumentException("FloweringPeriod must have an end date", nameof(end));

            Start = start;
            End = end;
        }

    }
}