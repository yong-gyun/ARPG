using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class StartEditorPlayModeTool
{
    [MenuItem("Tools/Editor Play Mode (StartScene)")]
    public static void StartEditorPlayModeToStartScene()
    {
        Scene scene = EditorSceneManager.OpenScene("Assets/Resources/Scenes/StartScene.unity");
        if (scene == null)
        {
            Debug.LogError("StartScene 없음");
            return;
        }

        EditorApplication.EnterPlaymode();
    }

    [MenuItem("Tools/Editor Play Mode (DemoScene)")]
    public static void StartEditorPlayModeToDemoScene()
    {
        Scene scene = EditorSceneManager.OpenScene("Assets/Scenes/DemoScene.unity");
        if (scene == null)
        {
            Debug.LogError("DemoScene 없음");
            return;
        }

        EditorApplication.EnterPlaymode();
    }
}
