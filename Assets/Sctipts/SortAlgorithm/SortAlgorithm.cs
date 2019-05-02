using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortAlgorithm
{

    #region  快排
    public static void QuickSort(int[] array,int low,int high)
    {
		if(low >= high)
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

}
