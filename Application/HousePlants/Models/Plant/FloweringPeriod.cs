using System;
using Microsoft.EntityFrameworkCore;

namespace HousePlants.Models.Plant
{    
    public enum Month
    { 
        NotSet = 0, 
        January = 1, 
        February = 2, 
        March = 3, 
        April = 4, 
        May = 5, 
        June = 6, 
        July = 7, 
        August = 8, 
        September = 9, 
        October = 10, 
        November = 11, 
        December = 12 
    } 

    [Owned] 
    public sealed class FloweringPeriod 
    { 
        public Month Start { get; private set; } 
        public Month End { get; private set; } 
 
        public FloweringPeriod(Month start, Month end) 
        { 
            if (start == Month.NotSet)
            {
                throw new ArgumentException("FloweringPeriod must have a start month", nameof(start));
            }

            if (end == Month.NotSet)
            {
                throw new ArgumentException("FloweringPeriod must have an end month", nameof(end));
            }

            Start = start; 
            End = end; 
        } 
    }
}