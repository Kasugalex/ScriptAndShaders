using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class ScanLine : PostEffectBase
{

    public Color LineColor;
    [Range(0.0f, 0.99f)]
    public float CurrentValue;
    [Range(0.0f,0.5f)]
    public float LineWidth;
    [Range(0f,1.0f)]
    public float ScanSpeed = 10;
    void Start()
    {
        _camera.depthTextureMode = DepthTextureMode.Depth;
    }

    private void Update()
    {
        CurrentValue += Time.deltaTime * ScanSpeed;
        CurrentValue = CurrentValue >= 0.95f ? 0 : CurrentValue;
    }
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_CurValue", CurrentValue);
            material.SetColor("_LineColor", LineColor);
            material.SetFloat("_LineWidth", LineWidth);
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}