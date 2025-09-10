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
		// get Player Items
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
		Console.WriteLine("press enter to start");
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

	public void SearchItem(Player character)
	{
		//get random item from loot table

		Console.Clear();
		Item item = items.ForestLoot();
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
			Console.WriteLine($"{count}: {item.Name}");
			count++;
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
            Console.WriteLine("what do you want to do:\n\n1. go to inn \n2. Bar\n3. shop\n4. forest");
            string choice = Console.ReadLine();
            switch (choice)
            {
				case "1":
					Inn(character);
					break;
				case "2":
					Bar(character);
					break;
				case "3":
					Shop(character);
					break;
				case "4":
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
            Console.WriteLine("what do you want to do:\n\n1. sell items\n2. buy items\n3. back to town\n");
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

	public void Bar(Player character)
	{
        bool choiceMade = false;
        while (choiceMade == false)
        {
            ShowInfo(character);
            Console.WriteLine("Town bar");
            Console.WriteLine("your standing in the bar. Maybe you could find someone offering a quest");
            Console.WriteLine("what do you want to do\n1. go to the bar\n2. back to town");
			if (character.Story == 1)
			{
                Console.WriteLine("\n3. scared looking old man");
            }
            string choice = Console.ReadLine();
            switch (choice)
            {
				case "1":
					if (character.Money < 20)
					{
						Console.WriteLine("you aproach the bar but remember you have no money to your name.\nyou shamefully walk back to a table");
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
            Console.WriteLine("");
            Console.WriteLine("what do you want to do:\n\n1. back to town\n2. check on items\n3. save game");
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
            Console.WriteLine("Town center");
            Console.WriteLine("your standing in the town center. there is not much around except for an inn a shop and a bar");
            Console.WriteLine("what do you want to do:\n\n1. back to town \n2. search the forest\n");
			if (character.Story == 2)
			{
				Console.WriteLine("3. search for the lost dog");
			}
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Town(character);
                    break;
                case "2":
                    SearchItem(character);
                    break;
				case "3":
					if (character.Story == 2)
					{
						Console.Clear();
						Console.WriteLine("After searching for a long time you hear barking coming from a bush. \nafter looking in the bush you find the old man's dog\nhe has a weird collar which appears to have key shaped orniment");
						Console.ReadLine();
						Console.Clear();
						Console.WriteLine("the dog has been added to your inventory");
						character.Items.Add(new Item("dog", "the dog of the old man you have been asked to receive", 0, 0, 0));
						character.Story++;
						character.Objective = "Return the old man's dog";
						Console.ReadLine();
					}
					break;
                default:
                    break;
            }
        }
    }
}
