using System;
using UnityEngine;

namespace IsometricVillageMob.DIIsometric
{
    public interface ISceneContext
    {
        void Install();
    }
    public class SceneContext : MonoBehaviour, ISceneContext
    {
        [Inject] 
        private DiContainer _diContainer;
        [SerializeField]
        private MonoInstaller[] _monoInstallers;
        public void Install()
        {
            foreach (var installer in _monoInstallers)
            {
                _diContainer.BindInstance(installer);
                // IsometricInjector.
                 installer.InstallBindings();
            }

        }
    }
}