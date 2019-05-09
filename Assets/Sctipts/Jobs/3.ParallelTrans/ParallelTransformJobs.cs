using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;
public class ParallelTransformJobs : MonoBehaviour
{
    ParallelTransStruct jobData;
    TransformAccessArray array;
    void Start()
    {

        jobData = new ParallelTransStruct();
        jobData.speed = 0.1f;
        jobData.deltaTime = 0.1f;
        Transform root = GameObject.Find("GameObjects").transform;
        array = new TransformAccessArray(root.childCount);
        for (int i = 0; i < root.childCount; i++)
        {
            array.Add(root.GetChild(i));
        }

    }

    private void Update()
    {
        JobHandle handle = jobData.Schedule(array);

        handle.Complete();
    }

    private void OnDestroy()
    {
        array.Dispose();
    }
}

struct ParallelTransStruct : IJobParallelForTransform
{
    public float speed;
    public float deltaTime;
    public void Execute(int index, TransformAccess transform)
    {
        Vector3 position = transform.position;
        position = position + new Vector3(0,speed,0) * deltaTime;
        transform.position = position;
    }
}
