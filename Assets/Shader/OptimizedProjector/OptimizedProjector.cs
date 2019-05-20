using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizedProjector : MonoBehaviour
{
    private Transform trans;
    private Material currentMat;
    void Start()
    {
        trans = transform;
        currentMat = trans.GetComponent<Projector>().material;
    }
    [SerializeField]
    private Vector3 lastPosition = Vector3.zero;
    void Update()
    {
        if(trans.position != lastPosition)
        {
            lastPosition = trans.position;
            currentMat.SetVector("_ProjectorWorldPos", lastPosition);

        }
    }
}
