using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsometricVillageMob.Game
{

    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private BuildingType _buildingType;
        public BuildingType BuildingType => _buildingType;
    }
}
