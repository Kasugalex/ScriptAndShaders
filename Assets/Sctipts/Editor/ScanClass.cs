using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
public class ScanClass
{
    private static Dictionary<string, int> scanedDic = new Dictionary<string, int>();
    private static string copyStr = "";
    private static int count = 0;
    [MenuItem("Tools/ScanUnityEngineCode")]
    public static void Scaner()
    {
        scanedDic.Clear();
        copyStr = "";
        count = 0;
        foreach (var curType in AotClass.scanNames)
        {
            var typeInfo = curType.GetTypeInfo();

            foreach (var type in typeInfo.DeclaredProperties)
            {
                var obsolete = type.GetCustomAttribute<ObsoleteAttribute>();
                if (obsolete != null)
                {
                    continue;
                }
                AddToDic(type.PropertyType);
            }

            foreach (var type in typeInfo.DeclaredMethods)
            {
                var obsolete = type.GetCustomAttribute<ObsoleteAttribute>();
                if (obsolete != null)
                {
                    continue;
                }
                if (type.IsGenericMethod) continue;
                AddToDic(type.ReturnType);

            }

            //GUIUtility.systemCopyBuffer = copyStr;
        }

        var scriptPath = Path.Combine(Application.dataPath, "Scripts/AotClass.cs");
        if(File.Exists(scriptPath))
        {
            using(StreamReader fs = new StreamReader(scriptPath))
            {
                var txt = fs.ReadToEnd();
                var startIndex = txt.IndexOf("// PLACE_START");
                var endIndex = txt.IndexOf("// PLACE_END");

                var startStr = txt.Substring(0, startIndex - 1);
                var endStr = txt.Substring(endIndex, txt.Length - endIndex);
                fs.Dispose();
                fs.Close();
                using (StreamWriter sw = new StreamWriter(scriptPath))
                {
                    sw.Write("");
                    sw.Write($"{startStr}\t// PLACE_START{copyStr}\n\t\t{endStr}");
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    private static void AddToDic(Type addType)
    {
        // 排除 非public、基元类型、泛型、指针、Unity.Collections
        if (addType == null ||
            addType.IsNotPublic ||
            addType.IsNestedPrivate ||
            !addType.IsVisible ||
            addType == typeof(void) ||
            string.IsNullOrEmpty(addType.FullName) ||
            addType.IsPrimitive ||
            addType.IsGenericType ||
            addType.IsGenericTypeDefinition || 
            addType.IsPointer)
            return;

        var obsolete = addType.GetCustomAttribute<ObsoleteAttribute>();
        if (obsolete != null)
        {
            return;
        }

        var addName = addType.FullName;
        if (addType.Namespace.IndexOf("Unity.Collections") >= 0) return;

        if (addType.IsArray)
            addName = addName.Replace("[]", "");

        if (addType.IsNested)
            addName = addName.Replace('+', '.');

        if (scanedDic.TryGetValue(addName, out _)) 
            return;

        //Debug.Log($"{addName} {addType.Namespace.IndexOf("Unity.Collections")}");

        scanedDic.Add(addName, 1);
        copyStr += $"\n\t\t{addName} temp{count++};";        
    }
}