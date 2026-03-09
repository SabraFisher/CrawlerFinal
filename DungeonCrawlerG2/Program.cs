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

            // Create a test creature
            Creature goblin = new Creature("Goblin", 15, 3, 5);

            while (true)
            {
                Console.WriteLine($"\nCurrent Room: {player.CurrentRoom.Name}");
                Console.WriteLine($"Player HP: {player.Health} | Level: {player.Level} | XP: {player.XP}/{player.XPToNextLevel}");

                Console.WriteLine("\nChoose an action:");
                Console.WriteLine("1 - Move");
                Console.WriteLine("2 - Fight Goblin");
                Console.WriteLine("3 - Show Inventory");
                Console.WriteLine("4 - Flee");
                Console.WriteLine("exit - Quit");

                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Exiting game...");
                    break;
                }

                switch (input)
                {
                    case "1":
                        player.ShowAvailableRooms();
                        Console.WriteLine("\nEnter the number of the room to move to:");
                        string roomInput = Console.ReadLine();

                        if (int.TryParse(roomInput, out int roomChoice))
                        {
                            player.MoveToRoom(roomChoice);
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("\nCombat begins!");

                        while (goblin.IsAlive() && player.IsAlive())
                        {
                            player.doDamage(goblin);

                            if (!goblin.IsAlive())
                            {
                                goblin.OnDefeated(player);
                                break;
                            }

                            goblin.AttackPlayer(player);

                            if (!player.IsAlive())
                            {
                                Console.WriteLine("\nYou have died. Game over.");
                                return;
                            }
                        }

                        // reset goblin so you can fight again
                        goblin = new Creature("Goblin", 15, 3, 5);

                        break;

                    case "3":
                        player.ShowInventory();
                        break;

                    case "4":
                        player.Flee();
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }
    }
}