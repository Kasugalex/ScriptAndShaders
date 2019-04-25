using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOctree : MonoBehaviour {
    BoundsOctree<GameObject> boundsTree;

    public Transform container;

    private List<GameObject> objList;
    void Start()
    {

      

    }

    void Update()
    {
        boundsTree = new BoundsOctree<GameObject>(15, container.position, 1, 1.25f);
        foreach (Transform item in container)
        {
            boundsTree.Add(item.gameObject, new Bounds(item.position, Vector3.one));
        }

    }


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        boundsTree.DrawAllBounds();
        boundsTree.DrawAllObjects();

        boundsTree.DrawCollisionChecks();
    }

#endif

}
