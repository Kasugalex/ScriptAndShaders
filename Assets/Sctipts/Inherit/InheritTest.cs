using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritTest : MonoBehaviour
{

    void Start()
    {
        KInherit1 k1 = new KInherit1(new GameObject());
        k1.t = 1;
        //报错
        //AddComponent<KInherit2, GameObject>(k1);

        KInherit2 k2 = new KInherit2();
        k2.t = 2;
        AddComponent2<KInherit2, GameObject>(k2);
    }

    //new() 约束可以让编译器知道：提供的任何类型参数都必须具有可访问的无参数（或默认）构造函数。例如：
    public K AddComponent<K,P>(K k1) where K : KInherit1, new()
    {
        print("123" + "|" + k1.t);
        return null;
    }

    public K AddComponent2<K, P>(K k1) where K : KInherit2, new()
    {
        print("123" + "|" + k1.t);
        return null;
    }
}

public class KInherit1
{
    public int t = 10;
    public KInherit1(GameObject g)
    {

    }
}


public class KInherit2
{
    public int t = 10;

}