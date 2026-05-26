using System.Collections.Generic;

namespace CS2_CaseOpening
{
    public class Case
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        // Price of this case (you can change this value in GameData.Cases initializers)
        public double Price { get; set; }

        public List<Skin> Skins { get; set; }
    }
}