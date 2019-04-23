using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
[My("456")]
public class AttributesTest : MonoBehaviour
{
   

    private void Start()
    {
        ATest();
    }

    public void ATest()
    {

        Type type = GetType();
        MyAttribute attribute = type.GetCustomAttribute<MyAttribute>(false);

        string n = attribute.AttributeName;

        print(n);

    }

}
public class MyAttribute : Attribute
{

    public string AttributeName { get; private set; }

    public MyAttribute(string _name)
    {
        this.AttributeName = _name;
    }
}


