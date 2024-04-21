using System;
using System.Collections.Generic;
using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.IsometricVillageMob.Scripts.Services.MergeTree;
using IsometricVillageMob.RuntimeData;
using IsometricVillageMob.Services;

namespace IsometricVillageMob.Game.Building
{

    public interface IManufactureBuilding
    {
        void StartBuild();

        void StopBuild();
        bool IsRun { get; }

        void NextResource(int index);

    }

    public class ManufactureBuilding : BaseBuilding, IManufactureBuilding
    {

        [Inject] private IMergeTreeService _mergeTreeService;
        [Inject] private IItemService _itemService;
        [Inject] private IResourceService _resourceService;
        [Inject] private ITimerController _timerController;
        [Inject] private IPlayerInventory _playerInventory;

        private ManufactureBuildingModel _manufactureBuildingModel = new ManufactureBuildingModel();
        public override BuildingType BuildingType => BuildingType.Manufacture;
        public bool IsRun { get; }

        private Timer _timer;
        
        private void TimerTick(float value)
        {

        } 

        private void TimerCompleted()
        {
            CreateItem();
        }

        private void CreateItem()
        {
          //  _playerInventory.AddResource( _resourceBuildingModel.CurrentResource);
        }
        
        public override void Init()
        {
            _timer = new Timer(TimerTick, TimerCompleted);
            _timerController.AddTimer(_timer);
            
            _manufactureBuildingModel.MergeTree = null;
            _manufactureBuildingModel.ItemModel = null;
            
            _manufactureBuildingModel.ResTypes =  new List<ResourceType>((ResourceType[])Enum.GetValues(typeof(ResourceType)));
            _manufactureBuildingModel.ResTypes .Remove(ResourceType.None);
        }

        public void StartBuild()
        {
            if(IsRun || _manufactureBuildingModel.IsEmpty) return;
        }

        public void StopBuild()
        {
            if(!IsRun) return;
        }

        public void NextResource(int index)
        {
            _manufactureBuildingModel.SelectResources[index] =  _manufactureBuildingModel.ResourceNext[index].Next();

            _manufactureBuildingModel.MergeTree = _mergeTreeService.Get(_manufactureBuildingModel.SelectResources[0],
                _manufactureBuildingModel.SelectResources[1]);

            _manufactureBuildingModel.ResourceModels[index] =
                _resourceService.Get(_manufactureBuildingModel.SelectResources[index]);

        }
        
    }
}
