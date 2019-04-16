using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class IEnumeratorTest : MonoBehaviour {

    private float waitTime = 3;
    private delegate void DoEventHandler();
    public Vector3 pos = new Vector3(2,0,0);
	// Use this for initialization
	void Start () {
        //StartCoroutine("wait");
        //Wait(()=> { print("执行事件"); });
        EqualTest();
    }

    void EqualTest()
    {
        IEnumeratorTest t = GetComponent<IEnumeratorTest>();
        t.transform.position = t.pos;
    }

    async void Wait(DoEventHandler endEvent = null)
    {
        if (endEvent == null)
        {
            print("终止");
            return;
        }

        endEvent();
        print("开始计时");
        await Task.Delay(TimeSpan.FromSeconds(waitTime));
        print("结束");
        transform.position += new Vector3(0, 0, 1);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine("wait");
            print("结束计时");
        }
	}

    IEnumerator wait()
    {
        float timer = 0;

        while(timer < waitTime)
        {
            timer += Time.deltaTime;
            print("计时中---");
            yield return null;
        }

        print("计时完成");
    }
}
