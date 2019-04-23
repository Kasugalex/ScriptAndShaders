using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCubeWithMono : MonoBehaviour {
    public int width = 0;
    public int height = 0;

    public float GenerateOffset_X = 10;
    public float GenerateOffset_Y = 10;
    public Material material;
    void Start()
    {
        material.SetPass(0);
        WatchExecuteTime.WatchExecute(CreateCube);
    }

    private void CreateCube()
    {
        Transform root = new GameObject("Root").transform;
        root.position = new Vector3(-95, -53, 100);
        Vector3 rootPos = root.position;

        Mesh mesh = CreateMesh.CreateCubeMesh();

        GameObject first = GameObject.CreatePrimitive(PrimitiveType.Quad);
        first.transform.localPosition = new Vector3(10000, 10000, 0);
        first.GetComponent<Renderer>().material = material;
        first.GetComponent<MeshFilter>().mesh = mesh;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject obj = Instantiate(first, root);
                obj.name = string.Format("{0}:{1}", y, x);
                obj.transform.localPosition = new Vector3(GenerateOffset_X + x, GenerateOffset_Y + y, 0);
            }
        }

    }

}

public class CreateMesh
{
    public static Mesh CreateCubeMesh()
    {
        Mesh mesh = new Mesh();


        List<Vector3> vertices = new List<Vector3>(24);
        List<int> _triList = new List<int>();
        List<Vector2> _uvList = new List<Vector2>();

        #region Forward

        vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
        vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
        vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));
        vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));

        _triList.Add(0);
        _triList.Add(1);
        _triList.Add(2);

        _triList.Add(0);
        _triList.Add(2);
        _triList.Add(3);

        _uvList.Add(new Vector2(1, 0));
        _uvList.Add(new Vector2(0, 0));
        _uvList.Add(new Vector2(0, 1));
        _uvList.Add(new Vector2(1, 1));
        #endregion

        #region Back
        vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
        vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
        vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
        vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));

        _triList.Add(4);
        _triList.Add(7);
        _triList.Add(5);

        _triList.Add(5);
        _triList.Add(7);
        _triList.Add(6);

        _uvList.Add(new Vector2(0, 0));
        _uvList.Add(new Vector2(1, 0));
        _uvList.Add(new Vector2(1, 1));
        _uvList.Add(new Vector2(0, 1));
        #endregion

        #region Up
        vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));
        vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));
        vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
        vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));

        _triList.Add(8);
        _triList.Add(9);
        _triList.Add(10);

        _triList.Add(8);
        _triList.Add(10);
        _triList.Add(11);

        _uvList.Add(new Vector2(1, 0));
        _uvList.Add(new Vector2(0, 0));
        _uvList.Add(new Vector2(0, 1));
        _uvList.Add(new Vector2(1, 1));
        #endregion

        #region Down
        vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
        vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
        vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
        vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));

        _triList.Add(14);
        _triList.Add(12);
        _triList.Add(15);

        _triList.Add(12);
        _triList.Add(14);
        _triList.Add(16);

        _uvList.Add(new Vector2(0, 0));
        _uvList.Add(new Vector2(1, 0));
        _uvList.Add(new Vector2(1, 1));
        _uvList.Add(new Vector2(0, 1));
        #endregion

        #region Left
        vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
        vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
        vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
        vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));

        _triList.Add(16);
        _triList.Add(17);
        _triList.Add(18);

        _triList.Add(16);
        _triList.Add(18);
        _triList.Add(19);

        _uvList.Add(new Vector2(1, 0));
        _uvList.Add(new Vector2(0, 0));
        _uvList.Add(new Vector2(0, 1));
        _uvList.Add(new Vector2(1, 1));
        #endregion

        #region Right
        vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
        vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
        vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
        vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));

        _triList.Add(20);
        _triList.Add(23);
        _triList.Add(21);

        _triList.Add(21);
        _triList.Add(23);
        _triList.Add(22);

        _uvList.Add(new Vector2(0, 0));
        _uvList.Add(new Vector2(1, 0));
        _uvList.Add(new Vector2(1, 1));
        _uvList.Add(new Vector2(0, 1));
        #endregion

        mesh.SetVertices(vertices);
        mesh.SetTriangles(_triList, 0);
        mesh.SetUVs(0, _uvList);
        return mesh;
    }
}     