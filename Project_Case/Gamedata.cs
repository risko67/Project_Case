using System.Collections.Generic;

namespace CS2_CaseOpening
{
    public static class GameData
    {
        
        public static string CurrencySymbol = "€";

        public static double Balance = 100.0;
        public static string BalanceFormatted => $"{CurrencySymbol}{Balance:0.00}";

        public static List<Skin> MySkins = new List<Skin>();

        public static bool TryOpenCase(Case selectedCase)
        {
            const double CaseCost = 2.5;
            if (Balance < CaseCost)
                return false;
            Balance -= CaseCost;
            return true;
        }

        public static List<Case> Cases = new List<Case>()
        {
            new Case
            {
                Name = "Fever Case",
                ImagePath = "Images/FeverCase.png",
                Skins = new List<Skin>()
                {
                    new Skin
                    {
                        Name = "M4A4 | Choppa",
                        Rarity = "Blue",
                        ImagePath = "Images/M4A4_Choppa.png",
                        Price = 5
                    },
                    new Skin
                    {
                        Name = "Galil AR | Control",
                        Rarity = "Purple",
                        ImagePath = "Images/GalilAR_Control.png",
                        Price = 10
                    },
                    new Skin
                    {
                        Name = "Glock-18 | Shinobu",
                        Rarity = "Pink",
                        ImagePath = "Images/Glock18_Shinobu.png",
                        Price = 25
                    },
                    new Skin
                    {
                        Name = "AWP | Printstream",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_Printstream.png",
                        Price = 60
                    },
                    new Skin
                    {
                        Name = "Skeleton Knife | Doppler",
                        Rarity = "Gold",
                        ImagePath = "Images/SkeletonKnife_Doppler.png",
                        Price = 150
                    }
                }
            },

            new Case
            {
                Name = "Dreams & Nightmares Case",
                ImagePath = "Images/Dreams&NightmaresCase.png",
                Skins = new List<Skin>()
                {
                    new Skin
                    {
                        Name = "Five-SeveN | Scrawl",
                        Rarity = "Blue",
                        ImagePath = "Images/Five-SeveN_Scrawl.png",
                        Price = 5
                    },
                    new Skin
                    {
                        Name = "USP-S | Ticket to Hell",
                        Rarity = "Purple",
                        ImagePath = "Images/USP-S_TickettoHell.png",
                        Price = 12
                    },
                    new Skin
                    {
                        Name = "FAMAS | Rapid Eye Movement",
                        Rarity = "Pink",
                        ImagePath = "Images/FAMAS_RapidEyeMovement.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "MP9 | Starlight Protector",
                        Rarity = "Red",
                        ImagePath = "Images/MP9_StarlightProtector.png",
                        Price = 70
                    },
                    new Skin
                    {
                        Name = "Butterfly Knife | Autotronic",
                        Rarity = "Gold",
                        ImagePath = "Images/ButterflyKnife_Autotronic.png",
                        Price = 200
                    }
                }
            },

            new Case
            {
                Name = "Gamma Case",
                ImagePath = "Images/GammaCase.png",
                Skins = new List<Skin>()
                {
                    new Skin
                    {
                        Name = "MAC-10 | Carnivore",
                        Rarity = "Blue",
                        ImagePath = "Images/MAC-10_Carnivore.png",
                        Price = 6
                    },
                    new Skin
                    {
                        Name = "AWP | Phobos",
                        Rarity = "Purple",
                        ImagePath = "Images/AWP_Phobos.png",
                        Price = 15
                    },
                    new Skin
                    {
                        Name = "P2000 | Imperial Dragon",
                        Rarity = "Pink",
                        ImagePath = "Images/P2000_ImperialDragon.png",
                        Price = 35
                    },
                    new Skin
                    {
                        Name = "M4A1-S | Mecha Industries",
                        Rarity = "Red",
                        ImagePath = "Images/M4A1-S_MechaIndustries.png",
                        Price = 80
                    },
                    new Skin
                    {
                        Name = "Karambit | Gamma Doppler",
                        Rarity = "Gold",
                        ImagePath = "Images/Karambit_GammaDoppler.png",
                        Price = 250
                    }
                }
            },

            new Case
            {
                Name = "Danger Zone Case",
                ImagePath = "Images/DangerZoneCase.png",
                Skins = new List<Skin>()
                {
                    new Skin
                    {
                        Name = "Tec-9 | Fubar",
                        Rarity = "Blue",
                        ImagePath = "Images/Tec-9_Fubar.png",
                        Price = 5
                    },
                    new Skin
                    {
                        Name = "P250 | Nevermore",
                        Rarity = "Purple",
                        ImagePath = "Images/P250_Nevermore.png",
                        Price = 12
                    },
                    new Skin
                    {
                        Name = "UMP-45 | Momentum",
                        Rarity = "Pink",
                        ImagePath = "Images/UMP-45_Momentum.png",
                        Price = 28
                    },
                    new Skin
                    {
                        Name = "AK-47 | Asiimov",
                        Rarity = "Red",
                        ImagePath = "Images/AK-47_Asiimov.png",
                        Price = 75
                    },
                    new Skin
                    {
                        Name = "Talon Knife | Fade",
                        Rarity = "Gold",
                        ImagePath = "Images/TalonKnife_Fade.png",
                        Price = 220
                    }
                }
            },
            new Case
            {
                Name = "Breakout Case",
                ImagePath = "Images/BreakoutCase.png",
                Skins = new List<Skin>()
                {
                    new Skin
                    {
                        Name = "MP7 | Urban Hazard",
                        Rarity = "Blue",
                        ImagePath = "Images/MP7_UrbanHazard.png",
                        Price = 5
                    },
                    new Skin
                    {
                        Name = "CZ75-Auto | Tigris",
                        Rarity = "Purple",
                        ImagePath = "Images/CZ75-Auto_Tigris.png",
                        Price = 12
                    },
                    new Skin
                    {
                        Name = "Glock-18 | Water Elemental",
                        Rarity = "Pink",
                        ImagePath = "Images/Glock-18_WaterElemental.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "P90 | Asiimov",
                        Rarity = "Red",
                        ImagePath = "Images/P90_Asiimov.png",
                        Price = 70
                    },
                    new Skin
                    {
                        Name = "Butterfly Knife | Slaughter",
                        Rarity = "Gold",
                        ImagePath = "Images/ButterflyKnife_Slaughter.png",
                        Price = 150
                    }
                }
            },
            new Case
            {
                Name = "AWP Case",
                ImagePath = "Images/AWPCase.png",
                Skins = new List<Skin>()
                {
                    new Skin
                    {
                        Name = "AWP | Desert Hydra",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_DesertHydra.png",
                        Price = 5
                    },
                    new Skin
                    {
                        Name = "AWP | Dragon Lore",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_DragonLore.png",
                        Price = 12
                    },
                    new Skin
                    {
                        Name = "AWP | Fade",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_Fade.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "AWP | Gungnir",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_Gungnir.png",
                        Price = 80
                    },
                    new Skin
                    {
                        Name = "AWP | Medusa",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_Medusa.png",
                        Price = 200
                    },
                    new Skin
                    {
                        Name = "AWP | The Prince",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_ThePrince.png",
                        Price = 150
                    }
                }
            },
          new Case
            {
                Name = "Knife Case",
                ImagePath = "Images/KNIFECase.png",
                Skins = new List<Skin>()
                {
                    new Skin
                    {
                        Name = "Butterfly Knife | Gamma Doppler",
                        Rarity = "Gold",
                        ImagePath = "Images/ButterflyKnife_GammaDoppler.png",
                        Price = 5
                    },
                    new Skin
                    {
                        Name = "M9 Bayonet | Autotronic",
                        Rarity = "Gold",
                        ImagePath = "Images/M9Bayonet_Autotronic.png",
                        Price = 12
                    },
                    new Skin
                    {
                        Name = "Huntsman Knife | Rust Coat",
                        Rarity = "Gold",
                        ImagePath = "Images/HuntsmanKnife_RustCoat.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "Karambit | Doppler",
                        Rarity = "Pink",
                        ImagePath = "Images/Karambit_Doppler.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "Navaja Knife | Vanilla",
                        Rarity = "Gold",
                        ImagePath = "Images/NavajaKnife_Vanilla.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "Stiletto Knife | Marble Fade",
                        Rarity = "Gold",
                        ImagePath = "Images/StillettoKnife_MarbleFade.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "Survival Knife | Fade",
                        Rarity = "Gold",
                        ImagePath = "Images/SurvivalKnife_Fade.png",
                        Price = 30
                    },
                    new Skin
                    {
                        Name = "Shadow Daggers | Šusťove kolíky",
                        Rarity = "Gold",
                        ImagePath = "Images/ShadowDaggers_BrightWater.png",
                        Price = 30
                    },
                }
            }
        };
    }
}