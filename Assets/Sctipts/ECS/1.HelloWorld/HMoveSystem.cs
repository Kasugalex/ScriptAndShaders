using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
public class HMoveSystem : ComponentSystem
{
    public struct Filter
    {
        public Transform trans;
        public HMoveComponent moveComponent;
    }

    protected override void OnUpdate()
    {
        foreach (var entity in GetEntities<Filter>())
        {
            Vector3 pos = entity.trans.position + entity.moveComponent.moveDirection * Time.deltaTime;
            entity.trans.position = pos;
        }
    }
}
