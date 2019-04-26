using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveWater : MonoBehaviour {


    public Texture waterTexture;

    public Texture noiseTex;

    public Shader waterShader;

    [Space(15)]
    public float WindForce = 10;

    public Vector3 WindDirection = Vector3.zero;

    private Transform _trans;

    private Material waterMaterial;

    private MeshRenderer waterMesh;
	void Start () {
        _trans = transform;
        WindDirection = _trans.forward.normalized;

        waterMaterial = new Material(waterShader);
        waterMesh = FindObjectOfType<MeshRenderer>();
        waterMaterial.mainTexture = waterTexture;
        waterMaterial.SetTexture("_NoiseTex", noiseTex);
        waterMesh.material = waterMaterial;
    }

    private Vector3 lastDirection;
    private float lastForce;
    private void Update()
    {

        WindDirection = _trans.forward.normalized;
        _trans.eulerAngles = new Vector3(0, _trans.eulerAngles.y, 0);

        if (lastDirection == WindDirection && WindForce == lastForce) return;
        lastDirection = WindDirection;
        lastForce = WindForce;

        SetShaderProperty();
    }


    private void SetShaderProperty()
    {

        //waterMaterial.SetFloat("_WaveFrequency", _WaveFrequency);
        waterMaterial.SetFloat("_WindForce", WindForce);
        waterMaterial.SetVector("_Direction", new Vector2(WindDirection.x,WindDirection.z));

        if(WindDirection.x != 0)
        {
            int noisexScroll = WindDirection.x > 0 ? 1 : -1;
            waterMaterial.SetFloat("_NoisexScroll", noisexScroll);
        }

        if (WindDirection.y != 0)
        {
            int noiseyScroll = WindDirection.y > 0 ? 1 : -1;
            waterMaterial.SetFloat("_NoiseyScroll", noiseyScroll);
        }
    }

}