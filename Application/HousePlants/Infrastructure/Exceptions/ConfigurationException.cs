using System;

namespace HousePlants.Infrastructure.Exceptions
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message) : base(message) { }
    }
}