using System;

public class TextAdventure
{
	static public void Main()
	{
		bool alive = true;

		Console.WriteLine($@"You wake up in a stange place.  You must build 5 buildings to leave.
Each builing will take longer to build.  

Building 1 takes 5 resources, 2 takes 8, 3 takes 12, 4 takes 17 and the 5th building takes 23. 


Resources can only be gotten from the dungeon.  Each room traveled in the dungeon gives minimum 1 resource.

The following commands can be used: Continue, Retreat, Build, and Enter
");


		string command = Console.ReadLine();
		
		Player player = new Player();

		while(command.ToUpper() != "Exit" && alive)
		{
			
			if(!Enum.TryParse(command, out Commands movement))
			{
				Console.WriteLine("Invalid Command.  Waking up."); 
				return;
			}
			

			if(movemnet == Commands.Build)
			{
				
			}
			
			if(player.Life <= 0)
			{
				alive = false;
			}

			command = Console.ReadLine();

		}


	}

	public enum Commands
	{
		Continue,
		Retreat,
		Build,
		Enter
	}

	public class Player 
	{
		public int Life;
		public int Resources;

		public Player()
		{
			Life = 20;
			Resources = 0;
		}

	}

	public class Town
	{
		public int Buildings;
		public int BuildingsLeft {
			get{
				return 5 - Buildings;
			}
		}

		public Town(){
			Buildings = 0;
		}
	}
}


