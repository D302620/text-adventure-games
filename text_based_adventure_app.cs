using CsvHelper;
using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Xml.Serialization;
using text_based_adventure;

public class text_based_adventure_app
{
	Locations locations = new Locations();
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


    //----------------------------locations-----------------------------------------------------

    public void Town(Player character)
    {
        bool choiceMade = false;
        while (choiceMade == false)
		{
            ShowInfo(character);
            Console.WriteLine("test location");
            Console.WriteLine("test dialogue");
            Console.WriteLine("what do you want to do:\n\n1. go to inn test1\n2. test2\n3. test3\n4. test4");
            string choice = Console.ReadLine();
            switch (choice)
            {
				case "1":
					Inn(character);
					break;
                default:
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
            Console.WriteLine("what do you want to do:\n\n1. Save Game\n2. back to town");
            string choice = Console.ReadLine();
            switch (choice)
            {
				case "1":
					var records = new List<Player>();
					records.Add(character);
					using (var writer = new StreamWriter("Save"))
					using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
					{
						csv.WriteRecords(records);
					}
					break;
				case "2":
					choiceMade = true;
					Town(character);
					break;
                default:
                    break;
            }
        }
    }
}
