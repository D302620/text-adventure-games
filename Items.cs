using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_based_adventure
{
    public class Items
    {
        public string Name {  get; set; }

        public string Description { get; set; }
        public int Worth { get; set; }
        public int Healing { get; set; }
        public int Damage { get; set; }
        public Items(string name, string description, int worth, int healing, int damage) 
        {
            Name = name;
            Description = description;
            Worth = worth;
            Healing = healing;
            Damage = damage;
        }

        public Items() { }

        public Items ForestLoot()
        {
            List<Items> list = new List<Items>();
            list.Add(new Items("old knife", "a rusty looking old knife. could propably be used once more", 5, 0, 3));
            list.Add(new Items("health potion", "a normal health potion", 10, 20, 0));

            Random random = new Random();
            return list[random.Next(0, list.Count())];

        }
    }
}
