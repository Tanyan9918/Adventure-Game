namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// Represents a base class for the characters
    /// </summary>
    public interface ICharacter
    {
        //both the player and the monster need to have these
        /// <summary>
        /// Retrieves the stats based on the characterId
        /// </summary>
        /// <param name="characterId">Specific identification for characters</param>
        /// <returns>The character's stats</returns>
        public int Stats(int characterId);
        /// <summary>
        /// Draws the characters on the maze
        /// </summary>
        public void Draw();
        /// <summary>
        /// Displays the character stats on the console
        /// </summary>
        public void DisplayStats();
        /// <summary>
        /// Updates the characters health with the given value
        /// </summary>
        /// <param name="health">New health value that the character has</param>
        /// <returns>The updated health</returns>
        public int CharacterUpdateHealth(int health);
        /// <summary>
        /// Updates the characters strength with the given value
        /// </summary>
        /// <param name="strength">New strength value that thte character has</param>
        /// <returns>The updated strength</returns>
        public int CharacterAttack(int strength);

    }
}
