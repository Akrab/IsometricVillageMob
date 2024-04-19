using Infrastructure.SaveLoad.Player;
using IsometricVillageMob.Infrastructure.Controllers;
using IsometricVillageMob.Infrastructure.Controllers.Inputs;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Infrastructure.SaveLoad;
using IsometricVillageMob.Infrastructure.States;
using IsometricVillageMob.Services;
using UnityEngine;

namespace IsometricVillageMob.Installers
{
    public class LobbyInstaller : MonoInstaller
    {
        [SerializeField]
        private UpdateController _updateController;

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
        }

        private void InstallServices()
        {
            var rs = new ResourceSrv();
            _diContainer.BindInterface<IResourceService>(rs);

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