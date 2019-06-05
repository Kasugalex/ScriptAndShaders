using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GlobalTest : MonoBehaviour
{
    public Material m;
    List<GameObject> allObjects = new List<GameObject>();
    public Transform target;
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            target.position = Vector3.Slerp(target.position, new Vector3(100, 0, 0),0.01f);
        }
    }

    void EqualTest()
    {
        string a = "123";
        string b = "123";

        print($"{a == b}");
        print($"{a.ToCharArray() == b.ToCharArray()}");
    }

    void FindWithID()
    {
        UnityEngine.Object g = FindObjectFromInstanceID(-2724);
        MeshRenderer mr = g as MeshRenderer;
        print(mr.material.name);
    }

    public static UnityEngine.Object FindObjectFromInstanceID(int iid)
    {
        return (UnityEngine.Object)typeof(UnityEngine.Object).GetMethod("FindObjectFromInstanceID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).Invoke(null, new object[] { iid });

    }
}

public static class GetComponentExtension
{
    public static T GetComponentWithName<T>(this GameObject self, string sourceName)
    {
        return GameObject.Find(sourceName).GetComponent<T>();
    }
}
