using System;
using System.Collections.Generic;

namespace Spoiler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var lookup = new Dictionary<int, string>()
            {
                { 0, ":zero:" },
                { 1, ":one:" },
                { 2, ":two:" },
                { 3, ":three:" },
                { 4, ":four:" },
                { 5, ":five:" },
                { 6, ":six:" },
                { 7, ":seven:" },
                { 8, ":eight:" },
                { 9, ":boom:" }
            };

            int[,] grid = new int[9, 9];

            var random = new Random();

            // Populate mines
            Do(grid, (x, y) => grid[x, y] = random.Next(0, 11) > 7 ? 9 : 0);

            // Debug
            DebugOutput(grid);

            // Calculate land values
            Do(grid, (x, y) =>
            {
                if (grid[x, y] == 9)
                {
                    return;
                }

                int minY = 0;
                int maxY = grid.GetLength(0) - 1;
                int minX = 0;
                int maxX = grid.GetLength(1) - 1;

                var offsets = new (int dx, int dy)[]
                {
                    (-1, -1),   // top left
                    (0, -1),    // top
                    (1, -1),    // top right
                    (-1, 0),    // left
                    (1, 0),     // right
                    (-1, 1),    // bottom left
                    (0, 1),     // bottom
                    (1, 1)      // bottom right
                };

                int neighborMines = 0;

                foreach (var (dx, dy) in offsets)
                {
                    int ix = x + dx;
                    int iy = y + dy;

                    if (ix < minX || ix > maxX
                        || iy < minY || iy > maxY)
                    {
                        continue;
                    }

                    if (grid[ix, iy] == 9)
                    {
                        neighborMines++;
                    }
                }

                grid[x, y] = neighborMines;
            });

            // Debug
            DebugOutput(grid);

            Do(grid, (x, y) => Console.Write($"||{lookup[grid[x, y]]}||"), (y) => Console.WriteLine());
            Console.WriteLine();

            Console.ReadKey();
        }

        public static void DebugOutput(int[,] grid)
        {
            Do(grid, (x, y) => Console.Write((grid[x, y] == 0 ? "_" : grid[x, y].ToString()) + " "), (y) => Console.WriteLine());
            Console.WriteLine();
        }

        public static void Do(int[,] grid, Action<int, int> action)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    action(x, y);
                }
            }
        }

        public static void Do(int[,] grid, Action<int, int> xAction, Action<int> yAction)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    xAction(x, y);
                }

                yAction(y);
            }
        }
    }
}
