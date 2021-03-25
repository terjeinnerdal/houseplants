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

            // Add plants
            Plant plant = context.Plants.FirstOrDefault(s => s.CommonName.Equals("Ficus Lyrata Bambino"));
            if (plant is not null) return;
            
            // Figs
            Genus ficusGenus = new Genus
            {
                Title = "Ficus",
                Description =
                    "Ficus is a genus of about 850 species of woody trees, shrubs, vines, epiphytes and hemiepiphytes in the family Moraceae. " +
                    "Collectively known as fig trees or figs, they are native throughout the tropics with a few species extending into the semi-warm temperate zone."
            };
            
            plant = new Plant
            {
                CommonName = "Ficus Lyrata",
                Description = "A commonly tricky houseplant despite its popularity, the fiddle-leaf fig does not respond well to being moved, " +
                              "especially from a spot where it is thriving. Proper drainage, adequate sunlight (direct but not harsh) will " +
                              "keep your fiddle-leaf fig bright green with its signature glossy finish.",
                AquiredDate = DateTime.Parse("20.09.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry,
                Genus = ficusGenus
            };
            context.Add(plant);
                
            plant = new Plant
            {
                CommonName = "Ficus Lyrata Bambino",
                Description = "The Bambino has smaller leaves than the orgininal Ficus Lyrata.",
                AquiredDate = DateTime.Parse("22.11.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry,
                Genus = ficusGenus
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Lærbladfiken",
                LatinName = "Ficus Cyathistipula",
                Description = "",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry,
                Genus = ficusGenus
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Unclassified Fig",
                Description = "Unlabeled Fig bought at Rema 1000 Sunndalsøra. Leaves are almost black.",
                AquiredDate = DateTime.Parse("26.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry,
                Genus = ficusGenus
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Fixus Benjamina",
                Description = "Bought at IKEA. Really thrives.",
                AquiredDate = DateTime.Parse("01.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry,
                Genus = ficusGenus
            };
            context.Add(plant);

            // Other
            plant = new Plant
            {
                CommonName = "Monstera Deliciosa",
                Description = "Kjøpt på Rema 1000 Øra. Står i pose med Plantasjen Premium Jord og Perlitt.",
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
                LatinName = "Spathiphyllum",
                Description = "Delt opp i 3 potter.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry
            };
            context.Add(plant);
                
            // Fra strå
            plant = new Plant
            {
                CommonName = "Gullranke",
                LatinName = "Epipremnum Aureum",
                Description = "Kjøpt på Strå Blomster. Voksvillig og enkel å holde, gullranken er en absolutt favoritt! " +
                              "Vannes 1 gang i uka og holdes unna direkte sollys, kan trives i litt mørkere deler av huset. Kjøpt på Strå Blomster.",
                AquiredDate = DateTime.Parse("06.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WaterRequirement = WaterRequirement.WetDry
            };
            context.Add(plant);
                
            plant = new Plant
            {
                CommonName = "Nerve plant",
                LatinName = "Fittonia albivenis",
                Description = "Kjøpt på Strå Blomster",
                AquiredDate = DateTime.Parse("06.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WaterRequirement = WaterRequirement.KeepMoist
            };
            context.Add(plant);

            // Spider plant
            plant = new Plant
            {
                CommonName = "Spider plant, Grønnrenner",
                LatinName = "Chlorophytum comosum",
                Description = "Kjøpt på Meny Solsiden. Er reversert variegata på denne med grønn stripe i midten av bladene. Har allerede skutt ut en \"pup\"." + Environment.NewLine + 
                              "Veldig voksevillig, katter elsker den pga. dens hallusinogene egenskaper. Har noen brune tupper, enten for mye gjødsel eller for lav luftfuktighet.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WaterRequirement = WaterRequirement.WetDry
            };
            context.Add(plant);
            
            // Pileas
            plant = new Plant
            {
                CommonName = "Kinesisk pengeplante",
                LatinName = "Pilea peperomia",
                Description = "Kjøpt på Meny Solsiden. Vokser som en superhelt.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Rain drop",
                LatinName = "Pilea peperomia polybotria",
                Description = "Kjøpt på Plantasjen. Vokser dårligst av de tre Pileaene.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Melonskall",
                LatinName = "Pilea peperomia argytreia",
                Description = "Kjøpt på Plantasjen. Bladene ligner på melonskall.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Elefantøre",
                LatinName = "Alocasia",
                Description = "Kjøpt på Plantasjen. Den blir mellom 25-40 cm høy og er en svært dekorativ stueplante, men pass på; denne vakringen er nemlig giftig!" + Environment.NewLine + 
                "- Denne planten trives best med en lys plassering, uten direkte sollys. Den liker stuetemperatur fra 15 grader og oppover." + Environment.NewLine + 
                "- Den liker normal vanning, dvs at den bør holdes jevnt fuktig; ikke for mye, ikke for lite vann!" + Environment.NewLine + 
                Environment.NewLine +
                "Elefantøreplanten «snakker» gjennom bladene sine. Ved å se på bladene kan du lese hvordan den har det:" + Environment.NewLine +
                "- Brune blader: Du har vannet den for mye, eller utsatt den for kulde." + Environment.NewLine +
                "- Lyse og brune blader:Den har blitt utsatt for direkte sollys." + Environment.NewLine +
                "- Alle bladene klapper sammen: Det er vinter og den har gått i dvale." + Environment.NewLine + 
                "Vannes sparsommelig, men jevnt inntil den ser ut til å våkne til igjen! Da kan du gjenoppta normal vanning.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WaterRequirement = WaterRequirement.WetDry
            };
            context.Add(plant);

            // Unidentified
            // All succulents and the shrub.

            context.SaveChanges();
        }
    }
}