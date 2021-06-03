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
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Database.Migrate();

            //if (context.Plants.Any())
            //{
            //    return;
            //}


            // Genus and family.
            // Figs I know by the Genus Fixus. My cactus may be different genuses but in the family Cactiaceae
            // When viewing them I'd liker to order by
            // - Minimum temperature
            // - Watering requirements
            // - Light requirements
            // - Genus
            // - Where I got it
            // - When I got it
            
            Plant plant = context.Plants.FirstOrDefault(s => s.CommonName.Equals("Ficus Lyrata Bambino"));
            if (plant != null)
            {
                return;
            }

            var format = new DateTimeFormatInfo { ShortDatePattern =  "d.M.yyyy" };

            #region Figs
            Genus ficusGenus = new Genus
            {
                Title = "Ficus",
                Description =
                    "Ficus is a genus of about 850 species of woody trees, shrubs, vines, epiphytes and hemiepiphytes in the family Moraceae. " +
                    "Collectively known as fig trees or figs, they are native throughout the tropics with a few species extending into the semi-warm temperate zone."
            };

            plant = new Plant
            {
                CommonName = "Fiolinfiken",
                LatinName = "Ficus Lyrata",
                Description = "Kjøpt på IKEA, kan bli gigantisk.",
                AquiredDate = DateTime.Parse("20.09.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                MinimumTemperature = 12,
                WateringTechnique = WateringTechnique.WetDry,
                Classification = new Classification
                {
                    Genus = ficusGenus
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Ficus Lyrata Bambino",
                Description = "The Bambino has smaller leaves than the orgininal Ficus Lyrata.",
                AquiredDate = DateTime.Parse("22.11.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                Classification = new Classification
                {
                    Genus = ficusGenus
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Lærbladfiken",
                LatinName = "Ficus Cyathistipula",
                Description = "",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                Classification = new Classification
                {
                    Genus = ficusGenus
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                LatinName = "Ficus Elastica",
                CommonName = "Gummi fiken",
                Description = "Unlabeled Fig bought at Rema 1000 Sunndalsøra. Leaves are almost black.",
                AquiredDate = DateTime.Parse("26.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                Classification = new Classification
                {
                    Genus = ficusGenus
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Fixus Benjamina",
                Description = "Bought at IKEA. Beatiful plant! Really thrives.",
                AquiredDate = DateTime.Parse("01.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                Classification = new Classification
                {
                    Genus = ficusGenus
                }
            };
            context.Add(plant);
            #endregion

            #region Succulents
            plant = new Plant
            {
                CommonName = "Gibbiflora",
                LatinName = "Echiveria Gibbiflora",
                Description = "Kjøpt på IKEA. Lilla sukkulent.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.FullSunlight | LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetBoneDry,
                SoilRequirement = SoilRequirement.Sandy,
            }; 
            plant = new Plant
            {
                CommonName = "Succulent",
                LatinName = "Gasteria Duval",
                Description = "Kjøpt på IKEA. Den lille som jeg trodde var Aloe Vera",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.FullSunlight | LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetBoneDry,
                SoilRequirement = SoilRequirement.Sandy,
            };
            plant = new Plant
            {
                CommonName = "Succulent",
                LatinName = "Gasteria Carinata",
                Description = "Kjøpt på IKEA. Den kule",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.FullSunlight | LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetBoneDry,
                SoilRequirement = SoilRequirement.Sandy,
            };
            #endregion

            #region Cactus
            var cactaceaeFamily = new Family
            {
                Title = "Cactaceae",
                Description = ""
            };
            plant = new Plant
            {
                CommonName = "Søylekaktus",
                LatinName = "Pilosocereus Pachycladus",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.FullSunlight,
                WateringTechnique = WateringTechnique.WetBoneDry,
                SoilRequirement = SoilRequirement.Sandy,
                Classification = new Classification
                {
                    Family = cactaceaeFamily
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Tønnekaktus",
                LatinName = "Echinocactus Grusonii",
                Description = "Kjøpt på Coop Øra.",
                AquiredDate = DateTime.Parse("26.03.2012", format),
                LightRequirement = LightRequirement.FullSunlight,
                WateringTechnique = WateringTechnique.WetBoneDry,
                SoilRequirement = SoilRequirement.Sandy,
                Classification = new Classification
                {
                    Family = cactaceaeFamily
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Orangu",
                LatinName = "",
                Description = "Har hatt denne lengst sammen med tønnekaktusen. Vokser som en superhelt. " +
                              "Har overlevd i alle år i alle kriker og kroker i kåken." +
                              "Har begynt å gro vorter på den høyeste grenen igjen nå(27 mars 2021). " +
                              "Sto kaldt hele desember/januar. Pottet om sommeren 2020. " +
                              "Har generelt fått lite oppmerksomhet, lys eller annen omsorg, men står lyst nå etter ompotting.",
                AquiredDate = DateTime.Parse("26.03.2012", format),
                LightRequirement = LightRequirement.FullSunlight | LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetBoneDry,
                SoilRequirement = SoilRequirement.Sandy,
                LegendStatus = LegendStatus.FamilyMember | LegendStatus.LowMaint | LegendStatus.Neglected | LegendStatus.Survivor,
                Classification = new Classification
                {
                    Family = cactaceaeFamily
                }
            };
            context.Add(plant);
            #endregion

            #region Pileas
            var urticaceaeFamily = new Family
            {
                Title = "Urticaceae",
                Description = "The Urticaceae are a family, the nettle family, of flowering plants."
            };
            var pileaGenus = new Genus
            {
                Title = "Pilea",
                Description =
                    "Pilea, with 600–715 species, is the largest genus of flowering plants in the nettle family Urticaceae, and one of the larger genera in the Urticales. " +
                    "It is distributed throughout the tropics, subtropics, and warm temperate regions (with the exception of Australia and New Zealand)."
            };
            plant = new Plant
            {
                CommonName = "Kinesisk pengeplante",
                LatinName = "Pilea Peperomia",
                Description = "Kjøpt på Meny Solsiden. Vokser som en superhelt.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix,
                Classification = new Classification
                {
                    Genus = pileaGenus
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Rain drop",
                LatinName = "Pilea Peperomia Polybotria",
                Description = "Kjøpt på Plantasjen. Vokser dårligst av de tre Pileaene.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix,
                Classification = new Classification
                {
                    Genus = pileaGenus
                }
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Melonskall",
                LatinName = "Pilea Peperomia Argytreia",
                Description = "Kjøpt på Plantasjen. Bladene ligner på melonskall.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix,
                Classification = new Classification
                {
                    Genus = pileaGenus
                }
            };
            context.Add(plant);
            #endregion
            
            #region Other
            plant = new Plant
            {
                CommonName = "Vindusblad (Rema 1000)",
                LatinName = "Monstera Deliciosa",
                Description = "Kjøpt på Rema 1000 Øra. Står i pose med Plantasjen Premium Jord og Perlitt.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Monstera Cuttings Plastic Bag",
                Description = "4 Stiklinger tatt fra den som ble kjøpt på Plantasjen som hadde fullt av tusenbein i seg og derfor ble kastet.",
                AquiredDate = DateTime.Parse("11.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetDry,
                MinimumTemperature = 15,
                MaximumTemperature = 30

            };
            context.Add(plant);
            plant = new Plant
            {
                LatinName = "Monstera Deliciosa",
                CommonName = "Monstera Cutting Self Watering",
                Description = "4 Stiklinger tatt fra den som ble kjøpt på Plantasjen som hadde fullt av tusenbein i seg og derfor ble kastet.",
                AquiredDate = DateTime.Parse("11.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetDry
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Dvergfjærpalme",
                LatinName = "Chamaedorea Elegans",
                Description = "Planten er sensitiv for overvanning. Dette kan være grunnen hvis bladene begynner å bli brune.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                WaterRequirement = WaterRequirement.Medium,
                SoilRequirement = SoilRequirement.PeatBasedMIx
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Fredslilje",
                LatinName = "Spathiphyllum",
                Description = "Delt opp i 3 potter.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                Toxic = true
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Gullranke",
                LatinName = "Epipremnum Aureum",
                Description = "Kjøpt på Strå Blomster. " +
                              "Vannes 1 gang i uka og holdes unna direkte sollys, kan trives i litt mørkere deler av huset.",
                AquiredDate = DateTime.Parse("06.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetDry
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Nerve plant",
                LatinName = "Fittonia albivenis",
                Description = "Kjøpt på Strå Blomster",
                AquiredDate = DateTime.Parse("06.01.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetDry
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Elefantøre",
                LatinName = "Alocasia",
                Description = "Kjøpt på Plantasjen. Den blir mellom 25-40 cm høy og er en svært dekorativ stueplante, men pass på; denne vakringen er nemlig giftig!" + 
                "- Denne planten trives best med en lys plassering, uten direkte sollys. Den liker stuetemperatur fra 15 grader og oppover." + 
                "- Den liker normal vanning, dvs at den bør holdes jevnt fuktig; ikke for mye, ikke for lite vann!" + 
                
                "Elefantøreplanten «snakker» gjennom bladene sine. Ved å se på bladene kan du lese hvordan den har det:" + 
                "- Brune blader: Du har vannet den for mye, eller utsatt den for kulde." + 
                "- Lyse og brune blader: Den har blitt utsatt for direkte sollys." + 
                "- Alle bladene klapper sammen: Det er vinter og den har gått i dvale." + 
                "Vannes sparsommelig, men jevnt inntil den ser ut til å våkne til igjen! Da kan du gjenoppta normal vanning.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                MinimumTemperature = 15,
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Svigermors tunge",
                LatinName = "Sansevieria Laurentil",
                Description = "Kjøpt på IKEA. The one with yellow edges.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetBoneDry
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Svigermors tunge",
                LatinName = "Sansevieria Trifasciata",
                Description = "Kjøpt på IKEA. Ikke gul/grønn fargekombinasjon slik som den andre.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetBoneDry,
                SoilRequirement = SoilRequirement.StandardMix,
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Venusfluefanger",
                LatinName = "Dionaea Muscipla",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.FullSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Paraplytre",
                LatinName = "Pachira Aquatica",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Marmorlanterne",
                LatinName = "Ceropegia Woodii",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix
            };
            context.Add(plant);
            plant = new Plant
            {
                CommonName = "Dragetre",
                LatinName = "Dracaena Fragrans",
                Description = "Kjøpt på IKEA. Miniplante, står enda i den orgiginale 9cm potta. Bør sjekke om denne kan ompottes.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetDry,
                WaterRequirement = WaterRequirement.Low, 
                SoilRequirement = SoilRequirement.StandardMix,
                MinimumTemperature = 12
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Coffea Arabica",
                LatinName = "Sansevieria Trifasciata",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight,
                WateringTechnique = WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix,
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Spider plant, Grønnrenner",
                LatinName = "Chlorophytum comosum",
                Description = "Kjøpt på Meny Solsiden. Er reversert variegata på denne med grønn stripe i midten av bladene. Har allerede skutt ut en \"pup\"." + 
                              "Veldig voksevillig, katter elsker den pga. dens hallusinogene egenskaper. Har noen brune tupper, enten for mye gjødsel eller for lav luftfuktighet.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetDry
            };
            context.Add(plant);

            plant = new Plant
            {
                CommonName = "Arrow head",
                LatinName = "Syngonium podophyllum",
                Description = "Kjøpt på Meny sammen med spider plant. Tror dette er en 'Pixie' eller en 'Holly'. " +
                              "Tåler godt å stå i vinduskarmen på stua enda, men følg med fra Juni-August. " +
                              "Tålte ikke kulden hjem fra Meny helt, mistet mange blader men har kommet seg og vokser helt vilt. " +
                              "Virker meget hardfør! ",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetDry,
                WaterRequirement = WaterRequirement.Medium,
                SoilRequirement = SoilRequirement.StandardMix,
                LegendStatus = LegendStatus.LowMaint | LegendStatus.Survivor,
                Toxic = true,
                Classification = new Classification
                {
                    Family = cactaceaeFamily
                }
            };
            context.Add(plant);
            #endregion

            #region Unidentified
            plant = new Plant
            {
                CommonName = "Blåstjerne",
                LatinName = "",
                Description = "Kjøpt på Meny sammen med spider plant. " +
                              "Virker som den tåler hva som helst av lysforhold." +
                              "Tålte kulden hjem fra Meny bra også. Virker meget hardfør! " +
                              "Tror dette er Leon planta faktisk.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                LightRequirement = LightRequirement.IndirectSunlight | LightRequirement.Shade,
                WateringTechnique = WateringTechnique.WetBoneDry | WateringTechnique.WetDry,
                SoilRequirement = SoilRequirement.StandardMix | SoilRequirement.Coarse,
                LegendStatus = LegendStatus.LowMaint | LegendStatus.Survivor,
                Classification = new Classification
                {
                    Family = cactaceaeFamily
                }
            };
            context.Add(plant);
            #endregion

            context.SaveChanges();
        }
    }
}