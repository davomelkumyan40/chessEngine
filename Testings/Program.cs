//Internal
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

//External
using ChessCore;
using ChessCore.Models;
using ChessCore.Native;
using ChessCore.CMath;

namespace Testings
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine c = new Engine(Color.White, true);
            var stepCorrect = new List<bool>();
            stepCorrect.Add(c.Board.Move(new Coordinate('E', 2), new Coordinate('E', 4)));
            stepCorrect.Add(c.Board.Move(new Coordinate('E', 7), new Coordinate('E', 5)));
            stepCorrect.Add(c.Board.Move(new Coordinate('D', 1), new Coordinate('H', 5)));
            stepCorrect.Add(c.Board.Move(new Coordinate('B', 8), new Coordinate('C', 6)));
            stepCorrect.Add(c.Board.Move(new Coordinate('F', 1), new Coordinate('C', 4)));
            stepCorrect.Add(c.Board.Move(new Coordinate('G', 8), new Coordinate('F', 6)));
            //stepCorrect.Add(c.Board.Move(new Coordinate('H', 5), new Coordinate('F', 7)));
            stepCorrect.ForEach((el) => Debug.WriteLine(el));


            while (true)
            {
                ChessUI2_0(c.Board);
                string command = Console.ReadLine();
                if (command.Length == 4)
                {
                    char s = command[0];
                    byte n = byte.Parse(command[1].ToString());

                    char goS = command[2];
                    byte goN = byte.Parse(command[3].ToString());

                    c.Board.Move(new Coordinate(s, n), new Coordinate(goS, goN));
                }
                else if (command == "r")
                    c.Board.RollBack();
            }

        }

        private static void ChessUI2_0(BoardModel board)
        {
            //Console UI 2.0
            Console.Clear();
            Console.BufferHeight = 90;
            Console.WindowHeight = 50;
            Console.WindowWidth = 70;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Title = "Chess UI 2.0";

            int x = 0;
            int y = 0;

            Console.WriteLine(new string('-', 65));
            for (int boardHeight = 0; boardHeight < 8; boardHeight++)
            {
                //Draw 8 Squares
                for (int squareHeight = 0; squareHeight < 3; squareHeight++)
                {
                    for (int boardWidth = 0; boardWidth < 8; boardWidth++)
                    {
                        Console.Write("|");
                        for (int squareWidth = 0; squareWidth < 7; squareWidth++)
                        {
                            if (squareWidth == 4 && squareHeight == 1)
                            {
                                if (board.SquareList[y, x].Figure != null)
                                {
                                    if (board.SquareList[y, x].Figure.Color == Color.White)
                                        Console.ForegroundColor = ConsoleColor.White;
                                    else
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    switch (board.SquareList[y, x].Figure.Type)
                                    {
                                        case Figure.Rook:
                                            Console.Write("R");
                                            break;
                                        case Figure.Knight:
                                            Console.Write("N");
                                            break;
                                        case Figure.Bishop:
                                            Console.Write("B");
                                            break;
                                        case Figure.King:
                                            Console.Write("K");
                                            break;
                                        case Figure.Queen:
                                            Console.Write("Q");
                                            break;
                                        case Figure.Pawn:
                                            Console.Write("P");
                                            break;
                                    }
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                }
                                else
                                    Console.Write(" ");

                                if (x == 7)
                                {
                                    x = 0;
                                    ++y;
                                }
                                else
                                    ++x;
                            }
                            else
                                Console.Write(" ");
                        }
                    }
                    if (squareHeight == 1)
                    {
                        Console.Write($"|");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"  {8 - boardHeight}");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                        Console.WriteLine("|");
                }
                Console.WriteLine(new string('-', 65));

                //End Drawing 8 squares

            }

            for (char p = 'A'; p < 'I'; p++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"    {p}   ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("\n\n");
            builder.AppendLine(new string('_', 65));
            builder.AppendLine($"Step : {board.PlayColor}");
            builder.Append($"\nWhite: {board.EatenList.Count(c => c.Color == Color.Black)}  ");
            builder.AppendLine($"Black: {board.EatenList.Count(c => c.Color == Color.White)}  ");
            builder.AppendLine($"Is Check: {board.CheckState.IsCheck} for : {board.PlayColor}");
            builder.AppendLine($"Is Mate: {board.CheckState.IsMate} for : {board.PlayColor}");
            foreach (FigureModel figure in board.CheckState.ThreatenFigures)
            {
                builder.AppendLine($"{figure.Type} Position:  X:{figure.Position.X} Y:{figure.Position.Y}");
            }
            Console.WriteLine(builder.ToString());
            builder.Clear();
        }

        private static void ChessUI1_0(object state)
        {
            //Console UI 1.0

            Console.Clear();
            BoardModel board = state as BoardModel;

            if (board != null)
            {
                Console.WriteLine(new string('_', 32));
                for (int i = 0; i < board.SquareList.GetLength(0); i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($" {i}. ");
                    for (int j = 0; j < board.SquareList.GetLength(1); j++)
                    {
                        if (board.SquareList[i, j].Figure != null)
                        {
                            if (board.SquareList[i, j].Figure.Color == Color.White)
                                Console.ForegroundColor = ConsoleColor.White;
                            else
                                Console.ForegroundColor = ConsoleColor.Red;
                            switch (board.SquareList[i, j].Figure.Type)
                            {
                                case Figure.Rook:
                                    Console.Write(" R ");
                                    break;
                                case Figure.Knight:
                                    Console.Write(" N ");
                                    break;
                                case Figure.Bishop:
                                    Console.Write(" B ");
                                    break;
                                case Figure.King:
                                    Console.Write(" K ");
                                    break;
                                case Figure.Queen:
                                    Console.Write(" Q ");
                                    break;
                                case Figure.Pawn:
                                    Console.Write(" P ");
                                    break;
                            }
                        }
                        else
                            Console.Write("   ");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine(new string('_', 30));
                Console.WriteLine($"Who's Step? : {board.PlayColor}");
                Console.Write($"\nWhite: {board.EatenList.Count(c => c.Color == Color.Black)}  ");
                Console.WriteLine($"Black: {board.EatenList.Count(c => c.Color == Color.White)}  ");
            }
        }





        //--- Testing 




        public static void Pawn_All_Steps_Not_Valid()
        {
            //---arrange организовать

            List<KeyValuePair<Coordinate, Coordinate>> pathList = new List<KeyValuePair<Coordinate, Coordinate>>();

            for (char m = 'A'; m < 'I'; m++)
            {
                for (char f = 'A'; f < 'I'; f++)
                {
                    for (byte j = 1; j < 9; j++)
                    {
                        if (j > 1 && j < 5)
                            j = 5;
                        pathList.Add(new KeyValuePair<Coordinate, Coordinate>(new Coordinate(m, 2), new Coordinate(f, j)));
                    }
                }
            }

            var expected = false;

            //---act действие

            Engine c = new Engine(Color.White);
            var resList = new List<bool>(pathList.Count);

            foreach (var path in pathList)
            {
                resList.Add(c.Board.Move(path.Key, path.Value));
                c.Board.SwapColor();
            }

            //---assert утверждение
            bool r = false;
            foreach (var res in resList)
            {
                if (expected != res)
                    r = true;
            }
            Console.WriteLine(r + "if true than not ok");
        }

    }
}
