using Unity.Entities;
using UnityEngine;
[System.Serializable]
public class GravityComponentData : IComponentData
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
