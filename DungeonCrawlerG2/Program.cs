using System;

namespace DungeonCrawlerG2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the dungeon map and initialize rooms
            Map dungeonMap = new Map();

            dungeonMap.DisplayMap();

            // Create the player starting in the Great Hall
            Player player = new Player("Hero", 30, 5, dungeonMap.Rooms[1, 1]);

            Console.WriteLine($"\nYou start in the {player.CurrentRoom.Name}");

            // Basic movement loop
            while (true)
            {
                Console.WriteLine($"\nCurrent Room: {player.CurrentRoom.Name}");

                // Show available rooms to move to
                player.ShowAvailableRooms();

                Console.WriteLine("\nEnter the number of the room to move to (or type 'exit'):");

                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting game...");
                    break;
                }

                if (int.TryParse(input, out int roomChoice))
                {
                    player.MoveToRoom(roomChoice);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }
    }
}