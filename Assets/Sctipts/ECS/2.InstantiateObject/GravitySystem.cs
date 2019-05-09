using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class GravitySystem : ComponentSystem
{

    struct Filter
    {
        public readonly int Length;
        public ComponentDataArray<GravityComponentData> gravity;
        public ComponentDataArray<Position> position;
    }

    [Inject] Filter data;
    public static float G = -20f;
    public static float Top = 20;
    public static float bottom = -100;

    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; i++)
        {
            var gravityData = data.gravity[i];

            if(gravityData.delay > 0)
            {
                gravityData.delay -= Time.deltaTime;
                data.gravity[i] = gravityData;
            }
            else
            {
                Vector3 pos = data.position[i].Value;
                float v = gravityData.velocity + G * gravityData.mass * Time.deltaTime;
                pos.y += v;

                if(pos.y < bottom)
                {
                    pos.y = Top;
                    gravityData.velocity = 0;
                    gravityData.delay = Random.Range(0, 10f);
                    data.gravity[i] = gravityData;
                }
                data.position[i] = new Position { Value = pos };
            }
        }
    }
}
