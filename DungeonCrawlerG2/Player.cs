using System;
using System.Collections.Generic;

namespace DungeonCrawlerG2
{
    public class Player : Character
    {
        private static Random rand = new Random();

        public Room CurrentRoom { get; set; }
        public List<Item> Inventory { get; set; }

        public int Level { get; set; }
        public int XP { get; set; }
        public int XPToNextLevel { get; set; }

        public Player(string name, int health, int attackDamage, Room startingRoom)
            : base(name, health, attackDamage)
        {
            CurrentRoom = startingRoom;
            Inventory = new List<Item>();

            Level = 1;
            XP = 0;
            XPToNextLevel = 10;
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
            int chance = rand.Next(0, 2);

            if (chance == 1)
            {
                Console.WriteLine($"{Name} successfully fled!");
                return true;
            }

            Console.WriteLine($"{Name} failed to flee!");
            return false;
        }

        public void GainXP(int amount)
        {
            XP += amount;
            Console.WriteLine($"{Name} gained {amount} XP!");

            if (XP >= XPToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            XP = 0;
            XPToNextLevel += 10;

            Health += 5;
            AttackDamage += 2;

            Console.WriteLine($"\n🎉 {Name} leveled up!");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Health increased to {Health}");
            Console.WriteLine($"Attack increased to {AttackDamage}\n");
        }
    }
}