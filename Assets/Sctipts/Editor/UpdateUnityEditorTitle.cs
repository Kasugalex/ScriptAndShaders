using UnityEditor;

namespace NF
{
#if UNITY_EDITOR_WIN

    [InitializeOnLoad]
    class UpdateUnityEditorTitle
    {
        private static bool isInGame = false;

        [System.Obsolete]
        static UpdateUnityEditorTitle()
        {
            EditorApplication.delayCall += DoUpdateTitleFunc;

            EditorApplication.hierarchyChanged += DoUpdateTitleFunc;

            EditorApplication.playmodeStateChanged += OnPlaymodeStateChanged;
        }

        static void OnPlaymodeStateChanged()
        {
            if (EditorApplication.isPlaying == isInGame) return;
            isInGame = EditorApplication.isPlaying;
            UpdateUnityEditorProcess.lasttime = 0;
            DoUpdateTitleFunc();
        }

        static void DoUpdateTitleFunc()
        {
            UpdateUnityEditorProcess.lasttime = 0;
            UpdateUnityEditorProcess.Instance.SetTitle();
        }
    }
#endif
}