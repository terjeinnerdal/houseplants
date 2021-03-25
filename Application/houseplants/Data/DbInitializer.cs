using System;
using System.Globalization;
using System.Linq;
using HousePlants.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HousePlants.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HousePlantsContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Database.Migrate();

            if (context.Plants.Any())
            {
                return;
            }

            var format = new DateTimeFormatInfo {ShortDatePattern = "d.M.yyyy"};

            // Add a plant
            Plant plant = context.Plants.FirstOrDefault(s => s.CommonName.Equals("Ficus Lyrata Bambino"));
            if (plant is null)
            {
                //
                // Figs
                // 
                plant = new Plant
                {
                    CommonName = "Ficus Lyrata",
                    Description = "A commonly tricky houseplant despite its popularity, the fiddle-leaf fig does not respond well to being moved, especially from a spot where it is thriving. Proper drainage, adequate sunlight (direct but not harsh) will keep your fiddle-leaf fig bright green with its signature glossy finish.",
                    AquiredDate = DateTime.Parse("20.09.2020", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };
                context.Add(plant);
                
                plant = new Plant
                {
                    CommonName = "Ficus Lyrata Bambino",
                    Description = "The Bambino has smaller leaves than the orgininal Ficus Lyrata.",
                    AquiredDate = DateTime.Parse("22.11.2020", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };
                context.Add(plant);
                plant = new Plant
                {
                    CommonName = "Lærbladfiken",
                    LatinName = "Ficus Cyathistipula",
                    Description = "",
                    AquiredDate = DateTime.Parse("01.10.2020", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };
                context.Add(plant);
                plant = new Plant
                {
                    CommonName = "Unknown Fig",
                    Description = "Unlabeled Fig bought at Rema 1000 Sunndalsøra. Leaves are almost black.",
                    AquiredDate = DateTime.Parse("26.01.2021", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };
                context.Add(plant);

                plant = new Plant
                {
                    CommonName = "Monstera Deliciosa",
                    Description = "",
                    AquiredDate = DateTime.Parse("01.10.2020", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };
                context.Add(plant);

                plant = new Plant
                {
                    CommonName = "Svigermors tunge",
                    LatinName = "Sansevieria Trifasciata",
                    AquiredDate = DateTime.Parse("01.10.2020", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };
                context.Add(plant);

                plant = new Plant
                {
                    CommonName = "Bergpalme",
                    Description = "",
                    AquiredDate = DateTime.Parse("01.10.2020", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };

                context.Add(plant);
                plant = new Plant
                {
                    CommonName = "Fredslilje",
                    Description = "Spathiphyllum",
                    AquiredDate = DateTime.Parse("01.10.2020", format),
                    LightRequirement = LightRequirement.IndirectSunlight,
                    WaterRequirement = WaterRequirement.WetDry
                };
                
                context.Add(plant);
                plant = new Plant
                {
                    CommonName = "Gullranke",
                    LatinName = "Epipremnum aureum",
                    Description = "Voksvillig og enkel å holde, gullranken er en absolutt favoritt! Vannes 1 gang i uka og holdes unna direkte sollys, kan trives i litt mørkere deler av huset. Kjøpt på Strå Blomster.",
                    AquiredDate = DateTime.Parse("06.01.2021", format),
                    LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                    WaterRequirement = WaterRequirement.WetDry
                };
                
                context.Add(plant);
                plant = new Plant
                {
                    CommonName = "Nerve plant, Fittonia",
                    LatinName = "Fittonia albivenis",
                    Description = "Kjøpt på Strå Blomster",
                    AquiredDate = DateTime.Parse("06.01.2021", format),
                    LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                    WaterRequirement = WaterRequirement.KeepMoist
                };
                
                context.Add(plant);
                plant = new Plant
                {
                    CommonName = "Primula",
                    LatinName = "Fittonia albivenis",
                    Description =
                        "Primulaen trives aller best innendørs dersom den får stå lyst uten direkte sol. " +
                        "For å holde blomstringen i gang så lenge som mulig, vil den også stå kjølig, så unngå " +
                        "å plassere primulaen din i et vindu eller over en ovn, da dette risikerer å tørke ut " +
                        "jorden for fort. Hold deretter primulaen din i god stand ved å vanne annenhver dag og " +
                        "sørge for å ta bort visne planter ettersom de kommer." +
                        Environment.NewLine +
                        "En del primulaer vil også gjerne ha små doser med næring under blomstringstiden, da " +
                        "spesielt primula obconica og primua malcoides. Det er da nok å tilsette dette annenhver uke.",
                    AquiredDate = DateTime.Parse("06.01.2021", format),
                    LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                    WaterRequirement = WaterRequirement.KeepMoist
                };
                context.Add(plant);
                context.SaveChanges();
            }
        }
    }
}