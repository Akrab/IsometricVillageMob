using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Game.Building;
using IsometricVillageMob.Services;
using UnityEngine;

namespace IsometricVillageMob.Game
{

    public class WorldCreator : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private IResourceService _resourceService;
        [SerializeField] private SpawnPoint[] _spawnPoints;
        
        public void Create()
        {
            foreach (var point in _spawnPoints)
            {
                //TODO: need used fabric 
                var prefab = _resourceService.LoadBuilding(point.BuildingType);
                var newObj = Instantiate(prefab, transform);
                newObj.transform.position = point.transform.position;
                var bb = newObj.GetComponent<BaseBuilding>();
                _diContainer.Inject(bb);
                bb.Init();
            }
        }
    }
}
