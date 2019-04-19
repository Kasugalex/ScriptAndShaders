using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiMosaicTest : PostEffectBase
{

    [Range(1, 50)]
    public int level = 10;
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null)
        {
            material.SetFloat("_Level", level);
            Graphics.Blit(src, dest, material);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }

}
