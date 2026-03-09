using System;

namespace DungeonCrawlerG2
{
    public class CommandParser
    {
        public string ParseCommand(string input)
        {
            input = input.ToLower().Trim();

            switch (input)
            {
                case "north":
                case "n":
                    return "north";

                case "south":
                case "s":
                    return "south";

                case "east":
                case "e":
                    return "east";

                case "west":
                case "w":
                    return "west";

                case "inventory":
                case "inv":
                    return "inventory";

                case "flee":
                    return "flee";

                case "exit":
                    return "exit";

                default:
                    return "unknown";
            }
        }
    }
}