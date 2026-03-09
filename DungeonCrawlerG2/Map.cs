using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawlerG2
{
    public class Map
    {
        // define the map as a 3 x 3 grid of interconnected rooms
        public Room[,] Rooms { get; set; }
        public Map()
        {
            Rooms = new Room[3, 3]
            {
                { new Room("Entrance"), new Room("Armory"), new Room("Library") },
                { new Room("Dining Room"), new Room("Great Hall"), new Room("Alchemy Lab") },
                { new Room("Kitchen"), new Room("Treasure Room"), new Room("Sleeping Quarters") }
            };
            // connect the rooms (for simplicity, we will just connect adjacent rooms)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i > 0) Rooms[i, j].ConnectedRooms.Add(Rooms[i - 1, j]); // connect to room above
                    if (i < 2) Rooms[i, j].ConnectedRooms.Add(Rooms[i + 1, j]); // connect to room below
                    if (j > 0) Rooms[i, j].ConnectedRooms.Add(Rooms[i, j - 1]); // connect to room on the left
                    if (j < 2) Rooms[i, j].ConnectedRooms.Add(Rooms[i, j + 1]); // connect to room on the right
                }
            }
        }

        public void DisplayMap()
        {
            Console.WriteLine("\n╔═══════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                         DUNGEON CRAWLER MAP                            ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════════════════════╝\n");

            for (int i = 0; i < 3; i++)
            {
                // Top border of rooms
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("┌───────────────────┐");
                    if (j < 2) Console.Write("─");
                }
                Console.WriteLine();

                // Room names (centered)
                for (int j = 0; j < 3; j++)
                {
                    const int cellWidth = 19;
                    string roomName = Rooms[i, j].Name;
                    string displayName = roomName.Length > cellWidth 
                        ? roomName.Substring(0, cellWidth) 
                        : roomName;

                    int padding = (cellWidth - displayName.Length) / 2;
                    Console.Write($"│{new string(' ', padding)}{displayName}" +
                        $"{new string(' ', cellWidth - padding - displayName.Length)}│");
                    
                    // Show horizontal connection to the right
                    if (j < 2)
                    {
                        Console.Write("═");
                    }
                }
                Console.WriteLine();

                // Bottom border with vertical connections
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("└───────────────────┘");
                    if (j < 2) Console.Write("─");
                }
                Console.WriteLine();

                // Show vertical connections to rooms below
                if (i < 2)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Console.Write("          ║         ");
                        if (j < 2) Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\n═ = Horizontal connection (East-West)");
            Console.WriteLine("║ = Vertical connection (North-South)\n");
        }
        
    }
}
