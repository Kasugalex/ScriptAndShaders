using Unity.Entities;
using UnityEngine;


public struct GravityComponentData : IComponentData
{
    public float mass;
    public float delay;
    public float velocity;
}

public class GravityComponent : ComponentDataWrapper<GravityComponentData>
{

}

public class ComponentDataWrapper<T> : MonoBehaviour where T : IComponentData
{

}
