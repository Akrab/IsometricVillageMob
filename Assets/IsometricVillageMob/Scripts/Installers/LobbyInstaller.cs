using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.Infrastructure.Controllers;
using IsometricVillageMob.Infrastructure.Controllers.Inputs;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Infrastructure.SaveLoad;
using IsometricVillageMob.Infrastructure.States;
using IsometricVillageMob.IsometricVillageMob.Scripts.Services.Items;
using IsometricVillageMob.IsometricVillageMob.Scripts.Services.MergeTree;
using IsometricVillageMob.Services;
using UnityEngine;

namespace IsometricVillageMob.Installers
{
    public class LobbyInstaller : MonoInstaller
    {
        [SerializeField]
        private UpdateController _updateController;

        [SerializeField] private BaseModel[] _models;
        private SaveLoadService _saveLoadService;
        
        
        private void InstallControllers()
        {
            _diContainer.BindInstance(_updateController);
            _diContainer.BindNew<TimerController>(out var timerController);
            _diContainer.BindInterface<ITimerController>(timerController);
            _diContainer.BindNew<InputController>(out var inputController)
                .BindInterface<IInputController>(inputController);
            _diContainer.BindNew<SelectBuildingController>();
            
            _updateController.Add(timerController);
            _updateController.Add(inputController);
            
            _diContainer.BindNew<WorldCreateController>();
        }

        private void InstallContainers()
        {
            _diContainer.BindNew<UIContainer>();

            _diContainer.BindNew<ModelContainer>(out var modelContainer);
            foreach (var model in _models)
                modelContainer.Add(model);

        }

        private void InstallServices()
        {
            var rs = new ResourceLoadSrv();
            _diContainer.BindInterface<IResourceLoadService>(rs);

            var modelContainer = _diContainer.Resolve<ModelContainer>();

            _diContainer.BindInterface<IItemService>(new ItemService(modelContainer));
            _diContainer.BindInterface<IResourceService>(new ResourceService(modelContainer));
            _diContainer.BindInterface<ICurrencyService>(new CurrencyService(modelContainer));
            _diContainer.BindInterface<IMergeTreeService>(new MergeTreeService(modelContainer));
            

        }
        
        private void InstallGameStateMachine()
        {
            var stateMachine = new GameStateMachine();
            _diContainer.BindInterface<IGameStateMachine>(stateMachine);

            _diContainer.BindNew<LoadingGState>(out var loadingState);
            stateMachine.AddState(loadingState);
            
            _diContainer.BindNew<MainMenuGState>(out var mainMenuState);
            stateMachine.AddState(mainMenuState);
            
            _diContainer.BindNew<GameGState>(out var gameGState);
            stateMachine.AddState(gameGState);
            
        }
        
        public override void InstallBindings()
        {
            _saveLoadService = new SaveLoadService();
            _diContainer.BindInterface<IPlayerInventory>(_saveLoadService.PlayerInventory);
            InstallControllers();
            InstallContainers();
            InstallServices();
            InstallGameStateMachine();
            
            _diContainer.Resolve<SelectBuildingController>().Init();
            _diContainer.Resolve<IGameStateMachine>().EnterToState<LoadingGState>();
            
            _diContainer.Resolve<TimerController>().RunTimers();
        }

        private void OnApplicationQuit()
        {
            _saveLoadService?.Save();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                _saveLoadService?.Save();
        }
    }
}