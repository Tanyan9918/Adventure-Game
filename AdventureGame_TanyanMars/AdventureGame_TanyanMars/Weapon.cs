namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// Represents a weapon that modifies the player strength once picked up
    /// </summary>
    public class Weapon : Item
    {
        private int WeaponId;
        private int Modifier;
        public int Wx;
        public int Wy;
        private string weaponMarker = "W";
        /// <summary>
        /// Intializes the weapon object with a weaponId and the x and y coordinates for it
        /// </summary>
        /// <param name="weaponId">Identification for the different weapons</param>
        /// <param name="wx">The x coordinate for the weapon</param>
        /// <param name="wy">The y coordinate for the weapon</param>
        public Weapon(int weaponId, int wx, int wy)
        {
            WeaponId = weaponId;
            Wx = wx;
            Wy = wy;
        }
        /// <summary>
        /// Gets the identification for the weapon
        /// </summary>
        public int weaponId => WeaponId; // Expose weaponId
        /// <summary>
        /// Gets the x location for the weapon
        /// </summary>
        public int X => Wx;              // Expose Wx
        /// <summary>
        /// Gets the y location for the weapon
        /// </summary>
        public int Y => Wy;

        /// <summary>
        /// Retrieves the strength modifier for each weapon
        /// </summary>
        /// <param name="WeaponId">The identification for the weapon</param>
        /// <returns>The modifier for the specific weapon</returns>
        public override int ItemStats(int WeaponId)
        {
            if (WeaponId == 1)//bronze sword
            {
                Modifier = 15;
            }
            else if (WeaponId == 2)//iron sword
            {
                Modifier = 30;
            }
            else if (WeaponId == 3)//diamond sword
            {
                Modifier = 60;

            }

            return Modifier;
        }
        /// <summary>
        /// Displays the stats of the weapon
        /// </summary>
        public override void DisplayStats()
        {
            Console.Clear();
            if (WeaponId == 1)
            {
                Console.WriteLine("You picked up a bronze sword that increases your strength by " + Modifier);
            }
            else if (WeaponId == 2)
            {
                Console.WriteLine("You picked up an iron sword that increases your strength by " + Modifier);
            }
            else if (WeaponId == 3)
            {
                Console.WriteLine("You picked up a diamond sword that increases your strength by " + Modifier);
            }

        }
        /// <summary>
        /// Edits the players strength according to the weapon picked up
        /// </summary>
        /// <param name="strength">The current player strength</param>
        /// <returns>The new strangth of the player</returns>
        public override int EditStats(int strength)
        {
            if (strength == 15)
            {
                strength = strength + Modifier;
                return strength;
            }
            else
            {
                return strength;
            }
        }
        /// <summary>
        /// Draws the weapon at its location
        /// </summary>
        public override void Draw()
        {
            Console.SetCursorPosition(Wx, Wy);
            if (WeaponId == 1)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (WeaponId == 2)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (WeaponId == 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            Console.Write(weaponMarker);
            Console.ResetColor();
        }
    }
}
