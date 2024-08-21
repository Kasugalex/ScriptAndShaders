#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
#if UNITY_2019_1_OR_NEWER
using UnityEngine.UIElements;
#else
using UnityEngine.Experimental.UIElements;
#endif

namespace DebugEditorTools
{

    [InitializeOnLoad]
    public static class ToolbarExtend
    {
        private static readonly Type kToolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static ScriptableObject sCurrentToolbar;
        private const string GAME_MODE_OPTION = "GAME_MODE_OPTION";

        static ToolbarExtend()
        {
            EditorApplication.update += OnUpdate;
        }

        private static void OnUpdate()
        {
            if (sCurrentToolbar == null)
            {
                UnityEngine.Object[] toolbars = Resources.FindObjectsOfTypeAll(kToolbarType);
                sCurrentToolbar = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
                if (sCurrentToolbar != null)
                {
                    FieldInfo root = sCurrentToolbar.GetType()
                        .GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
                    VisualElement concreteRoot = root.GetValue(sCurrentToolbar) as VisualElement;

                    VisualElement toolbarZone = concreteRoot.Q("ToolbarZoneRightAlign");
                    VisualElement parent = new VisualElement()
                    {
                        style =
                    {
                        flexGrow = 1,
                        flexDirection = FlexDirection.Row,
                    }
                    };
                    IMGUIContainer container = new IMGUIContainer();
                    container.onGUIHandler += OnGuiBody;
                    parent.Add(container);
                    toolbarZone.Add(parent);
                }
            }
        }

        private static void OnGuiBody()
        {
            //自定义按钮加在此处
            var curMode = EditorPrefs.GetBool(GAME_MODE_OPTION);
            // 图标样式参考 https://github.com/halak/unity-editor-icons
            var textureName = curMode == false ? "d_DebuggerDisabled" : "d_DebuggerAttached";
            var btnText = curMode == false ? "正常模式" : "调试模式";

            GUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent(btnText, EditorGUIUtility.FindTexture(textureName))))
            {
                Debug.Log(btnText);
            }

            GUILayout.EndHorizontal();
        }
    }
}
#endif
