using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
public class HInputSystem : ComponentSystem
{
    struct Data
    {
        public ComponentArray<HMoveComponent> moveArray;
    }
    [Inject] private Data _data;
    protected override void OnUpdate()
    {
        for (int i = 0; i < _data.moveArray.Length; i++)
        {
            _data.moveArray[i].moveDirection = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        }
    }
}
