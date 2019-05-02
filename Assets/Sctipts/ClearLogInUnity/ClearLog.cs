using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClearLog
{
    public static void Clear()
    {

        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));

        Type logEntries = assembly.GetType("UnityEditor.LogEntries");

        var method = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

        if (method != null)

        {

            method.Invoke(null, null);

            GUIUtility.keyboardControl = 0;

        }

    }
}
