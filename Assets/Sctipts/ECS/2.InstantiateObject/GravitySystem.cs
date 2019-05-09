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
        throw new System.NotImplementedException();
    }
}
