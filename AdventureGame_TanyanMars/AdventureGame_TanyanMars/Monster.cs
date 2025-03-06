namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// The Monster class represents a monster that will fight the player
    /// </summary>
    public class Monster : ICharacter
    {
        private int health;
        private int strength;

        public int Mx;
        public int My;
        private string MonsterMark = "M";
        private int monsterId;
        private ConsoleColor monsterColor;
        /// <summary>
        /// Gives the intializer of a monster object
        /// </summary>
        /// <param name="monsterId">monsterId is the identification of the monster</param>
        /// <param name="Mx">Mx is the x location of the monster</param>
        /// <param name="My">My is the y location of the monster</param>
        public Monster(int monsterId, int Mx, int My)
        {
            this.monsterId = monsterId;
            this.Mx = Mx;
            this.My = My;
            monsterColor = ConsoleColor.DarkRed;
        }
        /// <summary>
        /// Gets the identification for the monster
        /// </summary>
        public int MonsterId => monsterId;
        /// <summary>
        /// Gets the X location of the monster
        /// </summary>
        public int X => Mx;
        /// <summary>
        /// Gets the Y location for the monster   
        /// </summary>
        public int Y => My;
        /// <summary>
        /// Stats retrieve the monsterHealth for each monsterId
        /// </summary>
        /// <param name="monsterId">monsterId is the identification of the monster</param>
        /// <returns>Health for a certain monster</returns>
        public int Stats(int monsterId)
        {
            if (monsterId == 1)
            {
                health = 20;
                strength = 20;
            }
            else if (monsterId == 2)
            {
                health = 50;
                strength = 50;
            }
            else if (monsterId == 3)
            {
                health = 75;
                strength = 75;
            }
            return health;
        }
        /// <summary>
        /// CharacterAttack is to return the strength of the monster at which the id matches
        /// </summary>
        /// <param name="monsterId">monsterId is the identification of the monster</param>
        /// <returns>Strength of the monster</returns>
        public int CharacterAttack(int monsterId)
        {
            if (monsterId == 1)
            {
                strength = 20;
            }
            else if (monsterId == 2)
            {
                strength = 50;
            }
            else if (monsterId == 3)
            {
                strength = 75;
            }
            return strength;
        }
        /// <summary>
        /// CharacterUpdateHealth is to return the health after the player fights the monster
        /// </summary>
        /// <param name="updateMonsterHealth">updateHealth is the original health minus the player attacks</param>
        /// <returns>The new health</returns>
        public int CharacterUpdateHealth(int updateMonsterHealth)
        {
            health = updateMonsterHealth;
            return health;
        }
        /// <summary>
        /// Displays the monster's current health and strength
        /// </summary>
        public void DisplayStats()
        {
            Console.Clear();
            Console.WriteLine("Monster health: " + health);
            Console.WriteLine("Monster strength: " + strength);
        }
        /// <summary>
        /// Draw's the monster at it's location
        /// </summary>
        public void Draw()
        {
            Console.ForegroundColor = monsterColor;
            Console.SetCursorPosition(Mx, My);
            Console.Write(MonsterMark);
            Console.ResetColor();
        }

    }
}
