using Project_Case;
using System.Collections.Generic;

namespace CS2_CaseOpening
{
    public class Case
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public List<Skin> Skins { get; set; }
    }
}