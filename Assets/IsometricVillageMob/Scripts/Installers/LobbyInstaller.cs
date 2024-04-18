using IsometricVillageMob.Infrastructure.Controllers;
using IsometricVillageMob.Infrastructure.Controllers.Inputs;
using IsometricVillageMob.Infrastructure.Controllers.Timers;
using IsometricVillageMob.DIIsometric;
using IsometricVillageMob.Infrastructure;
using IsometricVillageMob.Infrastructure.Containers;
using IsometricVillageMob.Infrastructure.States;
using UnityEngine;

namespace IsometricVillageMob.Installers
{
    public class LobbyInstaller : MonoInstaller
    {
        [SerializeField]
        private UpdateController _updateController;


        private void InstallControllers()
        {
            _diContainer.BindInstance(_updateController);
            _diContainer.BindNew<TimerController>(out var timerController);
            _diContainer.BindNew<InputController>(out var inputController)
                .BindInterface<IInputController>(inputController);
            _diContainer.BindNew<SelectBuildingController>();
            
            _updateController.Add(timerController);
            _updateController.Add(inputController);

        }
        
        private void InstallContainers()
        {
            _diContainer.BindNew<UIContainer>(out var uiContainer);
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
            InstallControllers();
            InstallContainers();
            InstallGameStateMachine();

            _diContainer.Resolve<SelectBuildingController>().Init();
            _diContainer.Resolve<IGameStateMachine>().EnterToState<LoadingGState>();
        }
        
    }
}