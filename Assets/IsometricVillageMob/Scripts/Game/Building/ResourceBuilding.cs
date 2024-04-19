using System;
using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.RuntimeData;
using UnityEngine.Events;

namespace IsometricVillageMob.Game.Building
{
    public interface IResourceBuilding
    {
        void StartBuild();
        void StopBuild();
        void SetResource(ResourceType resourceType);

        void AddTimerListener(UnityAction<float> action);
        void DelTimerListener();
        bool IsRun { get; }
        ResourceViewData GetViewData();
        void NextResource();
    }
    
    
    public class ResourceBuilding : BaseBuilding, IResourceBuilding
    {
        [Inject] private ITimerController _timerController;
        [Inject] private IPlayerInventory _playerInventory;

        private ResourceBuildingModel _resourceBuildingModel = new ResourceBuildingModel();
        
        private ResourceViewData _resourceViewData = new ResourceViewData();
        public override BuildingType BuildingType => BuildingType.Resource;

        private UnityAction<float> _uiListener;

        public bool IsRun { get; private set; } = false;
        public ResourceViewData GetViewData()
        {
            return _resourceViewData;
        }

        public void NextResource()
        {
            var rts = (ResourceType[])Enum.GetValues(typeof(ResourceType));

            var index = Array.IndexOf(rts, _resourceBuildingModel.ResourceType);

            index++;
            if (index >= rts.Length)
            {
                index = 1;
            }

            _resourceBuildingModel.ResourceType = rts[index];

        }

        public float Duration = 3f;

        private Timer _timer;
        public override void Init()
        {
            _timer = new Timer(TimerTick, TimerCompleted);
            _timerController.AddTimer(_timer);
            _resourceViewData.Duration = Duration;
        }

        private void TimerTick(float value)
        {
            _uiListener?.Invoke(value);
        } 

        private void TimerCompleted()
        {
            CreateResource();
        }
        public void StartBuild()
        {
            if (_resourceBuildingModel.ResourceType == ResourceType.None)
                return;
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

        public void AddTimerListener(UnityAction<float> action)
        {
            _uiListener = action;
        }
        
        public void DelTimerListener()
        {
            _uiListener = null;
        }

        private void CreateResource()
        {
            _playerInventory.AddResource( _resourceBuildingModel.ResourceType);
        }

    }
}
