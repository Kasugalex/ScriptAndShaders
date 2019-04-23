using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshViewer : MonoBehaviour {
    [HideInInspector]
    public List<Vector3> verticesList = new List<Vector3>();
    [SerializeField]
    private List<Vector2> uvList = new List<Vector2>();
    [SerializeField]
    private List<int> triList = new List<int>();

    public List<Vector3> verticesWorldPosList = new List<Vector3>();

    private Transform trans;
    private void Start()
    {
        trans = transform;
        ReadMeshInfo();
    }

    private Vector3 lastPos = Vector3.zero;
    private void Update()
    {
        if(trans.position != lastPos)
        {
            Vector3 offset = trans.position - lastPos;
            lastPos = trans.position;
            for (int i = 0; i < verticesWorldPosList.Count; i++)
            {
                verticesWorldPosList[i] += offset;
            }
        }
    }

    private void ReadMeshInfo()
    {
        MeshFilter _meshFilter = GetComponent<MeshFilter>();
        Mesh _mesh = _meshFilter.mesh;

        for (int i = 0, imax = _mesh.vertexCount; i < imax; ++i)
        {
            verticesList.Add(_mesh.vertices[i]);
            uvList.Add(_mesh.uv[i]);
        }

        for (int i = 0, imax = _mesh.triangles.Length; i < imax; ++i)
        {
            triList.Add(_mesh.triangles[i]);
        }

        verticesWorldPosList.AddRange(verticesList);
    }
}
