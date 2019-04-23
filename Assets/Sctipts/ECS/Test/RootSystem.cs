using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class RootSystem : Feature
{

    public RootSystem(Contexts contexts)
    {
        Add(new CreateSystem(contexts));
        Add(new HelloWorldSystem(contexts));
    }
}