using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Game.Building;
using IsometricVillageMob.RuntimeData;
using IsometricVillageMob.Services;
using UnityEngine;

namespace IsometricVillageMob.Game
{

    public class WorldCreator : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private IResourceLoadService _resourceLoadService;
        [Inject] private RuntimeContainer _runtimeContainer;
        [SerializeField] private SpawnPoint[] _spawnPointsThree;
        [SerializeField] private SpawnPoint[] _spawnPointsTwo;
        [SerializeField] private SpawnPoint[] _spawnPointsOne;

        private SpawnPoint[] GetPoints()
        {
            switch (_runtimeContainer.ResourceCountMode)
            {
                case ResourceCountMode.Two: return _spawnPointsTwo;
                case ResourceCountMode.Three: return _spawnPointsThree;
                default: return _spawnPointsOne;
            }
        }
        
        public void Create()
        {
            var points = GetPoints();
            foreach (var point in points)
            {
                //TODO: need used fabric 
                var prefab = _resourceLoadService.LoadBuilding(point.BuildingType);
                var newObj = Instantiate(prefab, transform);
                newObj.transform.position = point.transform.position;
                var bb = newObj.GetComponent<BaseBuilding>();
                _diContainer.Inject(bb);
                bb.Init();
            }
        }
    }
}
