using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class IntersectionHighLight : PostEffectBase {

	void Start () {
        _camera.depthTextureMode = DepthTextureMode.Depth;
	}

}
