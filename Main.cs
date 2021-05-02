using System;
using System.Collections.Generic;
using System.Linq;

public class TextAdventure
{
	static public void Main()
	{
		bool alive = true;

		Console.WriteLine($@"You wake up in a stange place.  You must build 5 buildings to leave.
Each builing will take longer to build.  

Building 1 takes 5 resources, 2 takes 7, 3 takes 13, 4 takes 23 and the 5th building takes 37. 


Resources can only be gotten from the dungeon.  Each room traveled in the dungeon gives minimum 1 resource.

The following commands can be used: Continue, Retreat, Build, and Enter
");


		string command = Console.ReadLine();
		
		Player player = new Player();
		Town town = new Town();
		Dungeon dungeon = null;

		while(command.ToUpper() != "Exit" && alive)
		{
			
			if(!Enum.TryParse(command.ToUpper(), out Commands movement))
			{
				Console.WriteLine("Invalid Command.  You wake up realizing the whole thing was a dream."); 
				return;
			}
			

			if(movement == Commands.BUILD)
			{
				if(player.Resources >= town.Cost)
				{
					Console.WriteLine("The old man thanks you for building another building for the town");
					town.Buildings += 1;
					player.Resources -= town.Cost;
					Console.WriteLine($"Only {town.BuildingsLeft} buildings left.  The next building will cost {town.Cost} resources.  You have {player.Resources} left.");
					if(town.Buildings < 3)
					{
						player.MaxLife += 2;
					}
					else
					{
						player.MaxLife += 3;
					}
					player.Life = player.MaxLife;
				}	
			}
			
			if(movement == Commands.ENTER 
			   || movement ==  Commands.CONTINUE)
			{
				if (dungeon != null && movement == Commands.ENTER)
				{
					Console.WriteLine("You cannot enter the dungeon because you are already in it.  Continuing on");
				}
				else if(dungeon == null)
				{
					if(movement == Commands.CONTINUE)
					{
						Console.WriteLine("You cannot continue in a dungeon you have not entered.  Entering dungeon");
					}

					dungeon = new Dungeon();
				}

				Rooms room = dungeon.EnterRoom();
				
				player.Resources += 1;

				switch((int)room)
				{
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
						Console.WriteLine($"You have entered an empty room.  You are relieved nothing happened. you have {player.Resources} resources");
						break;
					case 6:
					case 7:
					case 8:
						player.Life -= 1;
						Console.WriteLine($"You have been attacked by a monster.  You lose 1 life.  You have {player.Life} life left you have {player.Resources} resources");  
					 	break;
					case 9:
					case 10:	
						player.Life -= 2;
						Console.WriteLine($"You have been attacked by a monster.  You lose 2 life.  You have {player.Life} life left you have {player.Resources} resources");  
						break;
					case 11:
						player.Life -=3;
						Console.WriteLine($"You have been attacked by a monster.  You lose 3 life.  You have {player.Life} life left you have {player.Resources} resources");  
						break;
					case 12:
						player.Resources +=3;
						Console.WriteLine($"You have found a treasure room.  After collecting the extra treasure you have {player.Resources} resources");  
						break;
					case 13:
						dungeon = null;
						Console.WriteLine($"You have reached the end of the dungeon a nd decide to leave. You have {player.Life} life left.  You also have {player.Resources} resources."); 
					       break;	
				}
			}

			if(movement == Commands.RETREAT)
			{
				if(dungeon == null)
				{
					Console.WriteLine("You are not in a dungeon.");
				}
				else{

				Console.WriteLine("You have retreated from the dungeon.  All progress will be lost.  You fully restor your health.");
				player.Life = player.MaxLife;
				dungeon = null;
				}
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
		CONTINUE,
		RETREAT,
		BUILD,
		ENTER
	}

	public class Player 
	{
		public int Life;
		public int Resources;
		public int MaxLife = 7;

		public Player()
		{
			Life = MaxLife;
			Resources = 0;
		}

	}

	public class Town
	{
		public int Buildings;
		public int BuildingsLeft 
		{
			get{
				return 5 - Buildings;
			}
		}

		public int Cost 
		{
			get
			{
				return 5 + Buildings * (2 * Buildings);
			}
		}

		public Town()
		{
			Buildings = 0;
		}
	}

	public enum Rooms : int
	{
		Empty1 = 1,
		Empty2 = 2,
		Empty3 = 3,
		Empty4 = 4,
		Empty5 = 5,
		LoseOne1 = 6,
		LoseOne2 = 7,
		LoseOne3 = 8,
		LoseTwo1 = 9,
		LoseTwo2 = 10,
		LoseThree = 11,
		ThreeResources = 12,
		End = 13
	}

	public class Dungeon
	{
		public List<Rooms> rooms = new List<Rooms>();


		public Dungeon()
		{
			rooms = new List<Rooms>()
				
			{
				Rooms.Empty1,
				Rooms.Empty2,
				Rooms.Empty3,
				Rooms.Empty4,
				Rooms.Empty5,
				Rooms.LoseOne1,
				Rooms.LoseOne2,
				Rooms.LoseOne3,
				Rooms.LoseTwo1,
				Rooms.LoseTwo2,
				Rooms.LoseThree,
				Rooms.ThreeResources
			};
		}
		
		public Rooms EnterRoom()
		{
			if(rooms.Count == 0)
			{
				return Rooms.End;
			}

			Random random = new Random();
			int index = random.Next(rooms.Count);
			Rooms room = (Rooms)rooms[index];
			rooms.Remove(room);

			return room;
		}		
	}
}


