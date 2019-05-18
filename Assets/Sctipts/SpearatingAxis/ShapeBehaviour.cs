using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeBehaviour : MonoBehaviour {

    public List<Vector2> edgeDir = new List<Vector2>();
    public List<Vector2> normalDir = new List<Vector2>();

    public Transform Trans { get; private set; }

    private List<Vector2> oriEdgeDir = new List<Vector2>();
   
    private void Start()
    {
        Trans = transform;

        oriEdgeDir.AddRange(edgeDir);
    }

    public void RotateUpdateEdge()
    {
        for (int i = 0; i < edgeDir.Count; i++)
        {
            edgeDir[i] = new Vector2();
            normalDir[i] = new Vector2(edgeDir[i].y, -edgeDir[i].x).normalized;
        }
    }
}
