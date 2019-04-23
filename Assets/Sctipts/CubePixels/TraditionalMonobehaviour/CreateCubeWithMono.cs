using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCubeWithMono : MonoBehaviour {
    public bool showIndex = false;
    public Material material;
    void Start()
    {
        CreateMesh.CreateCube(Vector3.zero, material, "First");
    }
	
}

public class CreateMesh
{
    public static GameObject CreateCube(Transform root, Material mat, string cubeName = "Cube")
    {
        GameObject cube = new GameObject(cubeName);
        MeshFilter filter = cube.AddComponent<MeshFilter>();
        MeshRenderer renderer = cube.AddComponent<MeshRenderer>();
        Transform t = cube.transform;
        t.parent = root;
        t.localPosition = Vector3.zero;

        List<Vector3> vertices = new List<Vector3>(24);
        List<int> _triList = new List<int>();
        List<Vector2> _uvList = new List<Vector2>();

        //forward
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



        //back
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


        //up
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

        //down
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

        //left
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

        //right
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

        filter.mesh.SetVertices(vertices);
        filter.mesh.SetTriangles(_triList, 0);
        filter.mesh.SetUVs(0, _uvList);

        renderer.material = mat;

#if UNITY_EDITOR
        cube.AddComponent<MeshViewer>();
#endif
        return cube;

    }

    public static GameObject CreateCube(Vector3 pos, Material mat, string cubeName = "Cube")
    {
        GameObject cube = new GameObject(cubeName);
        MeshFilter filter = cube.AddComponent<MeshFilter>();
        MeshRenderer renderer = cube.AddComponent<MeshRenderer>();
        cube.transform.position = pos;


        List<Vector3> vertices = new List<Vector3>(24);
        List<int> _triList = new List<int>();
        List<Vector2> _uvList = new List<Vector2>();

        //forward
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



        //back
        vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
        vertices.Add(new Vector3(-0.5f,-0.5f,0.5f));
        vertices.Add(new Vector3(-0.5f,0.5f, 0.5f));
        vertices.Add(new Vector3(0.5f,0.5f, 0.5f));

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


        //up
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

        //down
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

        //left
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

        //right
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

        filter.mesh.SetVertices(vertices);
        filter.mesh.SetTriangles(_triList,0);
        filter.mesh.SetUVs(0, _uvList);

        // 本来只需要8个顶点就可以了。
        // 但是由于6个四边形面的纹理坐标，不能共用顶点的纹理坐标
        // 所以需要8x3个顶点（同样的顶点被3个四边形面共用)
        /*filter.mesh.vertices = new Vector3[]
        {   new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 1),
        };
        //设置三角形顶点顺序，顺时针设置
        filter.mesh.triangles = new int[]
        {
          0,2,1,
          0,3,2,
          3,4,2,
          4,5,2,
          4,7,5,
          7,6,5,
          7,0,1,
          6,7,1,
          4,3,0,
          4,0,7,
          2,5,6,
          2,6,1
        };*/

        renderer.material = mat;

#if UNITY_EDITOR
        cube.AddComponent<MeshViewer>();
#endif
        return cube;

    }
}
