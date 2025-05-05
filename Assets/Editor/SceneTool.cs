using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneTool : MonoBehaviour
{
    [MenuItem("Scenes/Play First Scene", priority = 0)]
    public static void PlayFirstScene()
    {
        // ���ɍĐ����̏ꍇ�͉������Ȃ�.
        if (EditorApplication.isPlaying)
        {
            Debug.LogWarning("Editor is Playing.");
            return;
        }

        // Build Settings�ɃV�[�����o�^����Ă��Ȃ��ꍇ�͉������Ȃ�.
        if (EditorBuildSettings.scenes.Length <= 0)
        {
            Debug.LogWarning("Not Exist Scene.");
            return;
        }

        // ���݂̃V�[�����ҏW���̏ꍇ�A�ۑ�/��ۑ��𕷂�.�L�����Z���̏ꍇ�͂Ȃɂ����Ȃ�.
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            return;
        }

        // �ŏ��̃V�[�����J��.
        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
        // Unity Editor���Đ�������.
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Scenes/Lobby", priority = 11)]
    public static void OpenTitleScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/LobbyScene.unity", OpenSceneMode.Single);
    }

    [MenuItem("Scenes/Game", priority = 12)]
    public static void OpenGameScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/MainScene.unity", OpenSceneMode.Single);
    }
}
