using System;
using System.Collections.Generic;

namespace SortVisualisator._App
{
    public static class Shuffle<T> where T : IComparable<T>
    {
        private static readonly Random _random = new Random();
        public static void Run(T[] array, IList<ValueTuple<int, int>> swapSequence)
        {
            for (int k = 0; k < 2; k++)
            {
                for (int i = 1; i < array.Length; i++)
                {
                    var j = _random.Next(i + 1);
                    if (i != j)
                    {
                        Swap(array, i, j);
                        swapSequence.Add((i, j));
                    }
                }
            }
        }

        private static void Swap(T[] array, int i, int j)
        {
            T temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}
