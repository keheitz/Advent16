using Advent16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advent16.Library
{
    public static class Common
    {

        //Method for finding all numbers between two integers
        //(similar to python Range method)
        public static IEnumerable<int> Range(int start, int stop)
        {
            for (int i = start; i < stop; i++)
                yield return i;
        }

        //get coordinates of value in 2d array
        public static Tuple<int, int> CoordinatesOf<T>(this T[,] matrix, T value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }

        /// <summary>
        /// Break a list of items into chunks of a specific size
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }

        
    }
}