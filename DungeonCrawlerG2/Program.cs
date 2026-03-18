using System;
using System.Threading;

namespace DungeonCrawlerG2
{
    internal class Program
    {
        static Random rand = new Random();
        static bool bossAlive = false;
        static int roomsVisited = 0;

        static void Main(string[] args)
        {
            TitleScreen();

            Map dungeonMap = new Map();
            dungeonMap.DisplayMap();

            Player player = new Player("Hero", 50, 5, dungeonMap.Rooms[0, 0]);

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
                            roomsVisited++;

                            if (!bossAlive && roomsVisited >= 9)
                            {
                                StartBossFight(player);
                                bossAlive= true;
                            }
                            else if (rand.Next(100) < 80)
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
            Creature[] enemies =
            {
                new Creature("Baby Slime", 5, 1, 3),
                new Creature("Spider", 7, 2, 4),
                new Creature("Goblin", 10, 3, 5),
                new Creature("Skeleton", 12, 5, 8),
                new Creature("Ghoul", 15, 7, 10),
                new Creature("Ogre", 20, 8, 15),

            };

            Creature enemy = enemies[rand.Next(enemies.Length)];

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nA {enemy.Name} appears!");
            Console.WriteLine($"Press enter to fight the {enemy.Name}");
            Console.ReadKey();

            while (enemy.IsAlive() && player.IsAlive())
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("=================================");
                Console.WriteLine("               COMBAT");
                Console.WriteLine("=================================");
                Console.ResetColor();

                Console.WriteLine($"Hero   {GetHealthBar(player.Health, player.MaxHealth)} {player.Health}/{player.MaxHealth} HP");
                Console.WriteLine($"{enemy.Name} {GetHealthBar(enemy.Health, enemy.MaxHealth)} {enemy.Health}/{enemy.MaxHealth} HP");

                Console.WriteLine("\nChoose your action:");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1 - Attack");
                Console.WriteLine("2 - Use Item");
                Console.WriteLine("3 - Flee");
                Console.ResetColor();

                string combatChoice = Console.ReadLine();

                if (combatChoice == "1")
                {
                    player.doDamage(enemy);

                    if (!enemy.IsAlive())
                    {
                        enemy.OnDefeated(player);

                        int lootRoll = rand.Next(10);

                        if (lootRoll <= 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped a Health Potion!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Health Potion", "A red potion that heals 20 hp", "Consumable", 20));
                        }
                        else if (lootRoll == 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped a Iron Dagger (adds 3 attack damage)!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Iron Dagger", "A small but sturdy dagger", "Weapon", 3));
                        }
                        else if (lootRoll == 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped a Wooden Spear (adds 1 attack damage)!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Wooden Spear", "A sharp wooden spear", "Weapon", 1));
                        }
                        else if (lootRoll == 6)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped a Crossbow (adds 5 attack damage)!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Bow", "A deadly crossbow", "Weapon", 5));
                        }
                        else if (lootRoll == 7)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped a Skull Helmet (adds 5 health)!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Skull Helmet", "Skull of an enemy", "Armor", 5));
                        }
                        else if (lootRoll == 8)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped Magic Pants (adds 3 health)!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Magic Pants", "Pants that hold a magical aura", "Armor", 3));
                        }
                        else if (lootRoll == 9)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped a Colossal Chestplate (adds 10 health)!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Colossal Chestplate", "A rare chestplate forged deep in the dungeon", "Armor", 10));
                        }
                        else if (lootRoll == 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"{enemy.Name} dropped a Mystery Cloak (adds 15 health)!");
                            Console.ResetColor();
                            player.PickUpItem(new Item("Mystery Cloak", ".......", "Armor", 15));
                        }
                        break;
                    }
                    enemy.AttackPlayer(player);
                }
                    

                    
                
                else if (combatChoice == "2")
                {
                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("You have no items.");
                        continue;
                    }
                    Console.Clear();
                    player.ShowInventory();

                    Console.WriteLine("\nEnter item number to use:");
                    string itemInput = Console.ReadLine();

                    if (int.TryParse(itemInput, out int itemIndex))
                    {
                        player.UseItem(itemIndex);
                    }

                }
                else if (combatChoice == "3")
                {
                    if (player.Flee())
                    {
                        return;
                    }

                    enemy.AttackPlayer(player);
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

        static void StartBossFight(Player player)
        {
            Creature boss = new Creature("Ancient Dragon", 60, 10, 100);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("...the ground grumbles");
            Thread.Sleep(1000);
            Console.WriteLine("\n!!! THE ANCIENT DRAGON HAS APPEARED !!!");
            Console.ResetColor();

            Console.WriteLine($"\nPress enter to fight the {boss.Name}");
            Console.ReadKey();

            while (boss.IsAlive() && player.IsAlive())
            {
                Console.Clear();

                Console.WriteLine($"Hero   {GetHealthBar(player.Health, player.MaxHealth)} {player.Health}/{player.MaxHealth} HP");
                Console.WriteLine($"{boss.Name} {GetHealthBar(boss.Health, boss.MaxHealth)} {boss.Health}/{boss.MaxHealth} HP");
                Console.WriteLine("\nChoose your action:");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1 - Attack");
                Console.WriteLine("2 - Use Item");
                Console.ResetColor();

                string combatChoice = Console.ReadLine();

                if (combatChoice == "1")
                {
                    player.doDamage(boss);

                    if (!boss.IsAlive())
                    {
                        boss.OnDefeated(player);

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nYOU KILLED THE MIGHTY DRAGON");
                        Thread.Sleep(500);
                        Console.WriteLine($"\nThe {boss.Name} dropped a Ancient Sword (adds 15 attack damage!");
                        Console.ResetColor();

                        player.PickUpItem(new Item("Ancient Sword", "An ancient sword dropped from the ancient dragon", "Weapon", 15));

                        Thread.Sleep(500);
                        Console.WriteLine("Press enter to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        boss.AttackPlayer(player);
                    }

                }
                else if (combatChoice == "2")
                {
                    if (player.Inventory.Count == 0)
                    {
                        Console.WriteLine("You have no items.");
                        continue;
                    }
                    Console.Clear();
                    player.ShowInventory();

                    Console.WriteLine("\nEnter item number to use:");
                    string itemInput = Console.ReadLine();

                    if (int.TryParse(itemInput, out int itemIndex))
                    {
                        player.UseItem(itemIndex);
                    }

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