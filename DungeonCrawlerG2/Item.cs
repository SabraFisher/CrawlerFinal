using System;

namespace DungeonCrawlerG2
{
    public class Item
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }

        public Item(string name, string type, int value)
        {
            Name = name;
            Type = type;
            Value = value;
        }

        public void Use(Player player)
        {
            if (Type == "Consumable")
            {
                player.Health += Value;

                Console.WriteLine($"{player.Name} restored {Value} health!");
                Console.WriteLine($"Current HP: {player.Health}");
            }
            else
            {
                Console.WriteLine($"{Name} cannot be used.");
            }
        }
    }
}