using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;
public class SimpleJobs : MonoBehaviour
{

    void Start()
    {
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);

        MyJob jobData = new MyJob();
        jobData.a = 10;
        jobData.b = 10;
        jobData.result = result;
        //执行job1
        JobHandle handle = jobData.Schedule();

        //设置job2
        AddOneJob secJobData = new AddOneJob();
        secJobData.result = result;
        //执行job2，传入job1
        JobHandle secHandle = secJobData.Schedule(handle);
        //等待job2完成
        secHandle.Complete();

        float aPlusB = result[0];
        //释放内存
        result.Dispose();

        Debug.Log(aPlusB);
    }

}

struct MyJob : IJob
{
    public float a;
    public float b;
    public NativeArray<float> result;

    public void Execute()
    {
        result[0] = a + b;
    }
}

struct AddOneJob : IJob
{
    public NativeArray<float> result;
    public void Execute()
    {
        result[0] = result[0] + 1;
    }
}