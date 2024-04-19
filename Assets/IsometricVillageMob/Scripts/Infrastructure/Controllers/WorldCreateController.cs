
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Game;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace IsometricVillageMob.Infrastructure.Controllers
{
    public class WorldCreateController
    {
        [Inject] private DiContainer _diContainer;
        
        public WorldCreateController()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != CONSTANTS.GAME_SCENE) return;
            
            using var pooledObject1 = ListPool<GameObject>.Get(out var rootGameObjects);
            scene.GetRootGameObjects(rootGameObjects);


            foreach (var obj in rootGameObjects)
            {
                var worldCreator = obj.GetComponent<WorldCreator>();
                if (worldCreator != null)
                {
                    InitWorldCreator(worldCreator);
                    return;
                }
            }
        }

        private void InitWorldCreator(WorldCreator worldCreator)
        {
            _diContainer.Inject(worldCreator);
            worldCreator.Create();
        }
    }
}