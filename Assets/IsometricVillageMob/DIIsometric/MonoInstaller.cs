using UnityEngine;

namespace IsometricVillageMob.DIIsometric
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        [Inject] protected readonly DiContainer _diContainer;
        public virtual void InstallBindings()
        {
            
        }
    }
}