using System;

namespace DungeonCrawlerG2
{
    public class Creature : Character
    {
        public int XPReward { get; set; }
        public int MaxHealth { get; set; }

        public Creature(string name, int health, int attackDamage, int xpReward)
            : base(name, health, attackDamage)
        {
            XPReward = xpReward;
            MaxHealth = health;
        }

        public void AttackPlayer(Character player)
        {
            Console.WriteLine($"{Name} attacks {player.Name}!");
            doDamage(player);
        }

        public void OnDefeated(Player player)
        {
            Console.WriteLine($"{Name} has been defeated!");

            player.GainXP(XPReward);
        }
    }
}