using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawlerG2
{
    public class Room
    {
        public string Name { get; set; }
        public List<Room> ConnectedRooms { get; set; }

        public Room(string name)
        {
            Name = name;
            ConnectedRooms = new List<Room>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
