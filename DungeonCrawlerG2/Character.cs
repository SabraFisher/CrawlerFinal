using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawlerG2
{
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackDamage { get; set; }


        public Character(string name, int health, int attackDamage)
        {
            Name = name;
            Health = health;
            AttackDamage = attackDamage;
        }

        // Checks if the character is alive
        public bool IsAlive()
        {
            return Health > 0;
        }

        // Character can take damage
        public void takeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0)
            {
                Health = 0;
            }
            Console.WriteLine($"{Name} took {damage} damage! Remaining Health: {Health}");
        }

        // Characters can do damage to their targets
        public void doDamage(Character target)
        {
            Console.WriteLine($"{Name} attacks {target.Name} and deals {AttackDamage} damage.");
            target.takeDamage(AttackDamage);
        }
    }
}
