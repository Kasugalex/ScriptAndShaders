using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationGrid : MonoBehaviour {
    public Transform prefab;

    [Range (2, 10)]
    public int gridResolution = 10;

    private Transform[] grid;
    private List<Transformation> transformations;

    private Transform _Trans;
    private void Awake () {
        _Trans = transform;
        grid = new Transform[gridResolution * gridResolution * gridResolution];
        for (int i = 0, z = 0; z < gridResolution; z++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid[i] = CreateGridPoint (x, y, z);
                }
            }
        }

        transformations = new List<Transformation> ();
    }

    private void Update () {
        GetComponents<Transformation> (transformations);
        for (int i = 0, z = 0; z < gridResolution; z++) {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid[i].localPosition = TransformPoint (x, y, z);
                }
            }
        }
    }

    private Transform CreateGridPoint (int x, int y, int z) {
        Transform point = Instantiate<Transform> (prefab);
        point.localPosition = GetCoordinates (x, y, z);
        point.GetComponent<MeshRenderer> ().material.color = new Color (
            (float) x / gridResolution,
            (float) y / gridResolution,
            (float) z / gridResolution
        );
        point.parent = _Trans;
        return point;
    }

    private Vector3 GetCoordinates (int x, int y, int z) {
        return new Vector3 (
            x - (gridResolution - 1) * 0.5f,
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }

    private Vector3 TransformPoint (int x, int y, int z) {
        Vector3 coordinates = GetCoordinates (x, y, z);
        for (int i = 0; i < transformations.Count; i++) {
            coordinates = transformations[i].Apply (coordinates);
        }
        return coordinates;
    }
}