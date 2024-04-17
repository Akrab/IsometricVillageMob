using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace IsometricVillageMob.DIIsometric
{
    [DefaultExecutionOrder(-1)]
    public class ProjectDIInitialize: MonoBehaviour
    {
        private DiContainer _diContainer = new DiContainer();
        
        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
// #if UNITY_EDITOR
//             Debug.LogError($"Scene Loaded {scene.name}, mode {mode}");
// #endif
            InitSceneContext(scene);
        }

        private void InitSceneContext(Scene scene)
        {
            using var pooledObject1 = ListPool<GameObject>.Get(out var rootGameObjects);
            scene.GetRootGameObjects(rootGameObjects);

            foreach (var item in rootGameObjects)
            {
                var sceneContext = item.GetComponent<ISceneContext>();
                if (sceneContext ==  null) continue;

                _diContainer.BindInstance(sceneContext);
                sceneContext.Install();
            }
        }
    }
}