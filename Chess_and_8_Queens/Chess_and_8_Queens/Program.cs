using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess_and_8_Queens
{
    class Program
    {
        static List<int[,]> lsBoards = new List<int[,]>();
        static int[,] board;

        public static void Main(string[] args)
        {
            Console.WriteLine($"Start Time {DateTime.Now}");

            do
            {
                board = new int[8, 8];
                board = CreateNewCardonates(board);

                if ((!IsCollisionsWithOtherQueens(board) && lsBoards.Count == 0) || (!ComparesATwo_DimensionalArrayWithArrays(lsBoards, board) && !IsCollisionsWithOtherQueens(board)))
                {
                    lsBoards.Add(board);
                    Print(board);

                    int[,] tempot = DeployBoard(lsBoards[lsBoards.Count - 1]);
                    for (int i = 0; i < 3; i++)
                    {
                        if (!ComparesATwo_DimensionalArrayWithArrays(lsBoards, tempot))
                        {
                            lsBoards.Add(tempot);
                            tempot = DeployBoard(tempot);
                            Print(lsBoards[(lsBoards.Count - 1)]);
                        }
                        else
                            tempot = DeployBoard(tempot);
                    }

                }

            } while (lsBoards.Count != 92);

            Console.WriteLine($"Finish Time {DateTime.Now}");
            Console.ReadKey();
        }

        /// <summary>
        /// переворачивает шахматная доска
        /// </summary>
        static int[,] DeployBoard(int[,] board)
        {
            int columnLenght = board.GetLength(0);
            int rowLenght = (board.Length / board.GetLength(0));
            int[,] temp = new int[columnLenght, rowLenght];

            for (int i = 0; i < columnLenght; i++)
            {
                for (int j = 0; j < columnLenght; j++)
                {
                    if (board[j, i] != 0)
                    {
                        temp[((columnLenght - i) - 1), j] = (j + 1);
                        j = columnLenght;
                    }
                }
            }
            return temp;
        }


        /// <summary>
        /// сравнивает переданный двумерный массив с Массивами в List-е
        /// </summary>
        static bool ComparesATwo_DimensionalArrayWithArrays(List<int[,]> ls, int[,] arr)
        {
            bool bl = true;
            for (int i = 0; i < ls.Count; i++)
            {
                bl = true;
                for (int j = 0; j < ls[i].GetLength(0); j++)
                {
                    for (int f = 0; f < ls[i].GetLength(0); f++)
                    {
                        if (arr[j, f] != ls[i][j, f])
                        {
                            f = ls[i].GetLength(0);
                            j = ls[i].GetLength(0);
                            bl = false;
                        }
                    }
                }
                if (bl)
                    return bl;
            }
            return bl;
        }

        /// <summary>
        /// проверяет нет ли столкновения с другими ферзами .
        /// ходит по Х путями в матрице и ищет ненулевые значения․
        /// </summary>
        static bool IsCollisionsWithOtherQueens(int[,] arr)
        {
            int indexY_1 = 0, indexY_2 = 0, indexY_3 = 0, indexY_4 = 0;
            int indexX_1 = 0, indexX_2 = 0, indexX_3 = 0, indexX_4 = 0;
            int firstIndex = 0;

            for (int i = 0; i < (arr.GetLength(0)); i++)
            {
                indexY_1 = indexY_2 = indexY_3 = indexY_4 = i;

                #region ищет в строке матрицы ненулевой значения и фиксирует индекс
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (arr[i, j] != 0)
                    {
                        indexX_1 = indexX_2 = indexX_3 = indexX_4 = j;
                        j = arr.GetLength(0);
                    }
                }
                #endregion

                firstIndex = indexX_1 + 1;

                #region ходит по ( X ) путями и проверяет нет ли столкновения с другими цифрами ненулевого значения
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    if (arr[indexY_1, indexX_1] != 0 && arr[indexY_1, indexX_1] != firstIndex ||
                        arr[indexY_2, indexX_2] != 0 && arr[indexY_2, indexX_2] != firstIndex ||
                        arr[indexY_3, indexX_3] != 0 && arr[indexY_3, indexX_3] != firstIndex ||
                        arr[indexY_4, indexX_4] != 0 && arr[indexY_4, indexX_4] != firstIndex)
                        return true;
                    else
                    {
                        if (indexY_1 != (arr.GetLength(0) - 1) && indexX_1 != (arr.GetLength(0) - 1))
                        {
                            indexY_1++;//вниз
                            indexX_1++;//вправо
                        }
                        if ((indexY_2 != (arr.GetLength(0) - 1) && indexX_2 != 0))
                        {
                            indexY_2++;//вниз
                            indexX_2--;//влево
                        }
                        if (indexY_3 != 0 && indexX_3 != 0)
                        {
                            indexY_3--;//верх
                            indexX_3--;//влево
                        }
                        if (indexY_4 != 0 && indexX_4 != (arr.GetLength(0) - 1))
                        {
                            indexY_4--;//верх
                            indexX_4++;//вправо
                        }
                    }
                    #endregion
                }
            }
            return false;
        }

        /// <summary>
        /// ищет есть ли в массиве переданный параметр
        /// </summary>
        static bool SearchNumberInArray(int[] arr, int number)
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == number)
                    return true;
            return false;
        }

        /// <summary>
        /// Генерирует новые координаты для королевы
        /// </summary>
        static int[,] CreateNewCardonates(int[,] arr)
        {
            Random rnd = new Random();
            int columnLenght = board.GetLength(0);
            int rowLenght = (board.Length / board.GetLength(0));
            List<int> lsNumbers = GetArrNumbers(1, (rowLenght + 1)).ToList();

            for (int i = 0; i < columnLenght; i++)
            {
                int newNumber = rnd.Next(1, lsNumbers.Count + 1);
                arr[i, (lsNumbers[newNumber - 1] - 1)] = lsNumbers[newNumber - 1];
                lsNumbers.RemoveAt((newNumber - 1));
            }
            return arr;
        }

        /// <summary>
        /// возвращает массив с цифрами
        /// </summary>
        static int[] GetArrNumbers(int startNumber, int Finishlenght)
        {
            List<int> ls = new List<int>();
            for (int i = startNumber; i < Finishlenght; i++)
                ls.Add(i);

            return ls.ToArray();
        }

        /// <summary>
        /// печатает шахматную доску в Console
        /// </summary>
        static void Print(int[,] arr)
        {
            Console.Write(new string('=', 28));
            Console.WriteLine($"-> {lsBoards.Count}");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                Console.Write($" {arr.GetLength(0) - i} ");
                for (int j = 0; j < (arr.Length / arr.GetLength(0)); j++)
                {
                    if (arr[i, j] != 0)
                        Console.Write(" @ ");
                    else
                        Console.Write(" * ");
                }
                Console.WriteLine();
            }
            
            for (int i = 0; i < (arr.Length / arr.GetLength(0)); i++)
            {
                if (8 >= (arr.Length / arr.GetLength(0)))
                {
                    switch (i)
                    {
                        case 0:
                            Console.Write("    A ");
                            break;
                        case 1:
                            Console.Write(" B ");
                            break;
                        case 2:
                            Console.Write(" C ");
                            break;
                        case 3:
                            Console.Write(" D ");
                            break;
                        case 4:
                            Console.Write(" E ");
                            break;
                        case 5:
                            Console.Write(" F ");
                            break;
                        case 6:
                            Console.Write(" G ");
                            break;
                        case 7:
                            Console.Write(" H ");
                            break;

                    }
                }
                else
                {
                    if (i <= 0)
                        Console.Write($"    {(i + 1)} ");
                    else
                        Console.Write($" {(i+1)} ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }

    }

}
