using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectBase : MonoBehaviour {
	protected Material material;

	public Shader shader;

	protected Camera camera;

	private void OnAwake(){
		camera = GetComponent<Camera>();
		material = new Material(shader);
	}
}
