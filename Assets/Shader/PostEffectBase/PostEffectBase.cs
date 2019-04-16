using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PostEffectBase : MonoBehaviour {
	protected Material material;

	public Shader shader;

	protected Camera _camera;

	protected void Awake(){
		_camera = Camera.main;
		material = new Material(shader);
	}
}
