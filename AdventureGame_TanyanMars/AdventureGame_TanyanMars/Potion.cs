namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// Represents a potion that modifies the player health once landed on
    /// </summary>
    public class Potion : Item
    {
        private int potionId;
        private int modifier;
        private int Px;
        private int Py;
        private string potionMarker = "P";
        /// <summary>
        /// Initializes the potion object with a potionId and x and y coordinates for it
        /// </summary>
        /// <param name="potionId">Identification for the different potions</param>
        /// <param name="px">The x location for the potion</param>
        /// <param name="py">The y location for the potion</param>
        public Potion(int potionId, int px, int py)
        {
            this.potionId = potionId;
            Px = px;
            Py = py;
        }
        /// <summary>
        /// Gets the identification for the potion
        /// </summary>
        public int PotionId => potionId; // Expose potionId
        /// <summary>
        /// Gets the x location for the potion
        /// </summary>
        public int X => Px;              // Expose Px
        /// <summary>
        /// Gets the y location for the potion
        /// </summary>
        public int Y => Py;
        /// <summary>
        /// Retrieves the health modifier for each potion
        /// </summary>
        /// <param name="potionId">Identification for the different potions</param>
        /// <returns>The health modifier</returns>
        public override int ItemStats(int potionId)
        {
            if (potionId == 1)
            {
                modifier = 15;
            }
            else if (potionId == 2)
            {
                modifier = -15;
            }
            return modifier;
        }
        /// <summary>
        /// Edits the player's health stat
        /// </summary>
        /// <param name="health">The player's current health</param>
        /// <returns>The player's updated health</returns>
        public override int EditStats(int health)
        {
            if(health <= 100)
            {
                health = health + modifier;
                return health;
            }
            return health = 100;
            
        }
        /// <summary>
        /// Displays the stats of the ptoions
        /// </summary>
        public override void DisplayStats()
        {
            Console.Clear();
            if (potionId == 1)
            {
                Console.WriteLine("Yay! You landed on a potion of healing +" + modifier);
            }
            else if (potionId == 2)
            {
                Console.WriteLine("Oh no! You landed on poison " + modifier);
            }
        }
        /// <summary>
        /// Draws the potion to its location
        /// </summary>
        public override void Draw()
        {
            Console.SetCursorPosition(Px, Py);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write(potionMarker);
            Console.ResetColor();
        }
    }
}
