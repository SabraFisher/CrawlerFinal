using System;
using System.Threading;

namespace DungeonCrawlerG2
{
    internal class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            TitleScreen();

            Map dungeonMap = new Map();
            dungeonMap.DisplayMap();

            Player player = new Player("Hero", 30, 5, dungeonMap.Rooms[1, 1]);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nYou start in the {player.CurrentRoom.RoomName}");
            Console.ResetColor();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=================================");
                Console.WriteLine("             MAIN MENU");
                Console.WriteLine("=================================");
                Console.ResetColor();

                Console.WriteLine($"Current Room: {player.CurrentRoom.RoomName}");
                Console.WriteLine($"Player HP: {player.Health}/{player.MaxHealth} | Level: {player.Level} | XP: {player.XP}/{player.XPToNextLevel}");

                Console.WriteLine("\nChoose an action:");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1 - Move");
                Console.WriteLine("2 - Show Inventory");
                Console.WriteLine("3 - Flee");
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

                            if (rand.Next(2) == 1)
                            {
                                StartCombat(player);
                            }
                            else
                            {
                                Console.WriteLine("\nThe room is quiet...");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }

                        break;

                    case "2":
                        player.ShowInventory();
                        break;

                    case "3":
                        player.Flee();
                        break;

                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
        }

        static void StartCombat(Player player)
        {
            Creature goblin = new Creature("Goblin", 15, 3, 5);

            Console.WriteLine("\nA Goblin appears!");

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

                        if (rand.Next(4) == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("\nThe goblin dropped a Health Potion!");
                            Console.ResetColor();

                            player.PickUpItem(new Item("Health Potion", "Consumable", 10));
                        }

                        break;
                    }

                    goblin.AttackPlayer(player);
                }
                else if (combatChoice == "2")
                {
                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("You have no items.");
                        goblin.AttackPlayer(player);
                        continue;
                    }

                    player.ShowInventory();

                    Console.WriteLine("\nEnter item number to use:");
                    string itemInput = Console.ReadLine();

                    if (int.TryParse(itemInput, out int itemIndex))
                    {
                        player.UseItem(itemIndex);
                    }

                    goblin.AttackPlayer(player);
                }
                else if (combatChoice == "3")
                {
                    if (player.Flee())
                    {
                        return;
                    }

                    goblin.AttackPlayer(player);
                }

                if (!player.IsAlive())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nYou have died. Game over.");
                    Console.ResetColor();
                    Environment.Exit(0);
                }

                Console.WriteLine("\nPress any key for next turn...");
                Console.ReadKey();
            }
        }

        static void TitleScreen()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.ResetColor();

            TypeText("                 DUNGEON CRAWLER\n", 40);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===============================================");
            Console.ResetColor();

            Console.WriteLine();
            TypeText("              A Text Adventure Game\n", 20);
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Developed By:");
            Console.ResetColor();

            Console.WriteLine(" - Aaron Bataeff");
            Console.WriteLine(" - Porter Milloy");
            Console.WriteLine(" - Sabra Fisher");

            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("            Press any key to begin...");
            Console.WriteLine("===============================================");

            Console.ReadKey();
            Console.Clear();
        }

        static void TypeText(string text, int delay)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
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