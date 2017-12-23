using System;
using System.Collections.Generic;

namespace SortVisualisator._App
{
    public class InsertionSort<T>
        where T : IComparable<T>
    {

        public static void Sort(T[] array, IList<(int, int)> swapSequence)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (array[j].CompareTo(array[j - 1]) < 0)
                    {
                        Swap(array, j, j - 1);
                        swapSequence.Add((j, j - 1));
                    }
                    else break;
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
