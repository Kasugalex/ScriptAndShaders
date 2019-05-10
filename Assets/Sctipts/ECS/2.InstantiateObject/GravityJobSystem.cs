using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Transforms;
using Unity.Jobs;
using Unity.Entities;
public class GravityJobSystem : JobComponentSystem
{
    public static float G = -20;
    public static float Top = 20f;
    public static float Bottom = -100f;

    struct GravityJob : IJobProcessComponentData<GravityComponentData, Position>
    {
        //运行在其它线程中，需要拷贝一份GravityJobSystem的数据
        public float G;
        public float Top;
        public float Bottom;

        public float deltaTime;

        public Unity.Mathematics.Random random;

        //物理运算
        public void Execute(ref GravityComponentData gravityData, ref Position positionData)
        {
            if (gravityData.delay > 0)
            {
                gravityData.delay -= deltaTime;
            }
            else
            {
                Vector3 pos = positionData.Value;
                float v = gravityData.velocity + G * gravityData.mass * deltaTime;
                pos.y += v;
                if (pos.y < Bottom)
                {
                    pos.y = Top;
                    gravityData.velocity = 0f;
                    gravityData.delay = random.NextFloat(0, 10);
                }
                positionData.Value = pos;
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        GravityJob job = new GravityJob()
        {
            G = G,
            Top = Top,
            Bottom = Bottom,
            deltaTime = Time.deltaTime,
            random = new Unity.Mathematics.Random((uint)(Time.time * 1000 + 1)),
        };


        return job.Schedule(this, inputDeps);
    }
}


