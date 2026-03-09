using System;
using System.Collections.Generic;

namespace DungeonCrawlerG2
{
    public class Player : Character
    {
        public Room CurrentRoom { get; set; }
        public List<Item> Inventory { get; set; }

        public Player(string name, int health, int attackDamage, Room startingRoom)
            : base(name, health, attackDamage)
        {
            CurrentRoom = startingRoom;
            Inventory = new List<Item>();
        }

        public void Move(Room newRoom)
        {
            CurrentRoom = newRoom;
            Console.WriteLine($"{Name} moved to {CurrentRoom.Name}");
        }

        public void MoveToRoom(int index)
        {
            if (index < 0 || index >= CurrentRoom.ConnectedRooms.Count)
            {
                Console.WriteLine("You can't move that way.");
                return;
            }

            CurrentRoom = CurrentRoom.ConnectedRooms[index];
            Console.WriteLine($"You moved to {CurrentRoom.Name}");
        }

        public void ShowAvailableRooms()
        {
            Console.WriteLine("\nYou can move to:");

            for (int i = 0; i < CurrentRoom.ConnectedRooms.Count; i++)
            {
                Console.WriteLine($"{i}: {CurrentRoom.ConnectedRooms[i].Name}");
            }
        }

        public void PickUpItem(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{Name} picked up {item.Name}");
        }

        public void ShowInventory()
        {
            Console.WriteLine("\nInventory:");

            if (Inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }

            foreach (Item item in Inventory)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }

        public bool Flee()
        {
            Random rand = new Random();
            int chance = rand.Next(0, 2);

            if (chance == 1)
            {
                Console.WriteLine($"{Name} successfully fled!");
                return true;
            }

            Console.WriteLine($"{Name} failed to flee!");
            return false;
        }
    }
}