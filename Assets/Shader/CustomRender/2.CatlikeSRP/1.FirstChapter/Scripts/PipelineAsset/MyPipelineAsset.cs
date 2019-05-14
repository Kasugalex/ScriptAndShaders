using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

[CreateAssetMenu(menuName = "Kasug/My Pipeline")]
public class MyPipelineAsset : RenderPipelineAsset
{
    [SerializeField]
    bool dynamicBatching;
    [SerializeField]
    bool instance;
    protected override IRenderPipeline InternalCreatePipeline()
    {
        return new MyPipeline(dynamicBatching,instance);
    }
}
