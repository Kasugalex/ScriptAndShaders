using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
[ExecuteInEditMode]
public class BinarySearch : MonoBehaviour
{
    [Range(0, 1000000)]
    public int ArrayLength = 25;
    [SerializeField]
    private int MaxValue = 200;
    [SerializeField]
    private int searchValue;
    [SerializeField]
    private int[] sortArray;

    public enum SearchType
    {
        Binary,
        BinaryTree
    }
    public SearchType searchType = SearchType.Binary;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ClearLog.Clear();
            sortArray = null;
            sortArray = new int[ArrayLength];
            if (searchType == SearchType.Binary)
            {
                for (int i = 0; i < ArrayLength; i++)
                {
                    int value = Random.Range(0, MaxValue);
                    sortArray[i] = value;
                }
                searchValue = sortArray[Random.Range(0, sortArray.Length)];

                WatchExecuteTime.WatchExecute(QuickSort);

                WatchExecuteTime.WatchExecute(RecursiveSearch);

                WatchExecuteTime.WatchExecute(NonRecursiveSearch);
            }
            else if(searchType == SearchType.BinaryTree)
            {
                BinaryTreeSearch tree = new BinaryTreeSearch();
                for (int i = 0; i < ArrayLength; i++)
                {
                    int value = Random.Range(0, MaxValue);
                    sortArray[i] = value;
                    tree.Insert(value);
                }

                //数组大了，赋值过程很漫长
                if (ArrayLength <= 10000)
                    WatchExecuteTime.WatchExecute(() => { tree.SortAll(tree.root, sortArray); }, "BinaryTreeSearch");
            }
        }
    }

    #region 二分查找
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
    #endregion

    private void OnGUI() {
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.fontSize = 40;
		
        fontStyle.normal.textColor = Color.white;
        int width = 500;
        GUI.TextArea(new Rect(650,100,width,width),"选择排序方式并鼠标左键即可看输出结果",fontStyle);
	}
}

public class BinarySearchFunc
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

public class BinaryTreeSearch
{
    private int node;
    public BinaryTreeSearch root;
    public BinaryTreeSearch left;
    public BinaryTreeSearch right;

    public void Insert(int value)
    {
        BinaryTreeSearch Parent;
        //将所需插入的数据包装进节点
        BinaryTreeSearch newNode = new BinaryTreeSearch();
        newNode.node = value;

        //如果为空树，则插入根节点
        if (root == null)
        {
            root = newNode;
        }
        //否则找到合适叶子节点位置插入
        else
        {
            BinaryTreeSearch Current = root;
            while (true)
            {
                Parent = Current;
                if (newNode.node < Current.node)
                {
                    Current = Current.left;
                    if (Current == null)
                    {
                        Parent.left = newNode;
                        //插入叶子后跳出循环
                        break;
                    }
                }
                else
                {
                    Current = Current.right;
                    if (Current == null)
                    {
                        Parent.right = newNode;
                        //插入叶子后跳出循环
                        break;
                    }
                }
            }
        }
    }

    int index = 0;
    public void SortAll(BinaryTreeSearch tree, int[] sortArray)
    {
        if (tree == null)
        {
            return;
        }

        SortAll(tree.left, sortArray);
        sortArray[index] = tree.node;
        index++;
        SortAll(tree.right, sortArray);
    }
}
