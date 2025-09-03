using System;
using System.Xml.Serialization;

public class text_based_adventure_app
{
	public string name;
	public string story;

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
		Console.WriteLine("enter character name:");
		name = Console.ReadLine();
		Console.WriteLine(name);
		Console.ReadLine();
	}


}
