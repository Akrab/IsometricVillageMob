using System;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.Services
{
    public interface IResourceService
    {
        GameObject LoadBuilding(BuildingType buildingType);
    }
    
    public class ResourceSrv : IResourceService
    {
        public GameObject LoadBuilding(BuildingType buildingType)
        {
            return LoadPrefab<GameObject>($"Buildings/{Enum.GetName(typeof(BuildingType), buildingType)}");
        }

        private T LoadPrefab<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(path);
        }
    }
}