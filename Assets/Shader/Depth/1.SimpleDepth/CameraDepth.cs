using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class CameraDepth : MonoBehaviour {

    Material material;
    public Shader shader;
	void Start () {
        material = new Material(shader);

        Camera.main.depthTextureMode |= DepthTextureMode.Depth;
	}
	

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            Graphics.Blit(src, dest, material);
        }
        else
            Graphics.Blit(src, dest);
    }
}
