using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class SortTest : MonoBehaviour
{
    public int ArrayLength = 50;
    [Tooltip("数组值的绝对值范围")]
    public int ValueAbsolute = 30;
    [SerializeField]
    private int[] Array;
    void Start()
    {
        GenerateArray();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Sort();
        }
    }


    private void GenerateArray()
    {
        Array = null;
        Array = new int[ArrayLength];
        ValueAbsolute = ValueAbsolute < 0 ? -ValueAbsolute : ValueAbsolute;
        for (int i = 0; i < ArrayLength; i++)
        {
            int random = Random.Range(-ValueAbsolute, ValueAbsolute);
            Array[i] = random;
        }
    }
    private void Sort()
    {
        //WatchExecuteTime.WatchExecute(ShellSort);
        WatchExecuteTime.WatchExecute(InsertSort);
    }
    private void ShellSort()
    {
        SortAlgorithm.ShellSort(Array);
    }

    private void InsertSort()
    {
        SortAlgorithm.InsertSort(Array);
    }



    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 40;
        int width = 500;
        GUI.TextArea(new Rect((Screen.width >> 1) - width / 2, 100, width, 200), "运行后按下鼠标左键进行排序", style);
    }
}
