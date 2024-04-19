using UnityEngine;

namespace IsometricVillageMob.Game.Building
{
    public abstract class BaseBuilding : MonoBehaviour, IBuilding
    {
        public virtual BuildingType BuildingType { get; }
        public abstract void Init();
    }
}
