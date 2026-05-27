using System;
using System.Collections.Generic;

namespace CS2_CaseOpening
{
    public static class GameData
    {
        private static readonly Random _rnd = new();

        public static string CurrencySymbol = "€";

        
        public static double Balance = 100.0;
        public static string BalanceFormatted => $"{CurrencySymbol}{Balance:0.00}";

        public static List<Skin> MySkins = new List<Skin>();

        public static bool TryOpenCase(Case selectedCase)
        {
            double caseCost = selectedCase?.Price ?? 2.5;
            if (Balance < caseCost)
                return false;
            Balance -= caseCost;
            return true;
        }

        private static double RandomCasePrice(double min, double max)
            => Math.Round(_rnd.NextDouble() * (max - min) + min, 2);

        static GameData()
        {
            AdjustSkinPricesForCases();
        }

        private static void AdjustSkinPricesForCases()
        {
            var excluded = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "AWP Case",
                "Knife Case",
                "Glove Case"
            };

            foreach (var c in Cases)
            {
                if (c == null || c.Skins == null) continue;
                if (excluded.Contains(c.Name)) continue;

                foreach (var s in c.Skins)
                {
                    s.Price = ComputeSkinPriceFromCase(c.Price, s.Rarity);
                }
            }
        }

        private static double ComputeSkinPriceFromCase(double casePrice, string rarity)
        {
            double baseMultiplier = rarity switch
            {
                "Blue" => 0.12,
                "Purple" => 0.22,
                "Pink" => 1.2,
                "Red" => 4.0,
                "Gold" => 12.0,
                _ => 0.5
            };

            double price = Math.Round(casePrice * baseMultiplier, 2);
            if (price < 0.05) price = 0.05;
            return price;
        }

        public static List<Case> Cases = new List<Case>()
        {
            new Case
            {
                Name = "Fever Case",
                ImagePath = "Images/FeverCase.png",
                Price = 2.50,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "M4A4 | Choppa", Rarity = "Blue", ImagePath = "Images/M4A4_Choppa.png", Price = 0.30 },
                    new Skin { Name = "Galil AR | Control", Rarity = "Purple", ImagePath = "Images/GalilAR_Control.png", Price = 0.55 },
                    new Skin { Name = "Glock-18 | Shinobu", Rarity = "Pink", ImagePath = "Images/Glock18_Shinobu.png", Price = 3.00 },
                    new Skin { Name = "AWP | Printstream", Rarity = "Red", ImagePath = "Images/AWP_Printstream.png", Price = 10.00 },
                    new Skin { Name = "Skeleton Knife | Doppler", Rarity = "Gold", ImagePath = "Images/SkeletonKnife_Doppler.png", Price = 40.00 }
                }
            },

            new Case
            {
                Name = "Dreams & Nightmares Case",
                ImagePath = "Images/Dreams&NightmaresCase.png",
                Price = 3.00,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "Five-SeveN | Scrawl", Rarity = "Blue", ImagePath = "Images/Five-SeveN_Scrawl.png", Price = 0.40 },
                    new Skin { Name = "USP-S | Ticket to Hell", Rarity = "Purple", ImagePath = "Images/USP-S_TickettoHell.png", Price = 0.70 },
                    new Skin { Name = "FAMAS | Rapid Eye Movement", Rarity = "Pink", ImagePath = "Images/FAMAS_RapidEyeMovement.png", Price = 4.50 },
                    new Skin { Name = "MP9 | Starlight Protector", Rarity = "Red", ImagePath = "Images/MP9_StarlightProtector.png", Price = 12.00 },
                    new Skin { Name = "Butterfly Knife | Autotronic", Rarity = "Gold", ImagePath = "Images/ButterflyKnife_Autotronic.png", Price = 60.00 }
                }
            },

            new Case
            {
                Name = "Gamma Case",
                ImagePath = "Images/GammaCase.png",
                Price = 5.00,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "MAC-10 | Carnivore", Rarity = "Blue", ImagePath = "Images/MAC-10_Carnivore.png", Price = 0.75 },
                    new Skin { Name = "AWP | Phobos", Rarity = "Purple", ImagePath = "Images/AWP_Phobos.png", Price = 1.10 },
                    new Skin { Name = "P2000 | Imperial Dragon", Rarity = "Pink", ImagePath = "Images/P2000_ImperialDragon.png", Price = 6.00 },
                    new Skin { Name = "M4A1-S | Mecha Industries", Rarity = "Red", ImagePath = "Images/M4A1-S_MechaIndustries.png", Price = 25.00 },
                    new Skin { Name = "Karambit | Gamma Doppler", Rarity = "Gold", ImagePath = "Images/Karambit_GammaDoppler.png", Price = 180.00 }
                }
            },

            new Case
            {
                Name = "Danger Zone Case",
                ImagePath = "Images/DangerZoneCase.png",
                Price = 7.50,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "Tec-9 | Fubar", Rarity = "Blue", ImagePath = "Images/Tec-9_Fubar.png", Price = 0.90 },
                    new Skin { Name = "P250 | Nevermore", Rarity = "Purple", ImagePath = "Images/P250_Nevermore.png", Price = 1.50 },
                    new Skin { Name = "UMP-45 | Momentum", Rarity = "Pink", ImagePath = "Images/UMP-45_Momentum.png", Price = 10.00 },
                    new Skin { Name = "AK-47 | Asiimov", Rarity = "Red", ImagePath = "Images/AK-47_Asiimov.png", Price = 45.00 },
                    new Skin { Name = "Talon Knife | Fade", Rarity = "Gold", ImagePath = "Images/TalonKnife_Fade.png", Price = 220.00 }
                }
            },

            new Case
            {
                Name = "Breakout Case",
                ImagePath = "Images/BreakoutCase.png",
                Price = 15.00,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "MP7 | Urban Hazard", Rarity = "Blue", ImagePath = "Images/MP7_UrbanHazard.png", Price = 1.20 },
                    new Skin { Name = "CZ75-Auto | Tigris", Rarity = "Purple", ImagePath = "Images/CZ75-Auto_Tigris.png", Price = 2.50 },
                    new Skin { Name = "Glock-18 | Water Elemental", Rarity = "Pink", ImagePath = "Images/Glock-18_WaterElemental.png", Price = 12.00 },
                    new Skin { Name = "P90 | Asiimov", Rarity = "Red", ImagePath = "Images/P90_Asiimov.png", Price = 50.00 },
                    new Skin { Name = "Butterfly Knife | Slaughter", Rarity = "Gold", ImagePath = "Images/ButterflyKnife_Slaughter.png", Price = 300.00 }
                }
            },

            
            new Case
            {
                Name = "AWP Case",
                ImagePath = "Images/AWPCase.png",
                Price = 2000.00,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "AWP | Desert Hydra", Rarity = "Red", ImagePath = "Images/AWP_DesertHydra.png", Price = 2000.00 },
                    new Skin { Name = "AWP | Dragon Lore", Rarity = "Red", ImagePath = "Images/AWP_DragonLore.png", Price = 3000.00 },
                    new Skin { Name = "AWP | Fade", Rarity = "Red", ImagePath = "Images/AWP_Fade.png", Price = 800.00 },
                    new Skin { Name = "AWP | Gungnir", Rarity = "Red", ImagePath = "Images/AWP_Gungnir.png", Price = 2500.00 },
                    new Skin { Name = "AWP | Medusa", Rarity = "Red", ImagePath = "Images/AWP_Medusa.png", Price = 2000.00 },
                    new Skin { Name = "AWP | The Prince", Rarity = "Red", ImagePath = "Images/AWP_ThePrince.png", Price = 1500.00 }
                }
            },

            new Case
            {
                Name = "Knife Case",
                ImagePath = "Images/KNIFECase.png",
                Price = 1000.00,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "Butterfly Knife | Gamma Doppler", Rarity = "Gold", ImagePath = "Images/ButterflyKnife_GammaDoppler.png", Price = 1200.00 },
                    new Skin { Name = "M9 Bayonet | Autotronic", Rarity = "Gold", ImagePath = "Images/M9Bayonet_Autotronic.png", Price = 800.00 },
                    new Skin { Name = "Huntsman Knife | Rust Coat", Rarity = "Gold", ImagePath = "Images/HuntsmanKnife_RustCoat.png", Price = 200.00 },
                    new Skin { Name = "Karambit | Doppler", Rarity = "Gold", ImagePath = "Images/Karambit_Doppler.png", Price = 2000.00 },
                    new Skin { Name = "Navaja Knife | Vanilla", Rarity = "Gold", ImagePath = "Images/NavajaKnife_Vanilla.png", Price = 150.00 },
                    new Skin { Name = "Stiletto Knife | Marble Fade", Rarity = "Gold", ImagePath = "Images/StillettoKnife_MarbleFade.png", Price = 750.00 },
                    new Skin { Name = "Survival Knife | Fade", Rarity = "Gold", ImagePath = "Images/SurvivalKnife_Fade.png", Price = 600.00 },
                    new Skin { Name = "Shadow Daggers | Šustekové kolíky", Rarity = "Gold", ImagePath = "Images/ShadowDaggers_BrightWater.png", Price = 300.00 }
                }
            },

            new Case
            {
                Name = "Glove Case",
                ImagePath = "Images/GloveCase.png",
                Price = 900.00,
                Skins = new List<Skin>()
                {
                    new Skin { Name = "Sport Gloves | Vice", Rarity = "Gold", ImagePath = "Images/SportGloves_Vice.png", Price = 1200.00 },
                    new Skin { Name = "Specialist Gloves | Foundation", Rarity = "Gold", ImagePath = "Images/SpecialistGloves_Foundation.png", Price = 900.00 },
                    new Skin { Name = "Driver Gloves | Racing Green", Rarity = "Gold", ImagePath = "Images/DriverGloves_RacingGreen.png", Price = 300.00 },
                    new Skin { Name = "Moto Gloves | Polygon", Rarity = "Gold", ImagePath = "Images/MotoGloves_Polygon.png", Price = 850.00 },
                    new Skin { Name = "Sport Gloves | Snow Leopard", Rarity = "Gold", ImagePath = "Images/SportGloves_SnowLeopard.png", Price = 1400.00 },
                    new Skin { Name = "Bloodhound Gloves | Charred", Rarity = "Gold", ImagePath = "Images/BloodhoundGloves_Charred.png", Price = 200.00 }
                }
            }
        };
    }
}