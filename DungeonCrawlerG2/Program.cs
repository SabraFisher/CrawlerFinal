using System;

namespace DungeonCrawlerG2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TitleScreen();

            Map dungeonMap = new Map();
            dungeonMap.DisplayMap();

            Player player = new Player("Hero", 30, 5, dungeonMap.Rooms[1, 1]);

            player.PickUpItem(new Item("Health Potion", "Consumable", 10));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nYou start in the {player.CurrentRoom.Name}");
            Console.ResetColor();

            Creature goblin = new Creature("Goblin", 15, 3, 5);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=================================");
                Console.WriteLine("             MAIN MENU");
                Console.WriteLine("=================================");
                Console.ResetColor();

                Console.WriteLine($"Current Room: {player.CurrentRoom.Name}");
                Console.WriteLine($"Player HP: {player.Health}/{player.MaxHealth} | Level: {player.Level} | XP: {player.XP}/{player.XPToNextLevel}");

                Console.WriteLine("\nChoose an action:");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1 - Move");
                Console.WriteLine("2 - Fight Goblin");
                Console.WriteLine("3 - Show Inventory");
                Console.WriteLine("4 - Flee");
                Console.ResetColor();

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
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("=================================");
                            Console.WriteLine("               COMBAT");
                            Console.WriteLine("=================================");
                            Console.ResetColor();

                            Console.WriteLine($"Hero   {GetHealthBar(player.Health, player.MaxHealth)} {player.Health}/{player.MaxHealth} HP");
                            Console.WriteLine($"Goblin {GetHealthBar(goblin.Health, 15)} {goblin.Health}/15 HP");

                            Console.WriteLine("\nChoose your action:");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("1 - Attack");
                            Console.WriteLine("2 - Use Item");
                            Console.WriteLine("3 - Flee");
                            Console.ResetColor();

                            string combatChoice = Console.ReadLine();

                            if (combatChoice == "1")
                            {
                                player.doDamage(goblin);

                                if (!goblin.IsAlive())
                                {
                                    goblin.OnDefeated(player);
                                    break;
                                }

                                goblin.AttackPlayer(player);
                            }
                            else if (combatChoice == "2")
                            {
                                player.ShowInventory();

                                Console.WriteLine("\nEnter item number to use:");
                                string itemInput = Console.ReadLine();

                                if (int.TryParse(itemInput, out int itemIndex))
                                {
                                    player.UseItem(itemIndex);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid item.");
                                }

                                goblin.AttackPlayer(player);
                            }
                            else if (combatChoice == "3")
                            {
                                if (player.Flee())
                                {
                                    break;
                                }

                                goblin.AttackPlayer(player);
                            }
                            else
                            {
                                Console.WriteLine("Invalid action.");
                            }

                            if (!player.IsAlive())
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("\nYou have died. Game over.");
                                Console.ResetColor();
                                return;
                            }

                            Console.WriteLine("\nPress any key for next turn...");
                            Console.ReadKey();
                        }

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

        static void TitleScreen()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.WriteLine("            DUNGEON CRAWLER");
            Console.WriteLine("===============================================");
            Console.ResetColor();

            Console.WriteLine();
            Console.WriteLine("           A Text Adventure Game");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Developed By:");
            Console.ResetColor();

            Console.WriteLine(" - Aaron Bataeff");
            Console.WriteLine(" - Porter Milloy");
            Console.WriteLine(" - Sabra Fisher");

            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("        Press any key to begin...");
            Console.WriteLine("===============================================");

            Console.ReadKey();
            Console.Clear();
        }

        static string GetHealthBar(int current, int max)
        {
            int barLength = 10;

            double ratio = (double)current / max;
            int filled = (int)Math.Round(ratio * barLength);

            if (filled < 0) filled = 0;
            if (filled > barLength) filled = barLength;

            return "[" + new string('#', filled) + new string('-', barLength - filled) + "]";
        }
    }
}