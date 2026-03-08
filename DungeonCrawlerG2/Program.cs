namespace DungeonCrawlerG2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map dungeonMap = new Map();

            // Display the map layout
            Console.WriteLine("=== Dungeon Map (3x3 Grid) ===\n");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"[{dungeonMap.Rooms[i, j].Name}] ");
                }
                Console.WriteLine();
            }

            // Test room connections
            Console.WriteLine("\n=== Example: Great Hall Connections ===");
            Room greatHall = dungeonMap.Rooms[1, 1];
            Console.WriteLine($"{greatHall.Name} is connected to:");
            foreach (var room in greatHall.ConnectedRooms)
            {
                Console.WriteLine($"  - {room.Name}");
            }
        }
    }
}
