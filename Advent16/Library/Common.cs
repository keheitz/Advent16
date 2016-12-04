using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Advent16.Library
{
    public class Common
    {

        //Method for finding all numbers between two integers
        //(similar to python Range method)
        public static IEnumerable<int> Range(int start, int stop)
        {
            for (int i = start; i < stop; i++)
                yield return i;
        }

    }
}