using System;
using IsometricVillageMob.Game;
using UnityEngine;

namespace IsometricVillageMob.Services
{
    public interface IResourceLoadService
    {
        GameObject LoadBuilding(BuildingType buildingType);
    }
    
    public class ResourceLoadSrv : IResourceLoadService
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