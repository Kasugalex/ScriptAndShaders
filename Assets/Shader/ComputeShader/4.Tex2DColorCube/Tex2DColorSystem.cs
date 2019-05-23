using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Kasug;
using UnityEngine;

public class Tex2DColorSystem : MonoBehaviour {

    [SerializeField] int                    edgeLength                  = 100;
    [SerializeField] Shader                 particleShader;
    [SerializeField] ComputeShader          updateShader;
    [SerializeField, Range (0.0f, 1.0f)]    float deceleration          = 1f;

    private const int                       _Thred                      = 8;

    private void Start () {

        GenerateParticles ();
    }

    private void Update () {

    }

    private void GenerateParticles () {

    }

    private Mesh BuildMesh (int count, int xStart, int yStart) {

        Mesh particleMesh = new Mesh ();
        particleMesh.name = count.ToString ();

        var dcount = count * count;
        var tcount = dcount * count;

        var scale = (1f / count);
        var dscale = scale * scale;
        var offset = -Vector3.one * 0.5f;

        var vertices = new Vector3[tcount];
        var uvs = new Vector2[tcount];
        var indices = new int[tcount];

        for (int x = 0; x < count; x++) {
            var xoffset = x * dcount;
            for (int y = 0; y < count; y++) {
                var yoffset = y * count;

                var index = xoffset + yoffset;
                vertices[index] = new Vector3 (x + xStart, y + yStart, 0) * scale + offset;
                uvs[index] = new Vector2 (x * scale, y * scale);
                indices[index] = index;
            }
        }

        particleMesh.vertices = vertices;
        particleMesh.SetIndices (indices, MeshTopology.Points, 0);
        return particleMesh;
    }

    private void OnDisable () {

    }
}