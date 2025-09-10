using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_based_adventure
{
    public class Item
    {
        public string Name {  get; set; }

        public string Description { get; set; }
        public int Worth { get; set; }
        public int Healing { get; set; }
        public int Damage { get; set; }
        public Item(string name, string description, int worth, int healing, int damage) 
        {
            Name = name;
            Description = description;
            Worth = worth;
            Healing = healing;
            Damage = damage;
        }

        public Item() { }

        //create loot table for forest area
        public Item ForestLoot()
        {
            List<Item> list = new List<Item>();
            list.Add(new Item("old knife", "a rusty looking old knife. could propably be used once more", 5, 0, 3));
            list.Add(new Item("health potion", "a normal health potion", 10, 20, 0));

            Random random = new Random();
            return list[random.Next(0, list.Count())];

        }

        public Item CastleLoot()
        {
            List<Item> list = new List<Item>();
            list.Add(new Item("ancient necklace", "it looks to be a necklace from a time long past. could be worth a lot", 50, 0, 0));
            list.Add(new Item("Old spell book", "a rare spell book from ancient times. The knowledge lost to time", 60, 0, 0));

            Random random = new Random();
            return list[random.Next(0, list.Count())];
        }
        public List<Item> ShopItems()
        {
            List<Item> list = new List<Item>();
            list.Add(new Item("health potion", "a normal health potion", 10, 20, 0));
            list.Add(new Item("poison", "this might come in use when attacked", 20, 0, 6));
            list.Add(new Item("dagger", "a sharp looking dagger, altough it seems very fragile", 30, 0, 10));

            return list;
        }
    }
}
