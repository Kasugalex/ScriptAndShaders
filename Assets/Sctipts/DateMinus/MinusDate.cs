using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MinusDate : MonoBehaviour
{

    public enum CalculateType
    {
        Seconds,
        Minutes,
        Hour,
        Full//相差时：分：秒
    }

    public CalculateType caculateType = CalculateType.Full;

    public string startTime;
    public string endTime;
    [SerializeField]
    private string DifTime;

    private void Start()
    {

    }

    private TimeSpan lastTime;
    void Update()
    {
        DateTime start = DateTime.Parse(startTime);
        DateTime end   = DateTime.Parse(endTime);
        TimeSpan dif   = end - start;

        if (lastTime == dif) return;

        switch (caculateType)
        {
            case CalculateType.Hour:
                DifTime = dif.TotalHours.ToString();
                break;
            case CalculateType.Minutes:
                DifTime = dif.TotalMinutes.ToString();
                break;
            case CalculateType.Seconds:
                DifTime = dif.TotalSeconds.ToString();
                break;
            case CalculateType.Full:
                DifTime = dif.ToString();
                break;
        }

        lastTime = dif;
    }


}
