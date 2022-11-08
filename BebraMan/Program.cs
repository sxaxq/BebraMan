using System;
using System.IO;
using System.Threading;

namespace BebraMan
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Random random = new Random();

            bool isPlaying = true;

            int bebraX, bebraY;
            int bebraDX = 0, bebraDY = 1;
            bool bebraIsAlive = true;

            int ghostX, ghostY;
            int ghostDX = 0, ghostDY = -1;

            int allDots = 0;
            int collectDots = 0;

            char[,] map = ReadMap("map1", out bebraX, out bebraY, out ghostX, out ghostY, ref allDots);

            DrawMap(map);

            while (isPlaying)
            {
                Console.SetCursorPosition(0, 12);
                Console.Write($"Собрано: {collectDots}/{allDots}");
                // Если нажата клавиша поток выполнения переходит сюда
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    ChangeDirection(ref bebraDX, ref bebraDY, key);
                }
                if (map[bebraX + bebraDX, bebraY + bebraDY] != '#')
                {
                    CollectDots(map, bebraX, bebraY, ref collectDots);
                    Move(map, '@', ref bebraX, ref bebraY, bebraDX, bebraDY);
                }

                if (map[ghostX + ghostDX, ghostY + ghostDY] != '#')
                {
                    Move(map, '$', ref ghostX, ref ghostY, ghostDX, ghostDY);
                }
                else
                {
                    ChangeDirectgion(random, ref ghostDX, ref ghostDY);
                }

                Thread.Sleep(55);

                if (ghostX == bebraX && ghostY == bebraY)
                {
                    bebraIsAlive = false;
                }

                if (collectDots == allDots || bebraIsAlive == false)
                {
                    isPlaying = false;
                }
            }

            if(collectDots == allDots)
            {
                GameOver("Вы собрали все ягодки! Победа", 2, ConsoleColor.Green);
            }
            if(bebraIsAlive == false)
            {
                GameOver("Вас убил призрак, проигрыш бебры...", 1, ConsoleColor.Red);
            }
        }

        static void Move(char[,] map, char symbol, ref int X, ref int Y, int DX, int DY)
        {
            Console.SetCursorPosition(Y, X);
            Console.Write(map[X, Y]);

            X += DX;
            Y += DY;

            Console.SetCursorPosition(Y, X);
            Console.Write(symbol);
        }

        static void CollectDots(char[,] map, int X, int Y, ref int collectDots)
        {
            if (map[X, Y] == '.')
            {
                collectDots++;
                map[X, Y] = ' ';
            }
        }

        static void ChangeDirection(ref int DX, ref int DY, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    DX = -1; DY = 0;
                    break;
                case ConsoleKey.DownArrow:
                    DX = 1; DY = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    DX = 0; DY = -1;
                    break;
                case ConsoleKey.RightArrow:
                    DX = 0; DY = 1;
                    break;
            }
        }


        // Рандомное передвижение призрака
        static void ChangeDirectgion(Random random, ref int DX, ref int DY)
        {
            int ghostDir = random.Next(1, 5);

            switch (ghostDir)
            {
                case 1:
                    DX = -1; DY = 0;
                    break;
                case 2:
                    DX = 1; DY = 0;
                    break;
                case 3:
                    DX = 0; DY = -1;
                    break;
                case 4:
                    DX = 0; DY = 1;
                    break;
            }
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static char[,] ReadMap(string mapName, out int bebraX, out int bebraY, out int ghostX, out int ghostY, ref int allDots)
        {
            bebraX = 0; bebraY = 0;
            ghostX = 0; ghostY = 0;

            string[] newFile = File.ReadAllLines($"maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = newFile[i][j];

                    if (map[i, j] == '@')
                    {
                        bebraX = i;
                        bebraY = j;
                        map[i, j] = '.';
                    }
                    else if (map[i, j] == '$')
                    {
                        ghostX = i;
                        ghostY = j;
                        map[i, j] = '.';
                    }
                    else if (map[i, j] == ' ')
                    {
                        map[i, j] = '.';
                        allDots++;
                    }
                }
            }

            return map;
        }

        static void GameOver(string text, byte code, ConsoleColor color)
        {
            Console.SetCursorPosition(0, 13);
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
          
            if(code == 1)
            {
                StarWars();
            }
            else if(code == 2)
            {
                HappyBirthday();
            }

            Console.ReadKey();
        }

        static void HappyBirthday()
        {
            Thread.Sleep(2000);
            Console.Beep(264, 125);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(125);
            Console.Beep(297, 500);
            Thread.Sleep(125);
            Console.Beep(264, 500);
            Thread.Sleep(125);
            Console.Beep(352, 500);
            Thread.Sleep(125);
            Console.Beep(330, 1000);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(125);
            Console.Beep(297, 500);
            Thread.Sleep(125);
            Console.Beep(264, 500);
            Thread.Sleep(125);
            Console.Beep(396, 500);
            Thread.Sleep(125);
            Console.Beep(352, 1000);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(250);
            Console.Beep(264, 125);
            Thread.Sleep(125);
            Console.Beep(2642, 500);
            Thread.Sleep(125);
            Console.Beep(440, 500);
            Thread.Sleep(125);
            Console.Beep(352, 250);
            Thread.Sleep(125);
            Console.Beep(352, 125);
            Thread.Sleep(125);
            Console.Beep(330, 500);
            Thread.Sleep(125);
            Console.Beep(297, 1000);
            Thread.Sleep(250);
            Console.Beep(466, 125);
            Thread.Sleep(250);
            Console.Beep(466, 125);
            Thread.Sleep(125);
            Console.Beep(440, 500);
            Thread.Sleep(125);
            Console.Beep(352, 500);
            Thread.Sleep(125);
            Console.Beep(396, 500);
            Thread.Sleep(125);
            Console.Beep(352, 1000);
        }

        static void StarWars()
        {
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(250, 500);
            Thread.Sleep(50);
            Console.Beep(350, 250);
            Console.Beep(300, 500);
            Thread.Sleep(50);
            Console.Beep(250, 500);
            Thread.Sleep(50);
            Console.Beep(350, 250);
            Console.Beep(300, 500);
            Thread.Sleep(50);
        }
    }
}
