using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_based_adventure
{
    public class Enemies
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Dialougue { get; set; }
        public int Hitpoints { get; set; }

        public Enemies(string name, string description, string dialougue, int hitPoints) 
        {
            Name = name;
            Description = description;
            Dialougue = dialougue;
            Hitpoints = hitPoints;
        }


    }
}
