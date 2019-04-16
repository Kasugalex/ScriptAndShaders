using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MotionBlur : PostEffectBase
{

    [Range(0.0f, 1.0f)]
    public float blurSize = 0.5f;

    private Matrix4x4 previousViewProjectionMatrix;

    [Range(0, 10)]
    public int sampleTimes = 3;

    [Range(1.0f, 20.0f)]
    public float sampleVelocity = 2.0f;
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
            material.SetFloat("_SampleVelocity", sampleVelocity);
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
