using System;

namespace CS2_CaseOpening
{
    public class Skin
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public string Rarity { get; set; }
        public string ImagePath { get; set; }

        // Numeric float value between 0.0 and 1.0 (lower is generally better condition)
        public double Float { get; set; }

        public string Wear { get; set; }

        public int Price { get; set; }
    }
}