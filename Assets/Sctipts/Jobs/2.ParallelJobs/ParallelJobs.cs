using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;

public class ParallelJobs : MonoBehaviour
{
    void Start()
    {
        NativeArray<float> a = new NativeArray<float>(2, Allocator.TempJob);
        NativeArray<float> b = new NativeArray<float>(2, Allocator.TempJob);
        NativeArray<float> result = new NativeArray<float>(2, Allocator.TempJob);

        a[0] = 1.1f;
        b[0] = 2.2f;
        a[1] = 3.3f;
        b[1] = 4.4f;

        MyParallelJob jobData = new MyParallelJob();
        jobData.a = a;
        jobData.b = b;
        jobData.result = result;

        //执行当前Job
        JobHandle handle = jobData.Schedule(result.Length, 1);

        handle.Complete();

        a.Dispose();
        b.Dispose();
        result.Dispose();
    }

}

struct IncrementByDeltaTimeJob : IJobParallelFor
{
    public NativeArray<float> values;
    public float deltaTime;
    public void Execute(int index)
    {
        float temp = values[index];
        temp += deltaTime;
        values[index] = temp;
    }
}

struct MyParallelJob : IJobParallelFor
{
    [ReadOnly]
    public NativeArray<float> a;

    [ReadOnly]
    public NativeArray<float> b;

    public NativeArray<float> result;

    public void Execute(int index)
    {
        result[index] = a[index] + b[index];
        Debug.Log(index + "|" + result[index]);
    }
}
