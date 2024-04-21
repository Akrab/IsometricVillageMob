using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.IsometricVillageMob.Scripts.Services.MergeTree;
using IsometricVillageMob.RuntimeData;
using IsometricVillageMob.Services;
using UnityEngine.Events;

namespace IsometricVillageMob.Game.Building
{

    public interface IManufactureBuilding
    {
        bool IsRun { get; }
        IManufactureBuildingModel ViewData { get; }
        void StartBuild();

        void StopBuild();

        void NextResource(int index);
        
        void AddUpdateListener(UnityAction action);
        void DelUpdateListener();
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
        public bool IsRun { get; private set; }
        public IManufactureBuildingModel ViewData => _manufactureBuildingModel;

        private Timer _timer;
        private UnityAction _updateUI ;
        
        private void TimerTick(float value)
        {

        } 

        private void TimerCompleted()
        {
            CreateItem();

            if (HaveResources() == false)
            {
                StopBuild();
            }
            _updateUI?.Invoke();
        }

        private void CreateItem()
        {
            _playerInventory.AddItem(_manufactureBuildingModel.ItemType);

            _playerInventory.SubResource(_manufactureBuildingModel.MergeTree.Resource[0].Resource,
                _manufactureBuildingModel.MergeTree.Resource[0].Count);
            _playerInventory.SubResource(_manufactureBuildingModel.MergeTree.Resource[1].Resource,
                _manufactureBuildingModel.MergeTree.Resource[1].Count);
        }

        private void UpdateResource(ResourceType resourceType, int count)
        {
            _updateUI?.Invoke();
        }

        private bool HaveResources()
        {

            for (int i = 0; i < _manufactureBuildingModel.ResourceModels.Length; i++)
            {
                if (_manufactureBuildingModel.ResourceModel(i) == null )
                    return false;
                
                if (_playerInventory.GetResource(_manufactureBuildingModel.ResourceModel(i).ResourceType) <
                    _manufactureBuildingModel.MergeTree.Resource[i].Count)
                    return false;

            }

            return true;
        }

        public override void Init()
        {
            _timer = new Timer(TimerTick, TimerCompleted);
            _timerController.AddTimer(_timer);

            _manufactureBuildingModel.MergeTree = null;
            _manufactureBuildingModel.ItemModel = null;
            
            _playerInventory.AddListener<ResourceType>(UpdateResource);
        }

        public void StartBuild()
        {
            if(IsRun || _manufactureBuildingModel.IsEmpty) return;
        
            if (HaveResources() == false)
                return;


            _timer.PlayLoop(_manufactureBuildingModel.MergeTree.Duration);
            IsRun = true;

        }

        public void StopBuild()
        {
            if(!IsRun) return;
            
            _timer.Stop();
            IsRun = false;
        }

        public void NextResource(int index)
        {
            
            if(IsRun) return;
            
            _manufactureBuildingModel.ResourceModels[index] =
                _resourceService.Get(_manufactureBuildingModel.ResourceNext[index].Next());

            _manufactureBuildingModel.MergeTree = _mergeTreeService
                .Get(_manufactureBuildingModel.ResourceModel(0)?.ResourceType ?? ResourceType.None,
                    _manufactureBuildingModel.ResourceModel(1)?.ResourceType ?? ResourceType.None);
            
            _manufactureBuildingModel.ItemModel =
                _itemService.Get(_manufactureBuildingModel.MergeTree?.ItemType ?? ItemType.None);
            
        }

        public void AddUpdateListener(UnityAction action)
        {
            _updateUI = action;
        }

        public void DelUpdateListener()
        {
            _updateUI = null;
        }
    }
}
