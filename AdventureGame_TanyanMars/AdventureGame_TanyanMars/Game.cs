namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// The Game class handles everything involving the game
    /// </summary>
    public class Game
    {

        private Maze mazeG;
        private Player currentPlayer;
        private int mazeHeight;
        private int mazeWidth;
        private (int x, int y) currentPlayerLocation;
        private int userMazeWidth;
        private int userMazeHeight;
        private Random randomX;
        private Random randomY;
        private List<Potion> potions = new List<Potion>();
        private List<Weapon> weapons = new List<Weapon>();
        private List<Monster> monsters = new List<Monster>();
        private List<Battle> battles = new List<Battle>();
        private Random rPotionId;
        private Random rWeaponId;
        private Random rMonsterId;
        private int holdingWeapon = 0;
        private string swordChoice;
        private int playerHealth;
        private int playerStrength;
        private Battle currentBattle;
        private int bossX;
        private int bossY;
        /// <summary>
        /// The Start() starts the game
        /// </summary>
        public void Start()
        {
            DisplayIntro();
            (int x, int y) start = (1, 1);
            (int x, int y) end = (mazeWidth - 2, mazeHeight - 3);
            Random rand = new Random();

            mazeG = new Maze(mazeWidth, mazeHeight, start, end, rand);
            mazeG.MazeGenerator(start.x, start.y);
            mazeG.GetMaze()[end.x, end.y] = 0; // Ensure the end point is a path
            currentPlayer = new Player(start.x, start.y);

            bossX = end.x - 1;
            bossY = end.y;
            mazeG.GetLocation((bossX, bossY), "M", 3);
            PlacingItems(mazeHeight * mazeWidth);
            playerHealth = currentPlayer.Stats(1);
            playerStrength = currentPlayer.Stats(2);
            GameLoop();
            DisplayOutro();
        }
        /// <summary>
        /// The DrawFrame() is used to draw each of the frames and resets for each
        /// </summary>
        private void DrawFrame()
        {
            Console.Clear();
            mazeG.PrintMaze();
            foreach (var weapon in weapons)
            {
                weapon.Draw();
            }
            foreach (var potion in potions)
            {
                potion.Draw();
            }
            foreach (var monster in monsters)
            {
                monster.Draw();
            }
            currentPlayer.Draw();
            Console.SetCursorPosition(0, mazeHeight);
            currentPlayer.DisplayStats();
        }
        /// <summary>
        /// The HandlePlayerInput() handles what key the player clicks to move the player
        /// </summary>
        private void HandlePlayerInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (mazeG.IsWalkable(currentPlayer.X, currentPlayer.Y - 1))
                    {
                        currentPlayer.Y -= 1;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (mazeG.IsWalkable(currentPlayer.X, currentPlayer.Y + 1))
                    {
                        currentPlayer.Y += 1;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (mazeG.IsWalkable(currentPlayer.X + 1, currentPlayer.Y))
                    {
                        currentPlayer.X += 1;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (mazeG.IsWalkable(currentPlayer.X - 1, currentPlayer.Y))
                    {
                        currentPlayer.X -= 1;
                    }
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// The GetRandomCoordinates() gets random x and y coordinates that the player can step on to place items on
        /// </summary>
        public (int, int) GetRandomCoordinates()
        {
            int try1;
            int try2;
            (int x, int y) itemLocation;
            randomX = new Random();
            randomY = new Random();
            try1 = randomX.Next(0, mazeWidth);
            try2 = randomY.Next(0, mazeHeight);
            itemLocation = (try1, try2);
            while (!mazeG.IsWalkable(itemLocation.x, itemLocation.y))
            {
                try1 = randomX.Next(0, mazeWidth);
                try2 = randomY.Next(0, mazeHeight);
                itemLocation = (try1, try2);
            }
            return itemLocation;
        }
        /// <summary>
        /// /// <summary>
        /// Places items depending on the mazeArea
        /// </summary>
        /// <param name="mazeArea">Depending on the mazeArea(mazeWidth*mazeHeight) items are placed</param>
        public void PlacingItems(int mazeArea)
        {
            ///Gets the area of the maze to determine how many items need be in the maze
            (int x, int y) itemRandomLocation;
            int potionId;
            int weaponId;
            int monsterId;

            for (int i = 0; i < 3; i++)
            {
                rPotionId = new Random();
                itemRandomLocation = GetRandomCoordinates();
                potionId = rPotionId.Next(1, 3);
                potions.Add(new Potion(potionId, itemRandomLocation.x, itemRandomLocation.y)); // Add potion
                mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "P", potionId);
            }
            
            for (int i = 0; i < 1; i++)
            {
                rWeaponId = new Random();
                itemRandomLocation = GetRandomCoordinates();
                weaponId = rWeaponId.Next(1, 4);
                weapons.Add(new Weapon(weaponId, itemRandomLocation.x, itemRandomLocation.y)); // Add weapon
                mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "W", weaponId);
            }
            for (int i = 0; i < 1; i++)
            {
                rMonsterId = new Random();
                itemRandomLocation = GetRandomCoordinates();
                monsterId = rMonsterId.Next(1, 3);
                monsters.Add(new Monster(monsterId, itemRandomLocation.x, itemRandomLocation.y)); // Add monster
                mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "M", monsterId);
            }
            monsters.Add(new Monster(3, bossX, bossY));
            mazeG.GetLocation((bossX, bossY), "M", 3);


            if (mazeArea >= 1560)
            {
                for (int i = 0; i < 3; i++)
                {
                    rPotionId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    potionId = rPotionId.Next(1, 3);
                    potions.Add(new Potion(potionId, itemRandomLocation.x, itemRandomLocation.y)); // Add potion
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "P", potionId);
                }
                for (int i = 0; i < 3; i++)
                {
                    rWeaponId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    weaponId = rWeaponId.Next(1, 4);
                    weapons.Add(new Weapon(weaponId, itemRandomLocation.x, itemRandomLocation.y)); // Add weapon
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "W", weaponId);
                }
                for (int i = 0; i < 4; i++)
                {
                    rMonsterId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    monsterId = rMonsterId.Next(1, 3);
                    monsters.Add(new Monster(monsterId, itemRandomLocation.x, itemRandomLocation.y)); // Add monster
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "M", monsterId);
                }
            }
            if (mazeArea >= 3120)
            {
                for (int i = 0; i < 3; i++)
                {
                    rPotionId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    potionId = rPotionId.Next(1, 3);
                    potions.Add(new Potion(potionId, itemRandomLocation.x, itemRandomLocation.y)); // Add potion
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "P", potionId);
                }
                for (int i = 0; i < 2; i++)
                {
                    rWeaponId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    weaponId = rWeaponId.Next(1, 4);
                    weapons.Add(new Weapon(weaponId, itemRandomLocation.x, itemRandomLocation.y)); // Add weapon
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "W", weaponId);
                }
                for (int i = 0; i < 4; i++)
                {
                    rMonsterId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    monsterId = rMonsterId.Next(1, 3);
                    monsters.Add(new Monster(monsterId, itemRandomLocation.x, itemRandomLocation.y)); // Add monster
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "M", monsterId);
                }
            }
            if (mazeArea >= 4680)
            {
                for (int i = 0; i < 3; i++)
                {
                    rPotionId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    potionId = rPotionId.Next(1, 3);
                    potions.Add(new Potion(potionId, itemRandomLocation.x, itemRandomLocation.y)); // Add potion
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "P", potionId);
                }
                for (int i = 0; i < 2; i++)
                {
                    rWeaponId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    weaponId = rWeaponId.Next(1, 4);
                    weapons.Add(new Weapon(weaponId, itemRandomLocation.x, itemRandomLocation.y)); // Add weapon
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "W", weaponId);
                }
                for (int i = 0; i < 2; i++)
                {
                    rMonsterId = new Random();
                    itemRandomLocation = GetRandomCoordinates();
                    monsterId = rMonsterId.Next(1, 3);
                    monsters.Add(new Monster(monsterId, itemRandomLocation.x, itemRandomLocation.y)); // Add monster
                    mazeG.GetLocation((itemRandomLocation.x, itemRandomLocation.y), "M", monsterId);
                }
            }


        }
        /// <summary>
        /// The DisplayIntro() displays the directs and handles the size of the maze
        /// </summary>
        private void DisplayIntro()
        {
            Console.WriteLine("Welcome to the Maze!");
            Console.WriteLine("Please make sure you play in full screen");
            Console.WriteLine("\nInstrunctions:");
            Console.WriteLine("- Use the arrow keys to move");
            Console.WriteLine("- To end the maze you must reach the green E");
            Console.WriteLine("- If you want to past the final monster, M, you will need to pick up the sword. Labeled as the W");
            Console.WriteLine("- There are items scattered accross the map, labeled as I. Some of these items are healing potions");
            Console.WriteLine("and they will heal you, but be careful there are poison potions disquised as them and they will hurt you.");
            Console.WriteLine("Good luck traveler.");
            Console.WriteLine("What is the length of the maze you want to complete?(Min is 6)(Max is 38)");
            userMazeHeight = int.Parse(Console.ReadLine());
            if (userMazeHeight > 38)
            {
                Console.WriteLine("Sorry that is too high. Setting you to the max length.");
                mazeHeight = 39;
            } else if (userMazeHeight < 6)
            {
                Console.WriteLine("Sorry that is too small. Setting you to the min height");
                mazeHeight = 6;
            }
            else
            {

                mazeHeight = userMazeHeight;
            }
            Console.WriteLine("What is the width of the maze you want to complete?(Min is 6)(Max is 156)");
            userMazeWidth = int.Parse(Console.ReadLine());
            if (userMazeWidth > 156)
            {
                Console.WriteLine("Sorry that is too high. Setting you to the max width.");
                mazeWidth = 156;
            } else if(userMazeWidth < 6)
            {
                Console.WriteLine("Sorry that is too small. Setting you to the min width");
                mazeWidth = 6;
            }
            else
            {
                mazeWidth = userMazeWidth;
            }
            Console.Clear();
        }
        /// <summary>
        /// The DisplayOutro() displays the an outro once the game is finished
        /// </summary>
        private void DisplayOutro()
        {
            if(playerHealth < 0)
            {
                Console.Clear();
                Console.WriteLine("Oh no you died and weren't able to beat the maze");
                Console.WriteLine("Please exit the game and try again");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Congrats you defeated the maze!");
                Console.WriteLine("You can exit out and try again and get a different maze to prove you are the maze masterer.");
            }
        }
        /// <summary>
        /// The main game loop that handles what the player interacts with
        /// </summary>
        private void GameLoop()
        {
            while (true)
            {

                int monsterHealth;
                int monsterStrength;

                DrawFrame();

                HandlePlayerInput();
                //check if the player has reached the exit and end the game
                currentPlayerLocation = (currentPlayer.X, currentPlayer.Y);
                string elementAtPlayer = mazeG.GetElementAt(currentPlayerLocation);


                if (elementAtPlayer == "monster")
                {
                    //At the moment it wants you to fight the monsters in order 
                    foreach (var monster in monsters)
                    {
                        if (monster.X == currentPlayerLocation.x && monster.Y == currentPlayerLocation.y)
                        {
                            int monsterId = monster.MonsterId;
                            monsterHealth = monster.Stats(monsterId);
                            monsterStrength = monster.CharacterAttack(monsterId); 

                            Console.WriteLine("You ran into a monster. These are it's stats");
                            monsterHealth = monster.CharacterUpdateHealth(monsterHealth);
                            currentBattle = new Battle(monsterHealth, monsterStrength, playerHealth, playerStrength);
                            
                            playerHealth = currentBattle.Fight();
                            playerHealth = currentPlayer.CharacterUpdateHealth(playerHealth);
                            monsters.Remove(monster);
                            break;
                            /*battles.Add(new Battle(monsterHealth, monsterStrength, playerHealth, playerStrength));
                            monsters.Remove(monster);
                            foreach(var battle in battles)
                            {
                                battle.Fight();
                                battles.Remove(battle);
                            }*/


                        }
                        
                    }

                }
                if (elementAtPlayer == "potion")
                {

                    foreach (var potion in potions)
                    {

                        if (potion.X == currentPlayerLocation.x && potion.Y == currentPlayerLocation.y)
                        {
                            int potionId = potion.PotionId;
                            potion.ItemStats(potionId);
                            potion.DisplayStats(); // Show the potion's effect message

                            Console.WriteLine("Press any key to continue....");
                            Console.ReadKey();
                            potions.Remove(potion);
                            playerHealth = potion.EditStats(playerHealth); // Apply the potion effect
                            if (playerHealth < 100)
                            {
                                currentPlayer.CharacterUpdateHealth(playerHealth);
                            }
                            else
                            {
                                playerHealth = 100;
                                currentPlayer.CharacterUpdateHealth(playerHealth);
                            }

                            break;
                        }


                    }
                }
                if (elementAtPlayer == "weapon")
                {
                    foreach (var weapon in weapons)
                    {
                        if (weapon.X == currentPlayerLocation.x && weapon.Y == currentPlayerLocation.y)
                        {
                            
                            int weaponId = weapon.weaponId;
                            weapon.ItemStats(weaponId);
                            weapon.DisplayStats();
                            Console.WriteLine("Do you want to pick up this sword? Please click y for yes or n for no");
                            swordChoice = Console.ReadLine();
                            if (swordChoice == "y" || swordChoice == "Y")
                            {
                                if (holdingWeapon == 0)
                                {
                                    playerStrength = weapon.EditStats(playerStrength);
                                    currentPlayer.CharacterAttack(playerStrength);
                                    holdingWeapon = 1;
                                }
                                else
                                {
                                    playerStrength = 15;
                                    holdingWeapon = 0;
                                    playerStrength = weapon.EditStats(playerStrength);
                                    currentPlayer.CharacterAttack(playerStrength);
                                }
                                weapons.Remove(weapon);
                                break;
                            }
                            break;

                        }
                    }

                }
                if (elementAtPlayer == "end")
                {
                    break;
                }
                if (playerHealth <= 0)
                {
                    break;
                }
            }
        }
    }
}
