using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawlerG2
{
    public class Room
    {
        // Each room has a name and a list of connected rooms
        public string RoomName { get; set; }
        public List<Room> ConnectedRooms { get; set; }

        // Constructor to initialize the room with a name and an empty list of connected rooms
        public Room(string roomName)
        {
            RoomName = roomName;
            ConnectedRooms = new List<Room>();
        }
        
        // Override ToString() for easy display of room information
        public override string ToString()
        {
            return RoomName;
        }
    }
}
