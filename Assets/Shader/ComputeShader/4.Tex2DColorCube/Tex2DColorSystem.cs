using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Kasug;
using UnityEngine;

public class Tex2DColorSystem : MonoBehaviour {

    [SerializeField] int edgeLength = 100;
    [SerializeField] Material particleMaterial;
    [SerializeField] ComputeShader updateShader;
    [SerializeField, Range (0.0f, 1.0f)] float deceleration = 1f;

    private const int _Thred = 8;
    private Transform _trans;
    private MaterialPropertyBlock block;
    public Mesh m;
    private void Start () {
        _trans = transform;
        GenerateParticles ();
    }

    private void Update () {

        Graphics.DrawMesh (SimpleCubeMesh(1,0), _trans.localToWorldMatrix, particleMaterial, 0, null, 0, block);
        Graphics.DrawMesh (m, Vector3.one, Quaternion.identity, particleMaterial, 0, null, 0, block);

    }

    private void GenerateParticles () {
        
    }

    private Mesh SimpleCubeMesh(int xOffset,int yOffset){
        Mesh mesh = CreateMesh.CreateCubeMesh(xOffset,yOffset,0);
        return mesh;
    }

    private void OnDisable () {

    }
}