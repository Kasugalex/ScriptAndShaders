using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kasug.Math
{
    public class MathLibrary : MonoBehaviour
    {
        public static Quaternion Qmul(Quaternion q1, Quaternion q2)
        {
            Vector3 Q1xyz = new Vector3(q1.x, q1.y, q1.z);
            Vector3 Q2xyz = new Vector3(q2.x, q2.y, q2.z);
            Vector3 xyz = Q1xyz * q2.w + Q2xyz * q1.w + Vector3.Cross(Q1xyz, Q2xyz);

            float w = q1.w * q2.w - Vector3.Dot(Q1xyz, Q2xyz);
            return new Quaternion(xyz.x, xyz.y, xyz.z, w);
        }

        public static Vector3 Rotate_Vector(Vector3 v,Quaternion r)
        {
            return new Vector3();
        }
    }
}