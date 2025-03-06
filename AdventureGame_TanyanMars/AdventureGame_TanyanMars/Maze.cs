namespace AdventureGame_TanyanMars
{
    /// <summary>
    /// The Maze class represents a randomly generating maze
    /// </summary>
    public class Maze
    {
        //this class generates the maze
        private int width;
        private int height;
        private Random random;
        private int[,] maze;
        private (int x, int y) start;
        private (int x, int y) end;
        private string elementAt = "";
        private string InteractiveId;
        private List<(int x, int y)> MonsterLocations;
        private List<(int x, int y)> PotionLocations;
        private List<(int x, int y)> WeaponLocations;
        private int idItem;
        /// <summary>
        /// This intializes the maze object with specific dimensions and a specific start/end
        /// </summary>
        /// <param name="width">The width of the maze </param>
        /// <param name="height">The height of the maze</param>
        /// <param name="start">The starting position of the player</param>
        /// <param name="end">The endpoint for the player to win</param>
        /// <param name="random">The random number generator for the maze</param>
        public Maze(int width, int height, (int x, int y) start, (int x, int y) end, Random random)
        {
            this.width = width;
            this.height = height;
            this.random = random;
            this.maze = new int[width, height];
            this.start = start;
            this.end = end;
            MonsterLocations = new List<(int x, int y)>();
            PotionLocations = new List<(int x, int y)>();
            WeaponLocations = new List<(int x, int y)>();

            Initial();
        }//end of Maze constructor
        //game plan make the walls a binary system. 1 is on, 0 is off
        /// <summary>
        /// Starts the maze by making all the cells into walls
        /// </summary>
        public void Initial()
        {
            //anything in the maze size is made into a wall
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maze[x, y] = 1;
                }
            }
        }//end of Intial
        /// <summary>
        /// Carves out the paths for the maze
        /// </summary>
        /// <param name="startX">The starting x position for the player</param>
        /// <param name="startY">The starting y position for the player</param>
        public void MazeGenerator(int startX, int startY)
        //goes through and mines through the walls to get a maze

        {
            maze[startX, startY] = 0;
            var directions = new List<(int x, int y)>
            { (0, 2), //up
              (0, -2), //down
              (2, 0), //right
              (-2, 0) //left
            };
            /**how do I randomly choose a direction to go in?
            reading from stack overflow use Shuffle
            https://stackoverflow.com/questions/273313/randomize-a-listt
            Changed my mind if your scroll further down there is a simple one line code I am going to try**/
            var shuffleDirections = directions.OrderBy(d => random.Next()).ToList();

            //time to make it where when the direction is chosen you delete a wall
            //have to rename x in the begining to add these together
            foreach (var (dx, dy) in shuffleDirections)
            {
                int newX = startX + dx;
                int newY = startY + dy;
                int wallX = startX + dx / 2;
                int wallY = startY + dy / 2;

                if (newX > 0 && newX < width - 1 && newY > 0 && newY < height - 1 &&
                maze[newX, newY] == 1 && maze[wallX, wallY] == 1)
                {
                    maze[wallX, wallY] = 0;
                    MazeGenerator(newX, newY);
                }
            }
        }//end of MazeGenerator
        /// <summary>
        /// Gets the maze grid
        /// </summary>
        /// <returns>The maze grid</returns>
        public int[,] GetMaze()
        {
            return maze;
        }
        /// <summary>
        /// Prints the maze out on the console
        /// </summary>
        public void PrintMaze()
        {
            int height = maze.GetLength(1);
            int width = maze.GetLength(0);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (maze[x, y] == 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write("█");
                        elementAt = "";
                    }
                    else if ((x, y) == end)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("E");
                        Console.ResetColor();
                        elementAt = "end";
                    }
                    else
                    {
                        Console.Write(" ");
                        elementAt = "";
                    }
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Gets the location for the items and monster
        /// </summary>
        /// <param name="InteractiveLocat">The location of the monster and items on maze</param>
        /// <param name="Id">An identifier for the specific items/monster</param>
        /// <param name="ItemID">To save the monsters/items int id to use later for damage calculations</param>
        public void GetLocation((int x, int y) InteractiveLocat, string Id, int ItemID)
        {
            InteractiveId = Id;
            if (InteractiveId == "M")
            {
                MonsterLocations.Add(InteractiveLocat);
                ItemID = idItem;
            }
            else if (InteractiveId == "P")
            {
                PotionLocations.Add(InteractiveLocat);
                ItemID = idItem;
            }
            else if (InteractiveId == "W")
            {
                WeaponLocations.Add(InteractiveLocat);
                ItemID = idItem;
            }
            else
            {
                elementAt = "";
            }
        }
        /// <summary>
        /// Gets the element at the player's location
        /// </summary>
        /// <param name="location">Used to represent the player's current location</param>
        /// <returns></returns>
        public string GetElementAt((int x, int y) location)
        {
            if (location == end)
            {
                return elementAt = "end";
            }
            else if (MonsterLocations.Contains(location))
            {
                return elementAt = "monster";
            }
            else if (PotionLocations.Contains(location))
            {
                return elementAt = "potion";
            }
            else if (WeaponLocations.Contains(location))
            {
                return elementAt = "weapon";
            }
            else
            {
                return elementAt = "";
            }
        }
        /// <summary>
        /// To make sure the player can't walk through walls
        /// </summary>
        /// <param name="x">The x coordinate of the position being tested</param>
        /// <param name="y">The y coordinate of the postion being tested</param>
        /// <returns>Returns true if there isn't a wall or monster, and false if there is</returns>
        public bool IsWalkable(int x, int y)
        {
            // Check if the coordinates are within the bounds of the maze
            if (x < 0 || x >= width || y < 0 || y >= height)
            {
                return false;
            }

            if (elementAt == "end")
            {
                return true;
            }
            else if (elementAt == "monster")
            {
                return false;
            }

            // Check if the position in the maze is walkable
            return maze[x, y] == 0;
        }
    }

}//end of Maze class
//end of namespace
