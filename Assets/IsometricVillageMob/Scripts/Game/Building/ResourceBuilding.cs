using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.RuntimeData;

namespace IsometricVillageMob.Game.Building
{
    public interface IResourceBuilding
    {
        void StartBuild();
        void StopBuild();
        void SetResource(ResourceType resourceType);
        
        bool IsRun { get; }
    }
    
    
    public class ResourceBuilding : BaseBuilding, IResourceBuilding
    {
        [Inject] private ITimerController _timerController;
        [Inject] private IPlayerInventory _playerInventory;

        private ResourceBuildingModel _resourceBuildingModel = new ResourceBuildingModel();
        public override BuildingType BuildingType => BuildingType.Resource;

        public bool IsRun { get; private set; } = false;
        public float Duration = 3f;

        private Timer _timer;
        public override void Init()
        {
            _timer = new Timer(TimerTick, TimerCompleted);
            _timerController.AddTimer(_timer);
        }

        private void TimerTick(float value)
        {
            
        } 

        private void TimerCompleted()
        {
            CreateResource();
        }
        public void StartBuild()
        {
            _timer.PlayLoop(Duration);
        }

        public void StopBuild()
        {
            _timer.Reset();
        }

        public void SetResource(ResourceType resourceType)
        {
            _resourceBuildingModel.ResourceType = resourceType;
        }

        private void CreateResource()
        {
            _playerInventory.AddResource( _resourceBuildingModel.ResourceType);
        }

    }
}
