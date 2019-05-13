using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
public class MyPipeline : RenderPipeline
{
    protected virtual IRenderPipeline InternalCreatePipeline()
    {
        return new MyPipeline();
    }

    //Render function doesn't render anything, but check the object is valid to use
    public override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
    {
        base.Render(renderContext, cameras);

        foreach (var camera in cameras)
        {
            Render(renderContext, camera);
        }
    }

    private void Render(ScriptableRenderContext renderContext, Camera camera)
    {
        //this will take a camera as input and produce the cullign parameters as output
        ScriptableCullingParameters cullingParameters;
        //if settings are invalid,return
        if (!CullResults.GetCullingParameters(camera, out cullingParameters))
        {
            return;
        }

        //supply th culling parameters as a reference parameter
        CullResults cull = CullResults.Cull(ref cullingParameters, renderContext);

        //apply camera's properties to the context,draw the skybox correctly
        renderContext.SetupCameraProperties(camera);

        CameraClearFlags clearFlags = camera.clearFlags;

        var buffer = new CommandBuffer() { name = camera.name };
        buffer.ClearRenderTarget((clearFlags & CameraClearFlags.Depth) != 0, (clearFlags & CameraClearFlags.Color) != 0, camera.backgroundColor);
        renderContext.ExecuteCommandBuffer(buffer);
        buffer.Release();

        //Drawing
        var drawSettings = new DrawRendererSettings(camera, new ShaderPassName("SRPDefaultUnlit"));//in here, we must create a new Shader named SRPDefaultUnlit
        var filterSettings = new FilterRenderersSettings(true);
        //set the renderqueue
        filterSettings.renderQueueRange = RenderQueueRange.transparent;
        renderContext.DrawRenderers(cull.visibleRenderers, ref drawSettings, filterSettings);


        renderContext.DrawSkybox(camera);

        //drawSkybox will work after this
        renderContext.Submit();
    }
}
