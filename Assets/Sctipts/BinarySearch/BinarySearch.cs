using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
[ExecuteInEditMode]
public class BinarySearch : MonoBehaviour
{
    public int ArrayLength = 25;
    [SerializeField]
    private int searchValue;
    private int[] sortArray;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClearLog.Clear();
            sortArray = null;
            sortArray = new int[ArrayLength];
            for (int i = 0; i < ArrayLength; i++)
            {
                int value = Random.Range(0, 3000);
                sortArray[i] = value;
            }
            searchValue = sortArray[Random.Range(0, sortArray.Length)];

            WatchExecuteTime.WatchExecute(QuickSort);

            WatchExecuteTime.WatchExecute(RecursiveSearch);

            WatchExecuteTime.WatchExecute(NonRecursiveSearch);
        }
    }

    void QuickSort()
    {
        SortAlgorithm.QuickSort(sortArray, 0, sortArray.Length - 1);
    }

    void RecursiveSearch()
    {
        Debug.LogFormat("递归查询{0}的位置为:{1}", searchValue, BinarySearchFunc.BinarySearch(sortArray, searchValue, 0, sortArray.Length - 1));
    }

    void NonRecursiveSearch()
    {
        Debug.LogFormat("非递归查询{0}的位置为:{1}", searchValue, BinarySearchFunc.BinarySearch(sortArray, searchValue));
    }

	 private void OnGUI() {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.fontSize = 40;
		
        fontStyle.normal.textColor = Color.white;
        int width = 500;
        GUI.TextArea(new Rect(650,100,width,width),"场景运行后按下鼠标左键即可看输出结果",fontStyle);
	}
}

public static class BinarySearchFunc
{

    public static int BinarySearch(int[] arr, int value)
    {
        int low = 0;
        int high = arr.Length - 1;
        while (low <= high)
        {
            int middle = (low + high) >> 1;
            if (value == arr[middle])
                return middle;
            else if (value < arr[middle])
            {
                high = middle - 1;
            }
            else
            {
                low = middle + 1;
            }
        }
        return -1;
    }

    public static int BinarySearch(int[] arr, int value, int low, int high)
    {
        if (value < arr[low] || value > arr[high] || low > high)
            return -1;
        int middle = (low + high) >> 1;
        if (value < arr[middle])
            return BinarySearch(arr, value, low, middle - 1);
        if (value > arr[middle])
            return BinarySearch(arr, value, middle + 1, high);
        return middle;
    }
}
