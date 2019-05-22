using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kasug;
using System.Runtime.InteropServices;

public class Tex2DColorSystem : MonoBehaviour
{

    [SerializeField] int                edgeLength              = 100;
    [SerializeField] Material           particleMaterial;
    [SerializeField] ComputeShader      updateShader;
    [SerializeField, Range(0.0f,1.0f)]  float deceleration      = 1f;


    private List<Mesh>                  allMeshes               = new List<Mesh>();
    private List<GPUParticle[]>         particleList            = new List<GPUParticle[]>();
    private List<MaterialPropertyBlock> blockList               = new List<MaterialPropertyBlock>();
    private List<ComputeBuffer>         bufferList              = new List<ComputeBuffer>();
    private Transform                   trans;
    private const int                   _Thred                  = 8;
    

    private void Start()
    {
        trans = transform;
        GenerateParticles();
    }

    private void Update()
    {
        CheckInit();

        float t = Time.timeSinceLevelLoad;
        updateShader.SetVector("_Time", new Vector4(t / 20f, t, t * 2f, t * 3f));
        updateShader.SetFloat("_DT", Time.deltaTime);
        updateShader.SetFloat("_Deceleration", deceleration);

        Dispatch("Update");

        blockList[0].SetBuffer("_Particles", bufferList[0]);

       // Graphics.DrawMesh(allMeshes[0], trans.localToWorldMatrix, particleMaterial, 0, null, 0, blockList[0]);
    }

    private void GenerateParticles()
    {
        var count = edgeLength * edgeLength * 1;
        var scale = 1f / count;
        var offset = -Vector3.one * 0.5f;
        particleList.Add(new GPUParticle[count]);
        for (int x = 0; x < edgeLength; x++)
        {
            var xoffset = x * edgeLength;
            for (int y = 0; y < edgeLength; y++)
            {
                var index = xoffset + y;
                var particle = new GPUParticle(0.5f, new Vector3(x, y, 5) * scale + offset, Quaternion.identity, Vector3.one, Vector3.zero,Vector3.zero, Color.white);
                particleList[0][index] = particle;
            }
        }

        blockList.Add(new MaterialPropertyBlock());
        blockList[0].SetFloat("_Size", scale);

        allMeshes.Add(BuildMesh(count, 0, 0));
    }

    private void CheckInit()
    {
        if(bufferList.Count == 0)
        {
            bufferList.Add(new ComputeBuffer(particleList[0].Length, Marshal.SizeOf(typeof(GPUParticle))));
            bufferList[0].SetData(particleList[0]);
        }
    }

    private Mesh BuildMesh(int count,int xStart,int yStart)
    {

        Mesh particleMesh = new Mesh();
        particleMesh.name = count.ToString();

        var dcount = count * count;
        var tcount = dcount * count;

        var scale = (1f / count);
        var dscale = scale * scale;
        var offset = -Vector3.one * 0.5f;

        var vertices = new Vector3[tcount];
        var uvs = new Vector2[tcount];
        var indices = new int[tcount];

        for (int x = 0; x < count; x++)
        {
            var xoffset = x * dcount;
            for (int y = 0; y < count; y++)
            {
                var yoffset = y * count;

                var index = xoffset + yoffset;
                vertices[index] = new Vector3(x + xStart, y + yStart, 0) * scale + offset;
                uvs[index] = new Vector2(x * scale, y * scale);
                indices[index] = index;
            }
        }

        particleMesh.vertices = vertices;
        particleMesh.SetIndices(indices, MeshTopology.Points, 0);
        return particleMesh;
    }

    private void Dispatch(string key)
    {
        int kernel = updateShader.FindKernel(key);
        updateShader.SetBuffer(kernel, "_Particles", bufferList[0]);
        updateShader.Dispatch(kernel, bufferList[0].count / _Thred + 1, 1, 1);
    }



    private void OnDisable()
    {
        if (bufferList[0] != null)
        {
            bufferList[0].Release();
            bufferList[0] = null;
        }
    }
}
