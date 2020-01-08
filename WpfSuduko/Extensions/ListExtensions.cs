using System;
using System.Collections.Generic;
using System.Text;

namespace WpfSuduko.Extensions
{
    static class ListExtensions
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int listCount = list.Count;
            while (listCount > 1)
            {
                listCount--;
                int nextRandom = rng.Next(listCount + 1);
                T randomObject = list[nextRandom];
                list[nextRandom] = list[listCount];
                list[listCount] = randomObject;
            }
        }
        public static List<T> TwoDArrayAsList<T>(this T[,] array) 
        {
            List<T> newList = new List<T>();
            for (int row = 0; row < array.GetLength(0); ++row)
            {
                for (int column = 0; column < array.GetLength(0); ++column)
                {
                    newList.Add(array[row, column]);
                }
            }
            return newList;
        }
    }
}
