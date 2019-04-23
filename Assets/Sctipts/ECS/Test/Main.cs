using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    private RootSystem s;
	// Use this for initialization
	void Start () {
        s = new RootSystem(Contexts.sharedInstance);
        s.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        s.Execute();
	}
}
