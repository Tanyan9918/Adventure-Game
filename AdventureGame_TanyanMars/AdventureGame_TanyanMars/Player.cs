namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// The Player class represents the users player
    /// </summary>
    public class Player : ICharacter
    {
        /// <summary>
        /// Gets and sets the X coordinate of the player.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets and sets the Y coordinate of the player.
        /// </summary>
        public int Y { get; set; }
        private string PlayerMarker;
        private ConsoleColor PlayerColor;
        /// <summary>
        /// Gets and sets the health of the player.
        /// </summary>
        private int health { get; set; }
        /// <summary>
        /// Gets and sets the strength of the player.
        /// </summary>
        private int strength { get; set; }

        /// <summary>
        /// Gives the intializer of a player object
        /// </summary>
        /// <param name="startingX">startingX is the original x location of the player</param>
        /// <param name="startingY">startingY is the original y location of the player</param>
        public Player(int startingX, int startingY)
        {
            X = startingX;
            Y = startingY;
            health = 100;
            strength = 15;
            PlayerMarker = "0";
            PlayerColor = ConsoleColor.Magenta;
        }
        /// <summary>
        /// Stats retrieve the playerStrength or playerHealth
        /// </summary>
        /// <param name="playerId">playerId is the identification of the int you want to recieve</param>
        /// <returns>Returns health or strength</returns>
        public int Stats(int playerId)
        {
            if (playerId == 1)
            {
                
                return health;
            } else
            {
                
                return strength;
            }
        }
        /// <summary>
        /// CharacterAttack is to return the strength after you pick up a weapo
        /// </summary>
        /// <param name="updateStrength">updateStrength is the original strength plus the modifier from the weapon</param>
        /// <returns>Updated strength</returns>
        public int CharacterAttack(int updateStrength)
        {
            strength = updateStrength;
            return strength;
        }
        /// <summary>
        /// CharacterUpdateHealth is to return the health after you use a potion
        /// </summary>
        /// <param name="updateHealth">updateHealth is the original health plus the modifier from the potion</param>
        /// <returns>Updated health</returns>
        public int CharacterUpdateHealth(int updateHealth)
        {
            health = updateHealth;
            return health;
        }
        /// <summary>
        /// Displays the player's current health and strength
        /// </summary>
        public void DisplayStats()
        {
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Strength: " + strength);
        }
        /// <summary>
        /// Draw's the player at it's current location
        /// </summary>
        public void Draw()
        {
            Console.ForegroundColor = PlayerColor;
            Console.SetCursorPosition(X, Y);
            Console.Write(PlayerMarker);
            Console.ResetColor();

        }
    }
}
