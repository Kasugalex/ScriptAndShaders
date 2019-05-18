using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearatingAxis : MonoBehaviour {

    private ShapeBehaviour currentSelect;
    private Camera mainCam;

    private Dictionary<Transform, ShapeBehaviour> allShapeDictionary = new Dictionary<Transform, ShapeBehaviour>();
    void Start()
    {
        mainCam = Camera.main;
        CreateShape();

    }

    private Vector3 lastMousePosition;
    private Vector3 posOffset;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                allShapeDictionary.TryGetValue(hit.collider.transform, out currentSelect);
                posOffset = currentSelect.Trans.position - mainCam.ScreenToWorldPoint(Input.mousePosition);
            }
        } else if (Input.GetMouseButton(0))
        {
            if (currentSelect == null)
                return;

            DetecteCollision(currentSelect);

            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            if (lastMousePosition != mousePos)
            {
                Vector3 endPos = mousePos + posOffset;

                currentSelect.Trans.position = new Vector3(endPos.x, endPos.y, currentSelect.Trans.position.z);

                lastMousePosition = mousePos;
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentSelect = null;
        }
    }

    private bool DetecteCollision(ShapeBehaviour shape)
    {
        bool overlap = false;
        foreach (var behaviour in allShapeDictionary.Values)
        {
            if (behaviour == shape) continue;
            /*
             * 设边的向量为( x1 , y1 ) , 法向量为( x2 , y2 ) ,那么边在法向量 上的投影 dot的计算为 temp1 = x2*x2 + y2*y2; 
             * temp2 = x1*x1+y1*y1;
             * dot_x = temp2*x2/temp1;
             * dot_y = temp2*y2/temp1;
             * dot = dot_x*x2+dot_y*y2
             */
            for (int i = 0; i < shape.normalDir.Count; i++)
            {
                Vector2 normal = shape.normalDir[i];
                //检测是否重合
                var minDot1 = float.MaxValue; var maxDot1 = -float.MaxValue;
                for (int j = 0; j < shape.edgeDir.Count; j++)
                {
                    Vector2 edge = shape.edgeDir[j];
                    var normalLength = normal.x * normal.x + normal.y * normal.y;
                    var edgeLength = edge.x * edge.x + edge.y * edge.y;
                    var dot_x = edgeLength * normal.x / normalLength;
                    var dot_y = edgeLength * normal.y / normalLength;
                    var dot = dot_x * normal.x + dot_y * normal.y;

                    minDot1 = dot < minDot1 ? dot : minDot1;
                    maxDot1 = dot > maxDot1 ? dot : maxDot1;
                }

                var minDot2 = float.MaxValue; var maxDot2 = -float.MaxValue;
                for (int j = 0; j < behaviour.edgeDir.Count; j++)
                {
                    Vector2 edge = behaviour.edgeDir[j];
                    var normalLength = normal.x * normal.x + normal.y * normal.y;
                    var edgeLength = edge.x * edge.x + edge.y * edge.y;
                    var dot_x = edgeLength * normal.x / normalLength;
                    var dot_y = edgeLength * normal.y / normalLength;
                    var dot = dot_x * normal.x + dot_y * normal.y;

                    minDot2 = dot < minDot2 ? dot : minDot2;
                    maxDot2 = dot > maxDot2 ? dot : maxDot2;
                }

                if ((minDot1 <= minDot2 && maxDot1 >= minDot2) || (minDot2 <= minDot1 && maxDot2 >= minDot1))
                {
                    overlap = true;
                }
            }
        }

        return overlap;
    }

    private void CreateShape()
    {
        Material mat = Resources.Load("Materials/Line") as Material;
        Vector3[] pos = { new Vector3(0,0),new Vector3(0,2),new Vector3(2,2),new Vector3(2,0) };
        Transform square = ShapeGenerator.CreateTangram(TangramType.Square, pos, mat, Color.red);
        AddShapeToDictionary(square);

        Vector3[] pos2 = { new Vector3(0, 0), new Vector3(2, 2) , new Vector3(2, 0)};
        Transform triangle = ShapeGenerator.CreateTangram(TangramType.Triangle, pos2, mat, Color.yellow);
        triangle.position = new Vector3(5, 0, 0);
        AddShapeToDictionary(triangle);

        float sqrt2 = Mathf.Sqrt(2);
        Vector3[] pos3 = { new Vector3(0, 0), new Vector3(-sqrt2, sqrt2), new Vector3(sqrt2, sqrt2), new Vector3(2*sqrt2, 0) };
        Transform parallelogram = ShapeGenerator.CreateTangram(TangramType.Square, pos3, mat, Color.gray);
        parallelogram.position = new Vector3(-5, 0, 0);
        AddShapeToDictionary(parallelogram);
    }

    private void AddShapeToDictionary(Transform t)
    {
        allShapeDictionary.Add(t, t.GetComponent<ShapeBehaviour>());
    }
	
}

public static class ShapeGenerator
{

    public static Transform CreateTangram(TangramType type,Vector3[] vertices, Material material, Color color)
    {

        GameObject shapeObject = new GameObject(type.ToString());
        //初始化位置
        shapeObject.transform.position = Vector3.zero;

        //初始化
        Shape shape = null;
        switch (type)
        {
            case TangramType.Square:
                shape = new Square();
                break;
            case TangramType.Triangle:
                shape = new Triangle();
                break;
        }

        MeshFilter filter = shapeObject.AddComponent<MeshFilter>();

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        int[] triangles = shape.GetTirangles();
        mesh.triangles = triangles;

        filter.mesh = mesh;

        //初始化Renderer
        MeshRenderer renderer = shapeObject.AddComponent<MeshRenderer>();
        renderer.material = material;
        renderer.material.color = color;

        //初始化Collider
        PolygonCollider2D collider = shapeObject.AddComponent<PolygonCollider2D>();
        Vector2[] points = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            points[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        collider.points = points;

        //初始化Edge和Normal
        ShapeBehaviour behaviour =  shapeObject.AddComponent<ShapeBehaviour>();

        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 dir = new Vector2(points[i].x - points[i + 1].x, points[i].y - points[i + 1].y);
            behaviour.edgeDir.Add(dir.normalized);

            Vector2 normal = new Vector2(dir.y, -dir.x).normalized;
            behaviour.normalDir.Add(normal);
        }

        Vector2 firstAndLastPointDir = new Vector2(points[0].x - points[points.Length - 1].x, points[0].y - points[points.Length - 1].y);
        behaviour.edgeDir.Add(firstAndLastPointDir.normalized);

        Vector2 normal1 = new Vector2(firstAndLastPointDir.y,-firstAndLastPointDir.x).normalized;
        behaviour.normalDir.Add(normal1);

        return shapeObject.transform;
    }

    public static Transform SetName(this Transform self, string str)
    {
        self.name = str;
        return self;
    }


    #region 形状工厂
    private class Shape
    {
        public virtual int[] GetTirangles()
        {
            return null;
        }

        public virtual Vector2[] GetProjectiongVector()
        {
            return null;
        }
    }

    private class Square : Shape
    {

        public override int[] GetTirangles()
        {
            return new int[] {
                0,1,2,
                0,2,3
            };
        }

        public override Vector2[] GetProjectiongVector()
        {
            return base.GetProjectiongVector();
        }

    }

    private class Triangle : Shape
    {
        public override int[] GetTirangles()
        {
            return new int[] {
                0,1,2
            };
        }
        public override Vector2[] GetProjectiongVector()
        {
            return base.GetProjectiongVector();
        }

    }

    #endregion
}

public enum TangramType
{
    Square,
    Triangle
}
