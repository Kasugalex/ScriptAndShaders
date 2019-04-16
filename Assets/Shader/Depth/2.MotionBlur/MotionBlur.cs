using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MotionBlur : PostEffectBase
{

    [Range(0.0f, 1.0f)]
    public float blurSize = 0.5f;

    private Matrix4x4 previousViewProjectionMatrix;

	[Tooltip("采样次数")]
    [Range(0, 10)]
    public int sampleTimes = 3;

	[Tooltip("采样距离,值越大模糊越精细")]
    [Range(1.0f, 20.0f)]
    public float sampleFactor = 2.0f;
    void Start()
    {
        _camera.depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {

            material.SetFloat("_BlurSize", blurSize);
            material.SetFloat("_SampleTimes", sampleTimes);
            material.SetFloat("_SampleFactor", sampleFactor);
            material.SetMatrix("_PreviousViewProjectionMatrix", previousViewProjectionMatrix);
            Matrix4x4 currentViewProjectionMatrix = _camera.projectionMatrix * _camera.worldToCameraMatrix;
            Matrix4x4 currentViewProjectionInverseMatrix = currentViewProjectionMatrix.inverse;
            material.SetMatrix("_CurrentViewProjectionInverseMatrix", currentViewProjectionInverseMatrix);
            previousViewProjectionMatrix = currentViewProjectionMatrix;

            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}
