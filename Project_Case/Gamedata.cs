using System.Collections.Generic;

namespace CS2_CaseOpening
{
    public static class GameData
    {
        
        public static List<Skin> MySkins = new List<Skin>();

       
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
                        ImagePath = "Images/M4A4_Choppa.png"
                    },

                    new Skin
                    {
                        Name = "Galil AR | Control",
                        Rarity = "Purple",
                        ImagePath = "Images/GalilAR_Control.png"
                    },

                    new Skin
                    {
                        Name = "Glock-18 | Shinobu",
                        Rarity = "Pink",
                        ImagePath = "Images/Glock18_Shinobu.png"
                    },

                    new Skin
                    {
                        Name = "AWP | Printstream",
                        Rarity = "Red",
                        ImagePath = "Images/AWP_Printstream.png"
                    },

                    new Skin
                    {
                        Name = "Skeleton Knife | Doppler",
                        Rarity = "Gold",
                        ImagePath = "Images/SkeletonKnife_Doppler.png"
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
                        ImagePath = "Images/Five-SeveN_Scrawl.png"
                    },

                    new Skin
                    {
                        Name = "USP-S | Ticket to Hell",
                        Rarity = "Purple",
                        ImagePath = "Images/USP-S_TickettoHell.png"
                    },

                    new Skin
                    {
                        Name = "FAMAS | Rapid Eye Movement",
                        Rarity = "Pink",
                        ImagePath = "Images/FAMAS_RapidEyeMovement.png"
                    },

                    new Skin
                    {
                        Name = "MP9 | Starlight Protector",
                        Rarity = "Red",
                        ImagePath = "Images/MP9_StarlightProtector.png"
                    },

                    new Skin
                    {
                        Name = "Butterfly Knife | Autotronic",
                        Rarity = "Gold",
                        ImagePath = "Images/ButterflyKnife_Autotronic.png"
                    }
                }
            }
        };
    }
}
    
    
   
