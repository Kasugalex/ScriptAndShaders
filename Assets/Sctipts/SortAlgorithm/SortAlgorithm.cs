using System;
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

    #region 插入排序
    public static void InsertSort(int[] array)
    {
        int temp = 0;
        for (int i = 1; i < array.Length; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (array[j] < array[j - 1])
                {
                    temp = array[j];
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                }
            }
        }
    }

    #endregion

    #region 桶排序

    public static void BucketSort(int[] array,int bucketSize)
    {
 
        if (array == null || bucketSize < 1) return;

        int minValue = array[0];
        int maxValue = array[0];

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] < minValue) minValue = array[i];
            else if (array[i] > maxValue) maxValue = array[i];
        }

        int bucketCount = (int)Mathf.Floor((maxValue - minValue) / bucketSize) + 1;
        int[][] buckets = new int[bucketCount][];

        for (int i = 0; i < array.Length; i++)
        {

            int index = (int)Mathf.Floor((array[i] - minValue) / bucketSize);
            buckets[index] = ArrAppend(buckets[index], array[i]);
        }

        int arrIndex = 0;

        foreach (int[] bucket in buckets)
        {
            if (bucket == null || bucket.Length <= 0) continue;

            InsertSort(bucket);

            foreach (var value in bucket)
            {
                array[arrIndex++] = value;
            }
        }

        buckets = null;
    }

    private static int[] ArrAppend(int[] arr,int value)
    {
        if (arr == null)
            arr = new int[0];

        int length = arr.Length;
        int[] retArray = new int[length + 1];

        for (int i = 0; i < length; i++)
        {
            retArray[i] = arr[i];
        }

        retArray[retArray.Length - 1] = value;

        return retArray;
    }

    #endregion
}
