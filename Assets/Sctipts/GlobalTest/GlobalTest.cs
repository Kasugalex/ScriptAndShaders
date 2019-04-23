using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class GlobalTest : MonoBehaviour
{
    public Material m;
    List<GameObject> allObjects = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        // EqualTest();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AndorOr()
    {
        int i = 10;
        print(i & 1);
        print(i | 0);
        print(i & 3);
    }

    void EqualTest()
    {
        string a = "123";
        string b = "123";

        print($"{a == b}");
        print($"{a.ToCharArray() == b.ToCharArray()}");
    }
}
