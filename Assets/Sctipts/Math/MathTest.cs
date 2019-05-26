using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kasug.Math;

public class MathTest : MonoBehaviour
{
    public Transform target;


    private void Start()
    {
        
    }

    private void Update()
    {
        RotateWithQmul();
    }
    [Range(0,0.99f)]
    public float speed = 1;
    private void RotateWithQmul()
    {
        Quaternion newRot = MathLibrary.Qmul(target.rotation, new Quaternion(0, speed, 0, 1));
        target.rotation = newRot;
    }

}


internal static class Extension
{
    public static Vector4 TransferVec4(this Quaternion self)
    {
        return new Vector4(self.x,self.y,self.z,self.w);
    }
}