using CsvHelper;
using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using text_based_adventure;

public class text_based_adventure_app
{
	Item items = new Item();
	public void GameStart()
	{
		// give player option of loading previous save
		string choice;
		Console.WriteLine("1. start new game\n2. load from save");
		choice = Console.ReadLine();

		switch(choice){
			case "1":
				NewGame();
				break;
			case "2":
				LoadSave();
				break;
		}
	}

	public void LoadSave()
	{
		//load last save from csv
		Player character = new Player();
        using (var reader = new StreamReader("save"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
			try
			{
                var records = csv.GetRecords<Player>();
                List<Player> list = records.ToList();
                character = list[0];
                reader.Close();
            }
			catch
			{
				Console.Clear();
                reader.Close();
				Console.WriteLine("save game was not found\nredirecting to new game");
				Console.ReadLine();
				NewGame();
            }
		}
		// get Player Items from seperate csv
        using (var reader = new StreamReader("saveItems"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            try
            {
                var records = csv.GetRecords<Item>();
                List<Item> list = records.ToList();
                character.Items = list;
                reader.Close();
            }
            catch
            {
				reader.Close();
            }
        }

        Inn(character);
    }

	public void NewGame()
	{
		// create new player character

		Console.Clear();
		Console.WriteLine("enter character name:");
		string name = Console.ReadLine();
		Player character = new Player(name);
		Console.WriteLine("your traveler looking for quests around the world. you find yourself in a small town looking for a quest to earn some money");
		Console.ReadLine();
		Town(character);
	}

	public void ShowInfo(Player Character)
	{
		//show all important player info on screen

		Console.Clear();
		Console.WriteLine(Character.Name);
		Console.WriteLine($"Gold: {Character.Money}");
		Console.WriteLine($"current objective: {Character.Objective}");
		Console.WriteLine("--------------------------------------------");

	}

	public void SearchItem(Player character, int location)
	{
		//get random item from loot table
		Item item = new Item();

		Console.Clear();
		switch (location)
		{
			case 1:
                item = items.ForestLoot();
				break;
			case 2:
				item = items.CastleLoot();
				break;
			default: 
				break;
        }
		
		Console.WriteLine($"you found: {item.Name}");
		Console.WriteLine(item.Description);
		Console.WriteLine("\ndo you want to keep this item\n1. yes\n2. no");
		string choice = Console.ReadLine();
		switch (choice) 
		{
			case "1":
				// add item to character inventory

				Console.WriteLine("you decide to put item in your bag");
				Console.ReadLine();
				character.Items.Add(item);
				break;
			case "2":
				Console.WriteLine("you decide to throw it away deciding you don't need it");
				Console.ReadLine();
				break;
			default:
				Console.WriteLine("you can't decide what to do with the item so you put it in your bag anyway");
                Console.ReadLine();
                character.Items.Add(item);
				break;
        }
	}

	public void ShowItems(List<Item> items)
	{
		int count = 1;
		Console.Clear();
		foreach (Item item in items) 
		{
			Console.WriteLine($"{count}: {item.Name} - ({item.Worth}) gold");
			count++;
		}
	}

	public void Info()
	{
		// info for the user about the game. describes the basic mechanics of the game
		bool looking = true;
		while(looking)
		{
			Console.Clear();
            Console.WriteLine("the board says: welcome travelers below is information you might want to know on you travels");
            Console.WriteLine("\nyou see the options: \n1. Making choises\n2. the inn\n3. shop\n4. searching\n");
			string choice = Console.ReadLine();
			Console.Clear();
			switch (choice) 
			{
				case "1":
					Console.WriteLine("everytime you enter an area you be prompted with choises which are numbered.\njust enter the number shown to choose your option");
					Console.ReadLine();
					break;
				case "2":
                    Console.WriteLine("in the inn you can save your current progress, check in on your items and end exit the game");
                    Console.ReadLine();
                    break;
				case "3":
                    Console.WriteLine("in the shop you can sell you well earned items or buy items you might need on your journey.");
                    Console.ReadLine();
                    break;
				case "4":
                    Console.WriteLine("you can search unexplored area's to find new items for money or for use.\ndifferent locations might give you different items");
                    Console.ReadLine();
                    break;
				default :
					looking = false;
					break;
            }

        }
				
	}

    public void Fortune(Player character)
    {
		// give hint to player when stuck. baced of the current story plot
        Console.Clear();
        if (character.Money < 20)
        {
            Console.WriteLine("you don't have enough gold to get a fortune");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("it costs 20 gold to get you fortune. do you want to continue\n\n1. get fortune read");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                character.Money -= 20;
				Console.Clear();
				//check what part of the story the player is on
                switch (character.Story)
                {
                    case 1:
                        Console.WriteLine("fortune teller: i can see your journey will take you to the tavern.\nthere you will find a wise man in need of help");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.WriteLine("fortune teller: i can see dog in need of help. hiding in the bushes of the forest, there is where you will find your missing friend");
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.WriteLine("fortune teller: you must return to the place where your journey started. someone there will show you the way");
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.WriteLine("fortune teller: i see a castle deep in the forest barred behind a gate.\nthe key for this gate might be closer then you think");
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("fortune teller: i see a long staircase before you. \nat the end of this staircase i can sense an important choice will be bestowed upon you");
                        Console.ReadLine();
                        break;
					default:
						break;
                }
            }
        }
    }

    public void EndGame(int ending)
	{
		// all endings and normal game exit
		Console.Clear();
		switch (ending)
		{
			case 0:
				Console.WriteLine("exiting game");
				System.Environment.Exit(1);
				break;
			case 1:
				Console.WriteLine("u decided to sell of the old man's dog. After a few days in town the old man has found out about your action\nAs your actions became known to the town the towns folk began shunning you driving you out of there town. \n\nyou may never return");
                System.Environment.Exit(1);
                break;
			case 2:
				Console.WriteLine("u take wizard up on his offer, finding the ways of magic intriguing.\n\nyou begin working for the wizard as his attendent. slowly after a while he starts teaching you magic, after multible years of hard work you become one of only few magic users in the world");
                System.Environment.Exit(1);
                break;
			case 3:
				Console.WriteLine("u decide to decline the wizards offer to continue your life as an adventurer\n\nthe wizard thanks him for finding his dog and rewards him generously for his efforts.\nwith once again a quest finished you leave the castle and town in search for a new quest");
                System.Environment.Exit(1);
                break;
		}
	}



    //----------------------------locations-----------------------------------------------------

    public void Town(Player character)
    {
        bool choiceMade = false;
        while (choiceMade == false)
		{
            ShowInfo(character);
            Console.WriteLine("Town center");
            Console.WriteLine("your standing in the town center. there is not much around except for an inn a shop and a bar");
            Console.WriteLine("\nwhat do you want to do:\n\n1. inn \n2. tavern\n3. shop\n4. instruction board\n5. fortune teller\n6. forest");
            string choice = Console.ReadLine();
            switch (choice)
            {
				case "1":
					Inn(character);
					break;
				case "2":
					Tavern(character);
					break;
				case "3":
					Shop(character);
					break;
				case "4":
					Info();
					break;
				case "5":
					Fortune(character);
					break;
				case "6":
					Forest(character);
					break;
                default:
                    break;
            }
        }
    }

	public void Shop(Player character)
	{
        bool choiceMade = false;
        while (choiceMade == false)
        {
            ShowInfo(character);
            Console.WriteLine("Town Shop");
            Console.WriteLine("Maybe you can sell off some unimportant items");
            Console.WriteLine("\nwhat do you want to do:\n\n1. sell items\n2. buy items\n3. back to town\n");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
					ShowItems(character.Items);
					if (character.Items.Count <= 0)
					{
						Console.WriteLine("you don't have any items to sell");
					}
					else
					{
                        Console.WriteLine("select the item you want to sell");
                    }
					string itemChoice = Console.ReadLine();
					try
					{
						//remove item from player inventory and add gold amount
						Item item = character.Items[int.Parse(itemChoice) - 1];
						//check if the item is a story item
						if (item.Name == "dog")
						{
							EndGame(1);
						}
						Console.Clear();
						Console.WriteLine($"you sold {item.Name} for {item.Worth} gold");
						character.Money += item.Worth;
                        character.Items.Remove(item);
						Console.ReadLine();
					}
					catch 
					{
						Console.WriteLine("going back to store");
						Console.ReadLine();
					}
                    break;
				case "2":
					//get list from item class
					List<Item> itemList = items.ShopItems();
					ShowItems(itemList);
					Console.WriteLine("what would you like to buy");
					string buyChoice = Console.ReadLine();
					try
					{
                        Item item = itemList[int.Parse(buyChoice) - 1];
                        Console.Clear();
                        Console.WriteLine($"are you sure you want to buy {item.Name} for {item.Worth * 2} gold\npress 1 to except");
						buyChoice = Console.ReadLine();
						if (buyChoice == "1")
						{
							if (character.Money < item.Worth * 2)
							{
								Console.WriteLine("you don't have enough money to buy this item");
								Console.ReadLine();
							}
							else
							{
								Console.WriteLine($"you bought {item.Name}");
								character.Items.Add(item);
								character.Money -= item.Worth * 2;
								Console.ReadLine();
							}
						}
                    }
					catch
					{
						Console.WriteLine("going back to store");
						Console.ReadLine();
					}

                    break;
                case "3":
                    Town(character);
                    break;
                default:
                    break;
            }
        }
    }

	public void Tavern(Player character)
	{
        bool choiceMade = false;
        while (choiceMade == false)
        {
            ShowInfo(character);
            Console.WriteLine("Town Tavern");
            Console.WriteLine("your standing in the tavern. Maybe you could find someone offering a quest");
            Console.WriteLine("\nwhat do you want to do\n\n1. go to the bar\n2. back to town");
			if (character.Story == 1)
			{
                Console.WriteLine("\n3. anxious looking old man");
            }
			else if(character.Story == 3)
			{
				Console.WriteLine("\n3. look for the old man");
			}
				string choice = Console.ReadLine();
            switch (choice)
            {
				case "1":
					if (character.Money < 20)
					{
						Console.WriteLine("you aproach the bar but remember you don't have enough money to your name.\nyou shamefully walk back to a table");
						Console.ReadLine();
					}
					else
					{
						Console.WriteLine("you aquire a drink from the bar\nu gained 50 points to your health\n -20 gold");
						character.Money -= 20;
						character.Health += 50;
					}
					break;
				case "2":
					Town(character);
					break;
				case "3":
					// first story progression
					if (character.Story == 1)
					{
						Console.Clear();
						Console.WriteLine("Old man: could you please help me sir. i've lost my dog in the forest and can't find him anywhere\nyou you please look for him?");
						Console.ReadLine();
						Console.Clear();
						Console.WriteLine("new quest aquired: find the old man's dog");
						Console.ReadLine();
						character.Story++;
						character.Objective = "find the old man's dog";


                    }
					else if(character.Story == 3)
					{
						Console.Clear();
						Console.WriteLine("U look around for the old man but can't find him anywhere.\n\nBar owner: You were with that old man earlier right?. it's rare to see him down here, he usually hangs around in the old castle near here");
                        Console.ReadLine();
						Console.Clear();
						Console.WriteLine("new objective: Find the castle the bar owner was talking about");
						Console.ReadLine();
                        character.Story++;
						character.Objective = "Find the castle the bar owner was talking about";
					}
					break;
                default:
                    Console.WriteLine(choiceMade);
                    break;
            }
        }
    }

	public void Inn(Player character)
	{
        bool choiceMade = false;
        while (choiceMade == false)
        {
            ShowInfo(character);
            Console.WriteLine("Town inn");
            Console.WriteLine("a place where you can rest for the time being");
            Console.WriteLine("\nwhat do you want to do:\n\n1. back to town\n2. check on items\n3. save game\n4. exit game");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    choiceMade = true;
                    Town(character);
                    break;

				case "2": 
					ShowItems(character.Items);
                    Console.WriteLine("\npress enter to continue");
                    Console.ReadLine();
                    break; 
                case "3":

					// write player info to text file
					List<Player> records = new List<Player>();
					List<Item> Items = character.Items;
					records.Add(character);
					using (var writer = new StreamWriter("Save"))
					using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
					{
						csv.WriteRecords(records);
                    }
					using (var writer = new StreamWriter("saveItems"))
					using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
					{
						csv.WriteRecords(Items);
					}
					Console.Clear();
					Console.WriteLine("game saved");
					Console.ReadLine();
                    break;
				case "4":
					EndGame(0);
					break;
                default:
                    break;
            }
        }
    }

	public void Forest(Player character)
	{
        bool choiceMade = false;
        while (choiceMade == false)
        {
            ShowInfo(character);
            Console.WriteLine("Forest");
            Console.WriteLine("you find yourself deep in the forest on the edge of town. all you can see in the distance is an old castle");
            Console.WriteLine("\nwhat do you want to do:\n\n1. back to town \n2. search the forest\n3. go to the castle");
			if (character.Story == 2)
			{
				Console.WriteLine("4. search for the lost dog");
			}
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Town(character);
                    break;
                case "2":
                    SearchItem(character, 1);
                    break;
				case "3":
					if(character.Story < 4)
					{
						Console.WriteLine("the castle seems to be blocked off by a gate. Maybe the key is somewhere hidden around here");
					}
					else
					{
						Castle(character);
					}
						break;
				case "4":
					if (character.Story == 2)
					{
						Console.Clear();
						Console.WriteLine("After searching for a long time you hear barking coming from a bush. \nafter looking in the bush you find the old man's dog\nhe has a weird collar which appears to have key shaped orniment");
						Console.ReadLine();
						Console.Clear();
						Console.WriteLine("new objective: Return the old man's dog");
						Console.WriteLine("the dog has been added to your inventory");
						character.Items.Add(new Item("dog", "the dog of the old man you have been asked to receive", 0, 0, 0));
						character.Story++;
						character.Objective = "Return the old man's dog";
						Console.ReadLine();
						Console.WriteLine();

					}
					break;
                default:
                    break;
            }
        }
    }

	public void Castle(Player character)
	{
		bool choiceMade = false;
		while (choiceMade == false)
		{
			ShowInfo(character);
			Console.WriteLine("Old Castle");
			Console.WriteLine("altough it's old looking from the outside it's seems well kept");
			// check if player has unlocked the castle
			if (character.Story == 4)
			{
				Console.WriteLine("\nyou look at the locked gate and remember the dogs collar.\nyou take the key from the collar and try using it on the gate.\n\nthe gate springs open giving you acces to the castle");
                Console.ReadLine();
				Console.Clear();
				Console.WriteLine("new objective: Find where the old man is");
				Console.ReadLine();
                character.Story++;
				character.Objective = "Find where the old man is";
			}
			else
			{
                Console.WriteLine("\nwhat do you want to do:\n\n1. back to Forest \n2. search the castle\n3. Spiral staircase");
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						Forest(character);
						break;
					case "2":
						SearchItem(character, 2);
						break;
					case "3":
						Console.Clear();
						Console.WriteLine("you walk up the spiral staircase. after seeming almost never ending you finally find your way to the top");
						Console.WriteLine("you find yourself standing infornt of a decorated door. as you open it you enter a room filled with magical artifacts in the center stands an old wizard");
						Console.WriteLine("\nWizard: it seems you have found my dog for that i can not be more gratefull. who knew putting my trust in a stranger would pay off.\n\nyou know i've been quite lonely alone in this castle i wouldn't mind an assistant to keep me company in here. of course it's you choise");
						Console.ReadLine();
						Console.Clear();
						Console.WriteLine("what do you choose:\n\n1. stay with the wizard\n2. keep traveling on your own");
						string storyChoice = Console.ReadLine();
						if (storyChoice == "1")
						{
							EndGame(2);
						}
						else if (storyChoice == "2")
						{
							EndGame(3);
						}
						break;
					default:
						break;
				}
            }

		}
	}
}
