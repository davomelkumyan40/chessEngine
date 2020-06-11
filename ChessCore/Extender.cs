using ChessCore.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessCore
{
    internal static class Extender
    {
        public static void SwapColor(this ref Color color)
        {
            color = color == Color.White ? Color.Black : Color.White;
        }

        public static IEnumerable<T> AsEnumerable<T>(this T[,] array)
        {
            foreach (var square in array)
            {
                yield return square;
            }
        }

        public static T[,] As2Dimensional<T>(this IEnumerable<T> array)
        {
            int c = array.Count();
            int length = (int)Math.Sqrt(c);
            var twoDarr = new T[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (array.GetEnumerator().MoveNext())
                        twoDarr[i, j] = array.GetEnumerator().Current;
                }
            }
            return twoDarr;
        }
    }
}
