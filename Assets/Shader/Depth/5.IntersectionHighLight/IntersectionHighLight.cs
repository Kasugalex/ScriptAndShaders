using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class IntersectionHighLight : PostEffectBase {

    public Color intersectionColor;

    [Range(0.0f,1.0f)]
    public float intersectionWidth;

	void Start () {
        _camera.depthTextureMode = DepthTextureMode.Depth;
	}

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetColor("_IntersectionColor", intersectionColor);
            material.SetFloat("_IntersectionWidth", intersectionWidth);
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }


}
