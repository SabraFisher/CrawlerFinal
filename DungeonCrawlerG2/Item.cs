using System;

namespace DungeonCrawlerG2
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }

        public Item(string name, string description, string type, int value)
        {
            Name = name;
            Description = description;
            Type = type;
            Value = value;
        }

        public void PrintItem()
        {
            Console.WriteLine($"{Name} - {Description}");
        }

        public void Use(Player player)
        {
            if (Type == "Consumable")
            {
                player.Health += Value;

                Console.WriteLine($"{player.Name} restored {Value} health!");
                Console.WriteLine($"Current HP: {player.Health}");
            }
            else if (Type == "Weapon")
            {
                player.AttackDamage += Value;
                Console.WriteLine($"{player.Name}'s attack damage has increased by {Value}");
                Console.WriteLine($"Current attack damage: {player.AttackDamage}");
            }
            else if (Type == "Armor")
            {
                player.Health += Value;
                Console.WriteLine($"{player.Name}'s health has increased by {Value}");
                Console.WriteLine($"Current HP: {player.Health}");
            }
            else
            {
                Console.WriteLine($"{Name} cannot be used.");
            }
        }
    }
}