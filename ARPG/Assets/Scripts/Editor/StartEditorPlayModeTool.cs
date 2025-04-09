using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class StartEditorPlayModeTool
{
    [MenuItem("Tools/Editor Play Mode")]
    public static void StartEditorPlayMode()
    {
        Scene scene = EditorSceneManager.OpenScene("Assets/Resources/Scenes/StartScene.unity");
        if (scene == null)
        {
            Debug.LogError("StartScene ¾øÀ½");
            return;
        }

        EditorApplication.EnterPlaymode();
    }
}
