using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    class Game
    {
        static int x = Console.WindowWidth / 2;
        static int y = Console.WindowHeight - 1;
        static int generation = 0;
        static char input;
        public bool[,] board = new bool[x, y];

        private static DateTime lastTime;
        private static int lastGen;
        private static int frameRate;

        static void printboard(bool[,] board)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            for (int yy = 0; yy < y; yy++)
            {
                Console.SetCursorPosition(0, yy);
                for (int xx = 0; xx < x; xx++)
                {
                    Console.Write(board[xx, yy] ? "■ " : "  ");
                }
            }
            Console.SetCursorPosition(0, y);
            Console.Write("Generation: " + generation + "     (r)estart, (q)uit, (g)lider gun, (d)iehard   GPS: " + CalculateGenRate() + "   ");
        }

        public static int CalculateGenRate()
        {
            DateTime current = System.DateTime.Now;
            if (System.DateTime.Now > lastTime.AddSeconds(1))
            {
                frameRate = (generation - lastGen);
                lastTime = current;
                lastGen = generation;
            }
            return frameRate;
        }

        static bool[,] seedrandom(bool[,] board)
        {
            Random rnd = new Random();
            for (int yy = 0; yy < y; yy++)
            {
                for (int xx = 0; xx < x; xx++)
                {
                    board[xx, yy] = Convert.ToBoolean((int)rnd.Next(1, 3) % 2);
                }
            }
            return board;
        }

        static int checkLocals(int xx, int yy, bool[,] board)
        {
            int locals = 0;
            if (yy + 1 < y && board[xx, yy + 1] == true)
            { locals = locals + 1; }
            if (xx - 1 >= 0 && yy + 1 < y && board[xx - 1, yy + 1] == true)
            { locals = locals + 1; }
            if (xx + 1 < x && yy + 1 < y && board[xx + 1, yy + 1] == true)
            { locals = locals + 1; }
            if (xx - 1 >= 0 && board[xx - 1, yy] == true)
            { locals = locals + 1; }
            if (xx + 1 < x && board[xx + 1, yy] == true)
            { locals = locals + 1; }
            if (xx + 1 < x && yy - 1 >= 0 && board[xx + 1, yy - 1] == true)
            { locals = locals + 1; }
            if (xx - 1 >= 0 && yy - 1 >= 0 && board[xx - 1, yy - 1] == true)
            { locals = locals + 1; }
            if (yy - 1 >= 0 && board[xx, yy - 1] == true)
            { locals = locals + 1; }
            return locals;
        }

        static bool assertSituation(int xx, int yy, bool[,] board)
        {
            int locals = checkLocals(xx, yy, board);
            if (locals == 2 && board[xx, yy] == true)
            {
                return true;
            }
            else if (locals == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool[,] Reproduce(bool[,] board)
        {
            bool[,] nextgen = new bool[x, y];
            for (int yy = 0; yy < y; yy++)
            {
                for (int xx = 0; xx < x; xx++)
                {
                    nextgen[xx, yy] = assertSituation(xx, yy, board);
                }
            }
            return nextgen;
        }

        static bool[,] HandleKeys(bool[,] board)
        {
            Console.Clear();
            generation = 0;
            lastGen = 0;
            bool[,] switcharoo = new bool[x, y];
            switch (input)
            {
                case 'r':
                    switcharoo = seedrandom(board);
                    break;
                case 'd':
                    if (x > 25 && y > 15)
                    {
                        switcharoo[22, 13] = true;
                        switcharoo[23, 13] = true;
                        switcharoo[23, 14] = true;
                        switcharoo[27, 14] = true;
                        switcharoo[28, 14] = true;
                        switcharoo[29, 14] = true;
                        switcharoo[28, 12] = true;
                    }
                    else
                    {
                        switcharoo = seedrandom(board);
                    }
                    break;
                case 'g':
                    if (x > 36 && y > 9)
                    {
                        switcharoo[1, 5] = true;
                        switcharoo[2, 5] = true;
                        switcharoo[1, 6] = true;
                        switcharoo[2, 6] = true;
                        switcharoo[11, 5] = true;
                        switcharoo[11, 6] = true;
                        switcharoo[11, 7] = true;
                        switcharoo[12, 4] = true;
                        switcharoo[13, 3] = true;
                        switcharoo[14, 3] = true;
                        switcharoo[12, 8] = true;
                        switcharoo[13, 9] = true;
                        switcharoo[14, 9] = true;
                        switcharoo[15, 6] = true;
                        switcharoo[16, 4] = true;
                        switcharoo[17, 5] = true;
                        switcharoo[17, 6] = true;
                        switcharoo[17, 7] = true;
                        switcharoo[18, 6] = true;
                        switcharoo[16, 8] = true;

                        switcharoo[21, 3] = true;
                        switcharoo[21, 4] = true;
                        switcharoo[21, 5] = true;
                        switcharoo[22, 3] = true;
                        switcharoo[22, 4] = true;
                        switcharoo[22, 5] = true;
                        switcharoo[23, 2] = true;
                        switcharoo[23, 6] = true;
                        switcharoo[25, 1] = true;
                        switcharoo[25, 2] = true;
                        switcharoo[25, 6] = true;
                        switcharoo[25, 7] = true;
                        switcharoo[35, 3] = true;
                        switcharoo[35, 4] = true;
                        switcharoo[36, 3] = true;
                        switcharoo[36, 4] = true;
                    }
                    else
                    {
                        switcharoo = seedrandom(board);
                    }
                    break;

            }
            return switcharoo;
        }

        static void Main()
        {
            Console.Title = "A Game of Life";
            lastTime = System.DateTime.Now;

            Game game = new Game();
            game.board = seedrandom(game.board);
            printboard(game.board);

            while (input != 'q')
            {
                if (Console.KeyAvailable)
                {
                    input = System.Console.ReadKey(true).KeyChar;
                }
                if (Console.WindowHeight != y + 1)
                {
                    x = Console.WindowWidth / 2;
                    y = Console.WindowHeight - 1;
                    generation = 0;
                    lastGen = 0;
                    game.board = new bool[x, y];
                    game.board = seedrandom(game.board);
                    Console.Clear();
                }
                if (input == 'r' || input == 'd' || input == 'g')
                {
                    game.board = HandleKeys(game.board);
                    input = '0';
                }
                game.board = Reproduce(game.board);
                printboard(game.board);
                generation++;
            }
        }
    }

}
