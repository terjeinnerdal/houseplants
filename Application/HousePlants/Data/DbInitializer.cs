using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HousePlants.Areas.Identity.Data;
using HousePlants.Models;
using HousePlants.Models.Plant;
using HousePlants.Models.Plant.Requirements;
using HousePlants.Models.Plant.Taxonomy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// When viewing them I'd liker to order by
// - Minimum temperature (PP)
// - Watering requirements (PP)
// - Light requirements (PP)
// - Genus (Taxonomy)
// - Where I got it
// - When I got it

namespace HousePlants.Data
{
    public static class DbInitializer
    {
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
            string testUserPw, string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<HousePlantsUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new HousePlantsUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }


        public static async Task Initialize(IServiceProvider services)
        {
            var config = services.GetRequiredService<IConfiguration>();


            
            await using var dbContext = services.GetRequiredService<HousePlantsDbContext>();
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            try
            {
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
                await dbContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Get password for database test user.
            string GetTestUserPw()
            {
                string password = config["TestUserPassword"];
                return string.IsNullOrEmpty(password)
                    ? throw new ConfigurationException("Please specify the secret TestUserPW")
                    : password;
            }
            string testUserId = Guid.NewGuid().ToString();
            string adminUserId = Guid.NewGuid().ToString();
            await AddUsers();
            async Task AddUsers()
            {
                string testUserPw = GetTestUserPw();
                adminUserId = await EnsureUser(services, testUserPw, "admin@houseplants.net");
                await EnsureRole(services, adminUserId, "SuperUserRole");

                // allowed user can create and edit contacts that they create
                testUserId = await EnsureUser(services, testUserPw, "terje.innerdal@gmail.net");
                await EnsureRole(services, testUserId, "UserRole");
            }

            if (dbContext.Plants.Any())
            {
                return;
            }

            Plant plant = dbContext.Plants.FirstOrDefault(s => s.CommonName.Equals("Ficus Lyrata Bambino"));
            if (plant != null)
            {
                return;
            }

            #region Families
            var acanthaceaeFamily = new Family("Acanthaceae");
            var apocynaceaeFamily = new Family("Aapocynaceae");
            var araceaeFamily = new Family("Araceae");
            var asparagaceaeFamily = new Family("Asparagaceae");
            var asphodelaceaeFamily = new Family("Asphodelaceae");
            var cactaceaeFamily = new Family("Cactaceae");
            var crassulaceaeFamily = new Family("Crassulaceae")
            {
                Description = "The Crassulaceae, also known as the stonecrop family or the orpine family, " +
                              "are a diverse family of dicotyledon flowering plants characterized by succulent " +
                              "leaves and a unique form of photosynthesis, known as Crassulacean acid metabolism (CAM)."
            };
            var droseraceaeFamily = new Family("Droseraceae");
            var malvaceaeFamily = new Family("Malvaceae");
            var moraceaeFamily = new Family("Moraceae");
            var piperaceaeFamily = new Family("Piperaceae");
            var rubiaceaeFamily = new Family("Rubiaceae");
            var urticaceaeFamily = new Family("Urticaceae")
            {
                Description = "The Urticaceae are a family, the nettle family, of flowering plants."
            };
            #endregion

            #region Genuses
            var alocasioGenus = new Genus("Alocasio") { Family = araceaeFamily };
            var chlorophytumGenus = new Genus("Chlorophytum") { Family = asparagaceaeFamily };
            var cactaceaeGenus = new Genus("Cactaceae") { PlantPassport = PlantPassport.SucculentPassport };
            var ceropegiaGenus = new Genus("Ceropegia") { Family = apocynaceaeFamily };
            var chamaedoreaGenus = new Genus("Chamaedorea") { Family = araceaeFamily };
            var coffeaGenus = new Genus("Coffea") { Family = rubiaceaeFamily };
            var dionaeaGenus = new Genus("Dionaea") { Family = droseraceaeFamily };
            var dracaneaGenus = new Genus("Dracanea") { Family = asparagaceaeFamily };
            var echinocactusGenus = new Genus("Echinocactus") { Family = cactaceaeFamily };
            var echiveriaGenus = new Genus("Echiveria") { Family = crassulaceaeFamily };
            var epipremnumGenus = new Genus("Epipremnum") { Family = araceaeFamily };
            var fittoniaGenus = new Genus("Fittonia") { Family = acanthaceaeFamily };
            var ficusGenus = new Genus("Ficus")
            {
                Description =
                    "Ficus is a genus of about 850 species of woody trees, shrubs, vines, epiphytes and " +
                    "hemiepiphytes in the family Moraceae. Collectively known as fig trees or figs, they " +
                    "are native throughout the tropics with a few species extending into the semi-warm temperate zone.",
                Family = moraceaeFamily,
            };
            var gasteriaGenus = new Genus("Gasteria") { Family = asphodelaceaeFamily };
            var monsteraGenus = new Genus("Monstera") { Family = araceaeFamily };
            var pachiraGenus = new Genus("Pachira") { Family = malvaceaeFamily };
            var pileaGenus = new Genus("Pilea")
            {
                Description =
                    "Pilea, with 600–715 species, is the largest genus of flowering plants in the nettle family " +
                    "Urticaceae, and one of the larger genera in the Urticales. It is distributed throughout the " +
                    "tropics, subtropics, and warm temperate regions (with the exception of Australia and New Zealand).",
                Family = urticaceaeFamily
            };
            var pilosocereusGenus = new Genus("Pilosocereus") { Family = cactaceaeFamily };
            var sansevieriaGenus = new Genus("Sansevieria") { Family = asparagaceaeFamily };
            var spathiphyllumGenus = new Genus("Spathiphyllum") { Family = araceaeFamily };
            var syngoniumGenus = new Genus("Syngonium") { Family = araceaeFamily };
            var nepetaGenus = new Genus("Nepeta")
            {
                Family = new Family("Lamiaceae"),
                PlantPassport = new PlantPassport()
                {
                    LightRequirement = LightRequirement.FullSunlight,
                    Perennial = true,
                    WaterRequirement = WaterRequirement.Low,
                    NutrientRequirement = NutrientRequirement.Medium,
                    Edible = false,
                    FloweringPeriod = new FloweringPeriod(Month.June, Month.October),
                    Height = 40
                }
            };
            #endregion

            #region Species
            #region Fig species
            var lyrataSpecies = new Species("Ficus Lyrata", ficusGenus)
            {
                Genus = ficusGenus,
            };
            var lyrataBambinoSpecies = new Species("Ficus Lyrata Bambino", ficusGenus);
            var cyathistipulaSpecies = new Species("Ficus Cyathistipula", ficusGenus);
            var elasticaSpecies = new Species("Ficus Elastica", ficusGenus);
            var benjaminaSpecies = new Species("Ficus Benjamina", ficusGenus);
            #endregion

            #region Succulents species
            var echiveriaGibbifloraSpecies = new Species("Echeveria Gibbiflora", echiveriaGenus) { PlantPassport = PlantPassport.SucculentPassport };
            #endregion

            #region Cactus species
            var echinocactusGrusoniiSpecies = new Species("Echinocactus Grusonii", echinocactusGenus) { PlantPassport = PlantPassport.SucculentPassport };
            var gasteriaCarinataSpecies = new Species("Gasteria Carinata", gasteriaGenus) { PlantPassport = PlantPassport.SucculentPassport };
            var gasteriaDuvalSpecies = new Species("Gasteria Duval", gasteriaGenus) { PlantPassport = PlantPassport.SucculentPassport };
            var pilosocereusPachycladusSpecies = new Species("Pilosocereus PachycladusSpecies", pilosocereusGenus) { PlantPassport = PlantPassport.SucculentPassport };
            #endregion

            #region Pilea species
            var pileaPeperomiaSpecies = new Species("Pilea Peperomia", pileaGenus);
            var pileaPeperomiaPolybotria = new Species("Pilea Peperomia Polybotria", pileaGenus);
            var pileaPeperomiaArgytreia = new Species("Pilea Peperomia Argytreia", pileaGenus);
            #endregion

            #region Other species
            var alocasioAmazonicaSpecies = new Species("Alocasia Amazonica", alocasioGenus);
            var ceropegiaWoodiiSpecies = new Species("Ceropegia Woodii", ceropegiaGenus);
            var chamaedoreaElegansSpecies = new Species("Chamaedorea Elegans", chamaedoreaGenus);
            var chlorophytumComosumSpecies = new Species("Chlorophytum Comosum", chlorophytumGenus);
            var coffeaArabicaSpecies = new Species("Coffea Arabica", coffeaGenus);
            var dracaneaFragransSpecies = new Species("Dracaena Fragrans", dracaneaGenus);
            var epipremnumAureumSpecies = new Species("Epipremnum Aureum", epipremnumGenus);
            var fittoniaAlbivensisSpecies = new Species("Fittonia Albivensis", fittoniaGenus);
            var monsteraDeliciosaSpecies = new Species("Monstera Deliciosa", monsteraGenus);
            var sansevieriaLaurentilSpecies = new Species("Sansevieria Laurentil", sansevieriaGenus);
            var sansevieriaTrifiasciataSpecies = new Species("Sansevieria Trifiasciata", sansevieriaGenus);
            var spathiphyllumSpecies = new Species("Spathiphyllum Unknown", spathiphyllumGenus);
            var pachiraAquaticaSpecies = new Species("Pachira Aquatica", pachiraGenus);
            var syngoniumPodophyllumSpecies = new Species("Syngonium podophyllum", syngoniumGenus);
            var dionaeaMuscipula = new Species("Dionaea Muscipula", dionaeaGenus);
            var nepetaNervosaSpecies = new Species("Nepeta Nervosa 'Blue Carpet'", nepetaGenus);
            #endregion
            #endregion

            #region Plants
            var format = new DateTimeFormatInfo { ShortDatePattern = "d.M.yyyy" };

            #region Figs

            plant = CreatePlant("Fiolinfiken", DateTime.Parse("20.09.2020", format), testUserId);

            plant = new Plant(testUserId)
            {
                CommonName = "Fiolinfiken",
                AquiredDate = DateTime.Parse("20.09.2020", format),
                MinimumTemperature = 12,
                Species = lyrataSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Ficus Lyrata Bambino",
                AquiredDate = DateTime.Parse("22.11.2020", format),
                Species = lyrataBambinoSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Lærbladfiken",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                Species = cyathistipulaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Gummi fiken",
                AquiredDate = DateTime.Parse("26.01.2021", format),
                Species = elasticaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                Description = "Bought at IKEA. Beatiful plant! Really thrives.",
                AquiredDate = DateTime.Parse("01.02.2021", format),
                Species = benjaminaSpecies
            };
            dbContext.Add(plant);
            #endregion

            #region Succulents
            plant = new Plant(testUserId)
            {
                Description = "Kjøpt på IKEA. Lilla sukkulent.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                Species = echiveriaGibbifloraSpecies
            };
            plant = new Plant(testUserId)
            {
                Description = "Kjøpt på IKEA. Den lille som jeg trodde var Aloe Vera",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                Species = gasteriaDuvalSpecies
            };
            plant = new Plant(testUserId)
            {
                AquiredDate = DateTime.Parse("01.10.2020", format),
                MinimumTemperature = 10,
                Species = gasteriaCarinataSpecies
            };
            #endregion

            #region Cactus
            plant = new Plant(testUserId)
            {
                CommonName = "Søylekaktus",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                Species = pilosocereusPachycladusSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Tønnekaktus",
                Description = "Kjøpt på Coop Øra.",
                AquiredDate = DateTime.Parse("26.03.2012", format),
                Species = echinocactusGrusoniiSpecies
            };
            dbContext.Add(plant);
            #endregion

            #region Pileas
            plant = new Plant(testUserId)
            {
                CommonName = "Kinesisk pengeplante",
                Description = "Kjøpt på Meny Solsiden. Vokser som en superhelt.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                Species = pileaPeperomiaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Rain drop",
                Description = "Kjøpt på Plantasjen. Vokser dårligst av de tre Pileaene.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                Species = pileaPeperomiaPolybotria
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Melonskall",
                Description = "Kjøpt på Plantasjen. Bladene ligner på melonskall.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                Species = pileaPeperomiaArgytreia
            };
            dbContext.Add(plant);
            #endregion

            #region Monstera
            plant = new Plant(testUserId)
            {
                CommonName = "Vindusblad (Rema 1000)",
                Description = "Kjøpt på Rema 1000 Øra. Står i pose med Plantasjen Premium Jord og Perlitt.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                Species = monsteraDeliciosaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Monstera Cuttings Plastic Bag",
                Description = "4 Stiklinger tatt fra den som ble kjøpt på Plantasjen som hadde fullt av tusenbein i seg og derfor ble kastet.",
                AquiredDate = DateTime.Parse("11.01.2021", format),
                Species = monsteraDeliciosaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Monstera Cutting Self Watering",
                Description = "4 Stiklinger tatt fra den som ble kjøpt på Plantasjen som hadde fullt av tusenbein i seg og derfor ble kastet.",
                AquiredDate = DateTime.Parse("11.01.2021", format),
                Species = monsteraDeliciosaSpecies
            };
            dbContext.Add(plant);
            #endregion

            #region Other
            plant = new Plant(testUserId)
            {
                CommonName = "Dvergfjærpalme",
                Description = "Planten er sensitiv for overvanning. Dette kan være grunnen hvis bladene begynner å bli brune.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                Species = chamaedoreaElegansSpecies
            };
            dbContext.Add(plant);

            plant = new Plant(testUserId)
            {
                CommonName = "Fredslilje",
                Description = "Delt opp i 3 potter.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                Species = spathiphyllumSpecies

            };
            dbContext.Add(plant);

            plant = new Plant(testUserId)
            {
                CommonName = "Gullranke",
                Description = "Kjøpt på Strå Blomster. " +
                              "Vannes 1 gang i uka og holdes unna direkte sollys, kan trives i litt mørkere deler av huset.",
                AquiredDate = DateTime.Parse("06.01.2021", format),
                Species = epipremnumAureumSpecies
            };
            dbContext.Add(plant);

            plant = new Plant(testUserId)
            {
                CommonName = "Nerve plant",
                Description = "Kjøpt på Strå Blomster",
                AquiredDate = DateTime.Parse("06.01.2021", format),
                Species = fittoniaAlbivensisSpecies
            };
            dbContext.Add(plant);

            plant = new Plant(testUserId)
            {
                CommonName = "Elefantøre",
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
                Species = alocasioAmazonicaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Svigermors tunge",
                Description = "Kjøpt på IKEA. The one with yellow edges.",
                AquiredDate = DateTime.Parse("01.10.2020", format),
                Species = sansevieriaLaurentilSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Svigermors tunge",
                Description = "Kjøpt på IKEA. Ikke gul/grønn fargekombinasjon slik som den andre.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                Species = sansevieriaTrifiasciataSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Venusfluefanger",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                Species = dionaeaMuscipula
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Paraplytre",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                Species = pachiraAquaticaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Marmorlanterne",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                Species = ceropegiaWoodiiSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Dragetre",
                Description = "Kjøpt på IKEA. Miniplante, står enda i den orgiginale 9cm potta. Bør sjekke om denne kan ompottes.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                MinimumTemperature = 12,
                Species = dracaneaFragransSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Kaffeplante",
                Description = "Kjøpt på IKEA.",
                AquiredDate = DateTime.Parse("26.03.2021", format),
                Species = coffeaArabicaSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Spider plant, Grønnrenner",
                Description = "Kjøpt på Meny Solsiden. Er reversert variegata på denne med grønn stripe i midten av bladene. Har allerede skutt ut en \"pup\"." +
                              "Veldig voksevillig, katter elsker den pga. dens hallusinogene egenskaper. Har noen brune tupper, enten for mye gjødsel eller for lav luftfuktighet.",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                Species = chlorophytumComosumSpecies
            };
            dbContext.Add(plant);
            plant = new Plant(testUserId)
            {
                CommonName = "Arrow head",
                Description = "Kjøpt på Meny sammen med spider plant. Tror dette er en 'Pixie' eller en 'Holly'. " +
                              "Tåler godt å stå i vinduskarmen på stua enda, men følg med fra Juni-August. " +
                              "Tålte ikke kulden hjem fra Meny helt, mistet mange blader men har kommet seg og vokser helt vilt. " +
                              "Virker meget hardfør! ",
                AquiredDate = DateTime.Parse("05.02.2021", format),
                Species = syngoniumPodophyllumSpecies
            };
            dbContext.Add(plant);
            #endregion

            #region Unidentified
            // todo: identify
            plant = new Plant(testUserId)
            {
                CommonName = "Orangu",
                Description = "Har hatt denne lengst sammen med tønnekaktusen. Vokser som en superhelt. " +
                              "Har overlevd i alle år i alle kriker og kroker i kåken." +
                              "Har begynt å gro vorter på den høyeste grenen igjen nå(27 mars 2021). " +
                              "Sto kaldt hele desember/januar. Pottet om sommeren 2020. " +
                              "Har generelt fått lite oppmerksomhet, lys eller annen omsorg, men står lyst nå etter ompotting.",
                AquiredDate = DateTime.Parse("26.03.2012", format),
            };
            dbContext.Add(plant);
            //plant = new Plant(testUserId)
            //{
            //    CommonName = "Blåstjerne",
            //    Description = "Kjøpt på Meny.",
            //    AquiredDate = DateTime.Parse("05.02.2021", format),
            //    Species = new Species("Phlebodium aureum", new Genus("Phlebodium", new Family("Polypodiaceae")), new PlantPassport
            //    {
            //        NutrientRequirement = NutrientRequirement.Low,
            //        WaterRequirement = WaterRequirement.Medium,
            //        WateringTechnique = WateringTechnique.WetDry
            //    })
            //};

            //context.Add(plant);
            #endregion

            #region Balcony
            plant = new Plant(testUserId)
            {
                CommonName = "Kattemynte, Blue Carpet",
                AquiredDate = DateTime.Parse("02.06.2021", format),
                Species = nepetaNervosaSpecies
            };
            dbContext.Add(plant);

            plant = new Plant(testUserId)
            {
                CommonName = "Høstfloks",
                AquiredDate = DateTime.Parse("02.06.2021", format),
                Species = new Species("Phlox Sweet Summer Candy")
            };
            dbContext.Add(plant);
            #endregion
            
            #endregion

            await dbContext.SaveChangesAsync();

        }

        private static Plant CreatePlant(string name, DateTime aquiredDate, string owner)
        {
            return new Plant(owner) { CommonName = name, AquiredDate = aquiredDate};
        }

        public static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
            string uid, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<HousePlantsUser>>();

            var user = await userManager.FindByIdAsync(uid);

            return user == null
                ? throw new Exception("The testUserPw password was probably not strong enough!")
                : await userManager.AddToRoleAsync(user, role);
        }

    }
}