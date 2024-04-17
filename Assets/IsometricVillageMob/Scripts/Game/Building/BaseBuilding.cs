using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsometricVillageMob.Game.Building
{
    public class BaseBuilding : MonoBehaviour, IBuilding
    {
        public virtual BuildingType BuildingType { get; }
    }
}
