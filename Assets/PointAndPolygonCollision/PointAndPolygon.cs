using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PointAndPolygon : MonoBehaviour {
    /*const float RAYCAST_LEN = 100000f;
    public Transform[] points;
    public Transform compare;


    bool IsContract(Transform compare)
    {
        var comparePoint = (points[1].position + points[0].position) * 0.5f;
        var originPoint = compare.transform.position;
        comparePoint += (comparePoint - originPoint).normalized * RAYCAST_LEN;

        Debug.DrawLine(originPoint, comparePoint);

        int count = 0;
        for (int i = 0; i < points.Length; i++)
        {
            var a = points[i % points.Length];
            var b = points[(i + 1) % points.Length];

            var r = IsIntersection(a.position, b.position, originPoint, comparePoint);

            if (r) count++;
        }
        print(count);
        return count % 2 == 1;
    }

    void OnDrawGizmos()
    {
        if (compare == null) return;

        var oldColor = Gizmos.color;

        if (IsContract(compare))
            Gizmos.color = Color.red;

        for (int i = 0; i < points.Length; i++)
        {
            var a = points[i % points.Length];
            var b = points[(i + 1) % points.Length];

            Gizmos.DrawLine(a.position, b.position);
        }

        Gizmos.color = oldColor;
    }

    bool IsIntersection(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        var crossA = Mathf.Sign(Vector3.Cross(d - c, a - c).y);
        var crossB = Mathf.Sign(Vector3.Cross(d - c, b - c).y);

        if (Mathf.Approximately(crossA, crossB)) return false;

        var crossC = Mathf.Sign(Vector3.Cross(b - a, c - a).y);
        var crossD = Mathf.Sign(Vector3.Cross(b - a, d - a).y);

        if (Mathf.Approximately(crossC, crossD)) return false;

        return true;
    }
    */
    private Transform anchor;

    private Transform point;

    private Vector3 axisPosition;

    public bool drawPointLine = false;

    private Transform tempPoint;

    //public float RAYCAST_LENGTH = 1;
    void Start()
    {
        point = GameObject.Find("Point").transform;
        //tempPoint = GameObject.Find("Temp").transform;
        anchor = GameObject.Find("Anchors").transform;

    }

    private bool IsCollide()
    {

        //Vector2 pointLine = new Vector2(point.position.x, point.position.y) - axisPosition;

        int count = 0;

        for (int i = 0; i < anchor.childCount; i++)
        {
            var a = anchor.GetChild(i);
            var b = anchor.GetChild((i + 1) % anchor.childCount);

            var r = IsIntersection(a.position, b.position, point.position, axisPosition);

            if (r) count++;
        }
        print(count);
        //是奇数就碰撞到了
        return count % 2 == 1;
    }

    bool IsIntersection(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        var crossA = Mathf.Sign(Vector3.Cross(d - c, a - c).y);
        var crossB = Mathf.Sign(Vector3.Cross(d - c, b - c).y);

        if (Mathf.Approximately(crossA, crossB)) return false;

        var crossC = Mathf.Sign(Vector3.Cross(b - a, c - a).y);
        var crossD = Mathf.Sign(Vector3.Cross(b - a, d - a).y);

        if (Mathf.Approximately(crossC, crossD)) return false;

        return true;
    }

    private void OnDrawGizmos()
    {
        axisPosition = (anchor.GetChild(0).position + anchor.GetChild(1).position) / 2;

        if (!IsCollide())
            Gizmos.color = Color.white;
        else
            Gizmos.color = Color.red;

        int index = anchor.childCount - 1;
        for (int i = 0; i < index; i++)
        {
            Gizmos.DrawLine(anchor.GetChild(i).position, anchor.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(anchor.GetChild(0).position, anchor.GetChild(index).position);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(axisPosition, 0.1f);


        if (drawPointLine)
        {

            Gizmos.color = Color.green;
            Gizmos.DrawLine(axisPosition, point.position);
        }



    } 


}
