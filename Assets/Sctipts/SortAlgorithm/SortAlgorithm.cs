using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortAlgorithm
{

    #region  快排
    public static void QuickSort(int[] array, int low, int high)
    {

        if (low >= high)
            return;
        int index = SplitArray(array, low, high);
        QuickSort(array, low, index - 1);
        QuickSort(array, index + 1, high);

    }

    private static int SplitArray(int[] array, int low, int high)
    {
        int key = array[low];
        while (low < high)
        {
            while (array[high] >= key && low < high)
                --high;
            //比key小的放左边
            array[low] = array[high];
            while (array[low] <= key && low < high)
                ++low;
            //比key大的放右边
            array[high] = array[low];
        }
        array[low] = key;

        return high;
    }
    #endregion

    #region 希尔排序

    public static void ShellSort(int[] array)
    {
        int length = array.Length;
        int _d = array.Length;
        for (_d = _d / 2; _d > 0;)
        {
            if (_d % 2 == 0) _d++;
            for (int i = 0; i < _d; i++)
            {
                for (int j = i + _d; j < length; j++)
                {
                    if (j < length)
                    {
                        if (array[j] < array[j - _d])
                        {
                            int temp = array[j];
                            int k = 0;
                            for (k = j - _d; k >= i && temp < array[k]; k -= _d)
                            {
                                array[k + _d] = array[k];
                            }
                            array[k + _d] = temp;
                        }
                    }
                }
            }
            _d = _d / 2;
        }
    }

    #endregion
}
