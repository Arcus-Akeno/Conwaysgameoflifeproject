namespace ConwaysGameofLife
{
    class Program
    {
        static int GridWidth = 20;
        static int GridHeight = 20;
        static bool[,] currentGeneration = new bool[GridWidth, GridHeight];
        static bool[,] nextGeneration = new bool[GridWidth, GridHeight];
        static Random random = new Random();

        static void Main(string[] args)
        {
            InitializeGrid();
            Console.CursorVisible = false;

            while (true)
            {
                Console.Clear();
                DisplayGrid();
                UpdateGrid();
                System.Threading.Thread.Sleep(100); // Adjust the delay 
                                                    //to control the speed.
            }
        }

        static void InitializeGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    currentGeneration[x, y] = random.Next(2) == 0;
                }
            }
        }

        static void DisplayGrid()
        {
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    Console.Write(currentGeneration[x, y] ? "█" : " ");
                }
                Console.WriteLine();
            }
        }

        static void UpdateGrid()
        {
            for (int x = 0; x < GridWidth; x++)
            {
                for (int y = 0; y < GridHeight; y++)
                {
                    int liveNeighbors = CountLiveNeighbors(x, y);

                    if (currentGeneration[x, y])
                    {
                        // Apply the rules for live cells.
                        nextGeneration[x, y] = liveNeighbors == 2 || liveNeighbors == 3;
                    }
                    else
                    {
                        // Apply the rules for dead cells.
                        nextGeneration[x, y] = liveNeighbors == 3;
                    }
                }
            }

            // Swap current and next generation grids.
            bool[,] temp = currentGeneration;
            currentGeneration = nextGeneration;
            nextGeneration = temp;
        }

        static int CountLiveNeighbors(int x, int y)
        {
            int count = 0;

            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0)
                        continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && nx < GridWidth && ny >= 0 && ny < GridHeight &&
                      currentGeneration[nx, ny])
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}
