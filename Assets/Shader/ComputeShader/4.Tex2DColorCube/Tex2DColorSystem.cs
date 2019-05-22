using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kasug;
public class Tex2DColorSystem : MonoBehaviour
{

    [SerializeField] int                edgeLength              = 100;
    [SerializeField] Material           particleMaterial;
    [SerializeField] ComputeShader      updateShader;
    [SerializeField, Range(0.0f,1.0f)]  float deceleration      = 1f;


    private List<Mesh>                  allMeshes               = new List<Mesh>();
    private List<GPUParticle[]>         particleList            = new List<GPUParticle[]>();
    private List<MaterialPropertyBlock> blockList               = new List<MaterialPropertyBlock>();

    private Transform                   trans;
    private const int                   _Thred                  = 8;

    private void Start()
    {
        trans = transform;
        GenerateParticles();
    }


    private void GenerateParticles()
    {
        var count = edgeLength * edgeLength * 1;
        var scale = 1f / count;
        var offset = -Vector3.one * 0.5f;

        for (int x = 0; x < edgeLength; x++)
        {
            var xoffset = x * edgeLength;
            for (int y = 0; y < edgeLength; y++)
            {
                var index = xoffset + y;
                var particle = new GPUParticle(0.5f, new Vector3(x, y, 5) * scale + offset, Quaternion.identity, Vector3.one, Vector3.zero,Vector3.zero, Color.white);
            }
        }
    }
}
