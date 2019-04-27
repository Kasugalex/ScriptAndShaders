using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Flood : PostEffectBase {

    public Color WaterColor;
    [Range(-1.5f, 1.0f)]
    public float WaterHeight;

	void Start () {
        _camera.depthTextureMode = DepthTextureMode.Depth;
	}

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if(material != null)
        {
            material.SetColor("_WaterColor", WaterColor);
            material.SetFloat("_WaterHeight", WaterHeight);
            //Matrix4x4 frustumDir = CalculateFrustum.Frustum(_camera);        
            material.SetMatrix("_FrustumDir", CalculateFrustum.Frustum(_camera));
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}
