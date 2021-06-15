using HousePlants.Models;
using HousePlants.Models.Plant;
using HousePlants.Models.Plant.Taxonomy;
using NUnit.Framework;

namespace HousePlants.Tests.Models
{
    // If only CommonName is set, Title == CommonName
    // If only LatinName  is set, Title == LatinName
    // If both CommonName and LatinName are set, Title == CommonName - LatinName
    public class PlantTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GivenBothCommonNameAndSpeciesAreBlank_WhenCreatingNewPlant_ThenTitleEqualsEmptyString()
        {
            var plant = new Plant();
            Assert.AreEqual(plant.Title, string.Empty);
        }

        [Test]
        public void GivenSpeciesIsNull_WhenCommonNameIsSet_ThenTitleEqualsCommonName()
        {
            var plant = new Plant
            {
                CommonName = "CommonName"
            };

            Assert.That(plant.Title == plant.CommonName);
        }
        [Test]
        public void GivenCommonNameIsNull_WhenSpeciesIsSet_ThenTitleEqualsNameOfSpecies()
        {
            var plant = new Plant
            {
                Species = new Species("nameOfSpecies")
            };

            Assert.That(plant.Title == plant.Species.Name);
        }
    }
}