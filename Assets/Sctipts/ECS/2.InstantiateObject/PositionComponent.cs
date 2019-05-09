using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class PositionComponentData : IComponentData { }

public class PositionComponent : ComponentDataWrapper<Position>
{

}
