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
                { new Room("Entrance Hall"), new Room("Armory"), new Room("Library") },
                { new Room("Dining Room"), new Room("Great Hall"), new Room("Kitchen") },
                { new Room("Bedroom"), new Room("Study"), new Room("Treasure Room") }
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
        
    }
}
