namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// Represents a base style of class for each of the items
    /// </summary>
    public abstract class Item
    {
        /// <summary>
        /// Draws the item to the maze
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Retrieves the stats for specific id
        /// </summary>
        /// <param name="itemId">Specific Identification for the items</param>
        /// <returns>The items stats</returns>
        public abstract int ItemStats(int itemId);
        /// <summary>
        /// Displays the stats of the items on the console
        /// </summary>
        public abstract void DisplayStats();
        /// <summary>
        /// Edits the player's stats when they come into contact with the items
        /// </summary>
        /// <param name="itemId">Specific Identification for the items</param>
        /// <returns>Edited player stats</returns>
        public abstract int EditStats(int itemId);
    }
}
