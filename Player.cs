using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_based_adventure
{
    public class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Strength { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Story { get; set; }
        
        public string Objective { get; set; }

        public List<Item> Items { get; set; }

        //create new game
        public Player(string name) 
        {
            Name = name;
            Money = 0;
            Strength = 1;
            Health = 100;
            Speed = 1;
            Story = 1;
            Objective = "find a quest";
            Items = new List<Item>();
        }
        // loading save game
        public Player()
        {
            Money = 0;
            Strength = 1;
            Health = 100;
            Speed = 1;
            Story = 1;
            Objective = "find a quest";
            Items = new List<Item>();
        }
    }
}
