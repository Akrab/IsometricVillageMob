using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace IsometricVillageMob.Editor
{
    public class EditorCommands : MonoBehaviour
    {
        [MenuItem("IsometricVillageMob/Play")]
        private static void Play()
        {
            EditorSceneManager.OpenScene("Assets/IsometricVillageMob/Scenes/Lobby.unity");
            EditorApplication.isPlaying = true;
        }

        [MenuItem("IsometricVillageMob/Load Lobby")]
        private static void LoadLobby()
        {
            EditorSceneManager.OpenScene("Assets/IsometricVillageMob/Scenes/Lobby.unity");
            EditorApplication.isPlaying = false;
        }

        [MenuItem("IsometricVillageMob/Load UI")]
        private static void LoadUI()
        {
            EditorSceneManager.OpenScene("Assets/IsometricVillageMob/Scenes/UI.unity");
            EditorApplication.isPlaying = false;
        }
        
        [MenuItem("IsometricVillageMob/Load Game")]
        private static void LoadGame()
        {
            EditorSceneManager.OpenScene("Assets/IsometricVillageMob/Scenes/Game.unity");
            EditorApplication.isPlaying = false;
        }
    }
}
