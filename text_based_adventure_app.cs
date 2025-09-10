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
	Items items = new Items();
	public void GameStart()
	{
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
        using (var reader = new StreamReader("save"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
			var records = csv.GetRecords<Player>();
			List<Player> list = records.ToList();
			Player character = list[0];
            reader.Close();
            Inn(character);
		}
    }

	public void NewGame()
	{
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
		Console.Clear();
		Console.WriteLine(Character.Name);
		Console.WriteLine($"Money: {Character.Money}");
		Console.WriteLine($"current objective: {Character.Objective}");
		Console.WriteLine("--------------------------------------------");

	}

	public void SearchItem(Player character)
	{
		Console.Clear();
		Items item = items.ForestLoot();
		Console.WriteLine($"you found: {item.Name}");
		Console.WriteLine(item.Description);
		Console.WriteLine("\ndo you want to keep this item\n1. yes\n2. no");
		string choice = Console.ReadLine();
		switch (choice) 
		{
			case "1":
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

	public void ShowItems(Player character)
	{
		int count = 1;
		Console.Clear();
		foreach (Items item in character.Items) 
		{
			Console.WriteLine($"{count}: {item.Name}");

		}
		Console.WriteLine("\npress enter to continue");
		Console.ReadLine();
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
            Console.WriteLine("what do you want to do:\n\n1. go to inn \n2. Bar\n3. forest");
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
					Forest(character);
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
			Console.WriteLine("\n3. scared looking old man");
            string choice = Console.ReadLine();
            switch (choice)
            {
				case "1":
					if (character.Money <= 0)
					{
						Console.WriteLine("you aproach the bar but remember you have no money to your name.\nyou shamefully walk back to a table");
						Console.ReadLine();
					}
                    break;
				case "2":
					Town(character);
					break;
				case "3":
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
            Console.WriteLine("test location");
            Console.WriteLine("test dialogue");
            Console.WriteLine("what do you want to do:\n\n1. back to town\n2. check on items\n3. save game");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    choiceMade = true;
                    Town(character);
                    break;

				case "2": 
					ShowItems(character);
					break; 
                case "3":
					var records = new List<Player>();
					records.Add(character);
					using (var writer = new StreamWriter("Save"))
					using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
					{
						csv.WriteRecords(records);
                        //writer.Close();
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
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Town(character);
                    break;
                case "2":
                    SearchItem(character);
                    break;
                default:
                    break;
            }
        }
    }
}
