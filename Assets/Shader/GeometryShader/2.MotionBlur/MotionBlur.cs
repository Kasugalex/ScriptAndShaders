using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MotionBlur : PostEffectBase {

	[Range(0.0f,1.0f)]
	public float blurSize = 0.5f;

	private Matrix4x4 previousViewProjectionMatrix;
	void Start () {
		camera.depthTextureMode |= DepthTextureMode.Depth; 	
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest) {
		if(material != null){
			
			material.SetFloat("_BlurSize",blurSize);
			material.SetMatrix("_PreviousViewProjectionMatrix",previousViewProjectionMatrix);
			
			Matrix4x4 currentProjectionMatrix = camera.projectionMatrix;
			
			material.SetMatrix("_CurrentProjectionInverseMatrix",currentProjectionMatrix.inverse);
			previousViewProjectionMatrix = currentProjectionMatrix;
			Graphics.Blit(src,dest,material);
		}else
		{
			Graphics.Blit(src,dest);
		}
	}
	
}
