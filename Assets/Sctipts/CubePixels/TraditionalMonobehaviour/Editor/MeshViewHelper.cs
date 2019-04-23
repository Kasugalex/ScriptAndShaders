using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;

[CustomEditor(typeof(MeshViewer))]
public class MeshViewHelper : Editor
{

    private void OnSceneGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        MeshViewer viewer = target as MeshViewer;
        Dictionary<Vector3, StringBuilder> posList = new Dictionary<Vector3, StringBuilder>();

        for (int i = 0, max = viewer.verticesList.Count; i < max; i++)
        {
            Vector3 vPos = viewer.transform.TransformPoint(viewer.verticesList[i]);

            StringBuilder sb;
            if (posList.TryGetValue(vPos, out sb))
            {
                sb.AppendLine("index:" + i);
            }
            else
            {
                sb = new StringBuilder();
                sb.AppendLine("index" + i);
                posList.Add(vPos, sb);

            }

            Handles.Label(vPos, sb.ToString(), style);
        }
    }

}
