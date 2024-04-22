using System;
using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.RuntimeData;
using IsometricVillageMob.Services;
using UnityEngine.Events;

namespace IsometricVillageMob.Game.Building
{
    public interface IResourceBuilding
    {
        IResourceBuildingViewModel ViewModel { get; }
        bool IsRun { get; }

        void StartBuild();
        void StopBuild();
        void AddTimerListener(UnityAction<float> action);
        void DelTimerListener();
        void NextResource();
    }


    public class ResourceBuilding : BaseBuilding, IResourceBuilding
    {
        [Inject] private ITimerController _timerController;
        [Inject] private IPlayerInventory _playerInventory;
        [Inject] private IResourceService _resourceService;

        private ResourceBuildingModel _resourceBuildingModel = new ResourceBuildingModel();
        private UnityAction<float> _uiListener;
        private Timer _timer;
        public override BuildingType BuildingType => BuildingType.Resource;
        public bool IsRun { get; private set; } = false;
        public IResourceBuildingViewModel ViewModel => _resourceBuildingModel;

        private void OnDestroy()
        {
            _playerInventory.SubListener(this);
        }
        
        private void CreateResource()
        {
            _timerController?.RemoveTimer(_timer);
            _playerInventory?.AddResource(_resourceBuildingModel.CurrentResource);
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
            if (_resourceBuildingModel.CurrentResource == ResourceType.None)
                return;
            _timer.PlayLoop(_resourceService.Get(_resourceBuildingModel.CurrentResource).Duration);
            IsRun = true;
        }

        public void StopBuild()
        {
            _timer.Reset();
            IsRun = false;
        }

        public void AddTimerListener(UnityAction<float> action)
        {
            _uiListener = action;
        }

        public void DelTimerListener()
        {
            _uiListener = null;
        }

        public void NextResource()
        {
            _resourceBuildingModel.ResourceModel = _resourceService.Get(_resourceBuildingModel.EnumNextValue.Next());
        }

        public override void Init()
        {
            _timer = new Timer(TimerTick, TimerCompleted);
            _timerController.AddTimer(_timer);
        }
        
    }
}