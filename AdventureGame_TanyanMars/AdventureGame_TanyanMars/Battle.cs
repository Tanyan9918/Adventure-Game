using System.ComponentModel.DataAnnotations;

namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// Represents a battle between a player and a monster
    /// </summary>
    public class Battle
    {
        private int monsterHea;
        private int monsterStr;
        private int playerHea;
        private int playerStr;
        /// <summary>
        /// Intializes the battle object with the health and strength from both monster and player
        /// </summary>
        /// <param name="monsterHea">The monster's current health</param>
        /// <param name="monsterStr">The monster's current strength</param>
        /// <param name="playerHea">The player's current health</param>
        /// <param name="playerStr">The player's current strength</param>
        public Battle(int monsterHea, int monsterStr, int playerHea, int playerStr)
        {
            this.monsterHea = monsterHea;
            this.monsterStr = monsterStr;
            this.playerHea = playerHea;
            this.playerStr = playerStr;
        }
        /// <summary>
        /// Makes the two characters fight till one reaches zero and displays it on the console
        /// </summary>
        /// <returns>The player's health at the end</returns>
        public int Fight()
        {
            while(monsterHea > 0 && playerHea > 0)
            {
                Console.Clear();
                Console.WriteLine("Monster Health: " + monsterHea);
                Console.WriteLine("Your Health: " + playerHea);
                playerHea -= monsterStr;
                Console.WriteLine("The monster attacks you for " + monsterStr);
                
                monsterHea -= playerStr;
                Console.WriteLine("You attack the monster back for " +  playerStr);
                Console.WriteLine("Press any key to continue.....");
                Console.ReadKey();
                
            }
            return playerHea;
        }

        
    }
}
