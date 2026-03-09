namespace DungeonCrawlerG2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create the dungeon map and initialize rooms
            Map dungeonMap = new Map();
            
            dungeonMap.DisplayMap();

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
