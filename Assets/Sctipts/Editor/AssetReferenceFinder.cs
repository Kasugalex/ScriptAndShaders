using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.SceneManagement;

public class AssetReferenceFinder : EditorWindow
{
    private Vector2 _scrollPosition;
    private Object[] searchTargets;

    private List<bool> groupStates = new List<bool>();

    private Dictionary<Object, List<ReferenceObject>> _referenceObjects = new Dictionary<Object, List<ReferenceObject>>();

    /// <summary>
    /// 竖直对齐ObjectFiled
    /// </summary>
    private float verticalAlignment = 148;

    [MenuItem("Assets/Show References")]
    private static void Init()
    {
        var window = (AssetReferenceFinder)GetWindow(typeof(AssetReferenceFinder), false, "Asset Reference Finder");
        window.minSize = new Vector2(600, 600);
        window.Refresh();
    }

    public void Refresh()
    {
        searchTargets = Selection.objects;

        if (searchTargets.Length > 0)
        {
            Search();
        }

        Selection.objects = null;
    }

    private void OnGUI()
    {
        DisplayReferenceObject();
    }

    private void Search()
    {
        foreach (var asset in searchTargets)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            //Debug.LogFormat ($"<color=#{ColorUtility.ToHtmlStringRGB(Color.yellow)}>开始搜索文件引用: {assetPath}</color>");

            // 找到所有引用此资产的资源
            string[] allAssets = AssetDatabase.FindAssets("", null);
            List<Object> references = new List<Object>();

            foreach (var guid in allAssets)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var dependency = AssetDatabase.LoadAssetAtPath<Object>(path);

                // 检查依赖项
                if (dependency != null && path != assetPath)
                {
                    var dependencies = EditorUtility.CollectDependencies(new Object[] { dependency });

                    foreach (var dep in dependencies)
                    {
                        if (dep == asset)
                        {
                            references.Add(dependency);
                            break;
                        }
                    }
                }
            }

            // 输出引用路径
            if (references.Count > 0)
            {
                if (!_referenceObjects.ContainsKey(asset))
                {
                    _referenceObjects.Add(asset, new List<ReferenceObject>());

                    foreach (var item in references)
                    {
                        var path = AssetDatabase.GetAssetPath(item);
                        _referenceObjects[asset].Add(new ReferenceObject(item, path, asset));
                    }
                }

            }
            else
            {
                // Util_Debug.LogRandomColor($"No references found for {assetPath}.");
                _referenceObjects[asset] = new List<ReferenceObject>();
            }
        }
    }

    private void DisplayReferenceObject()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        int groupIndex = 0;
        Object removeObjKey = null;
        foreach (var referenceObject in _referenceObjects)
        {
            var target = referenceObject.Key;
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical("box");

            if (groupStates.Count <= groupIndex)
                groupStates.Add(true);

            var showTip = groupStates[groupIndex] ? $"折叠  {target.name}" : $"展开   {target.name}";
            groupStates[groupIndex] = EditorGUILayout.Foldout(groupStates[groupIndex], showTip, true);

            var referenceObjects = referenceObject.Value;
            if (groupStates[groupIndex])
            {
                DisplayReferenceObject(target, "当前搜索：");

                if (referenceObjects.Count > 0)
                {
                    EditorGUILayout.Space(5);
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(verticalAlignment));
                    EditorGUILayout.LabelField("查询到以下引用：");
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(5);
                    foreach (var item in referenceObjects)
                    {
                        DisplayReferenceObject(item.Target);
                        if (item.gameObjects != null && item.Target is GameObject prefab)
                        {
                            foreach (var obj in item.gameObjects)
                            {
                                if (obj == null) continue;
                                EditorGUILayout.BeginHorizontal();
                                // 148对齐
                                EditorGUILayout.LabelField("", GUILayout.Width(verticalAlignment));
                                var objNodeName = GetRelativePath(obj, obj.name);
                                if (EditorGUILayout.LinkButton(objNodeName))
                                {
                                    // 进入预制体编辑模式
                                    GameObject instanceRoot = null;
#if UNITY_2022_3_OR_NEWER
                                    instanceRoot = UnityEditor.SceneManagement.PrefabStageUtility.OpenPrefab(item.Path).prefabContentsRoot;
#else
                                    AssetDatabase.OpenAsset(item.Target);
                                    var curStage = PrefabStageUtility.GetCurrentPrefabStage();
                                    if (curStage != null)
                                    {
                                        instanceRoot = curStage.prefabContentsRoot;
                                    }
                                    else
                                    {
                                        Debug.Log("null stage");
                                    }
#endif
                                    if (instanceRoot != null)
                                    {
                                        var rootTrans = instanceRoot.transform;
                                        var targetTrans = rootTrans.Find(objNodeName);
                                        if (targetTrans == null)
                                            targetTrans = rootTrans;
                                        Selection.activeTransform = targetTrans;
                                        SceneView.lastActiveSceneView.FrameSelected();
                                        EditorGUIUtility.PingObject(targetTrans);
                                    }
                                }
                                EditorGUILayout.EndHorizontal();
                                EditorGUILayout.Space(5);
                            }
                        }
                        EditorGUILayout.Space(5);
                    }
                }
                else
                {
                    DisplayReferenceObject(null, "未找到引用");
                }

                if (target is MonoScript)
                {
                    if (GUILayout.Button("一键移除脚本(使用撤回快捷键撤回)"))
                    {
                        foreach (var item in referenceObjects)
                        {
                            if (item.gameObjects != null)
                            {
                                foreach (var obj in item.gameObjects)
                                {
                                    if (obj == null) continue;
                                    Undo.DestroyObjectImmediate(obj);
                                }
                            }
                        }
                        AssetDatabase.SaveAssets();
                    }
                }
            }
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("移除显示", GUILayout.Width(100)))
            {
                // 刷新list状态
                for (int i = groupIndex; i < groupStates.Count - 1; i++)
                {
                    groupStates[i] = groupStates[i + 1];
                }
                if (groupStates.Count > 1)
                    groupStates[groupStates.Count - 1] = groupStates[groupStates.Count - 2];

                removeObjKey = target;
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
            groupIndex++;
        }

        if (removeObjKey != null)
            _referenceObjects.Remove(removeObjKey);

        if (GUILayout.Button("刷新引用"))
        {
            List<Object> objects = new List<Object>(_referenceObjects.Count);
            foreach (var item in _referenceObjects)
            {
                objects.Add(item.Key);
            }
            _referenceObjects.Clear();
            Selection.objects = objects.ToArray();
            Refresh();
        }

        EditorGUILayout.EndScrollView();
    }

    private void DisplayReferenceObject(Object target, string title = null)
    {
        EditorGUI.BeginDisabledGroup(true);

        if (target == null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(verticalAlignment));
            EditorGUILayout.LabelField(title == null ? "Asset does not exist" : title);
            EditorGUILayout.EndHorizontal();

        }
        else
        {
            var content = new GUIContent(title);
            EditorGUILayout.ObjectField(content, target, typeof(Object), false);
        }

        EditorGUI.EndDisabledGroup();
    }

    public static string GetRelativePath(Object target, string path)
    {
        if (target is Component)
        {
            var trans = (target as Component).transform;

            if (trans.parent != null)
            {
                if (trans.parent.name != trans.root.name)
                    path = string.Format("{0}/{1}", trans.parent.name, path);
                return GetRelativePath(trans.parent, path);
            }
        }
        return path;
    }

    public void OnInspectorUpdate()
    {
        this.Repaint();
    }

    private class ReferenceObject
    {
        #region Fields

        public readonly string Name;
        public readonly Object Target;
        public readonly string Path;
        public Component[] gameObjects { get; private set; }

        #endregion

        #region Constructors

        public ReferenceObject(Object target, string path, Object asset)
        {
            this.Name = target.name;
            this.Target = target;
            this.Path = path;

            // 判断搜索类型
            if (target is GameObject)
            {
                var obj = target as GameObject;

                if (asset is MonoScript)
                {
                    OnProcessMonoScript(obj, asset);
                }
                else if (asset is Texture)
                {
                    OnProcessTexture(obj, asset);
                }
                else if (asset is Material)
                {
                    OnProcessMaterial(obj, asset);
                }
                else if (asset is Shader)
                {
                    OnProcessShader(obj, asset);
                }
                else if (asset is GameObject)
                {
                    OnProcessGameObject(obj, asset);
                }
            }
        }

        private void OnProcessMonoScript(GameObject obj,Object asset)
        {
            var monoScript = asset as MonoScript;
            var classType = monoScript.GetClass();
            if (typeof(MonoBehaviour).IsAssignableFrom(classType))
            {
                var components = obj.GetComponentsInChildren(classType, true);
                if (components.Length > 0)
                {
                    gameObjects = components;
                }
            }
        }

        private void OnProcessTexture(GameObject obj, Object asset)
        {
            var components = obj.GetComponentsInChildren<Renderer>(true);
            List<Component> allComs = new List<Component>();
            if (components.Length > 0)
            {
                string[] texturePropertyNames = {
                            "_MainTex",
                            "_Bump",         // 法线贴图
                            "_MetallicGlossMap", // 金属光泽贴图
                            "_SpecGlossMap", // 反射光泽贴图
                            "_DetailAlbedoMap", // 细节贴图
                            "_DetailNormalMap", // 细节法线贴图
                            "_EmissionMap",  // 自发光贴图
                            // 添加其他贴图属性名
                        };
                foreach (var com in components)
                {
                    if (com.sharedMaterials != null)
                    {
                        foreach (var mat in com.sharedMaterials)
                        {
                            if (mat != null && mat.mainTexture != null && mat.mainTexture.name == asset.name)
                            {
                                allComs.Add(com);
                                // 避免重复添加 break
                                break;
                            }

                            foreach (var pName in texturePropertyNames)
                            {
                                if (mat != null && mat.HasProperty(pName) && mat.GetTexture(pName) == asset)
                                {
                                    allComs.Add(com);
                                    break;
                                }
                            }
                        }
                    }
                }

            }

            var imgs = obj.GetComponentsInChildren<UnityEngine.UI.Image>(true);
            foreach (var com in imgs)
            {
                if (com.sprite == asset)
                {
                    allComs.Add(com);
                }
            }

            var rawImgs = obj.GetComponentsInChildren<UnityEngine.UI.RawImage>(true);
            foreach (var com in imgs)
            {
                if (com.mainTexture == asset)
                {
                    allComs.Add(com);
                }
            }

            gameObjects = allComs.ToArray();
        }

        private void OnProcessMaterial(GameObject obj, Object asset)
        {
            var components = obj.GetComponentsInChildren<Renderer>(true);
            if (components.Length > 0)
            {
                List<Component> allComs = new List<Component>();
                foreach (var com in components)
                {
                    if (com.sharedMaterials != null)
                    {
                        foreach (var mat in com.sharedMaterials)
                        {
                            if (mat == asset)
                            {
                                allComs.Add(com);
                                // 避免重复添加 break
                                break;
                            }
                        }
                    }
                }
                gameObjects = allComs.ToArray();
            }
        }

        private void OnProcessShader(GameObject obj, Object asset)
        {
            var components = obj.GetComponentsInChildren<Renderer>(true);
            if (components.Length > 0)
            {
                List<Component> allComs = new List<Component>();
                foreach (var com in components)
                {
                    if (com.sharedMaterials != null)
                    {
                        foreach (var mat in com.sharedMaterials)
                        {
                            if (mat != null && mat.shader == asset)
                            {
                                allComs.Add(com);
                                // 避免重复添加 break
                                break;
                            }
                        }
                    }
                }
                gameObjects = allComs.ToArray();
            }
        }

        private void OnProcessGameObject(GameObject obj, Object asset)
        {
            List<Component> allComs = new List<Component>();
            var curAssetPath = AssetDatabase.GetAssetPath(asset);
            var allChilds = obj.transform.GetComponentsInChildren<Transform>(true);
            foreach (var item in allChilds)
            {
                if (PrefabUtility.IsAnyPrefabInstanceRoot(item.gameObject))
                {
                    Transform originAsset = PrefabUtility.GetCorrespondingObjectFromOriginalSource(item);
                    string prefabPath = AssetDatabase.GetAssetPath(originAsset);
                    if (prefabPath == curAssetPath)
                    {
                        allComs.Add(item);
                    }
                }
            }
            gameObjects = allComs.ToArray();
        }

        #endregion
    }
}
