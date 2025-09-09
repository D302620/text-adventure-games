using System;
using System.ComponentModel;
using System.Data.SqlTypes;
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
			Console.WriteLine("loop");
            ShowInfo(character);
            Console.WriteLine("test location");
            Console.WriteLine("test dialogue");
            Console.WriteLine("what do you want to do:\n\n1. test1\n2. test2\n3. test3\n4. test4");
            string choice = Console.ReadLine();
            switch (choice)
            {
                default:
                    break;
            }
        }
    }
}
