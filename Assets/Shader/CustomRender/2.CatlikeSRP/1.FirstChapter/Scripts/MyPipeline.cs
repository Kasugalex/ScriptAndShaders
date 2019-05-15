using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
//Conditional Code Execution
using Conditional = System.Diagnostics.ConditionalAttribute;
public class MyPipeline : RenderPipeline
{

    private CullResults cull;

    private CommandBuffer cameraBuffer = new CommandBuffer() { name = "Render Camera" };

    private Material errorMaterial;

    private DrawRendererFlags drawFlags;

    private const int maxVisibleLights = 4;
    private Vector4[] visibleLightColors = new Vector4[maxVisibleLights];
    private Vector4[] visibleLightDirections = new Vector4[maxVisibleLights];
    private static int visibleLightColorsId = Shader.PropertyToID("_VisibleLightColors");
    private static int visibleLightDirectionsId = Shader.PropertyToID("_VisibleLightDirections");


    public MyPipeline(bool dynamicBatching,bool instance)
    {
        //By default Unity considers the light's intensity to be defined in gamma space, even through we're working in linear space
        GraphicsSettings.lightsUseLinearIntensity = true;

        if (dynamicBatching)
        {
            drawFlags = DrawRendererFlags.EnableDynamicBatching;
        }
        if(instance)
        {
            drawFlags |= DrawRendererFlags.EnableInstancing;
        }
    }

    public MyPipeline() { }

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
#if UNITY_EDITOR
        //UI
        if (camera.cameraType == CameraType.SceneView)
            ScriptableRenderContext.EmitWorldGeometryForSceneView(camera);
#endif
        //supply th culling parameters as a reference parameter
        CullResults.Cull(ref cullingParameters, renderContext, ref cull);

        //apply camera's properties to the context,draw the skybox correctly
        renderContext.SetupCameraProperties(camera);

        CameraClearFlags clearFlags = camera.clearFlags;
        cameraBuffer.ClearRenderTarget((clearFlags & CameraClearFlags.Depth) != 0, (clearFlags & CameraClearFlags.Color) != 0, camera.backgroundColor);
        ConfigureLights();
        cameraBuffer.BeginSample("Render Camera");
        cameraBuffer.SetGlobalVectorArray(visibleLightColorsId, visibleLightColors);
        cameraBuffer.SetGlobalVectorArray(visibleLightDirectionsId, visibleLightDirections);
        renderContext.ExecuteCommandBuffer(cameraBuffer);
        cameraBuffer.Clear();

        //Drawing
        var drawSettings = new DrawRendererSettings(camera, new ShaderPassName("SRPDefaultUnlit"));//in here, we must create a new Shader named SRPDefaultUnlit
        drawSettings.sorting.flags = SortFlags.CommonOpaque;
        //open the dynamic batching
        drawSettings.flags = drawFlags;
        var filterSettings = new FilterRenderersSettings(true);
        //set the renderqueue

        // Draw Opaque renderers before the skybox to prevent overdraw
        // RenderQueueRange.opaque, which covers the render queues from 0 up to and including 2500.
        filterSettings.renderQueueRange = RenderQueueRange.opaque;
        renderContext.DrawRenderers(cull.visibleRenderers, ref drawSettings, filterSettings);

        renderContext.DrawSkybox(camera);

        //RenderQueueRange.transparent—from 2501 up to and including 5000—after rendering the skybox, and render again.
        drawSettings.sorting.flags = SortFlags.CommonTransparent;
        filterSettings.renderQueueRange = RenderQueueRange.transparent;
        renderContext.DrawRenderers(cull.visibleRenderers, ref drawSettings, filterSettings);

        //my pipeline only supports unlit shader,objects that use different shaders aren't rendered(such as Standard Shader)
        //so use this function to fix it
        DrawDefaultPipeline(renderContext, camera);

        //this just show information in frame debugger in one root
        cameraBuffer.EndSample("Render Camera");
        renderContext.ExecuteCommandBuffer(cameraBuffer);
        cameraBuffer.Clear();

        //drawSkybox will work after this
        renderContext.Submit();
    }

    //To only include the invocation when compiling for the Unity Editor and development builds
    [Conditional("UNITY_EDITOR"),Conditional("DEVELOPMENT_BUILD")]
    private void DrawDefaultPipeline(ScriptableRenderContext renderContext,Camera camera)
    {
        if(errorMaterial == null)
        {
            Shader errorShader = Shader.Find("Hidden/InternalErrorShader");
            errorMaterial = new Material(errorShader) {
                hideFlags = HideFlags.HideAndDontSave
            };
        }

        var drawSettings = new DrawRendererSettings(camera, new ShaderPassName("ForwardBase"));
        // there are other built-in shaders that we can identify with different passes
        drawSettings.SetShaderPassName(1, new ShaderPassName("PrepassBase"));
        drawSettings.SetShaderPassName(2, new ShaderPassName("Always"));
        drawSettings.SetShaderPassName(3, new ShaderPassName("Vertex"));
        drawSettings.SetShaderPassName(4, new ShaderPassName("VertexLMRGBM"));
        drawSettings.SetShaderPassName(5, new ShaderPassName("VertexLM"));

        //use this func to set errorMaterial
        drawSettings.SetOverrideMaterial(errorMaterial, 0);

        var filterSettings = new FilterRenderersSettings(true);

        renderContext.DrawRenderers(cull.visibleRenderers, ref drawSettings, filterSettings);
    }

    //Figures out which lights are visible
    private void ConfigureLights()
    {
        int i = 0;
        for (; i < cull.visibleLights.Count; i++)
        {
            //prevent the light count exceed maxVisibleLights
            if (i == maxVisibleLights) break;
            VisibleLight light = cull.visibleLights[i];
            //use finalColor 
            visibleLightColors[i] = light.finalColor;
            //The third column of that matrix defines the transformed local Z direction vector, which we can get via the Matrix4x4.GetColumn method, with index 2 as an argument.
            Vector4 v = light.localToWorld.GetColumn(2);
            v.x = -v.x;
            v.y = -v.y;
            v.z = -v.z;
            visibleLightDirections[i] = v;
        }
        //when the amount of visible lights decreases. They remain visible, because we don't reset their data. 
        for (; i < maxVisibleLights; i++)
        {
            visibleLightColors[i] = Color.clear;
        }
    }
}
